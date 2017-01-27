using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCRUD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora: " + DateTime.Now.ToLongTimeString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblData.Text = "Data: " + DateTime.Now.ToShortDateString();
        }

        private void contatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmCadContato frm = new frmCadContato())
            {
                frm.ShowDialog();
            }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmCadUsuario frm = new frmCadUsuario())
            {
                frm.ShowDialog();
            }
        }

        private void telefonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmCadTelefones frm = new frmCadTelefones())
            {
                frm.ShowDialog();
            }
        }
    }
}
