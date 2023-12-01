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

namespace Navegation
{
    public partial class NewCustomer : Form
    {
        public NewCustomer()
        {
            InitializeComponent();
        }
        //Armazenamento dos valores IDENTITY retornados do banco de dados.
        private int analisadoCustomerID;
        private int orderID;

        /// Verifica se a caixa de texto do nome do cliente não está vazia.
        private bool IsCustomerNameValid()
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter a name.");
                return false;
            }
            else
            {
                return true;
            }
        }

        // Verifica se a caixa de texto do nome do cliente não está vazia. 
        // Verifica se um ID do cliente e o valor do pedido foram fornecidos.
        private bool IsOrderDataValid()
        {
            // Verifique se CustomerID está presente.
            if (txtCustomerID.Text == "")
            {
                MessageBox.Show("Por favor, crie uma conta de cliente antes de fazer o pedido.");
                return false;
            }
            // Verifique se Amount não é 0.
            else if ((numOrderAmount.Value < 1))
            {
                MessageBox.Show("Especifique o valor do pedido.");
                return false;
            }
            else
            {
                // O pedido pode ser enviado.
                return true;
            }
        }

        // Limpa os dados do formulário.
        private void ClearForm()
        {
            txtCustomerName.Clear();
            txtCustomerID.Clear();
            dtpOrderDate.Value = DateTime.Now;
            numOrderAmount.Value = 0;
            this.analisadoCustomerID = 0;
        }
        
        // Cria um novo cliente chamando o procedimento armazenado Sales.uspNewCustomer.
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (IsCustomerNameValid())
            {
                // Cria a conexão.
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    // Crie um SqlCommand e identifique-o como um procedimento armazenado.
                    using (SqlCommand sqlCommand = new SqlCommand("Sales.uspNewCustomer", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Adicione o parâmetro de entrada para o procedimento armazenado e especifique o que usar como valor.
                        sqlCommand.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar, 40));
                        sqlCommand.Parameters["@CustomerName"].Value = txtCustomerName.Text;

                        // Adicione o parâmetro de saída.
                        sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                        sqlCommand.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

                        try
                        {
                            connection.Open();

                            //Executa o procedimento armazenado.
                            sqlCommand.ExecuteNonQuery();

                            // ID do cliente é um valor IDENTITY do banco de dados.
                            analisadoCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                            // Coloque o valor do ID do cliente na caixa de texto somente leitura.
                            this.txtCustomerID.Text = Convert.ToString(analisadoCustomerID);
                        }
                        catch
                        {
                            MessageBox.Show("O ID do cliente não foi retornado. Não foi possível criar a conta.");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        // Chama o procedimento armazenado Sales.uspPlaceNewOrder para fazer um pedido.
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // Certifique-se de que a entrada necessária esteja presente.
            if (IsOrderDataValid())
            {
                // Cria a conexão.
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    // Cria SqlCommand e identifica-o como um procedimento armazenado.
                    using (SqlCommand sqlCommand = new SqlCommand("Sales.uspPlaceNewOrder", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Adicione o parâmetro de entrada @CustomerID, obtido de uspNewCustomer.
                        sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                        sqlCommand.Parameters["@CustomerID"].Value = this.analisadoCustomerID;

                        // Adicione o parâmetro de entrada @OrderDate.
                        sqlCommand.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime, 8));
                        sqlCommand.Parameters["@OrderDate"].Value = dtpOrderDate.Value;

                        // Adicione o parâmetro de entrada do valor do pedido @Amount.
                        sqlCommand.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                        sqlCommand.Parameters["@Amount"].Value = numOrderAmount.Value;

                        // Adicione o parâmetro de entrada de status do pedido @Status.
                        // Para um novo pedido, o status é sempre O (aberto).
                        sqlCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1));
                        sqlCommand.Parameters["@Status"].Value = "O";

                        // Adicione o valor de retorno do procedimento armazenado, que é o ID do pedido.
                        sqlCommand.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
                        sqlCommand.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

                        try
                        {
                            //Abre conexão.
                            connection.Open();

                            //Executa o procedimento armazenado.
                            sqlCommand.ExecuteNonQuery();

                            //Exibe o número do pedido.
                            this.orderID = (int)sqlCommand.Parameters["@RC"].Value;
                            MessageBox.Show("Número do pedido" + this.orderID + " foi submetido.");
                        }
                        catch
                        {
                            MessageBox.Show("Não foi possível fazer o pedido.");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        // Limpa os dados do formulário para que outra nova conta possa ser criada.
        private void btnAddAnotherAccount_Click(object sender, EventArgs e)
        {
            this.ClearForm();
        }
        
        // Fecha o formulário/caixa de diálogo.
        private void btnAddFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
