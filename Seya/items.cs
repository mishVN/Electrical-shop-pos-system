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
    public partial class items : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public items()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadItemData();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcode.Text) ||
                string.IsNullOrWhiteSpace(txtdescription.Text) ||
                    string.IsNullOrWhiteSpace(txtname.Text) ||
                    string.IsNullOrWhiteSpace(txtsupplier.Text) ||
                    string.IsNullOrWhiteSpace(txtsupplierid.Text) ||
                    string.IsNullOrWhiteSpace(txtcost.Text) ||
                    string.IsNullOrWhiteSpace(txtsallingprice.Text) ||
                    string.IsNullOrWhiteSpace(txtwarranty.Text))
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
                        string insert = @"INSERT INTO [item] 
                          (item_code, barcode, description, item_name, supplier_id, cost, salling_price, minimum_price, min_quantity, warranty, stock)
                          VALUES 
                          (@item_code, @barcode, @description, @name, @supplier_id, @cost, @salling_price, @minimum_price, @min_quantity, @warranty, @stock)";
                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            if (double.TryParse(txtcode.Text.Trim(), out double itemCode))
                            {
                                cmd.Parameters.AddWithValue("@item_code", itemCode);
                            }
                            else
                            {
                                MessageBox.Show("Invalid item code. Please enter a valid number.");
                                return;
                            }

                            // Null for varchar
                            if (string.IsNullOrWhiteSpace(txtbarcode.Text))
                            {
                                cmd.Parameters.AddWithValue("@barcode", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@barcode", txtbarcode.Text.Trim());
                            }

                            cmd.Parameters.AddWithValue("@description", txtdescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());

                            if (double.TryParse(txtsupplierid.Text.Trim(), out double supid))
                            {
                                cmd.Parameters.AddWithValue("@supplier_id", supid);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Supplier ID. Please enter a valid number.");
                                return;
                            }

                            cmd.Parameters.AddWithValue("@cost", txtcost.Text.Trim());
                            cmd.Parameters.AddWithValue("@salling_price", txtsallingprice.Text.Trim());

                            if (string.IsNullOrWhiteSpace(txtminimumprice.Text))
                            {
                                cmd.Parameters.AddWithValue("@minimum_price", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@minimum_price", txtminimumprice.Text.Trim());
                            }

                            if (string.IsNullOrWhiteSpace(txtminquantity.Text))
                            {
                                cmd.Parameters.AddWithValue("@min_quantity", DBNull.Value);
                            }
                            else if (double.TryParse(txtminquantity.Text.Trim(), out double minqnty))
                            {
                                cmd.Parameters.AddWithValue("@min_quantity", minqnty);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Quantity. Please enter a valid number.");
                                return;
                            }

                            if (double.TryParse(txtwarranty.Text.Trim(), out double warranty))
                            {
                                cmd.Parameters.AddWithValue("@warranty", warranty);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Warranty. Please enter a valid number.");
                                return;
                            }

                            // Set stock value
                            int stock = 0; // You can modify this based on your logic
                            cmd.Parameters.AddWithValue("@stock", stock);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                LoadItemData();
                                MessageBox.Show("Item successfully added to the database.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void butnew_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    // Use TOP 1 to get the last item_code in descending order
                    string query = "SELECT TOP 1 item_code FROM [item] ORDER BY item_code DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int code = Convert.ToInt32(result);
                            code = code + 1;
                            txtcode.Text = code.ToString();

                            txtbarcode.Clear();
                            txtname.Clear();
                            txtdescription.Clear();
                            txtsupplier.Clear();
                            txtsupplierid.Clear();
                            txtcost.Clear();
                            txtwarranty.Clear();
                            txtminimumprice.Clear();
                            txtsallingprice.Clear();
                            txtminquantity.Clear();
                            txtstockadd.Clear();
                            lblavailable.Text = "0";
                        }
                        else
                        {

                            txtcode.Text = "100001";
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

        private void txtcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        // Assuming you fetch data by item_code
                        string fetchQuery = @"SELECT item_code, barcode, description, item_name, supplier_id, cost, salling_price, minimum_price, min_quantity, warranty, stock 
                              FROM item 
                              WHERE item_code = @item_code";

                        using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                        {
                            if (double.TryParse(txtcode.Text.Trim(), out double itemCode))
                            {
                                // Set the parameter for item_code
                                cmd.Parameters.AddWithValue("@item_code", itemCode);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read()) // If a record is found
                                    {
                                        // Populate text boxes with data
                                        txtbarcode.Text = reader["barcode"] == DBNull.Value ? string.Empty : reader["barcode"].ToString();
                                        txtdescription.Text = reader["description"] == DBNull.Value ? string.Empty : reader["description"].ToString();
                                        txtname.Text = reader["item_name"] == DBNull.Value ? string.Empty : reader["item_name"].ToString();
                                        txtsupplierid.Text = reader["supplier_id"] == DBNull.Value ? string.Empty : reader["supplier_id"].ToString();
                                        txtcost.Text = reader["cost"] == DBNull.Value ? string.Empty : reader["cost"].ToString();
                                        txtsallingprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();
                                        txtminimumprice.Text = reader["minimum_price"] == DBNull.Value ? string.Empty : reader["minimum_price"].ToString();
                                        txtminquantity.Text = reader["min_quantity"] == DBNull.Value ? string.Empty : reader["min_quantity"].ToString();
                                        txtwarranty.Text = reader["warranty"] == DBNull.Value ? string.Empty : reader["warranty"].ToString();
                                        lblavailable.Text = reader["stock"] == DBNull.Value ? string.Empty : reader["stock"].ToString();

                                        txtstockadd.Clear();
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

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtdescription.Focus();
            }
        }

        private void txtdescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtname.Focus();
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtsupplier.Focus();
            }
        }

        private void txtsupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string query = @"SELECT supplier_id ,supplier_name
                             FROM supplier 
                             WHERE supplier_name LIKE @supplier_name";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@supplier_name", $"{txtsupplier.Text.Trim()}%");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // If a match is found
                                {
                                    // Fill the supplier ID in the text box

                                    txtsupplierid.Text = reader["supplier_id"].ToString();
                                    txtsupplier.Text = reader["supplier_name"].ToString() ;
                                    txtcost.Focus();
                                }
                                else
                                {
                                    // Clear the supplier ID if no match is found
                                    txtsupplierid.Clear();
                                }
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

        private void txtcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtwarranty.Focus();
            }
        }

        private void txtwarranty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtsallingprice.Focus();
            }
        }

        private void txtsallingprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtminimumprice.Focus();
            }
        }

        private void txtminimumprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtminquantity.Focus();
            }
        }

        private void txtsallingprice_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtcost.Text.Trim(), out double cost))
            {
                return;
            }

            if (double.TryParse(txtsallingprice.Text.Trim(), out double sellingprice))
            {
                return;
            }
            double profit = (cost / sellingprice) * 100;
            lblprofit.Text= profit.ToString();
        }

        private void butupdatestock_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    // Step 1: Get the current stock for the item
                    string fetchStockQuery = @"SELECT stock FROM item WHERE item_code = @item_code";
                    using (SqlCommand fetchCmd = new SqlCommand(fetchStockQuery, conn))
                    {
                        if (double.TryParse(txtcode.Text.Trim(), out double itemCode))
                        {
                            fetchCmd.Parameters.AddWithValue("@item_code", itemCode);

                            object currentStockObj = fetchCmd.ExecuteScalar();
                            if (currentStockObj != null)
                            {
                                int currentStock = Convert.ToInt32(currentStockObj);

                                // Step 2: Add the new stock value
                                if (int.TryParse(txtstockadd.Text.Trim(), out int additionalStock))
                                {
                                    int updatedStock = currentStock + additionalStock;

                                    // Step 3: Update the stock value in the database
                                    string updateStockQuery = @"UPDATE item SET stock = @updated_stock WHERE item_code = @item_code";
                                    using (SqlCommand updateCmd = new SqlCommand(updateStockQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@updated_stock", updatedStock);
                                        updateCmd.Parameters.AddWithValue("@item_code", itemCode);

                                        int rowsAffected = updateCmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Stock updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            lblavailable.Text=updatedStock.ToString();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to update stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid stock value. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Item not found. Please enter a valid item code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid item code. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butupdateitem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    // Update query
                    string updateQuery = @"UPDATE item 
                                   SET barcode = @barcode, 
                                       item_name = @name, 
                                       description = @description, 
                                       supplier_id = @supplier_id, 
                                       cost = @cost, 
                                       salling_price = @salling_price, 
                                       minimum_price = @minimum_price, 
                                       min_quantity = @min_quantity, 
                                       warranty = @warranty
                                   WHERE item_code = @item_code";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        // Validate item code
                        if (double.TryParse(txtcode.Text.Trim(), out double itemCode))
                        {
                            cmd.Parameters.AddWithValue("@item_code", itemCode);
                        }
                        else
                        {
                            MessageBox.Show("Invalid item code. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Barcode
                        cmd.Parameters.AddWithValue("@barcode", txtbarcode.Text.Trim());

                        // Name
                        cmd.Parameters.AddWithValue("@name",  txtname.Text.Trim());

                        // Description
                        cmd.Parameters.AddWithValue("@description", txtdescription.Text.Trim());

                        // Supplier ID
                        if (int.TryParse(txtsupplierid.Text.Trim(), out int supplierId))
                        {
                            cmd.Parameters.AddWithValue("@supplier_id", supplierId);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Supplier ID. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Cost
                        cmd.Parameters.AddWithValue("@cost", txtcost.Text.Trim());

                        // Selling Price
                        cmd.Parameters.AddWithValue("@salling_price", txtsallingprice.Text.Trim());

                        // Minimum Price
                        cmd.Parameters.AddWithValue("@minimum_price", txtminimumprice.Text.Trim());

                        // Minimum Quantity
                        if (string.IsNullOrWhiteSpace(txtminquantity.Text))
                        {
                            cmd.Parameters.AddWithValue("@min_quantity", DBNull.Value);
                        }
                        else if (int.TryParse(txtminquantity.Text.Trim(), out int minQuantity))
                        {
                            cmd.Parameters.AddWithValue("@min_quantity", minQuantity);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Minimum Quantity. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Warranty
                        if (string.IsNullOrWhiteSpace(txtwarranty.Text))
                        {
                            cmd.Parameters.AddWithValue("@warranty", DBNull.Value);
                        }
                        else if (int.TryParse(txtwarranty.Text.Trim(), out int warranty))
                        {
                            cmd.Parameters.AddWithValue("@warranty", warranty);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Warranty. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LoadItemData();
                            MessageBox.Show("Item updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No item was updated. Please check the item code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchbar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtsearchbar_TextChanged(object sender, EventArgs e)
        {
            if(txtsearchbar.Text=="")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();

                        // SQL query to fetch all item codes and names
                        string query = @"SELECT item_code, item_name FROM item";

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
                        string query = @"SELECT item_code , description
                             FROM item 
                             WHERE description LIKE @description";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@description", $"{txtsearchbar.Text.Trim()}%");

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
                catch(Exception ex) 
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
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
                    if (selectedRow.Cells["item_code"].Value != null)
                    {
                        txtcode.Text = selectedRow.Cells["item_code"].Value.ToString();
                        button2.PerformClick();

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
                    string query = "SELECT item_code,description FROM item";

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    // Assuming you fetch data by item_code
                    string fetchQuery = @"SELECT item_code, barcode, description, item_name, supplier_id, cost, salling_price, minimum_price, min_quantity, warranty, stock 
                              FROM item 
                              WHERE item_code = @item_code";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        if (double.TryParse(txtcode.Text.Trim(), out double itemCode))
                        {
                            // Set the parameter for item_code
                            cmd.Parameters.AddWithValue("@item_code", itemCode);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // If a record is found
                                {
                                    // Populate text boxes with data
                                    txtbarcode.Text = reader["barcode"] == DBNull.Value ? string.Empty : reader["barcode"].ToString();
                                    txtdescription.Text = reader["description"] == DBNull.Value ? string.Empty : reader["description"].ToString();
                                    txtname.Text = reader["item_name"] == DBNull.Value ? string.Empty : reader["item_name"].ToString();
                                    txtsupplierid.Text = reader["supplier_id"] == DBNull.Value ? string.Empty : reader["supplier_id"].ToString();
                                    txtcost.Text = reader["cost"] == DBNull.Value ? string.Empty : reader["cost"].ToString();
                                    txtsallingprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();
                                    txtminimumprice.Text = reader["minimum_price"] == DBNull.Value ? string.Empty : reader["minimum_price"].ToString();
                                    txtminquantity.Text = reader["min_quantity"] == DBNull.Value ? string.Empty : reader["min_quantity"].ToString();
                                    txtwarranty.Text = reader["warranty"] == DBNull.Value ? string.Empty : reader["warranty"].ToString();
                                    lblavailable.Text = reader["stock"] == DBNull.Value ? string.Empty : reader["stock"].ToString();

                                    txtstockadd.Clear();
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

        private void txtstockadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                butupdatestock.PerformClick();
            }
        }
    }
}
