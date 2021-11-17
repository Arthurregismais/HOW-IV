using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace LocadoraDeCarros
{
    public partial class CadastroForm : Form // cria o form de cadastro
    {
        public CadastroForm()
        {
            InitializeComponent();
        }

        private MySqlConnectionStringBuilder conexaoBanco() // faz a conexão com o banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "mysql";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }

        private void limparCampos() // limpa os campos
        {
            tbLogin.Clear();
            tbSenha.Clear();
            tbUsuario.Clear();
        }
        private void btnNovoCadastro_Click(object sender, EventArgs e) // Cadastra o usuário no banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "INSERT INTO usuario (NOME_USU, LOGIN_USU, SENHA_USU) " + "VALUES ('" + tbUsuario.Text + " ', '" + tbLogin.Text + " ', '" + tbSenha.Text + " ') ";

                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Cadastrado com sucesso");
                
                limparCampos();
            }
            catch (Exception ex) // verificação de erro
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
