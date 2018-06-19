﻿using DMR11.Core;
using DMR11.Core.Net;
using ModernFolderBrowserDialog;
using System;
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
using System.Threading;
using System.Threading.Tasks;

namespace DMR11
{
    public partial class FormMain : Form
    {
        protected BindingList<IChapter> DownloadQueue;

        protected const string FILENAME_ICHAPTER_COLLECTION = "IChapterCollection.bin";

        protected CancellationTokenSource _cts;

        protected BookmarkManager bookmarks = null;

        protected ITitle currentTitle = null;

        protected FolderBrowser browserDialog = null;
        
        protected MainFormSettingsManager WindowSettings = null;

        private string SaveDestination
        {
            get
            {
                if (rdSeriesDestination.Checked)
                {
                    return lbSeriesDestination.Text;
                }
                else
                    return lbDefaultDestination.Text;
            }
            set
            {
                if (rdSeriesDestination.Checked)
                    lbSeriesDestination.Text = value;

                else
                    lbDefaultDestination.Text = value;
            }
        }

        public FormMain()
        {
            InitializeComponent();

            this.Icon = SystemIcons.Application;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            SetFormTitle();
            SetButtonStyle();

            // Todo: Refactor
            foreach (var primaryButton in new[] { btnDownload, btnGetChapter })
            {
                primaryButton.BackColor = Color.SlateGray;
                primaryButton.ForeColor = Color.White;
                primaryButton.FlatAppearance.MouseOverBackColor = Color.SlateGray;
                primaryButton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.SlateGray, 0.05f);
            }

            StatusPanel.BackColor = ControlPaint.Dark(Color.SlateGray, 0.15f);
            txtMessage.BackColor = StatusPanel.BackColor;
            txtMessage.ForeColor = Color.White;
            
            SetDataGridColumnStyle();
            
            /* While the implementation is being decided as well as safe-guards are being set,
             * disable access to chapter formatting in the 'Release' configuration. In addition
             * to the formatting, remove the connection counter until it's implemented.
             * 
             */
#if !DEBUG
            // Hide the buttons related to chapter and page formatting.
            btnPresetDialog.Visible = false;
            btnFromatPreset.Visible = false;
        
            // Hide the connection selector.
            nudThread.Visible = false;
#endif

            bookmarks = new BookmarkManager("user/bookmarks.json");
            
        }

