using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvertedIndex
{
    public partial class fmMainWindow : Form
    {
        public fmMainWindow()
        {
            InitializeComponent();
        }

        private void btnCreateInvertedIndex_Click(object sender, EventArgs e)
        {
            fmInvIndex frm = new fmInvIndex();

            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnSearchByIndex_Click(object sender, EventArgs e)
        {
            fmSearchEngine frm = new fmSearchEngine();

            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
