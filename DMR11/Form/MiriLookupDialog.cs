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
        private Label CompletedLabel;
        private Label ScanlatedLabel;
        private Button OpenInBrowserButton;
        private Button CloseButton;

        protected void Initialize()
        {
            this.ShowIcon = false;
            this.Text = "MIRI Series Look-up";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(415, 215);
        }

        protected void InitializeControls()
        {
            SeriesNameLabel = new Label()
            {
                Text = "Kono Subarashii Sekai ni Shukufuku o!",
                Font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Point)
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
            SeriesNameLabel.Width = this.ClientRectangle.Right - ExternalMarginDouble;
            CloseButton.Location = new Point(this.ClientRectangle.Right - CloseButton.Width - ExternalMargin, this.ClientRectangle.Bottom - CloseButton.Height - ExternalMargin);
            OpenInBrowserButton.Location = new Point(CloseButton.Left - OpenInBrowserButton.Width - InternalMargin, CloseButton.Top);
            
            OpenInBrowserButton.MouseClick += OpenInBrowserButton_MouseClick;

            this.CancelButton = CloseButton;
            this.Controls.AddRange(new Control[] {
                SeriesNameLabel,
                OpenInBrowserButton,
                CloseButton
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
            SeriesNameLabel.DataBindings.Add("Text", this, "SeriesTitle", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public async void OpenInBrowserButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                await Task.Run(new Action(() => System.Diagnostics.Process.Start("https://www.github.com/")));
            }
        }

        public async void LookupSeries(string seriesName)
        {

        }

        protected string SeriesTitle
        {
            get;
            set;
        }

        private bool _seriesIsCompleted;


        protected bool SeriesIsCompleted
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

        protected bool SeriesIsFullyScanlated
        {
            get;
            set;
        }

        public DialogResult ShowDialog(MIRI.ISeriesData data)
        {
            SeriesTitle = data.Title;
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
