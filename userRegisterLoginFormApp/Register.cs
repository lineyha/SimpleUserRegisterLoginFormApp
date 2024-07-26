using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userRegisterLoginFormApp
{
    public partial class Register : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-6NK0VQRQ\SQLEXPRESS;Initial Catalog=UserLogin;Integrated Security=True;Encrypt=False");
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    String checkUserMail = $"SELECT * FROM [dbo].[formUserRegister] WHERE userMail = '{textBox2.Text.Trim()}'";
                    using (SqlCommand chekUser = new SqlCommand(checkUserMail, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(chekUser);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count >= 1)
                        {
                            MessageBox.Show(textBox2.Text.Trim() + "is already exist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string insertData = "INSERT INTO [dbo].[formUserRegister] (userName, userMail, userPassword)" +
                                "VALUES(@userName,@userMail,@userPassword)";

                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {

                                cmd.Parameters.AddWithValue("@userName", textBox1.Text.Trim());
                                cmd.Parameters.AddWithValue("@userMail", textBox2.Text.Trim());
                                cmd.Parameters.AddWithValue("@userPassword", textBox3.Text.Trim());

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Registered successfully", "Invormation Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Login login = new Login();
                                login.Show();
                                this.Hide();

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting Database " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
         }

            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
