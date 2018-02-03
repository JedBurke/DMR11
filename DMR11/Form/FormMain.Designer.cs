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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnChangeSaveTo = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnOptions = new System.Windows.Forms.Button();
            this.dgvChapter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbTitleUrl = new System.Windows.Forms.ComboBox();
            this.btnAddBookmark = new System.Windows.Forms.Button();
            this.btnRemoveBookmark = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddPrefixCounter = new System.Windows.Forms.Button();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.btnPasteUrl = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lbSeriesDestination = new System.Windows.Forms.Label();
            this.lbDefaultDestination = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbDestination = new System.Windows.Forms.Label();
            this.rdDefaultDestination = new System.Windows.Forms.RadioButton();
            this.rdSeriesDestination = new System.Windows.Forms.RadioButton();
            this.txtPercent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueChapter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetChapter
            // 
            this.btnGetChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetChapter.Location = new System.Drawing.Point(901, 10);
            this.btnGetChapter.Name = "btnGetChapter";
            this.btnGetChapter.Size = new System.Drawing.Size(91, 23);
            this.btnGetChapter.TabIndex = 5;
            this.btnGetChapter.Text = "Get Chapters";
            this.toolTip1.SetToolTip(this.btnGetChapter, "Retrieve chapters from the current URL");
            this.btnGetChapter.UseVisualStyleBackColor = true;
            this.btnGetChapter.Click += new System.EventHandler(this.btnGetChapter_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(800, 563);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(108, 23);
            this.btnDownload.TabIndex = 18;
            this.btnDownload.Text = "Start Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(143, 427);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(163, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Selected";
            this.toolTip1.SetToolTip(this.btnAdd, "Add selected chapters to queue");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddAll.Location = new System.Drawing.Point(311, 427);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(60, 23);
            this.btnAddAll.TabIndex = 9;
            this.btnAddAll.Text = "All";
            this.toolTip1.SetToolTip(this.btnAddAll, "Add all chapters to queue");
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(537, 564);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(111, 23);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "Remove Selected";
            this.toolTip1.SetToolTip(this.btnRemove, "Remove selected chapters from the queue");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveAll.Location = new System.Drawing.Point(654, 564);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(87, 23);
            this.btnRemoveAll.TabIndex = 17;
            this.btnRemoveAll.Text = "Remove All";
            this.toolTip1.SetToolTip(this.btnRemoveAll, "Remove all chapters from the queue");
            this.btnRemoveAll.UseVisualStyleBackColor = true;
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
            this.dgvQueueChapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueueChapter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColChapterName,
            this.ColChapterStatus,
            this.ColChapterUrl});
            this.dgvQueueChapter.Location = new System.Drawing.Point(443, 38);
            this.dgvQueueChapter.Name = "dgvQueueChapter";
            this.dgvQueueChapter.ReadOnly = true;
            this.dgvQueueChapter.RowHeadersVisible = false;
            this.dgvQueueChapter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueueChapter.Size = new System.Drawing.Size(549, 519);
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
            this.ColChapterUrl.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColChapterUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(914, 563);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(78, 23);
            this.btnStop.TabIndex = 19;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnChangeSaveTo
            // 
            this.btnChangeSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeSaveTo.Location = new System.Drawing.Point(341, 472);
            this.btnChangeSaveTo.Name = "btnChangeSaveTo";
            this.btnChangeSaveTo.Size = new System.Drawing.Size(30, 22);
            this.btnChangeSaveTo.TabIndex = 13;
            this.btnChangeSaveTo.Text = "...";
            this.toolTip1.SetToolTip(this.btnChangeSaveTo, "Change folder");
            this.btnChangeSaveTo.UseVisualStyleBackColor = true;
            this.btnChangeSaveTo.Click += new System.EventHandler(this.btnChangeSaveTo_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFolder.Location = new System.Drawing.Point(377, 472);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(60, 22);
            this.btnOpenFolder.TabIndex = 14;
            this.btnOpenFolder.Text = "Open";
            this.toolTip1.SetToolTip(this.btnOpenFolder, "Open destination folder");
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(12, 14);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(27, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL";
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOptions.Location = new System.Drawing.Point(443, 564);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(88, 23);
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
            this.dgvChapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChapter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4});
            this.dgvChapter.Location = new System.Drawing.Point(12, 38);
            this.dgvChapter.Name = "dgvChapter";
            this.dgvChapter.ReadOnly = true;
            this.dgvChapter.RowHeadersVisible = false;
            this.dgvChapter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChapter.Size = new System.Drawing.Size(425, 383);
            this.dgvChapter.TabIndex = 6;
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
            this.cbTitleUrl.Location = new System.Drawing.Point(40, 11);
            this.cbTitleUrl.Name = "cbTitleUrl";
            this.cbTitleUrl.Size = new System.Drawing.Size(703, 21);
            this.cbTitleUrl.TabIndex = 1;
            this.cbTitleUrl.Text = global::DMR11.Properties.Settings.Default.Url;
            // 
            // btnAddBookmark
            // 
            this.btnAddBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBookmark.Location = new System.Drawing.Point(775, 9);
            this.btnAddBookmark.Name = "btnAddBookmark";
            this.btnAddBookmark.Size = new System.Drawing.Size(24, 23);
            this.btnAddBookmark.TabIndex = 2;
            this.btnAddBookmark.Text = "B";
            this.toolTip1.SetToolTip(this.btnAddBookmark, "Bookmark this URL");
            this.btnAddBookmark.UseVisualStyleBackColor = true;
            this.btnAddBookmark.Click += new System.EventHandler(this.btnAddBookmark_Click);
            // 
            // btnRemoveBookmark
            // 
            this.btnRemoveBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBookmark.Location = new System.Drawing.Point(805, 9);
            this.btnRemoveBookmark.Name = "btnRemoveBookmark";
            this.btnRemoveBookmark.Size = new System.Drawing.Size(24, 23);
            this.btnRemoveBookmark.TabIndex = 3;
            this.btnRemoveBookmark.Text = "R";
            this.toolTip1.SetToolTip(this.btnRemoveBookmark, "Remove this bookmarked URL");
            this.btnRemoveBookmark.UseVisualStyleBackColor = true;
            this.btnRemoveBookmark.Click += new System.EventHandler(this.btnRemoveBookmark_Click);
            // 
            // btnAddPrefixCounter
            // 
            this.btnAddPrefixCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPrefixCounter.Location = new System.Drawing.Point(12, 427);
            this.btnAddPrefixCounter.Name = "btnAddPrefixCounter";
            this.btnAddPrefixCounter.Size = new System.Drawing.Size(120, 23);
            this.btnAddPrefixCounter.TabIndex = 7;
            this.btnAddPrefixCounter.Text = "Add Prefix Counter";
            this.toolTip1.SetToolTip(this.btnAddPrefixCounter, "Add prefix counter to chapter name");
            this.btnAddPrefixCounter.UseVisualStyleBackColor = true;
            this.btnAddPrefixCounter.Click += new System.EventHandler(this.btnAddPrefixCounter_Click);
            // 
            // nudThread
            // 
            this.nudThread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudThread.Location = new System.Drawing.Point(747, 564);
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
            this.nudThread.Size = new System.Drawing.Size(47, 22);
            this.nudThread.TabIndex = 24;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.nudThread, "Maximum number of connections");
            this.nudThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnPasteUrl
            // 
            this.btnPasteUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteUrl.Location = new System.Drawing.Point(749, 9);
            this.btnPasteUrl.Name = "btnPasteUrl";
            this.btnPasteUrl.Size = new System.Drawing.Size(24, 23);
            this.btnPasteUrl.TabIndex = 25;
            this.btnPasteUrl.Text = "P";
            this.toolTip1.SetToolTip(this.btnPasteUrl, "Paste URL from clipboard and get chapters");
            this.btnPasteUrl.UseVisualStyleBackColor = true;
            this.btnPasteUrl.Click += new System.EventHandler(this.btnPasteUrl_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.Location = new System.Drawing.Point(377, 427);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(60, 23);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "New";
            this.toolTip1.SetToolTip(this.btnAddNew, "Add latest chapters to queue\r\nHold SHIFT to only add the 5 latest chapters");
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lbSeriesDestination
            // 
            this.lbSeriesDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSeriesDestination.AutoSize = true;
            this.lbSeriesDestination.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeriesDestination.Location = new System.Drawing.Point(57, 550);
            this.lbSeriesDestination.MaximumSize = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.MinimumSize = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.Name = "lbSeriesDestination";
            this.lbSeriesDestination.Size = new System.Drawing.Size(380, 17);
            this.lbSeriesDestination.TabIndex = 29;
            this.lbSeriesDestination.Text = "Series-Specific Destination";
            this.toolTip1.SetToolTip(this.lbSeriesDestination, "Saves the chapter to the series\' folder");
            this.lbSeriesDestination.UseMnemonic = false;
            this.lbSeriesDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbSeriesDestination_MouseClick);
            // 
            // lbDefaultDestination
            // 
            this.lbDefaultDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDefaultDestination.AutoSize = true;
            this.lbDefaultDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DMR11.Properties.Settings.Default, "SaveTo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lbDefaultDestination.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDefaultDestination.Location = new System.Drawing.Point(57, 507);
            this.lbDefaultDestination.MaximumSize = new System.Drawing.Size(385, 17);
            this.lbDefaultDestination.MinimumSize = new System.Drawing.Size(380, 17);
            this.lbDefaultDestination.Name = "lbDefaultDestination";
            this.lbDefaultDestination.Size = new System.Drawing.Size(380, 17);
            this.lbDefaultDestination.TabIndex = 30;
            this.lbDefaultDestination.Text = global::DMR11.Properties.Settings.Default.SaveTo;
            this.toolTip1.SetToolTip(this.lbDefaultDestination, "Saves the chapter to the default manga folder");
            this.lbDefaultDestination.UseMnemonic = false;
            this.lbDefaultDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbDefaultDestination_MouseClick);
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Location = new System.Drawing.Point(0, 592);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(1012, 22);
            this.txtMessage.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbDestination
            // 
            this.lbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDestination.AutoSize = true;
            this.lbDestination.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDestination.Location = new System.Drawing.Point(12, 472);
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
            this.rdDefaultDestination.Location = new System.Drawing.Point(37, 510);
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
            this.rdSeriesDestination.Location = new System.Drawing.Point(37, 553);
            this.rdSeriesDestination.Name = "rdSeriesDestination";
            this.rdSeriesDestination.Size = new System.Drawing.Size(14, 13);
            this.rdSeriesDestination.TabIndex = 28;
            this.rdSeriesDestination.UseVisualStyleBackColor = true;
            // 
            // txtPercent
            // 
            this.txtPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPercent.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPercent.Location = new System.Drawing.Point(847, 11);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(48, 21);
            this.txtPercent.TabIndex = 31;
            this.txtPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 614);
            this.Controls.Add(this.txtPercent);
            this.Controls.Add(this.lbDefaultDestination);
            this.Controls.Add(this.lbSeriesDestination);
            this.Controls.Add(this.rdSeriesDestination);
            this.Controls.Add(this.rdDefaultDestination);
            this.Controls.Add(this.lbDestination);
            this.Controls.Add(this.btnPasteUrl);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.nudThread);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnAddPrefixCounter);
            this.Controls.Add(this.btnRemoveBookmark);
            this.Controls.Add(this.btnAddBookmark);
            this.Controls.Add(this.cbTitleUrl);
            this.Controls.Add(this.dgvChapter);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.btnChangeSaveTo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.dgvQueueChapter);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnGetChapter);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 644);
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueChapter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChapterUrl;
        private System.Windows.Forms.ComboBox cbTitleUrl;
        private System.Windows.Forms.Button btnAddBookmark;
        private System.Windows.Forms.Button btnRemoveBookmark;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAddPrefixCounter;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnPasteUrl;
        private System.Windows.Forms.Label lbDestination;
        private System.Windows.Forms.RadioButton rdDefaultDestination;
        private System.Windows.Forms.RadioButton rdSeriesDestination;
        private System.Windows.Forms.Label lbSeriesDestination;
        private System.Windows.Forms.Label lbDefaultDestination;
        private System.Windows.Forms.Label txtPercent;
    }
}