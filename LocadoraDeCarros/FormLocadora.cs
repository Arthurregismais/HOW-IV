using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;



namespace LocadoraDeCarros
{
    
    public partial class FormLocadora : Form
    {
        Thread NewThread;
        public FormLocadora()
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

        private void limparCampos() // Cria o comando para limpar os campos
        {
            tbID.Clear();
            tbAno.Clear();
            tbNome.Clear();
            tbMarca.Clear();
            tbDescrição.Clear();
        }

        private void btLimpar_Click(object sender, EventArgs e) // chama o comando que limpa os campos
        {
            limparCampos();
        }

        private void atualizarDataGrid() // cria o comando para atualizar o Data Grid View
        {
            MySqlBaseConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            { 
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM carro WHERE ativoCarro = 1";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dgLocadoraDeCarros.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgLocadoraDeCarros.Rows[0].Clone(); // Faz um CAST e clona a linha da Tabela 
                    row.Cells[0].Value = reader.GetInt32(0); //ID
                    row.Cells[1].Value = reader.GetString(1); //Nome
                    row.Cells[2].Value = reader.GetString(2); //Marca
                    row.Cells[3].Value = reader.GetString(3); //Descrição
                    row.Cells[4].Value = reader.GetString(4); //Ano
                    dgLocadoraDeCarros.Rows.Add(row); //Adiciona a linha na tabela
                }
                realizaConexaoBD.Close();
            }
            catch (Exception ex) // verifica se algum erro ocorreu durante a execução
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    

        private void FormLocadora_Load(object sender, EventArgs e) // faz com que o Data Grid View mostre os carros do banco de dados
            {
                atualizarDataGrid();
            }

        private void btInserir_Click(object sender, EventArgs e) // adicona o carro no banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand();

                comandoMySql.CommandText = " INSERT INTO carro (nomeCarro, marcaCarro, descriçãoCarro, anoCarro ) " +
                    "VALUES('" + tbNome.Text + " ',  '" + tbMarca.Text + "', '" + tbDescrição.Text + "', " + Convert.ToInt16(tbAno.Text) + ")";
                comandoMySql.ExecuteNonQuery();

                realizarConexaoBD.Close();
                MessageBox.Show("Inserido com sucesso");
                atualizarDataGrid();
                limparCampos();
            }
            catch (Exception ex) // faz uma verificação de erro
            {
                Console.WriteLine(ex.Message);
            }

        }
        

        private void btnAlterar_Click(object sender, EventArgs e) // faz uma alteração no banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand(); 
                comandoMySql.CommandText = "UPDATE carro SET nomeCarro = '" + tbNome.Text + "', " +
                    "descriçãoCarro = '" + tbDescrição.Text + "', " +
                    "marcaCarro = '" + tbMarca.Text + "', " +
                    "anoCarro = " + Convert.ToInt16(tbAno.Text) +
                    " WHERE idCarro = " + tbID.Text + "";
                comandoMySql.ExecuteNonQuery();

                realizarConexaoBD.Close();
                MessageBox.Show("Alterado com sucesso");
                atualizarDataGrid();
                limparCampos();
            }
            catch (Exception ex) // outra verificação de erro
            {
                MessageBox.Show("Não foi possível alterar");
                Console.WriteLine(ex.Message);

            }
        }
            

        private void btnRemover_Click(object sender, EventArgs e) // faz com que o carro selecionado não apareça mais como disponível 
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand();
                //comandoMySql.CommandText = "DELETE FROM carro WHERE idCarro = " + tbID.Text + "";
                comandoMySql.CommandText = "UPDATE carro SET ativoCarro = 0 WHERE idCarro = " + tbID.Text + "";

                comandoMySql.ExecuteNonQuery();

                realizarConexaoBD.Close();
                MessageBox.Show("Deletado com Sucesso");
                atualizarDataGrid();
                limparCampos();
            }
            catch (Exception ex) // outra verificação de erro
            {
                MessageBox.Show("Não foi possível deletar");
                Console.WriteLine(ex.Message);

              }
            }

        private void dgLocadoraDeCarros_CellContentClick(object sender, DataGridViewCellEventArgs e) // insere os dados do Data Grid View nos campos
        {
            if (dgLocadoraDeCarros.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgLocadoraDeCarros.CurrentRow.Selected = true;

                tbNome.Text = dgLocadoraDeCarros.Rows[e.RowIndex].Cells["colNome"].FormattedValue.ToString();
                tbMarca.Text = dgLocadoraDeCarros.Rows[e.RowIndex].Cells["colMarca"].FormattedValue.ToString();
                tbDescrição.Text = dgLocadoraDeCarros.Rows[e.RowIndex].Cells["colDescrição"].FormattedValue.ToString();
                tbAno.Text = dgLocadoraDeCarros.Rows[e.RowIndex].Cells["colAno"].FormattedValue.ToString();
                tbID.Text = dgLocadoraDeCarros.Rows[e.RowIndex].Cells["colID"].FormattedValue.ToString();
            }
        }
        
        private void btn_TrocarUsuario_Click(object sender, EventArgs e) // volta para a interface de login
        {
            this.Close();
            NewThread = new Thread(novoLoginForm);
            NewThread.SetApartmentState(ApartmentState.MTA);
            NewThread.Start();

        }
        private void novoLoginForm() // gera o método para voltar a interface de login
        {
            Application.Run(new LoginForm());

        }

        private void novoClientesForm() // gerar a interface de gestão de clientes
        {
            Application.Run(new ClientesForm());
        }
        private void btn_GestaoClientes_Click(object sender, EventArgs e) // abre a interface de gestão de clientes
        {
            NewThread = new Thread(novoClientesForm);
            NewThread.SetApartmentState(ApartmentState.MTA);
            NewThread.Start();
        }
    }
}
    

