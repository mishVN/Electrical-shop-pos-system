using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seya
{
    public partial class useraccount : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public useraccount()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadItemData();
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            if(txtid.Text == ""  ||
                txtname.Text==""||
                txtpassword.Text==""||
                txtacounttype.Text == "")
            {
                MessageBox.Show("Please fill in all required fields.");
            }
            else
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string insert = @"INSERT INTO user_account(user_id,user_name,user_password,account_type) VALUES(@user_id,@user_name,@user_password,@account_type)";
                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            if (double.TryParse(txtid.Text.Trim(), out double id))
                            {
                                cmd.Parameters.AddWithValue("@user_id", id);
                            }
                            cmd.Parameters.AddWithValue("@user_name", txtname.Text.Trim());
                            cmd.Parameters.AddWithValue("@user_password", txtpassword.Text.Trim());
                            cmd.Parameters.AddWithValue("@account_type", txtacounttype.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully added to the database.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadItemData();
                            }
                            else
                            {
                                MessageBox.Show("No rows were inserted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void butupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string update = @"UPDATE user_account
                                     SET user_name = @user_name,
                                         user_password = @user_password,
                                         account_type = @account_type 
                                     WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_name", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@user_password", txtpassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@account_type", txtacounttype.Text.Trim());

                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {
                            cmd.Parameters.AddWithValue("@user_id", id);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadItemData();
                        }
                        else
                        {
                            MessageBox.Show("No User was updated. Please check the item code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butnew_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    // Use TOP 1 to get the last item_code in descending order
                    string query = "SELECT TOP 1 user_id FROM [user_account] ORDER BY user_id DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int code = Convert.ToInt32(result);
                            code = code + 1;
                            txtid.Text = code.ToString();

                            txtname.Clear();
                            txtpassword.Clear();
                            

                        }
                        else
                        {

                            txtid.Text = "1";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void LoadItemData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    // SQL query to fetch all data from the item table
                    string query = "SELECT user_id,user_name FROM user_account";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row index is valid
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Get the item_code from the selected row and fill txtcode
                    if (selectedRow.Cells["user_id"].Value != null)
                    {
                        txtid.Text = selectedRow.Cells["user_id"].Value.ToString();
                        txtid.Focus();
                        button1.PerformClick();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string fetchQuery = @"SELECT * FROM user_account WHERE user_id=@user_id";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {

                            cmd.Parameters.AddWithValue("@user_id", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtname.Text = reader["user_name"] == DBNull.Value ? string.Empty : reader["user_name"].ToString();
                                    txtpassword.Text = reader["user_password"] == DBNull.Value ? string.Empty : reader["user_password"].ToString();
                                    txtacounttype.Text = reader["account_type"] == DBNull.Value ? string.Empty : reader["account_type"].ToString();

                                }
                                else
                                {
                                    MessageBox.Show("No user found with the specified code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid user id. Please enter a valid number.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
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
                txtacounttype.Focus();
            }
        }
    }
}
