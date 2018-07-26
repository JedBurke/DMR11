namespace DMR11
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnGetChapter = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.dgvQueueChapter = new System.Windows.Forms.DataGridView();
            this.ColChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColChapterStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColChapterUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSaveTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnChangeSaveTo = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnOptions = new System.Windows.Forms.Button();
            this.dgvChapter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbTitleUrl = new System.Windows.Forms.ComboBox();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPresetDialog = new System.Windows.Forms.Button();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lbSeriesDestination = new System.Windows.Forms.Label();
            this.lbDefaultDestination = new System.Windows.Forms.Label();
            this.btnAddBookmark = new System.Windows.Forms.Button();
            this.btnRemoveBookmark = new System.Windows.Forms.Button();
            this.btnPasteUrl = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.RetryDownloadTimer = new System.Windows.Forms.Timer(this.components);
            this.lbDestination = new System.Windows.Forms.Label();
            this.rdDefaultDestination = new System.Windows.Forms.RadioButton();
            this.rdSeriesDestination = new System.Windows.Forms.RadioButton();
            this.txtPercent = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.btnFromatPreset = new System.Windows.Forms.Button();
            this.ChapterAuxiliaryDock = new System.Windows.Forms.Panel();
            this.MiriSeriesLookupButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueChapter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.ChapterAuxiliaryDock.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetChapter
            // 
            this.btnGetChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetChapter.FlatAppearance.BorderSize = 0;
            this.btnGetChapter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnGetChapter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnGetChapter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGetChapter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetChapter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetChapter.Location = new System.Drawing.Point(902, 11);
            this.btnGetChapter.Name = "btnGetChapter";
            this.btnGetChapter.Size = new System.Drawing.Size(98, 25);
            this.btnGetChapter.TabIndex = 5;
            this.btnGetChapter.Text = "Get Chapters";
            this.MainToolTip.SetToolTip(this.btnGetChapter, "Retrieve chapters from the current URL");
            this.btnGetChapter.UseVisualStyleBackColor = false;
            this.btnGetChapter.Click += new System.EventHandler(this.btnGetChapter_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDownload.Location = new System.Drawing.Point(890, 565);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(110, 25);
            this.btnDownload.TabIndex = 18;
            this.btnDownload.Text = "Start Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(192, 433);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(113, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Selected";
            this.MainToolTip.SetToolTip(this.btnAdd, "Add selected chapters to queue");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddAll.Location = new System.Drawing.Point(311, 433);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(60, 25);
            this.btnAddAll.TabIndex = 9;
            this.btnAddAll.Text = "All";
            this.MainToolTip.SetToolTip(this.btnAddAll, "Add all chapters to queue");
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRemove.Location = new System.Drawing.Point(522, 565);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(72, 25);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "Remove";
            this.MainToolTip.SetToolTip(this.btnRemove, "Remove selected chapters from the queue");
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveAll.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveAll.FlatAppearance.BorderSize = 0;
            this.btnRemoveAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRemoveAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemoveAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRemoveAll.Location = new System.Drawing.Point(600, 565);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(89, 25);
            this.btnRemoveAll.TabIndex = 17;
            this.btnRemoveAll.Text = "Remove All";
            this.MainToolTip.SetToolTip(this.btnRemoveAll, "Remove all chapters from the queue");
            this.btnRemoveAll.UseVisualStyleBackColor = false;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // dgvQueueChapter
            // 
            this.dgvQueueChapter.AllowUserToAddRows = false;
            this.dgvQueueChapter.AllowUserToDeleteRows = false;
            this.dgvQueueChapter.AllowUserToResizeRows = false;
            this.dgvQueueChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQueueChapter.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvQueueChapter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvQueueChapter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvQueueChapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueueChapter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColChapterName,
            this.ColChapterStatus,
            this.ColChapterUrl,
            this.ColSaveTo});
            this.dgvQueueChapter.GridColor = System.Drawing.Color.Silver;
            this.dgvQueueChapter.Location = new System.Drawing.Point(449, 47);
            this.dgvQueueChapter.Name = "dgvQueueChapter";
            this.dgvQueueChapter.ReadOnly = true;
            this.dgvQueueChapter.RowHeadersVisible = false;
            this.dgvQueueChapter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueueChapter.Size = new System.Drawing.Size(551, 512);
            this.dgvQueueChapter.TabIndex = 10;
            // 
            // ColChapterName
            // 
            this.ColChapterName.DataPropertyName = "Name";
            this.ColChapterName.HeaderText = "Chapter Name";
            this.ColChapterName.Name = "ColChapterName";
            this.ColChapterName.ReadOnly = true;
            this.ColChapterName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColChapterName.Width = 200;
            // 
            // ColChapterStatus
            // 
            this.ColChapterStatus.HeaderText = "%";
            this.ColChapterStatus.Name = "ColChapterStatus";
            this.ColChapterStatus.ReadOnly = true;
            this.ColChapterStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColChapterStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColChapterStatus.Width = 35;
            // 
            // ColChapterUrl
            // 
            this.ColChapterUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColChapterUrl.DataPropertyName = "Address";
            this.ColChapterUrl.HeaderText = "Address";
            this.ColChapterUrl.Name = "ColChapterUrl";
            this.ColChapterUrl.ReadOnly = true;
            this.ColChapterUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColChapterUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColSaveTo
            // 
            this.ColSaveTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSaveTo.DataPropertyName = "SaveDestination";
            this.ColSaveTo.HeaderText = "Destination";
            this.ColSaveTo.Name = "ColSaveTo";
            this.ColSaveTo.ReadOnly = true;
            this.ColSaveTo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColSaveTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop.Location = new System.Drawing.Point(795, 565);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 25);
            this.btnStop.TabIndex = 19;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnChangeSaveTo
            // 
            this.btnChangeSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeSaveTo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnChangeSaveTo.Location = new System.Drawing.Point(321, 477);
            this.btnChangeSaveTo.Name = "btnChangeSaveTo";
            this.btnChangeSaveTo.Size = new System.Drawing.Size(39, 25);
            this.btnChangeSaveTo.TabIndex = 13;
            this.btnChangeSaveTo.Text = "...";
            this.MainToolTip.SetToolTip(this.btnChangeSaveTo, "Change folder");
            this.btnChangeSaveTo.UseVisualStyleBackColor = true;
            this.btnChangeSaveTo.Click += new System.EventHandler(this.btnChangeSaveTo_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenFolder.Location = new System.Drawing.Point(366, 477);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(71, 25);
            this.btnOpenFolder.TabIndex = 14;
            this.btnOpenFolder.Text = "Open";
            this.MainToolTip.SetToolTip(this.btnOpenFolder, "Open destination folder");
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUrl.Location = new System.Drawing.Point(8, 11);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(87, 21);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Series URL";
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOptions.Location = new System.Drawing.Point(449, 565);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(67, 25);
            this.btnOptions.TabIndex = 20;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // dgvChapter
            // 
            this.dgvChapter.AllowUserToAddRows = false;
            this.dgvChapter.AllowUserToDeleteRows = false;
            this.dgvChapter.AllowUserToResizeRows = false;
            this.dgvChapter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvChapter.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChapter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChapter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvChapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChapter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4});
            this.dgvChapter.GridColor = System.Drawing.Color.Silver;
            this.dgvChapter.Location = new System.Drawing.Point(12, 47);
            this.dgvChapter.Name = "dgvChapter";
            this.dgvChapter.ReadOnly = true;
            this.dgvChapter.RowHeadersVisible = false;
            this.dgvChapter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChapter.Size = new System.Drawing.Size(425, 377);
            this.dgvChapter.TabIndex = 6;
            this.dgvChapter.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvChapter_ColumnWidthChanged);
            this.dgvChapter.SizeChanged += new System.EventHandler(this.dgvChapter_SizeChanged);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn4.HeaderText = "Chapter Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbTitleUrl
            // 
            this.cbTitleUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTitleUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbTitleUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTitleUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DMR11.Properties.Settings.Default, "Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbTitleUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTitleUrl.Location = new System.Drawing.Point(101, 11);
            this.cbTitleUrl.Name = "cbTitleUrl";
            this.cbTitleUrl.Size = new System.Drawing.Size(635, 25);
            this.cbTitleUrl.TabIndex = 1;
            this.cbTitleUrl.Text = global::DMR11.Properties.Settings.Default.Url;
            this.cbTitleUrl.SelectedIndexChanged += new System.EventHandler(this.cbTitleUrl_SelectedIndexChanged);
            this.cbTitleUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbTitleUrl_KeyUp);
            // 
            // btnPresetDialog
            // 
            this.btnPresetDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPresetDialog.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPresetDialog.FlatAppearance.BorderSize = 0;
            this.btnPresetDialog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnPresetDialog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnPresetDialog.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPresetDialog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPresetDialog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPresetDialog.Location = new System.Drawing.Point(12, 433);
            this.btnPresetDialog.Name = "btnPresetDialog";
            this.btnPresetDialog.Size = new System.Drawing.Size(78, 25);
            this.btnPresetDialog.TabIndex = 7;
            this.btnPresetDialog.Text = "Format...";
            this.MainToolTip.SetToolTip(this.btnPresetDialog, "Format the directory name of the downloaded chapter(s)");
            this.btnPresetDialog.UseVisualStyleBackColor = false;
            this.btnPresetDialog.Click += new System.EventHandler(this.btnAddPrefixCounter_Click);
            // 
            // nudThread
            // 
            this.nudThread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudThread.Location = new System.Drawing.Point(721, 567);
            this.nudThread.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThread.Name = "nudThread";
            this.nudThread.ReadOnly = true;
            this.nudThread.Size = new System.Drawing.Size(49, 22);
            this.nudThread.TabIndex = 24;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MainToolTip.SetToolTip(this.nudThread, "Maximum number of connections");
            this.nudThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddNew.Location = new System.Drawing.Point(377, 433);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(60, 25);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "New";
            this.MainToolTip.SetToolTip(this.btnAddNew, "Add latest chapters to queue\r\nHold SHIFT to only add the 5 latest chapters");
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lbSeriesDestination
            // 
            this.lbSeriesDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSeriesDestination.AutoSize = true;
            this.lbSeriesDestination.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeriesDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbSeriesDestination.Location = new System.Drawing.Point(57, 556);
            this.lbSeriesDestination.MaximumSize = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.MinimumSize = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.Name = "lbSeriesDestination";
            this.lbSeriesDestination.Size = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.TabIndex = 29;
            this.lbSeriesDestination.Text = "Series-Specific Destination";
            this.MainToolTip.SetToolTip(this.lbSeriesDestination, "Saves the chapter to the series\' folder");
            this.lbSeriesDestination.UseMnemonic = false;
            this.lbSeriesDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbSeriesDestination_MouseClick);
            // 
            // lbDefaultDestination
            // 
            this.lbDefaultDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDefaultDestination.AutoSize = true;
            this.lbDefaultDestination.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDefaultDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDefaultDestination.Location = new System.Drawing.Point(57, 513);
            this.lbDefaultDestination.MaximumSize = new System.Drawing.Size(385, 17);
            this.lbDefaultDestination.MinimumSize = new System.Drawing.Size(380, 17);
            this.lbDefaultDestination.Name = "lbDefaultDestination";
            this.lbDefaultDestination.Size = new System.Drawing.Size(380, 17);
            this.lbDefaultDestination.TabIndex = 30;
            this.MainToolTip.SetToolTip(this.lbDefaultDestination, "Saves the chapter to the default manga folder");
            this.lbDefaultDestination.UseMnemonic = false;
            this.lbDefaultDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbDefaultDestination_MouseClick);
            // 
            // btnAddBookmark
            // 
            this.btnAddBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBookmark.BackgroundImage = global::DMR11.Properties.Resources.appbar_page_corner_bookmark;
            this.btnAddBookmark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddBookmark.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddBookmark.FlatAppearance.BorderSize = 0;
            this.btnAddBookmark.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnAddBookmark.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnAddBookmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBookmark.Location = new System.Drawing.Point(776, 11);
            this.btnAddBookmark.Name = "btnAddBookmark";
            this.btnAddBookmark.Size = new System.Drawing.Size(28, 25);
            this.btnAddBookmark.TabIndex = 2;
            this.MainToolTip.SetToolTip(this.btnAddBookmark, "Bookmark this URL");
            this.btnAddBookmark.UseVisualStyleBackColor = true;
            this.btnAddBookmark.Click += new System.EventHandler(this.btnAddBookmark_Click);
            // 
            // btnRemoveBookmark
            // 
            this.btnRemoveBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBookmark.BackgroundImage = global::DMR11.Properties.Resources.appbar_page_corner_text;
            this.btnRemoveBookmark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRemoveBookmark.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveBookmark.FlatAppearance.BorderSize = 0;
            this.btnRemoveBookmark.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRemoveBookmark.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnRemoveBookmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveBookmark.Location = new System.Drawing.Point(806, 11);
            this.btnRemoveBookmark.Name = "btnRemoveBookmark";
            this.btnRemoveBookmark.Size = new System.Drawing.Size(28, 25);
            this.btnRemoveBookmark.TabIndex = 3;
            this.MainToolTip.SetToolTip(this.btnRemoveBookmark, "Remove this bookmarked URL");
            this.btnRemoveBookmark.UseVisualStyleBackColor = true;
            this.btnRemoveBookmark.Click += new System.EventHandler(this.btnRemoveBookmark_Click);
            // 
            // btnPasteUrl
            // 
            this.btnPasteUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteUrl.BackgroundImage = global::DMR11.Properties.Resources.appbar_clipboard_paste;
            this.btnPasteUrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPasteUrl.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPasteUrl.FlatAppearance.BorderSize = 0;
            this.btnPasteUrl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnPasteUrl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnPasteUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPasteUrl.Location = new System.Drawing.Point(742, 11);
            this.btnPasteUrl.Name = "btnPasteUrl";
            this.btnPasteUrl.Size = new System.Drawing.Size(28, 25);
            this.btnPasteUrl.TabIndex = 25;
            this.MainToolTip.SetToolTip(this.btnPasteUrl, "Paste URL from clipboard and get chapters");
            this.btnPasteUrl.UseVisualStyleBackColor = true;
            this.btnPasteUrl.Click += new System.EventHandler(this.btnPasteUrl_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMessage.Location = new System.Drawing.Point(12, 6);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(5);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(957, 16);
            this.txtMessage.TabIndex = 23;
            // 
            // RetryDownloadTimer
            // 
            this.RetryDownloadTimer.Interval = 1000;
            this.RetryDownloadTimer.Tick += new System.EventHandler(this.RetryDownloadTimer_Tick);
            // 
            // lbDestination
            // 
            this.lbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDestination.AutoSize = true;
            this.lbDestination.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDestination.Location = new System.Drawing.Point(12, 478);
            this.lbDestination.Name = "lbDestination";
            this.lbDestination.Size = new System.Drawing.Size(123, 20);
            this.lbDestination.TabIndex = 26;
            this.lbDestination.Text = "Save Destination";
            // 
            // rdDefaultDestination
            // 
            this.rdDefaultDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdDefaultDestination.AutoSize = true;
            this.rdDefaultDestination.Checked = true;
            this.rdDefaultDestination.Location = new System.Drawing.Point(37, 516);
            this.rdDefaultDestination.Name = "rdDefaultDestination";
            this.rdDefaultDestination.Size = new System.Drawing.Size(14, 13);
            this.rdDefaultDestination.TabIndex = 27;
            this.rdDefaultDestination.TabStop = true;
            this.rdDefaultDestination.UseVisualStyleBackColor = true;
            // 
            // rdSeriesDestination
            // 
            this.rdSeriesDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdSeriesDestination.AutoSize = true;
            this.rdSeriesDestination.Location = new System.Drawing.Point(37, 559);
            this.rdSeriesDestination.Name = "rdSeriesDestination";
            this.rdSeriesDestination.Size = new System.Drawing.Size(14, 13);
            this.rdSeriesDestination.TabIndex = 28;
            this.rdSeriesDestination.UseVisualStyleBackColor = true;
            // 
            // txtPercent
            // 
            this.txtPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPercent.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPercent.Location = new System.Drawing.Point(848, 11);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(48, 25);
            this.txtPercent.TabIndex = 31;
            this.txtPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headerPanel
            // 
            this.headerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerPanel.Controls.Add(this.lblUrl);
            this.headerPanel.Controls.Add(this.txtPercent);
            this.headerPanel.Controls.Add(this.btnGetChapter);
            this.headerPanel.Controls.Add(this.cbTitleUrl);
            this.headerPanel.Controls.Add(this.btnAddBookmark);
            this.headerPanel.Controls.Add(this.btnRemoveBookmark);
            this.headerPanel.Controls.Add(this.btnPasteUrl);
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1012, 47);
            this.headerPanel.TabIndex = 32;
            // 
            // StatusPanel
            // 
            this.StatusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.StatusPanel.Controls.Add(this.txtMessage);
            this.StatusPanel.Location = new System.Drawing.Point(0, 600);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(1012, 27);
            this.StatusPanel.TabIndex = 33;
            // 
            // btnFromatPreset
            // 
            this.btnFromatPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFromatPreset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFromatPreset.Location = new System.Drawing.Point(93, 433);
            this.btnFromatPreset.Name = "btnFromatPreset";
            this.btnFromatPreset.Size = new System.Drawing.Size(24, 25);
            this.btnFromatPreset.TabIndex = 34;
            this.btnFromatPreset.Text = "▾";
            this.btnFromatPreset.UseVisualStyleBackColor = true;
            // 
            // ChapterAuxiliaryDock
            // 
            this.ChapterAuxiliaryDock.Controls.Add(this.MiriSeriesLookupButton);
            this.ChapterAuxiliaryDock.Location = new System.Drawing.Point(215, 47);
            this.ChapterAuxiliaryDock.Name = "ChapterAuxiliaryDock";
            this.ChapterAuxiliaryDock.Size = new System.Drawing.Size(222, 34);
            this.ChapterAuxiliaryDock.TabIndex = 35;
            // 
            // MiriSeriesLookupButton
            // 
            this.MiriSeriesLookupButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MiriSeriesLookupButton.BackgroundImage = global::DMR11.Properties.Resources.appbar_information;
            this.MiriSeriesLookupButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MiriSeriesLookupButton.Location = new System.Drawing.Point(181, 0);
            this.MiriSeriesLookupButton.Name = "MiriSeriesLookupButton";
            this.MiriSeriesLookupButton.Size = new System.Drawing.Size(41, 34);
            this.MiriSeriesLookupButton.TabIndex = 0;
            this.MainToolTip.SetToolTip(this.MiriSeriesLookupButton, "Look up series information");
            this.MiriSeriesLookupButton.UseVisualStyleBackColor = true;
            this.MiriSeriesLookupButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MiriSeriesLookupButton_MouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1012, 627);
            this.Controls.Add(this.ChapterAuxiliaryDock);
            this.Controls.Add(this.btnFromatPreset);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.lbDefaultDestination);
            this.Controls.Add(this.lbSeriesDestination);
            this.Controls.Add(this.rdSeriesDestination);
            this.Controls.Add(this.rdDefaultDestination);
            this.Controls.Add(this.lbDestination);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.nudThread);
            this.Controls.Add(this.btnPresetDialog);
            this.Controls.Add(this.dgvChapter);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnChangeSaveTo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.dgvQueueChapter);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 644);
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueChapter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.ChapterAuxiliaryDock.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetChapter;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.DataGridView dgvQueueChapter;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnChangeSaveTo;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.DataGridView dgvChapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ComboBox cbTitleUrl;
        private System.Windows.Forms.Button btnAddBookmark;
        private System.Windows.Forms.Button btnRemoveBookmark;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.Button btnPresetDialog;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.Timer RetryDownloadTimer;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnPasteUrl;
        private System.Windows.Forms.Label lbDestination;
        private System.Windows.Forms.RadioButton rdDefaultDestination;
        private System.Windows.Forms.RadioButton rdSeriesDestination;
        private System.Windows.Forms.Label lbSeriesDestination;
        private System.Windows.Forms.Label lbDefaultDestination;
        private System.Windows.Forms.Label txtPercent;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Button btnFromatPreset;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSaveTo;
        private System.Windows.Forms.Panel ChapterAuxiliaryDock;
        private System.Windows.Forms.Button MiriSeriesLookupButton;
    }
}