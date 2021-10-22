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
    public partial class ClientesForm : Form
    {
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
        public ClientesForm()
        {
            InitializeComponent();
        }

        private void ClientesForm_Load(object sender, EventArgs e) // faz com que o data grid view exiba os dados na tabela
        {
            atualizarDataGrid();
        }
        private void limparCampos() // Cria o comando para limpar os campos
        {
            tbID.Clear();
            tbTelefone.Clear();
            tbNome.Clear();
            tbCidade.Clear();
        }

        private void btn_Limpar_Click(object sender, EventArgs e) // chama o comando que limpa os campos
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
                comandoMySql.CommandText = "SELECT * FROM cliente WHERE ativoCliente = 1";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dgGestaoClientes.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgGestaoClientes.Rows[0].Clone(); // Faz um CAST e clona a linha da Tabela 
                    row.Cells[0].Value = reader.GetInt32(0); //ID
                    row.Cells[1].Value = reader.GetString(1); //Nome
                    row.Cells[2].Value = reader.GetInt32(2); //Telefone
                    row.Cells[3].Value = reader.GetString(3); //Cidade
                    
                    dgGestaoClientes.Rows.Add(row); //Adiciona a linha na tabela
                }
                realizaConexaoBD.Close();
            }
            catch (Exception ex) // verifica se algum erro ocorreu durante a execução
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        private void dgGestaoClientes_CellContentClick_1(object sender, DataGridViewCellEventArgs e) // insere os dados do Data Grid View nos campos
        {
            if (dgGestaoClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgGestaoClientes.CurrentRow.Selected = true;

                tbNome.Text = dgGestaoClientes.Rows[e.RowIndex].Cells["colNome"].FormattedValue.ToString();
                tbCidade.Text = dgGestaoClientes.Rows[e.RowIndex].Cells["colCidade"].FormattedValue.ToString();
                tbTelefone.Text = dgGestaoClientes.Rows[e.RowIndex].Cells["colTelefone"].FormattedValue.ToString();
                tbID.Text = dgGestaoClientes.Rows[e.RowIndex].Cells["colID"].FormattedValue.ToString();
                
            }
        }
        

        private void btn_Inserir_Click(object sender, EventArgs e) // adiciona o cliente no banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand();

                comandoMySql.CommandText = " INSERT INTO cliente (nomeCliente, telefoneCliente, cidadeCliente ) " +
                    "VALUES('" + tbNome.Text + " ',  '" + Convert.ToInt32(tbTelefone.Text) + "', '" + tbCidade.Text + ")";
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

        private void btn_Remover_Click(object sender, EventArgs e) // troca o cliente para inativo, ou seja ativoCliente = 0
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand();
                
                comandoMySql.CommandText = "UPDATE cliente SET ativoCliente = 0 WHERE idCliente = " + tbID.Text + "";

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

        private void btn_Alterar_Click(object sender, EventArgs e) // altera os dados no banco de dados
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizarConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizarConexaoBD.Open();

                MySqlCommand comandoMySql = realizarConexaoBD.CreateCommand();
                comandoMySql.CommandText = "UPDATE cliente SET nomeCliente = '" + tbNome.Text + "', "  +
                    "cidadeCliente = '" + tbCidade.Text + "', " +
                    "telefoneCliente = " + Convert.ToInt32(tbTelefone.Text) + 
                    
                    " WHERE idCliente = " + tbID.Text + "";
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

        private void btn_Atualizar_Click(object sender, EventArgs e)
        {
            atualizarDataGrid();
        }
    }
}
