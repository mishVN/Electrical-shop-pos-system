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
    public partial class customer : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public customer()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadItemData();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();

                        string fetchQuery = @"SELECT * FROM customer WHERE customer_id=@customer_id";

                        using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                        {
                            if (double.TryParse(txtid.Text.Trim(), out double id))
                            {

                                cmd.Parameters.AddWithValue("@customer_id", id);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        txtname.Text = reader["customer_name"] == DBNull.Value ? string.Empty : reader["customer_name"].ToString();
                                        txtnic.Text = reader["customer_nic"] == DBNull.Value ? string.Empty : reader["customer_nic"].ToString();
                                        txtaddress.Text = reader["customer_address"] == DBNull.Value ? string.Empty : reader["customer_address"].ToString();
                                        txtcontact.Text = reader["customer_contact"] == DBNull.Value ? string.Empty : reader["customer_contact"].ToString();
                                        txtmore.Text = reader["more"] == DBNull.Value ? string.Empty : reader["more"].ToString();
                                        lblbalance.Text = reader["credit_balance"] == DBNull.Value ? string.Empty : reader["credit_balance"].ToString();

                                        txtname.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No Customer found with the specified id.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Invalid customer id. Please enter a valid number.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void butnew_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    
                    string query = "SELECT TOP 1 customer_id FROM [customer] ORDER BY customer_id DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int code = Convert.ToInt32(result);
                            code = code + 1;
                            txtid.Text = code.ToString();

                            txtname.Clear();
                            txtcontact.Clear();
                            txtmore.Clear();
                            txtnic.Clear();
                            txtaddress.Clear();
                            lblbalance.Text = "-";
                            txtcashback.Clear();

                        }
                        else
                        {

                            txtid.Text = "1001";
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

        private void butadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text) ||
                string.IsNullOrWhiteSpace(txtname.Text) ||
                string.IsNullOrWhiteSpace(txtcontact.Text)||
                string.IsNullOrWhiteSpace(txtnic.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string insert = @"INSERT INTO customer(customer_id,customer_name,customer_address,customer_contact,customer_nic,more,credit_balance) VALUES(@customer_id,@customer_name,@customer_address,@customer_contact,@customer_nic,@more,@credit_balance)";
                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            if (double.TryParse(txtid.Text.Trim(), out double id))
                            {
                                cmd.Parameters.AddWithValue("@customer_id", id);
                            }

                            if (string.IsNullOrWhiteSpace(txtaddress.Text))
                            {
                                cmd.Parameters.AddWithValue("@customer_address", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@customer_address", txtaddress.Text.Trim());
                            }


                            cmd.Parameters.AddWithValue("@customer_name", txtname.Text.Trim());
                            cmd.Parameters.AddWithValue("@customer_contact", txtcontact.Text.Trim());
                            cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());
                            if (string.IsNullOrWhiteSpace(txtmore.Text))
                            {
                                cmd.Parameters.AddWithValue("@more", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@more", txtmore.Text.Trim());
                            }
                            string credit = "0";
                            cmd.Parameters.AddWithValue("@credit_balance", credit);

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
                catch (Exception ex)
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
                    string update = @"UPDATE customer 
                                     SET customer_name = @Customer_name,
                                         customer_address = @customer_address,
                                         customer_contact = @customer_contact,
                                         customer_nic = @customer_nic,
                                         more = @more 
                                     WHERE customer_id = @customer_id";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {
                            cmd.Parameters.AddWithValue("@customer_id", id);
                        }

                        if (string.IsNullOrWhiteSpace(txtaddress.Text))
                        {
                            cmd.Parameters.AddWithValue("@customer_address", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@customer_address", txtaddress.Text.Trim());
                        }


                        cmd.Parameters.AddWithValue("@customer_name", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@customer_contact", txtcontact.Text.Trim());
                        cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());

                        if (string.IsNullOrWhiteSpace(txtmore.Text))
                        {
                            cmd.Parameters.AddWithValue("@more", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@more", txtmore.Text.Trim());
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadItemData();
                        }
                        else
                        {
                            MessageBox.Show("No Customer was updated. Please check the item code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtnic.Focus();
            }
        }

        private void txtnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtcontact.Focus();
            }
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtmore.Focus();
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
                    string query = "SELECT customer_id,customer_name FROM customer";

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
                    if (selectedRow.Cells["customer_id"].Value != null)
                    {
                        txtid.Text = selectedRow.Cells["customer_id"].Value.ToString();
                        txtid.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtserachbar_TextChanged(object sender, EventArgs e)
        {
            if (txtserachbar.Text == "")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();

                        // SQL query to fetch all item codes and names
                        string query = @"SELECT customer_id, customer_name FROM customer";;

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Execute the query and fill the results into a DataTable
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
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string query = @"SELECT customer_id , customer_name
                             FROM customer
                             WHERE customer_name LIKE @customer_name";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@customer_name", $"{txtserachbar.Text.Trim()}%");

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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row index is valid
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Get the item_code from the selected row and fill txtcode
                    if (selectedRow.Cells["customer_id"].Value != null)
                    {
                        txtid.Text = selectedRow.Cells["customer_id"].Value.ToString();
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

                    string fetchQuery = @"SELECT * FROM customer WHERE customer_id=@customer_id";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {

                            cmd.Parameters.AddWithValue("@customer_id", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtname.Text = reader["customer_name"] == DBNull.Value ? string.Empty : reader["customer_name"].ToString();
                                    txtnic.Text = reader["customer_nic"] == DBNull.Value ? string.Empty : reader["customer_nic"].ToString();
                                    txtaddress.Text = reader["customer_address"] == DBNull.Value ? string.Empty : reader["customer_address"].ToString();
                                    txtcontact.Text = reader["customer_contact"] == DBNull.Value ? string.Empty : reader["customer_contact"].ToString();
                                    txtmore.Text = reader["more"] == DBNull.Value ? string.Empty : reader["more"].ToString();
                                    lblbalance.Text = reader["credit_balance"] == DBNull.Value ? string.Empty : reader["credit_balance"].ToString();

                                    txtname.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("No Customer found with the specified id.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid customer id. Please enter a valid number.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butpaid_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    // 1️⃣ Fetch existing credit_balance
                    string fetchQuery = "SELECT credit_balance FROM customer WHERE customer_id = @CustomerId";
                    decimal existingBalance = 0;

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", txtid.Text.Trim());
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            existingBalance = Convert.ToDecimal(result);
                        }
                        else
                        {
                            MessageBox.Show("Customer ID not found or credit balance is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 2️⃣ Calculate new balance
                    decimal newBalance = existingBalance - Convert.ToDecimal(txtcashback.Text); // Modify logic if needed

                    // 3️⃣ Update the new balance in the database
                    string updateQuery = "UPDATE customer SET credit_balance = @NewBalance WHERE customer_id = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewBalance", newBalance);
                        cmd.Parameters.AddWithValue("@CustomerId", txtid.Text.Trim());
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Credit balance updated successfully! New Balance: "+ newBalance, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            lblbalance.Text = newBalance.ToString();
                           
                        }
                        else
                        {
                            MessageBox.Show("Failed to update credit balance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row index is valid
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Get the item_code from the selected row and fill txtcode
                    if (selectedRow.Cells["customer_id"].Value != null)
                    {
                        txtid.Text = selectedRow.Cells["customer_id"].Value.ToString();
                        button1.PerformClick();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
