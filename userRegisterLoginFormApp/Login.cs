using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace userRegisterLoginFormApp
{
    public partial class Login : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-6NK0VQRQ\SQLEXPRESS;Initial Catalog=UserLogin;Integrated Security=True;Encrypt=False");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if(connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        String selectData = $"SELECT * FROM [dbo].[formUserRegister] WHERE userMail = @userMail AND userPassword = @userPassword";
                        using (SqlCommand cmd = new SqlCommand(selectData,connect))
                        {
                            cmd.Parameters.AddWithValue("@userMail", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@userPassword", textBox2.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if(table.Rows.Count>=1)
                            {

                                MessageBox.Show("Logged In successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                DashBoard dashForm = new DashBoard();
                                dashForm.Show();
                                this.Hide();

                            }
                            else
                            {

                                MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Eroor Connecting:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        if(connect.State == ConnectionState.Open)
                        {
                            connect.Close();
                        }
                    }
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
