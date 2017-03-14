﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Deployment.Application;
using System.Collections.Specialized;
using MangaRipper.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MangaRipper
{
    public partial class FormMain : Form
    {

        BindingList<IChapter> DownloadQueue;

        protected const string FILENAME_ICHAPTER_COLLECTION = "IChapterCollection.bin";

        private CancellationTokenSource _cts;

        public FormMain()
        {
            InitializeComponent();

            //var items = cbTitleUrl.Items;
            //object[] sorted = new object[items.Count];

            //List<object> itemsToBeSorted = new List<object>();
            //itemsToBeSorted.Sort();

            //cbTitleUrl.BeginUpdate();

            //cbTitleUrl.Items.AddRange(itemsToBeSorted.ToArray());

            //cbTitleUrl.EndUpdate();

        }

        private void btnGetChapter_Click(object sender, EventArgs e)
        {
            try
            {
                var titleUrl = new Uri(cbTitleUrl.Text);
                ITitle title = TitleFactory.CreateTitle(titleUrl);
                title.Proxy = Option.GetProxy();
                btnGetChapter.Enabled = false;
                var task = title.PopulateChapterAsync(new MangaRipper.Core.Progress<int>(progress => txtPercent.Text = progress + "%"));
                task.ContinueWith(t =>
                {
                    btnGetChapter.Enabled = true;
                    dgvChapter.DataSource = title.Chapters;

                    if (t.Exception != null && t.Exception.InnerException != null)
                    {
                        txtMessage.Text = t.Exception.InnerException.Message;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var items = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                if (row.Selected == true)
                {
                    items.Add((IChapter)row.DataBoundItem);
                }
            }

            items.Reverse();
            foreach (IChapter item in items)
            {
                if (DownloadQueue.IndexOf(item) < 0)
                {
                    DownloadQueue.Add(item);
                }
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            var items = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                items.Add((IChapter)row.DataBoundItem);
            }
            items.Reverse();
            foreach (IChapter item in items)
            {
                if (DownloadQueue.IndexOf(item) < 0)
                {
                    DownloadQueue.Add(item);
                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            int limit = 0,
                chaptersAdded = 0;

            if (Helper.KeyState.IsShiftKeyPressed())
                limit = 5;

            //foreach (DataGridViewRow row in dgvChapter.Rows)
            //{
            //    var item = (IChapter)row.DataBoundItem;
            //    string destinationDirectory = txtSaveTo.Text;

            //    string testPath = Path.Combine(destinationDirectory, item.Name);
            //    bool chapterExists = Directory.Exists(testPath);

            //    if (chapterExists)
            //    {
            //        // Check if the directory has any files (pages).
            //        int pageCount = Directory.GetFiles(testPath).Length;

            //        // If the page count is zero, treat it as the user hasn't downloaded
            //        // that chapter yet.
            //        if (pageCount == 0)
            //            chapterExists = false;

            //    }

            //    if (!chapterExists)
            //    {
            //        items.Add((IChapter)row.DataBoundItem);

            //        if (limit > 0 && (++chaptersAdded == limit))
            //            break;
            //    }
            //}

            for (int i = dgvChapter.RowCount; i-- > 0; )
            {
                var item = (IChapter)dgvChapter.Rows[i].DataBoundItem;
                string destinationDirectory = txtSaveTo.Text;

                string testPath = Path.Combine(destinationDirectory, item.Name);
                bool chapterExists = Directory.Exists(testPath);

                if (chapterExists)
                {
                    // Check if the directory has any files (pages).
                    int pageCount = Directory.GetFiles(testPath).Length;

                    // If the page count is zero, treat it as the user hasn't downloaded
                    // that chapter yet.
                    if (pageCount == 0)
                        chapterExists = false;

                }

                if (!chapterExists)
                {
                    if (DownloadQueue.IndexOf(item) < 0)
                    {
                        DownloadQueue.Add(item);

                        if (limit > 0 && (++chaptersAdded == limit))
                            break;
                    }

                }
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvQueueChapter.SelectedRows)
            {
                IChapter chapter = (IChapter)item.DataBoundItem;
                if (chapter.IsBusy == false)
                {
                    DownloadQueue.Remove(chapter);
                }
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            var removeItems = DownloadQueue.Where(r => r.IsBusy == false).ToList();

            foreach (var item in removeItems)
            {
                DownloadQueue.Remove(item);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            DownloadChapter();
        }


        private void DownloadChapter(int millisecond)
        {
            timer1.Interval = millisecond;
            timer1.Enabled = true;
        }

        private void DownloadChapter()
        {
            if (DownloadQueue.Count > 0 && _cts.IsCancellationRequested == false)
            {
                int current = DownloadQueue.Where(c => c.IsBusy == true).Count();
                int max = Convert.ToInt32(nudThread.Value);
                int remain = max - current;
                var chapters = DownloadQueue.Where(c => c.IsBusy == false).Take(remain);

                foreach (var chapter in chapters)
                {
                    chapter.Proxy = Option.GetProxy();
                    btnDownload.Enabled = false;
                    var task = chapter.DownloadImageAsync(txtSaveTo.Text, _cts.Token, new MangaRipper.Core.Progress<ChapterProgress>(c =>
                        {
                            foreach (DataGridViewRow item in dgvQueueChapter.Rows)
                            {
                                if (c.Chapter == item.DataBoundItem)
                                {
                                    item.Cells[ColChapterStatus.Name].Value = c.Percent + "%";
                                    break;
                                }
                            }
                        }));

                    task.ContinueWith(t =>
                    {
                        switch (t.Status)
                        {
                            case TaskStatus.Canceled:
                                btnDownload.Enabled = true;
                                break;
                            case TaskStatus.Faulted:
                                txtMessage.Text = t.Exception.InnerException.Message;
                                DownloadChapter(1000);
                                break;
                            case TaskStatus.RanToCompletion:
                                DownloadQueue.Remove(chapter);
                                DownloadChapter();
                                break;
                            default:
                                break;
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            else
            {
                btnDownload.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }

        private void btnChangeSaveTo_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtSaveTo.Text;
            DialogResult dr = folderBrowserDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                txtSaveTo.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(txtSaveTo.Text);
        }

        private void dgvSupportedSites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                Process.Start(dgvSupportedSites.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = MangaRipper.Properties.Settings.Default.Size;
            this.Location = MangaRipper.Properties.Settings.Default.Location;
            this.WindowState = MangaRipper.Properties.Settings.Default.WindowState;

            dgvQueueChapter.AutoGenerateColumns = false;
            dgvChapter.AutoGenerateColumns = false;

            this.Text = String.Format("{0} {1}", Application.ProductName, AppInfo.DeploymentVersion);

            foreach (string[] item in TitleFactory.GetSupportedSites())
            {
                dgvSupportedSites.Rows.Add(item);
            }

            if (String.IsNullOrEmpty(txtSaveTo.Text))
            {
                txtSaveTo.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            DownloadQueue = Common.LoadIChapterCollection(FILENAME_ICHAPTER_COLLECTION);
            dgvQueueChapter.DataSource = DownloadQueue;

            LoadBookmark();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void btnHowToUse_Click(object sender, EventArgs e)
        {
            Process.Start("http://mangaripper.codeplex.com/documentation");
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                MangaRipper.Properties.Settings.Default.Size = this.Size;
                MangaRipper.Properties.Settings.Default.Location = this.Location;
                MangaRipper.Properties.Settings.Default.WindowState = this.WindowState;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                MangaRipper.Properties.Settings.Default.WindowState = this.WindowState;
            }

            Properties.Settings.Default.Save();
            Common.SaveIChapterCollection(DownloadQueue, FILENAME_ICHAPTER_COLLECTION);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormOption form = new FormOption();
            form.ShowDialog(this);
        }

        private void LoadBookmark()
        {
            cbTitleUrl.Items.Clear();
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;
            if (sc != null)
            {
                foreach (string item in sc)
                {
                    cbTitleUrl.Items.Add(item);
                }
            }
        }

        private void btnAddBookmark_Click(object sender, EventArgs e)
        {
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;
            if (sc == null)
            {
                sc = new StringCollection();
            }
            if (sc.Contains(cbTitleUrl.Text) == false)
            {
                sc.Add(cbTitleUrl.Text);
                MangaRipper.Properties.Settings.Default.Bookmark = sc;
                LoadBookmark();
            }
        }

        private void btnRemoveBookmark_Click(object sender, EventArgs e)
        {
            StringCollection sc = MangaRipper.Properties.Settings.Default.Bookmark;
            if (sc != null)
            {
                sc.Remove(cbTitleUrl.Text);
                MangaRipper.Properties.Settings.Default.Bookmark = sc;

                LoadBookmark();
            }
        }

        private void btnAddPrefixCounter_Click(object sender, EventArgs e)
        {
            var chapters = new List<IChapter>();
            foreach (DataGridViewRow row in dgvChapter.Rows)
            {
                IChapter chapter = row.DataBoundItem as IChapter;
                chapters.Add(chapter);
            }
            chapters = Common.CloneIChapterCollection(chapters);

            chapters.Reverse();
            chapters.ForEach(r => r.Name = String.Format("[{0:000}] - {1}", chapters.IndexOf(r) + 1, r.Name));
            chapters.Reverse();

            dgvChapter.DataSource = chapters;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DownloadChapter();
        }

        private void btnPasteUrl_Click(object sender, EventArgs e)
        {
            bool clipboardCopied = false;

            try
            {
                if (Clipboard.ContainsText())
                {
                    string pastedUrl = Clipboard.GetData(DataFormats.StringFormat).ToString();

                    if (!string.IsNullOrWhiteSpace(pastedUrl))
                    {
                        pastedUrl = pastedUrl.Trim();
                        cbTitleUrl.Text = pastedUrl;
                        clipboardCopied = true;
                    }

                }
            }
            catch (Exception)
            {
                string error = String.Format("{0} - Clipboard operation failed, please try again.", DateTime.Now.ToLongTimeString());
                txtMessage.Text = error;
            }


            if (clipboardCopied)
            {
                btnGetChapter_Click(null, EventArgs.Empty);
            }

        }

    }

}

