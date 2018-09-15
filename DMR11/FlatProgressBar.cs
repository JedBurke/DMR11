using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMR11.Controls
{
    public class FlatProgressBar : Control, INotifyPropertyChanged
    {
        private double _progressValue = 0.0f;

        public double ProgressValue
        {
            get
            {
                return _progressValue;
            }
            set
            {
                if (_progressValue != value)
                {
                    _progressValue = value;
                    Invalidate();

                    if (_progressValue >= 1.0)
                    {
                        if (Completed != null)
                        {
                            var handler = Completed;

                            if (handler != null)
                            {
                                handler(this, EventArgs.Empty);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("same: {0}", value);
                }
            }
        }

        public int Duration
        {
            get;
            set;
        }

        private Color _progressColor;

        public Color ProgressColor
        {
            get
            {
                return _progressColor;
            }
            set
            {
                if (_progressColor != value)
                {
                    _progressColor = value;

                    if (ProgressBrush != null)
                        ProgressBrush.Dispose();

                    ProgressBrush = new SolidBrush(_progressColor);
                }
            }
        }

        public FlatProgressBar()
        {
            ProgressColor = Color.SlateGray;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            this.Paint += FlatProgressBar_Paint;

        }

        protected float _interval = 0;

        protected SolidBrush ProgressBrush { get; set; }

        void FlatProgressBar_Paint(object sender, PaintEventArgs e)
        {
            var fillArea = CalculateFillArea(ProgressValue);

            if (fillArea.Width < ClientRectangle.Width)
            {
                e.Graphics.FillRectangle(Brushes.Silver, e.ClipRectangle);
            }

            e.Graphics.FillRectangle(ProgressBrush, fillArea);

        }

        Rectangle CalculateFillArea(double value)
        {
            var fillAreaRect = ClientRectangle;

            var area = (this.ClientRectangle.Right * value);
            fillAreaRect.Width = Convert.ToInt32(area);

            return fillAreaRect;
        }

        Rectangle CalculateRemainingArea(double value)
        {
            var remainingArea = CalculateFillArea(value);

            remainingArea.X = remainingArea.Right;
            remainingArea.Width = Convert.ToInt32(ClientRectangle.Width - remainingArea.Width);

            return remainingArea;
        }

        protected double SetProgressValue(double value, double duration = 0)
        {
            if (value > 1.0)
            {
                value = 1.0;
            }
            else if (value < 0.0)
            {
                value = 0.0;
            }



            return value;
        }

        public event EventHandler Completed = delegate { };
        public event EventHandler ProgressChanged;
        public event EventHandler Started;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }

}
