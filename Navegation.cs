using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegation
{
    public partial class Navegation : Form
    {
        public Navegation()
        {
            InitializeComponent();
        }

        private void btnGoToAdd_Click(object sender, EventArgs e)
        {
            NewCustomer frm = new NewCustomer();
            frm.ShowDialog();
        }

        private void btnGoToFillOrCancel_Click(object sender, EventArgs e)
        {
            FillOrCancel frm = new FillOrCancel();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
