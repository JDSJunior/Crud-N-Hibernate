using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repository.Entities;
using Repository;

namespace WinCRUD
{
    public partial class frmCadContato : Form
    {
        public frmCadContato()
        {
            InitializeComponent();
        }

        private enum Tipo
        {
            insert,
            update,
            reload
        }
        private Tipo _tipo;
        private int idRegistro = 0;
        private ContatoRepository contatoRepository;

        private void desabilitaHabilita()
        {
            btnSalvar.Enabled    = _tipo == Tipo.insert || _tipo == Tipo.update;
            btnAlterar.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnExcluir.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnCadastrar.Enabled = _tipo == Tipo.reload;
            btnCancelar.Enabled  = btnSalvar.Enabled || btnAlterar.Enabled;
        }
        private void limparCampos()
        {
            txtNome.Text           = string.Empty;
            txtEndereco.Text       = string.Empty;
            txtBairro.Text         = string.Empty;
            txtCidade.Text         = string.Empty;
            mkdCEP.Text            = string.Empty;
            mkdNumero.Text         = string.Empty;
            cbxEstado.SelectedText = string.Empty;

            idRegistro = 0;
            txtNome.Focus();
            
        }
        private void carregarRegistros()
        {
            contatoRepository = new ContatoRepository();
            grvDados.DataSource = contatoRepository.Consultar();
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtNome.Text == string.Empty)
            {
                errorProvider1.SetError(txtNome, "Preencha o campo Nome.");
                return;
            }
            if (txtEndereco.Text == string.Empty)
            {
                errorProvider1.SetError(txtEndereco, "Preencha o campo Endereço.");
                return;
            }
            if (txtBairro.Text == string.Empty)
            {
                errorProvider1.SetError(txtBairro, "Preencha o campo Endereço.");
                return;
            }
            if (mkdNumero.Text == string.Empty)
            {
                errorProvider1.SetError(mkdNumero, "Preencha o campo numero");
                return;
            }

            contatoRepository = new ContatoRepository();
            if(_tipo == Tipo.insert)
            {
                if(contatoRepository.validarContato(txtNome.Text.Trim()))
                {
                    MessageBox.Show("Contato encontra-se cadastrado!","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
            }

            try
            {
                Contato model = new Contato();
                model.nome     = txtNome.Text;
                model.endereco = txtEndereco.Text;
                model.bairro   = txtBairro.Text;
                model.cidade   = txtCidade.Text;
                model.cep      = mkdCEP.Text;
                model.numero   = mkdNumero.Text;
                model.estado   = cbxEstado.Text;
                model.status   = "A";
                if(_tipo == Tipo.insert)
                {
                    contatoRepository.Inserir(model);
                    MessageBox.Show("Contato cadastrado com sucesso!", "Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtNome.Focus();
                }
                else if(_tipo == Tipo.update)
                {
                    model.id = idRegistro;
                    contatoRepository.Alterar(model);
                    MessageBox.Show("Contato Cadastrado com sucesso!","Informação",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    groupBox2.Enabled = false;
                    _tipo = Tipo.reload;
                }
                carregarRegistros();
                limparCampos();
                desabilitaHabilita();
            }
            catch (Exception ex)
            {
                if (_tipo == Tipo.insert)
                {
                    MessageBox.Show("Erro ao cadastrar contato:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (_tipo == Tipo.update)
                {
                    MessageBox.Show("Erro ao atualizar contato:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.insert;
            groupBox2.Enabled = true;
            txtNome.Focus();
            desabilitaHabilita();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.reload;
            carregarRegistros();
            desabilitaHabilita();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                contatoRepository = new ContatoRepository();
                Contato model = contatoRepository.RetornarPorId(Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value));
                contatoRepository.Excluir(model);
                carregarRegistros();
                MessageBox.Show("Registro Excuido", "informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.update;
            groupBox2.Enabled = true;
            desabilitaHabilita();

            idRegistro = Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value);
            txtNome.Text           = grvDados.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text       = grvDados.CurrentRow.Cells[2].Value.ToString();
            txtBairro.Text         = grvDados.CurrentRow.Cells[3].Value.ToString();
            txtCidade.Text         = grvDados.CurrentRow.Cells[4].Value.ToString();
            mkdCEP.Text            = grvDados.CurrentRow.Cells[7].Value.ToString();
            mkdNumero.Text         = grvDados.CurrentRow.Cells[5].Value.ToString();
            cbxEstado.SelectedItem = grvDados.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.reload;
            limparCampos();
            desabilitaHabilita();
            groupBox2.Enabled = false;
        }

    }
}
