using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMR11
{
    /// <summary>
    /// Represents the view which all DMR11 views inherit. Used to keep the layout uniform among the application.
    /// </summary>
    public abstract class Dmr11BaseView : System.Windows.Forms.Form
    {
        public Dmr11BaseView()
        {
            InitializeLayout();

        }

        /// <summary>
        /// Represents the size in pixels between controls.
        /// </summary>
        protected readonly static int InternalMargin = 7;

        /// <summary>
        /// Represents the distance in pixels between the control and the chrome.
        /// </summary>
        protected readonly static int ExternalMargin = 15;

        /// <summary>
        /// Represents a doubled distance in pixles between the control and the window chrome.
        /// </summary>
        /// <remarks>Use for alignment.</remarks>
        protected readonly static int ExternalMarginDouble = ExternalMargin * 2;
        
        protected List<Button> primaryButtons;
        protected List<Button> secondaryButtons;

        private void InitializeLayout()
        {
            primaryButtons = new List<Button>();
            secondaryButtons = new List<Button>();

            DoubleBuffered = true;
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = Color.FromArgb(230, 230, 230);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            
        }

        protected bool RegisterPrimaryButton(Button button)
        {
            return RegisterButton<Button>(primaryButtons, button);
        }

        protected void RegisterPrimaryButtons(Button[] buttons)
        {
            RegisterButtons<Button>(primaryButtons, buttons);
        }

        protected bool RegisterButton<T>(List<T> buttonList, T button)
        {
            if (!buttonList.Contains(button))
            {
                buttonList.Add(button);
                return true;
            }

            return false;
        }

        protected bool RegisterButtons<T>(List<T> buttonList, params T[] buttons)
        {
            var i = buttons.GetEnumerator();

            while (i.MoveNext())
            {
                if (!RegisterButton<T>(buttonList, (T)i.Current))
                {
                    return false;
                }
            }

            return true;
        }

        protected void StyleGenericButtons()
        {
            var buttonFont = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);

            primaryButtons.ForEach((button) =>
            {
                button.FlatStyle = FlatStyle.Flat;
                button.Font = buttonFont;
            });

            secondaryButtons.ForEach((button) =>
            {
                button.FlatStyle = FlatStyle.Flat;
                button.Font = buttonFont;
            });
        }

        protected void StylePrimaryButtons()
        {
            primaryButtons.ForEach((primaryButton) => StylePrimaryButton(primaryButton));
        }

        protected void StyleSecondaryButtons()
        {
            secondaryButtons.ForEach((secondaryButton) => StyleSecondaryButton(secondaryButton));
        }

        protected void StylePrimaryButton(Button primaryButton)
        {
            primaryButton.BackColor = Color.SlateGray;
            primaryButton.ForeColor = Color.White;
            primaryButton.FlatAppearance.BorderSize = 0;
            primaryButton.FlatAppearance.BorderColor = primaryButton.BackColor;
            primaryButton.FlatAppearance.MouseOverBackColor = Color.SlateGray;
            primaryButton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.SlateGray, 0.05f);
        }

        protected void StyleSecondaryButton(Button secondaryButton)
        {
            secondaryButton.FlatAppearance.BorderColor = Color.DarkGray;
            secondaryButton.FlatAppearance.BorderSize = 0;
            secondaryButton.FlatAppearance.MouseOverBackColor = Color.LightGray;
            secondaryButton.FlatAppearance.MouseDownBackColor = Color.Silver;

            if (secondaryButton.BackgroundImage == null)
            {
                secondaryButton.BackColor = Color.FromArgb(215, 215, 215);
                secondaryButton.ForeColor = Color.FromArgb(45, 45, 45);
            }
            else
            {
                secondaryButton.BackColor = Color.FromArgb(230, 230, 230);
            }
        }
        
    }
}
