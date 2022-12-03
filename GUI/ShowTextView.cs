using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ShowTextView : Form
    {
        public ShowTextView(List<string> text)
        {
            InitializeComponent();
            ListBoxShow.Items.Clear();
            foreach (var item in text)
            {
                ListBoxShow.Items.Add(item);
            }
        }
        public ShowTextView(string text)
        {
            InitializeComponent();
            ListBoxShow.Items.Clear();
            ListBoxShow.Items.Add(text);
        }
    }
}
