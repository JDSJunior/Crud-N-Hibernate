using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repository;
using Repository.Entities;

namespace WinCRUD
{
    public partial class frmCadUsuario : Form
    {
        private enum Tipo
        {
            insert,
            update,
            reload
        }
        private Tipo _tipo;
        private int idRegistro = 0;


        private UsuarioRepository usuarioRepository;
        public frmCadUsuario()
        {
            InitializeComponent();
        }
        private void carregarRegistros()
        {
            usuarioRepository = new UsuarioRepository();

            grvDados.DataSource = usuarioRepository.Consultar();
        }
        private void limparCampos()
        {
            txtNome.Text  = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            idRegistro    = 0;

            txtNome.Focus();
        }
        private void desabilitaHabilita()
        {
            btnSalvar.Enabled    = _tipo == Tipo.insert || _tipo == Tipo.update;
            btnAlterar.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnExcluir.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload; 
            btnCadastrar.Enabled = _tipo == Tipo.reload;
            btnCancelar.Enabled  = btnSalvar.Enabled || btnAlterar.Enabled; ;
        }


        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.reload;
            carregarRegistros();
            desabilitaHabilita();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.insert;
            groupBox2.Enabled = true;
            txtNome.Focus();
            desabilitaHabilita();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grvDados.RowCount > 0)
            {
                try
                {
                    usuarioRepository = new UsuarioRepository();
                    Usuario model = usuarioRepository.RetornarPorId(Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value));
                    usuarioRepository.Excluir(model);
                    carregarRegistros();
                    MessageBox.Show("Registro Excluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro a o excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtNome.Text == string.Empty)
            {
                errorProvider1.SetError(txtNome, "Preencha o campo nome.");
                return;
            }
            if (txtLogin.Text == string.Empty)
            {
                errorProvider1.SetError(txtLogin, "Preencha o campo login.");
                return;
            }
            if (txtSenha.Text == string.Empty)
            {
                errorProvider1.SetError(txtSenha, "Preencha o campo senha,");
                return;
            }

            usuarioRepository = new UsuarioRepository();
            if (_tipo == Tipo.insert)
            {
                if(usuarioRepository.validarLogin(txtLogin.Text.Trim()))
                {
                    MessageBox.Show("Login encontra-se cadastrado!","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
            }
            try
            {
                Usuario model = new Usuario();
                model.nome    = txtNome.Text;
                model.login   = txtLogin.Text;
                model.senha   = txtSenha.Text;
                model.status  = "A";
                if (_tipo == Tipo.insert)
                {
                    usuarioRepository.Inserir(model);
                    MessageBox.Show("Usuário cadastrado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus();
                }
                else if(_tipo == Tipo.update)
                {
                    model.id = idRegistro;
                    usuarioRepository.Alterar(model);
                    MessageBox.Show("Usuário Atualizado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }
                if (_tipo == Tipo.update)
                {
                    MessageBox.Show("Erro ao atualizar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
 
           // MessageBox.Show("OK");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.update;
            groupBox2.Enabled = true;
            desabilitaHabilita();

            idRegistro    = Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value);
            txtNome.Text  = grvDados.CurrentRow.Cells[1].Value.ToString();
            txtLogin.Text = grvDados.CurrentRow.Cells[2].Value.ToString();
            txtSenha.Text = grvDados.CurrentRow.Cells[3].Value.ToString();


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
