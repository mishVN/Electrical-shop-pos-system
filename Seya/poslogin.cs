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

namespace Seya
{
    public partial class poslogin : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public poslogin()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();


                if (txtusername.Text == " " || txtpassword.Text == "")
                {
                    MessageBox.Show("Fill Username Password", "Empty Username Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "SELECT * FROM [user_account] WHERE user_Name = @username AND user_password = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);


                    cmd.Parameters.AddWithValue("@username", txtusername.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);


                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        using (SqlConnection conn = new SqlConnection(connstring))
                        {
                            conn.Open();

                            string insert = @"INSERT INTO [temp_login_user] (user_name, password) VALUES (@user_name, @password)";

                            using (SqlCommand cmd1 = new SqlCommand(insert, conn))
                            {

                                cmd1.Parameters.AddWithValue("@user_name", txtusername.Text.Trim());
                                cmd1.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                                int rowsAffected = cmd1.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    POS pos = new POS();
                                    pos.Show();
                                    this.Hide();

                                }
                                else
                                {
                                    MessageBox.Show("No rows were inserted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {

                        MessageBox.Show("Wrong username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
