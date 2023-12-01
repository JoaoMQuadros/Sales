using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Navegation
{
    public partial class FillOrCancel : Form
    {
        public FillOrCancel()
        {
            InitializeComponent();
        }
        // Armazenamento para o valor do ID do pedido.
        private int parsedOrderID;

        // Verifica se um ID de pedido está presente e contém caracteres válidos.
        private bool IsOrderIDValid()
        {
            // Verifique a entrada na caixa de texto ID do pedido.
            if (txtOrderID.Text == "")
            {
                MessageBox.Show("Especifique o ID do pedido.");
                return false;
            }

            // Verifica se há caracteres diferentes de números inteiros.
            else if (Regex.IsMatch(txtOrderID.Text, @"^\D*$"))
            {
                //Mostra mensagem e limpa entrada.
                MessageBox.Show("O ID do cliente deve conter apenas números.");
                txtOrderID.Clear();
                return false;
            }
            else
            {
                // Converte o texto da caixa de texto em um número inteiro para enviar ao banco de dados.
                parsedOrderID = Int32.Parse(txtOrderID.Text);
                return true;
            }
        }

        // Executa uma instrução t-SQL SELECT para obter dados de pedido para um determinado
        // ID do pedido e, em seguida, exibe-o no DataGridView do formulário.
        private void btnFindByOrderID_Click(object sender, EventArgs e)
        {
            if (IsOrderIDValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    // Define uma string de consulta t-SQL que possui um parâmetro para orderID.
                    const string sql = "SELECT * FROM Sales.Orders WHERE orderID = @orderID";

                    // Cria um objeto SqlCommand.
                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        // Defina o parâmetro @orderID e defina seu valor.
                        sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                        sqlCommand.Parameters["@orderID"].Value = parsedOrderID;

                        try
                        {
                            connection.Open();

                            // Execute a consulta chamando ExecuteReader().
                            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                            {
                                // Cria uma tabela de dados para armazenar os dados recuperados.
                                DataTable dataTable = new DataTable();

                                // Carrega os dados do SqlDataReader na tabela de dados.
                                dataTable.Load(dataReader);

                                // Exibe os dados da tabela de dados na visualização da grade de dados.
                                this.dgvCustomerOrders.DataSource = dataTable;

                                //Fecha o SqlDataReader.
                                dataReader.Close();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("O pedido solicitado não pôde ser carregado no formulário.");
                        }
                        finally
                        {
                            //Fecha a conexão.
                            connection.Close();
                        }
                    }
                }
            }
        }

        // Cancela um pedido chamando Sales.uspCancelOrder
        // procedimento armazenado no banco de dados.
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (IsOrderIDValid())
            {
                // Cria a conexão.
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    // Crie o objeto SqlCommand e identifique-o como um procedimento armazenado.
                    using (SqlCommand sqlCommand = new SqlCommand("Sales.uspCancelOrder", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Adicione o parâmetro de entrada do ID do pedido para o procedimento armazenado.
                        sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                        sqlCommand.Parameters["@orderID"].Value = parsedOrderID;

                        try
                        {
                            //Abre a conexão.
                            connection.Open();

                            // Execute o comando para executar o procedimento armazenado.
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("A operação de cancelamento não foi concluída.");
                        }
                        finally
                        {
                            //Fecha a conexão.
                            connection.Close();
                        }
                    }
                }
            }
        }

        // Preenche um pedido chamando o Sales.uspFillOrder armazenado
        // procedimento no banco de dados.
        private void btnFillOrder_Click(object sender, EventArgs e)
        {
            if (IsOrderIDValid())
            {
                // Cria a conexão.
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    // Cria o comando e identifica-o como um procedimento armazenado.
                    using (SqlCommand sqlCommand = new SqlCommand("Sales.uspFillOrder", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Adicione o parâmetro de entrada do ID do pedido para o procedimento armazenado.
                        sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                        sqlCommand.Parameters["@orderID"].Value = parsedOrderID;

                        // Adicione o parâmetro de entrada de data preenchido para o procedimento armazenado.
                        sqlCommand.Parameters.Add(new SqlParameter("@FilledDate", SqlDbType.DateTime, 8));
                        sqlCommand.Parameters["@FilledDate"].Value = dtpFillDate.Value;

                        try
                        {
                            connection.Open();

                            //Executa o procedimento armazenado.
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("A operação de preenchimento não foi concluída.");
                        }
                        finally
                        {
                            //Fecha a conexão.
                            connection.Close();
                        }
                    }
                }
            }
        }

        //Fecha o formulário.
        private void btnFinishUpdates_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
