using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinCRUD.Helper;

namespace WinCRUD
{
    public partial class frmCadTelefones : Form
    {
        public frmCadTelefones()
        {
            InitializeComponent();
        }

        private void Limpar()
        {
            foreach (var item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Text = string.Empty;
                }
                if (item is MaskedTextBox)
                {
                    (item as MaskedTextBox).Text = string.Empty;
                }

                cbxContato.SelectedIndex = 0;
                cbxTipo.SelectedItem     = 0;
                cbxContato.Focus();
            }
        }

        private void frmCadTelefones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void frmCadTelefones_Load(object sender, EventArgs e)
        {
            ContatoRepository repositoryContatos = new ContatoRepository();
            cbxContato.DataSource                = repositoryContatos.Consultar();
            cbxPesquisarContato.DataSource       = repositoryContatos.Consultar();
            cbxTipo.SelectedValue = 0;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (mkdTelefone.Text.Replace("_", "").Replace("(", "").Replace(")", "").Replace("-", "").Trim().Length < 10)
            {
                errorProvider1.SetError(mkdTelefone, "Telefone Inválido");
                return;
            }


            if (!string.IsNullOrEmpty(txtEmail.Text) &&  !Funcoes.validarEmail(txtEmail, errorProvider1))
            {
                return;
            }

            if (!string.IsNullOrEmpty(txtSite.Text) && !Funcoes.validarURL(txtSite, errorProvider1))
            {
                return;
            }
            try
            {
                TelefoneRepository repositoryTelefone = new TelefoneRepository();
                Telefone telefoneModel = new Telefone();

                telefoneModel.id_contato = Convert.ToInt32(cbxContato.SelectedValue);
                telefoneModel.email = txtEmail.Text.Trim();
                telefoneModel.telefone = mkdTelefone.Text.Trim();
                telefoneModel.site = txtSite.Text.Trim();
                telefoneModel.tipo = cbxTipo.Text;
                repositoryTelefone.Inserir(telefoneModel);
                Limpar();
                MessageBox.Show("Contato cadastrado com sucesso", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao cadastrar contato: " +ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            TelefoneRepository repositoryTelefone = new TelefoneRepository();
            dgvPesquisaContato.DataSource = repositoryTelefone.retornarPorContato(Convert.ToInt32(cbxPesquisarContato.SelectedValue));
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvPesquisaContato.RowCount > 0)
            {
                if (MessageBox.Show("Deseja excluir?","Atenção",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TelefoneRepository repositoryTelefone = new TelefoneRepository();

                    int codigo = Convert.ToInt32(dgvPesquisaContato.CurrentRow.Cells[0].Value.ToString());
                    Telefone telefoneModel = repositoryTelefone.RetornarPorId(codigo);

                    try
                    {
                        repositoryTelefone.Excluir(telefoneModel);
                        MessageBox.Show("Registro excluido com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnPesquisar_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir registro: "+ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Não há registro(s) para excluir!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
 