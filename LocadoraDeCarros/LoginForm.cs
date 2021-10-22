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
using LocadoraDeCarros;


namespace LocadoraDeCarros
{
    public partial class LoginForm : Form
    {
        Thread NewThread;
        public LoginForm()
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
        public class MySqlConexao
        {
            public MySqlConnection Conectar() // Faz um outro tipo de conexão com banco de dados
            {
                MySqlConnection conn = new MySqlConnection("User ID=root;Password=;Host=localhost;Port=3306;Database=mysql;Protocol=TCP;Compress=false;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;SslMode= 0"); 

                conn.Open();

                return conn;
            }
            public DataRow SelecionarRegistro(string sql)
            {
                DataTable tabela = new DataTable();
                using (MySqlConnection conn = Conectar())
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        tabela.Load(reader);
                }
                if (tabela.Rows.Count > 0)
                    return tabela.Rows[0];
                return null;

            }
        }

        private void btn_Cadastro_Click(object sender, EventArgs e) // Abre a interface de cadastro de novos usuários
        {
            NewThread = new Thread(novoCadastroForm);
            NewThread.SetApartmentState(ApartmentState.MTA);
            NewThread.Start();
        }
        
        private void novoCadastroForm() // gera a interface de cadastro
        {
            Application.Run(new CadastroForm());
        }

        private void novoFormLocadora() // gera a interface de gestão de carros
        {
            Application.Run(new FormLocadora());
        }
        private void btn_Entrar_Click(object sender, EventArgs e) // abre a interface da gestão de carros
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaconexaoBD = new MySqlConnection(conexaoBD.ToString());



            try
            {
                login login = new login()
                {
                    LOGIN_USU = tb_Login.Text,
                    SENHA_USU = tb_Senha.Text

                };

                tb_loginControle tb_LoginControle = new tb_loginControle();

                if (tb_LoginControle.Logar(login))
                {
                    Hide();

                    FormLocadora frm = new FormLocadora();

                    frm.Show();
                }
                else
                {
                    throw new ArgumentException("Usuário ou senha inválida!");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class login
        {
            public string NOME_USU { get; set; }
            public string LOGIN_USU { get; set; }
            public string SENHA_USU { get; set; }


        }
        public class tb_loginControle
        {
            public bool Logar(login login)
            {

                try
                {

                    if (string.IsNullOrWhiteSpace(login.LOGIN_USU))
                        throw new ArgumentException("Informe o Usuario!");

                    if (string.IsNullOrWhiteSpace(login.SENHA_USU))
                        throw new ArgumentException("Informe a senha!");

                    MySqlConexao realizarConexaoBD = new MySqlConexao();

                    DataRow linha = realizarConexaoBD.SelecionarRegistro($"SELECT * FROM usuario WHERE LOGIN_USU = '{login.LOGIN_USU}' AND SENHA_USU = '{login.SENHA_USU}'");

                    return linha != null;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}


        


    
     
