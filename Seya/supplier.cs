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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Seya
{
    public partial class supplier : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public supplier()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadItemData();
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

                        string fetchQuery = @"SELECT * FROM supplier WHERE supplier_id=@supplier_id";

                        using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                        {
                            if (double.TryParse(txtid.Text.Trim(), out double id))
                            {

                                cmd.Parameters.AddWithValue("@supplier_id", id);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        txtname.Text = reader["supplier_name"] == DBNull.Value ? string.Empty : reader["supplier_name"].ToString();
                                        txtcontact.Text = reader["contact"] == DBNull.Value ? string.Empty : reader["contact"].ToString();
                                        txtmore.Text = reader["more"] == DBNull.Value ? string.Empty : reader["more"].ToString();

                                    }
                                    else
                                    {
                                        MessageBox.Show("No item found with the specified code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Invalid item code. Please enter a valid number.");
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

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void butadd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtid.Text)||
                string.IsNullOrWhiteSpace(txtname.Text)||
                string.IsNullOrWhiteSpace(txtcontact.Text))
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
                        string insert = @"INSERT INTO supplier(supplier_id,supplier_name,contact,more) VALUES(@supplier_id,@supplier_name,@contact,@more)";
                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            if (double.TryParse(txtid.Text.Trim(), out double id))
                            {
                                cmd.Parameters.AddWithValue("@supplier_id", id);
                            }
                            cmd.Parameters.AddWithValue("@supplier_name", txtname.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", txtcontact.Text.Trim());
                            cmd.Parameters.AddWithValue("@more", txtmore.Text.Trim());

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

        private void butnew_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    // Use TOP 1 to get the last item_code in descending order
                    string query = "SELECT TOP 1 supplier_id FROM [supplier] ORDER BY supplier_id DESC";

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
                            
                        }
                        else
                        {

                            txtid.Text = "101";
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

        private void butupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string update = @"UPDATE supplier 
                                     SET supplier_name = @supplier_name,
                                         contact = @contact,
                                         more = @more 
                                     WHERE supplier_id = @supplier_id";
                    using(SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@supplier_name", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact", txtcontact.Text.Trim());
                        cmd.Parameters.AddWithValue("@more", txtmore.Text.Trim());

                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {
                            cmd.Parameters.AddWithValue("@supplier_id", id);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadItemData();
                        }
                        else
                        {
                            MessageBox.Show("No supplier was updated. Please check the item code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                        string query = @"SELECT supplier_id, supplier_name FROM supplier";

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
                        string query = @"SELECT supplier_id , supplier_name
                             FROM supplier 
                             WHERE supplier_name LIKE @supplier_name";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@supplier_name", $"{txtserachbar.Text.Trim()}%");

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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure a valid row is clicked (avoid header row clicks)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Ensure the "supplier_id" column exists and has a value
                    if (selectedRow.Cells["supplier_id"] != null &&
                        selectedRow.Cells["supplier_id"].Value != null &&
                        !string.IsNullOrEmpty(selectedRow.Cells["supplier_id"].Value.ToString()))
                    {
                        txtid.Text = selectedRow.Cells["supplier_id"].Value.ToString();
                        button1.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("No valid supplier ID found in the selected row.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "SELECT supplier_id,supplier_name FROM supplier";

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtmore_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtcontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string fetchQuery = @"SELECT * FROM supplier WHERE supplier_id=@supplier_id";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        if (double.TryParse(txtid.Text.Trim(), out double id))
                        {

                            cmd.Parameters.AddWithValue("@supplier_id", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtname.Text = reader["supplier_name"] == DBNull.Value ? string.Empty : reader["supplier_name"].ToString();
                                    txtcontact.Text = reader["contact"] == DBNull.Value ? string.Empty : reader["contact"].ToString();
                                    txtmore.Text = reader["more"] == DBNull.Value ? string.Empty : reader["more"].ToString();

                                }
                                else
                                {
                                    MessageBox.Show("No item found with the specified code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid item code. Please enter a valid number.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure a valid row is clicked (avoid header row clicks)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Ensure the "supplier_id" column exists and has a value
                    if (selectedRow.Cells["supplier_id"] != null &&
                        selectedRow.Cells["supplier_id"].Value != null &&
                        !string.IsNullOrEmpty(selectedRow.Cells["supplier_id"].Value.ToString()))
                    {
                        txtid.Text = selectedRow.Cells["supplier_id"].Value.ToString();
                        button1.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("No valid supplier ID found in the selected row.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            try
            {
                // Ensure a valid row is clicked (avoid header row clicks)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Ensure the "supplier_id" column exists and has a value
                    if (selectedRow.Cells["supplier_id"] != null &&
                        selectedRow.Cells["supplier_id"].Value != null &&
                        !string.IsNullOrEmpty(selectedRow.Cells["supplier_id"].Value.ToString()))
                    {
                        txtid.Text = selectedRow.Cells["supplier_id"].Value.ToString();
                        button1.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("No valid supplier ID found in the selected row.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
