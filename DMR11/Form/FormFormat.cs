using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMR11
{
    public partial class FormFormat : Form
    {
        public FormFormat(IDictionary<string, string> chapterValues)
        {
            InitializeComponent();
        }

        public string Formatting
        {
            get;
            protected set;
        }


        public void LoadFormatPresets()
        {
        }

        public void LoadSavedFormatting()
        {
        }

    }
}