        private void cbTitleUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChapters();
        }

        private void cbTitleUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) && !string.IsNullOrWhiteSpace(cbTitleUrl.Text))
            {
                GetChapters();

                e.Handled = true;
            }
        }
        
        private void btnGetChapter_Click(object sender, EventArgs e)
        {
            GetChapters();
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
                    item.SaveDestination = SaveDestination;
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
                    item.SaveDestination = SaveDestination;
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

            for (int i = dgvChapter.RowCount; i-- > 0; )
            {
                var item = (IChapter)dgvChapter.Rows[i].DataBoundItem;

                // Todo: Check the default AND the series directory for the chapters.
                string destinationDirectory = lbDefaultDestination.Text; //txtSaveTo.Text;

                // Todo: Check if the series destination exists, create the directory if it doesn't.

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
                else
                {
                    testPath = Path.Combine(lbSeriesDestination.Text, item.Name);
                    chapterExists = Directory.Exists(testPath);
                    string seriesDestination = lbSeriesDestination.Text;
                    bool seriesDestinationExists = Directory.Exists(seriesDestination);

                    if (seriesDestinationExists)
                    {
                        if (!chapterExists)
                        {
                            foreach (var sub in Directory.GetDirectories(seriesDestination))
                            {
                                testPath = Path.Combine(sub, item.Name);
                                chapterExists = Directory.Exists(testPath);

                                Console.WriteLine("Path: {0} | Exists: {1}", testPath, chapterExists);
                            }
                        }


                        if (chapterExists)
                        {
                            // Check if the directory has any files (pages).
                            int pageCount = Directory.GetFiles(testPath).Length;

                            // If the page count is zero, treat it as the user hasn't downloaded
                            // that chapter yet.
                            if (pageCount == 0)
                                chapterExists = false;

                        }

                    }
                }

                if (!chapterExists)
                {
                    if (DownloadQueue.IndexOf(item) < 0)
                    {
                        // Save the chapter to the current save destination.
                        item.SaveDestination = SaveDestination;

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
            if (btnDownload.Text == "Start Download" && DownloadQueue.Count > 0)
            {
                _cts = new CancellationTokenSource();
                DownloadChapter();

                btnDownload.Text = "Stop";
            }
            else if (_cts != null)
            {
                _cts.Cancel();

                btnDownload.Text = "Start Download";
            }

            
        }

        private void DownloadChapter(int millisecond)
        {
            RetryDownloadTimer.Interval = millisecond;
            RetryDownloadTimer.Enabled = true;
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
                    //btnDownload.Enabled = false;
                    btnDownload.Text = "Stop";

                    var task = chapter.DownloadImageAsync(chapter.SaveDestination, _cts.Token, new DMR11.Core.Progress<ChapterProgress>(c =>
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
                                //btnDownload.Enabled = true;
                                btnDownload.Text = "Start Download";
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
                //btnDownload.Enabled = true;
                btnDownload.Text = "Start Download";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }
                
        private void btnChangeSaveTo_Click(object sender, EventArgs e)
        {            
            browserDialog.SelectedPath = SaveDestination;

            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveDestination = browserDialog.SelectedPath;
            }
            
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveDestination))
                Process.Start(SaveDestination);
        }

        private void dgvChapter_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            CheckIfChapterVerticalScrollBarIsVisible();
        }

        private void dgvChapter_SizeChanged(object sender, EventArgs e)
        {
            CheckIfChapterVerticalScrollBarIsVisible();
        }
        
        private void LoadSettings()
        {
            WindowSettings = new MainFormSettingsManager("user/window.json");

            if (File.Exists("user/window.json"))
            {
                WindowSettings.Load();
            }
        }

        private void ParseSettings()
        {
            this.Size = WindowSettings.Subject.FormSize;
            this.Location = WindowSettings.Subject.FormLocation;
            this.WindowState = WindowSettings.Subject.State;
            
            this.cbTitleUrl.Text = WindowSettings.Subject.Url;
            this.lbDefaultDestination.Text = WindowSettings.Subject.SaveTo;
        }

        private void AlignChapterDockStyle()
        {
            ChapterAuxiliaryDock.Height = dgvChapter.ColumnHeadersHeight;
            ChapterAuxiliaryDock.BackColor = dgvChapter.ColumnHeadersDefaultCellStyle.BackColor;

            ((Button)ChapterAuxiliaryDock.Controls[0]).BackColor = Color.DarkGray;
            ((Button)ChapterAuxiliaryDock.Controls[0]).FlatAppearance.MouseOverBackColor = Color.Silver;
            ((Button)ChapterAuxiliaryDock.Controls[0]).FlatAppearance.MouseDownBackColor = Color.LightGray;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadSettings();          
            ParseSettings();

            dgvQueueChapter.AutoGenerateColumns = false;
            dgvChapter.AutoGenerateColumns = false;

            AlignChapterDockStyle();

            //foreach (string[] item in TitleFactory.GetSupportedSites())
            //{
            //    dgvSupportedSites.Rows.Add(item);
            //}

            browserDialog = new ModernFolderBrowserDialog.FolderBrowser();

            if (String.IsNullOrEmpty(lbDefaultDestination.Text))
            {
                lbDefaultDestination.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            DownloadQueue = Common.LoadIChapterCollection(FILENAME_ICHAPTER_COLLECTION);
            dgvQueueChapter.DataSource = DownloadQueue;

            LoadBookmark();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowSettings.Subject.Url = cbTitleUrl.Text;
            WindowSettings.Subject.State = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                WindowSettings.Subject.FormSize = this.Size;
                WindowSettings.Subject.FormLocation = this.Location;
            }


            //Properties.Settings.Default.Save();
            Common.SaveIChapterCollection(DownloadQueue, FILENAME_ICHAPTER_COLLECTION);
            
            WindowSettings.Save();
            bookmarks.Save();
            bookmarks.Dispose();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            using (var optionsDialog = new FormOption(WindowSettings.Subject))
            {
                optionsDialog.ShowDialog(this);
            }

            Console.WriteLine(WindowSettings.Subject.DefaultSaveDestination);
        }

        private void LoadBookmark()
        {
            cbTitleUrl.Items.Clear();

            cbTitleUrl.Items.AddRange(bookmarks.GetBookmarks().Select(bookmark => bookmark.SeriesUri).ToArray());

        }

        private void btnAddBookmark_Click(object sender, EventArgs e)
        {
            var bookmark = new Bookmark()
            {
                Name = cbTitleUrl.Text,
                SeriesUri = new ValidatedUri(cbTitleUrl.Text)
            };

            if (bookmarks.AddBookmark(bookmark) == AddBookmarkStatus.Success)
            {
                LoadBookmark();
            }

        }

        private void btnRemoveBookmark_Click(object sender, EventArgs e)
        {
            if (bookmarks.RemoveBookmark(cbTitleUrl.Text))
            {
                LoadBookmark();
            }
        }

        private void btnAddPrefixCounter_Click(object sender, EventArgs e)
        {
            //var chapters = new List<IChapter>();
            //foreach (DataGridViewRow row in dgvChapter.Rows)
            //{
            //    IChapter chapter = row.DataBoundItem as IChapter;
            //    chapters.Add(chapter);
            //}
            //chapters = Common.CloneIChapterCollection(chapters);

            //chapters.Reverse();
            //chapters.ForEach(r => r.Name = String.Format("[{0:000}] - {1}", chapters.IndexOf(r) + 1, r.Name));
            //chapters.Reverse();

            //dgvChapter.DataSource = chapters;

            //if (currentTitle != null && currentTitle.HostData != null)
            //{
            //    using (var formatDialog = new FormFormat())
            //    {
            //        if (formatDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {

            //        }
            //    }
            //}


        }

        private void RetryDownloadTimer_Tick(object sender, EventArgs e)
        {
            RetryDownloadTimer.Enabled = false;
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
                GetChapters();
            }

        }

        private void lbDefaultDestination_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                rdDefaultDestination.Checked = true;
        }

        private void lbSeriesDestination_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                rdSeriesDestination.Checked = true;
        }

        private void GetChapters()
        {
            GetChapters(new ValidatedUri(cbTitleUrl.Text));
        }

        private void GetChapters(Uri seriesUri)
        {
            try
            {
                ITitle title = TitleFactory.CreateTitle(seriesUri);
                currentTitle = title;
                title.Proxy = Option.GetProxy();
                btnGetChapter.Enabled = false;

                var task = title.PopulateChapterAsync(new DMR11.Core.Progress<int>(progress => txtPercent.Text = progress + "%"));
                task.ContinueWith(t =>
                {
                    btnGetChapter.Enabled = true;
                    dgvChapter.DataSource = title.Chapters;

                    PrepareSeriesDirectory();

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

        private void PrepareSeriesDirectory()
        {
            // Todo: Refactor.

            /* Todo: Format names before checking.
             * 
             * Check for the names with the formatting applied instead of what is shown
             * in the page. If the page contains an invalid character (e.g. :), the
             * application will determine it hasn't been downloaded yet. This is due to
             * the application stripping invalid characters from the output.
             */

            // Todo: Set series-specific directory path to default.
            if (dgvChapter.RowCount == 0)
            {
                return;
            }

            string
                defaultSeriesDestination = WindowSettings.Subject.DefaultSaveDestination,
                series = string.Empty,
                path = string.Empty;

            if (string.IsNullOrEmpty(defaultSeriesDestination))
            {
                defaultSeriesDestination = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            series = currentTitle.SeriesTitle;

            // Todo: Replace invalid characters.

            path = defaultSeriesDestination;
            
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path = string.Concat(path, Path.DirectorySeparatorChar);
            }
            
            path = DMR11.Core.Helper.FileSystem.GetSafePath(string.Concat(path, series));

            lbSeriesDestination.Text = path;

            if (Directory.Exists(path))
            {
                rdSeriesDestination.Checked = true;
            }
            else
            {
                rdDefaultDestination.Checked = true;
            }

        }
        
        /// <summary>
        /// Changes the location of the auxiliary dock if its vertical scroll bar is visible.
        /// </summary>
        private void CheckIfChapterVerticalScrollBarIsVisible()
        {
            /// Todo: Refactor into multiple methods, allowing reuse.
            /// 1. Check if the scroll bar is visible.
            /// 2. Perform an action based on the result.

            var scrollBar = dgvChapter.Controls.OfType<VScrollBar>().FirstOrDefault();
            var scrollBarWidth = 0;

            if (scrollBar != null && scrollBar.Visible)
            {
                scrollBarWidth = scrollBar.Width;
            }

            ChapterAuxiliaryDock.Left = dgvChapter.Right - ChapterAuxiliaryDock.Width - scrollBarWidth;
        }

        private void SetButtonStyle()
        {
            SetFormButtonStyle(new[] { this.Controls, this.headerPanel.Controls, this.ChapterAuxiliaryDock.Controls });
        }

        public static void SetFormButtonStyle(Control.ControlCollection[] controlContainers)
        {
            var buttonFont = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);

            foreach (var container in controlContainers)
            {
                container.OfType<Button>().ToList().ForEach((button) => StyleButton(button, buttonFont));
            }

        }

        private static void StyleButton(Button button, Font buttonFont)
        {
            if (button != null && buttonFont != null)
            {
                button.FlatStyle = FlatStyle.Flat;
                
                button.FlatAppearance.BorderColor = Color.DarkGray;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.LightGray;
                button.FlatAppearance.MouseDownBackColor = Color.Silver;

                if (button.BackgroundImage == null)
                {
                    button.BackColor = Color.FromArgb(215, 215, 215);
                    button.ForeColor = Color.FromArgb(45, 45, 45);
                    button.Font = buttonFont;
                }
                else
                {
                    button.BackColor = Color.FromArgb(230, 230, 230);
                }
            }
        }

        private void SetDataGridColumnStyle()
        {
            var chapterQueueFont = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);
            var downloadQueueFont = new Font(chapterQueueFont.FontFamily, 10, FontStyle.Regular, GraphicsUnit.Point);

            StyleDataGridColumnHeader(dgvChapter, chapterQueueFont);
            StyleDataGridColumnHeader(dgvQueueChapter, downloadQueueFont);
        }

        private static void StyleDataGridColumnHeader(DataGridView dataGrid, Font columnHeaderFont)
        {
            var defaultColumnStyle = new DataGridViewCellStyle();
            defaultColumnStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            defaultColumnStyle.BackColor = Color.SlateGray;  //Color.FromArgb(215, 215, 215);
            defaultColumnStyle.ForeColor = Color.White; //Color.FromArgb(64, 64, 64);
            defaultColumnStyle.Padding = new Padding(0, 2, 0, 3);

            if (columnHeaderFont != null)
            {
                defaultColumnStyle.Font = columnHeaderFont;
            }

            dataGrid.ColumnHeadersDefaultCellStyle = defaultColumnStyle;
            dataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dataGrid.EnableHeadersVisualStyles = false;

            // Todo: Refactor.
            dataGrid.BorderStyle = BorderStyle.None;
        }

        private void SetFormTitle()
        {            
            this.Text = string.Format("{0} - {1}", Application.ProductName, GetSemanticVersion());
        }

        private string GetSemanticVersion()
        {
            return GetSemanticVersion(Application.ProductVersion);
        }

        public static string GetSemanticVersion(string version)
        {
            var semanticVersion = version.Substring(0, Application.ProductVersion.LastIndexOf('.'));

            return semanticVersion;
        }


    }

}

