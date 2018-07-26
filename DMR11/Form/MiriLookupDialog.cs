using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMR11
{
    public class MiriLookupDialog : Dmr11BaseView, INotifyPropertyChanged
    {

        public MiriLookupDialog()
        {
            this.Load += MiriLookupDialog_Load;
        }

        void MiriLookupDialog_Load(object sender, EventArgs e)
        {
            Initialize();
            InitializeControls();
            InitializeDataBinding();
        }

        private Label SeriesNameLabel;
        private Label SeriesAuthorLabel;
        private Label SeriesIllustratorLabel;
        private Label CompletedLabel;
        private Label ScanlatedLabel;
        private Label WebsiteAttributionLabel;
        private Button OpenInBrowserButton;
        private Button CloseButton;

        protected void Initialize()
        {
            this.ShowIcon = false;
            this.Text = "MIRI Series Look up";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(415, 165);

            this.Paint += MiriLookupDialog_Paint;
        }

        void MiriLookupDialog_Paint(object sender, PaintEventArgs e)
        {
            if (SeriesNameLabel != null)
            {
                var width = Convert.ToInt32(e.ClipRectangle.Width * 0.75);
                var startWdith = Convert.ToInt32(e.ClipRectangle.Width * 0.25);

                e.Graphics.DrawLine(
                    Pens.DarkGray,
                    new Point(startWdith, SeriesNameLabel.Bottom + 5),
                    new Point(width, SeriesNameLabel.Bottom + 5)
                );
            }
        }

        protected void InitializeControls()
        {
            var secondaryDisplayFont = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point);

            SeriesNameLabel = new Label()
            {
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleCenter
            };

            SeriesAuthorLabel = new Label() { 
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                AutoSize = true,
                Font = secondaryDisplayFont
            };

            SeriesIllustratorLabel = new Label()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                AutoSize = true,
                Font = secondaryDisplayFont
            };

            CompletedLabel = new Label() {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                AutoSize = true,
                Font = secondaryDisplayFont
            };

            ScanlatedLabel = new Label() { 
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                AutoSize = true,
                Font = secondaryDisplayFont
            };

            WebsiteAttributionLabel = new Label()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                AutoSize = true,
                ForeColor = Color.DimGray,
                Text = "Results by MangaUpdates"
            };

            OpenInBrowserButton = new Button()
            {
                AutoSize = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "View in Browser"
            };

            CloseButton = new Button()
            {
                AutoSize = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Close"
            };

            SeriesNameLabel.Location = new Point(ExternalMargin, ExternalMargin);

            /// Sets the width of the series name label to the width of the Form minus the external margins on
            /// each side.
            SeriesNameLabel.Width = this.ClientRectangle.Right - ExternalMarginDouble;

            SeriesAuthorLabel.TextChanged += (_, __) => 
            {
                SeriesAuthorLabel.Location = new Point(ExternalMargin, SeriesNameLabel.Bottom + ExternalMargin);
                SeriesIllustratorLabel.Location = new Point(SeriesAuthorLabel.Right + InternalMargin, SeriesAuthorLabel.Top);
            };

            CompletedLabel.TextChanged += (_, __) => { 
                CompletedLabel.Location = new Point(ExternalMargin, SeriesAuthorLabel.Bottom + InternalMargin);
                ScanlatedLabel.Location = new Point(CompletedLabel.Right + InternalMargin, CompletedLabel.Top);
            };
            
            CloseButton.Location = new Point(this.ClientRectangle.Right - CloseButton.Width - ExternalMargin, this.ClientRectangle.Bottom - CloseButton.Height - ExternalMargin);
            OpenInBrowserButton.Location = new Point(CloseButton.Left - OpenInBrowserButton.Width - InternalMargin, CloseButton.Top);
            WebsiteAttributionLabel.Location = new Point(ExternalMargin, CloseButton.Bottom - WebsiteAttributionLabel.Height);
            
            OpenInBrowserButton.MouseClick += OpenInBrowserButton_MouseClick;

            this.CancelButton = CloseButton;
            this.Controls.AddRange(new Control[] {
                SeriesNameLabel,
                SeriesAuthorLabel,
                SeriesIllustratorLabel,
                CompletedLabel,
                ScanlatedLabel,
                // ToDo: Uncomment when ready.
                //OpenInBrowserButton,
                CloseButton,
                WebsiteAttributionLabel
            });

            RegisterButtons(secondaryButtons, this.Controls.OfType<Button>().ToArray());
            secondaryButtons.Remove(CloseButton);
            RegisterPrimaryButton(CloseButton);
            
            StyleGenericButtons();
            StylePrimaryButtons();
            StyleSecondaryButtons();
        }

        protected void InitializeDataBinding()
        {
            var seriesAuthorBinding = new Binding("Text", this, "SeriesAuthor");
            seriesAuthorBinding.Format += new ConvertEventHandler((sender, e) => e.Value = string.Format("Author: {0}", e.Value));

            var seriesCompletedBinding = new Binding("Text", this, "SeriesIsCompleted");
            seriesCompletedBinding.Format += new ConvertEventHandler(CompletedBinding_Format);

            var seriesScanlatedBinding = new Binding("Text", this, "SeriesIsFullyScanlated");
            seriesScanlatedBinding.Format += new ConvertEventHandler(SeriesScanlatedBinding_Format);
                        
            var seriesIllustratorBinding = new Binding("Text", this, "SeriesIllustrator");
            seriesIllustratorBinding.Format += new ConvertEventHandler((sender, e) => e.Value = string.Format("Illustrator: {0}", e.Value));


            SeriesNameLabel.DataBindings.Add("Text", this, "SeriesTitle", false, DataSourceUpdateMode.OnPropertyChanged);
            SeriesAuthorLabel.DataBindings.Add(seriesAuthorBinding);
            SeriesIllustratorLabel.DataBindings.Add(seriesIllustratorBinding);
            CompletedLabel.DataBindings.Add(seriesCompletedBinding);
            ScanlatedLabel.DataBindings.Add(seriesScanlatedBinding);
        }

        void CompletedBinding_Format(object sender, ConvertEventArgs e)
        {
            e.Value = string.Format( "Completed: {0}", MIRI.Helpers.Results.BoolToNaturalEnglish((bool)e.Value));
        }

        void SeriesScanlatedBinding_Format(object sender, ConvertEventArgs e)
        {
            e.Value = string.Format("Fully scanlated: {0}", MIRI.Helpers.Results.BoolToNaturalEnglish((bool)e.Value));
        }

        public async void OpenInBrowserButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Todo: Open the MangaUpdates page.
                //await Task.Run(new Action(() => System.Diagnostics.Process.Start("")));
            }
        }
        
        public string SeriesTitle
        {
            get;
            set;
        }

        private bool _seriesIsCompleted;

        private string _seriesAuthor;
        private string _seriesIllustrator;

        public string SeriesIllustrator
        {
            get
            {
                return _seriesIllustrator;
            }
            set
            {
                if (_seriesIllustrator != value)
                {
                    _seriesIllustrator = value;
                    OnPropertyChange();
                }
            }
        }

        public string SeriesAuthor
        {
            get
            {
                return _seriesAuthor;
            }
            set
            {
                if (_seriesAuthor != value)
                {
                    _seriesAuthor = value;
                    OnPropertyChange();
                }
            }
        }

        public Uri SeriesUri
        {
            get;
            set;
        }

        public bool SeriesIsCompleted
        {
            get
            {
                return _seriesIsCompleted;
            }
            set
            {
                if (_seriesIsCompleted != value)
                {
                    _seriesIsCompleted = value;
                    OnPropertyChange();
                }
            }
        }

        public bool SeriesIsFullyScanlated
        {
            get;
            set;
        }

        public DialogResult ShowDialog(MIRI.ISeriesData data)
        {
            SeriesTitle = data.Title;
            SeriesAuthor = data.Author;
            SeriesIllustrator = data.Illustrator;
            SeriesIsCompleted = data.IsCompleted;
            SeriesIsFullyScanlated = data.IsFullyScanlated;
            
            return ShowDialog();
        }

        protected void OnPropertyChange([CallerMemberName] string property = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }

}
