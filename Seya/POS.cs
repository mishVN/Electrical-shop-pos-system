using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Seya
{
    public partial class POS : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public POS()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadItemData();
            combpayment.Text = "Cash";
            fetchdata();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            singoffpos off = new singoffpos();
            off.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelitemsdetails.Enabled = true;
            panelcashdetails.Enabled = true;    
            panelcustomer.Enabled = true;
            dataGridViewsearchresult.Enabled = true;    

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    // Use TOP 1 to get the last item_code in descending order
                    string query = "SELECT TOP 1 bill_number FROM [bill_details] ORDER BY bill_number DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int code = Convert.ToInt32(result);
                            code = code + 1;
                            lblbillno.Text = code.ToString();

                            txtcode.Clear();
                            txtname.Clear();
                            txtprice.Clear();
                            txtcost.Clear();
                            txtquantity.Clear();
                            txtwarranty.Clear();    
                            txtnic.Clear();
                            txtcusbalance.Clear();
                            txtcusname.Clear();
                            txttotal.Clear();

                            txtcode.Focus();

                        }
                        else
                        {

                            lblbillno.Text = "1000001";
                            txtcode.Focus();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            singoffpos off = new singoffpos();
            off.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            logdashboard log = new logdashboard();
            log.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            logdashboard log = new logdashboard();
            log.Show();
            this.Hide();
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

                        string fetchQuery = @"SELECT * FROM item WHERE item_code=@item_code";

                        using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                        {
                            if (double.TryParse(txtcode.Text.Trim(), out double id))
                            {

                                cmd.Parameters.AddWithValue("@item_code", id);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        txtname.Text = reader["description"] == DBNull.Value ? string.Empty : reader["description"].ToString();
                                        txtprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();
                                        txtcost.Text = reader["cost"] == DBNull.Value ? string.Empty : reader["cost"].ToString();
                                        txtwarranty.Text = reader["warranty"] == DBNull.Value ? string.Empty : reader["warranty"].ToString();

                                        lblsalingprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();


                                        lblname.Text = reader["item_name"] == DBNull.Value ? string.Empty : reader["item_name"].ToString();
                                        lblminimumprice.Text = reader["minimum_price"] == DBNull.Value ? "0" : reader["minimum_price"].ToString();
                                        lblminquantity.Text = reader["min_quantity"] == DBNull.Value ? "0" : reader["min_quantity"].ToString();

                                        lblstock.Text = reader["stock"] == DBNull.Value ? string.Empty : reader["stock"].ToString();

                                        txtquantity.Focus();

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

            if (e.KeyChar == (char)Keys.Escape)
            {
                txtnic.Focus();
            }
        }
        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double billingquantity = 0;
                if (!double.TryParse(txtquantity.Text.Trim(), out billingquantity))
                {
                       return;
                }

                
                
                if (!double.TryParse(lblminquantity.Text.Trim(), out double minquantity))
                {
                       return;
                }

                if (!double.TryParse(lblsalingprice.Text.Trim(), out double saling))
                {
                    return;
                }

                if (!double.TryParse(lblminimumprice.Text.Trim(), out double minimumprice))
                {
                    MessageBox.Show("Invalid minimum price. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (billingquantity >= minquantity && minquantity>0)
                {
                    txtprice.Text = minimumprice.ToString("F2"); 
                    double total = billingquantity * minimumprice;
                    txttotal.Text = total.ToString("F2");

                    double discount = (saling - minimumprice)*billingquantity;
                    lbldiscount.Text = discount.ToString();
                    

                }
                else
                {
                    if (!double.TryParse(txtprice.Text.Trim(), out double sallingprice))
                    {
                        MessageBox.Show("Invalid selling price. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    double total = billingquantity * sallingprice;
                    txttotal.Text = total.ToString("F2");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(lblname.Text) ||
                    string.IsNullOrWhiteSpace(txtprice.Text) ||
                    string.IsNullOrWhiteSpace(txtquantity.Text) ||
                    string.IsNullOrWhiteSpace(lbldiscount.Text) ||
                    string.IsNullOrWhiteSpace(txttotal.Text) ||
                    string.IsNullOrWhiteSpace(txtwarranty.Text))
                {
                    MessageBox.Show("Please fill in all required fields before adding.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (!double.TryParse(txtprice.Text.Trim(), out double price) ||
                    !double.TryParse(txtquantity.Text.Trim(), out double quantity) ||
                    !double.TryParse(lbldiscount.Text.Trim(), out double discount) ||
                    !double.TryParse(txttotal.Text.Trim(), out double total))
                {
                    MessageBox.Show("Please enter valid numeric values for price, quantity, discount, and total.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                dataGridViewsold.Rows.Add(txtcode.Text.Trim(), lblname.Text.Trim(), price.ToString("F2"), quantity.ToString("F2"),
                                  discount.ToString("F2"), total.ToString("F2"), txtwarranty.Text.Trim());


                //totalamount
                double totalSum = 0;

                foreach (DataGridViewRow row in dataGridViewsold.Rows)
                {
                    if (row.Cells["dgvtotal"].Value != null &&
                        double.TryParse(row.Cells["dgvtotal"].Value.ToString(), out double value))
                    {
                        totalSum += value; 
                    }
                }

                lbltotalamount.Text = totalSum.ToString("F2");


                //totaldiscount
                double discountSum = 0;

                foreach (DataGridViewRow row in dataGridViewsold.Rows)
                {
                    if (row.Cells["dgvdiscount"].Value != null &&
                        double.TryParse(row.Cells["dgvdiscount"].Value.ToString(), out double value1))
                    {
                        discountSum += value1;
                    }
                }

                lbltotaldiscount.Text = discountSum.ToString("F2");


                int totalRows = 0;

                // Loop through the rows
                foreach (DataGridViewRow row in dataGridViewsold.Rows)
                {
                    // Check if the row is not a new row and contains data
                    if (!row.IsNewRow && row.Cells.Cast<DataGridViewCell>().Any(cell => cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString())))
                    {
                        totalRows++;
                    }
                }

                // Update the label with the count of non-empty rows
                lblitemcount.Text = totalRows.ToString();


                ClearInputFields();
                txtcode.Focus();


            }
        }
        private void ClearInputFields()
        {
            txtcode.Clear();
            txtname.Clear();
            txtcost.Clear();
            txtquantity.Clear();
            lblstock.Text = "-";
            lblminquantity.Text= "-";
            lblminimumprice.Text= "-";
            lblname.Text = string.Empty;
            txtprice.Text = string.Empty;
            txtquantity.Text = string.Empty;
            lbldiscount.Text = "0";
            txttotal.Text = string.Empty;
            txtwarranty.Text = string.Empty;
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (!double.TryParse(txtprice.Text.Trim(), out double price))
                    {
                        return;
                    }

                    if (!double.TryParse(txtquantity.Text.Trim(), out double quantity))
                    {
                        return;
                    }

                    if (!double.TryParse(txtcost.Text.Trim(), out double cost))
                    {
                        return;
                    }

                    if (!double.TryParse(lblsalingprice.Text.Trim(), out double saling))
                    {
                        return;
                    }

                    if (price >= cost)
                    {
                        double newprice = quantity * price;
                        txttotal.Text = newprice.ToString();

                        double discount = (saling - price)*quantity;
                        lbldiscount.Text = discount.ToString();

                        txtquantity.Focus();

                    }
                    else
                    {
                        txtprice.Text = saling.ToString();
                        txtprice.Focus();
                        
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    string query = "SELECT item_code , description FROM item";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            dataGridViewsearchresult.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchbar_TextChanged(object sender, EventArgs e)
        {
            if (txtsearchbar.Text == "")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();

                        // SQL query to fetch all item codes and names
                        string query = @"SELECT item_code , description FROM item";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Execute the query and fill the results into a DataTable
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Bind the DataTable to the DataGridView
                                dataGridViewsearchresult.DataSource = dataTable;
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
                        string query = @"SELECT item_code,description
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
                                dataGridViewsearchresult.DataSource = dataTable;
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

        private void dataGridViewsearchresult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.RowIndex >= 0)
                {
                    
                    DataGridViewRow selectedRow = dataGridViewsearchresult.Rows[e.RowIndex];

                    
                    if (selectedRow.Cells["item_code"].Value != null)
                    {
                        txtcode.Text = selectedRow.Cells["item_code"].Value.ToString();
                       butadd.PerformClick();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string query = @"SELECT customer_name , credit_balance
                             FROM customer 
                             WHERE customer_nic = @customer_nic";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // If a match is found
                                {
                                    // Fill the supplier ID in the text box

                                    txtcusname.Text = reader["customer_name"].ToString();
                                    txtcusbalance.Text = reader["credit_balance"].ToString();
                                    txtcash.Focus();
                                }
                                else
                                {
                                    // Clear the supplier ID if no match is found
                                    txtnic.Clear();
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

        private void txtcusname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        string query = @"SELECT customer_nic, credit_balance,customer_name
                             FROM customer
                             WHERE customer_name LIKE @customer_name";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Use a partial match with wildcard (%)
                            cmd.Parameters.AddWithValue("@customer_name", $"{txtcusname.Text.Trim()}%");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // If a match is found
                                {
                                    // Fill the supplier ID in the text box

                                    txtnic.Text = reader["customer_nic"].ToString();
                                    txtcusname.Text = reader["customer_name"].ToString() ;
                                    txtcusbalance.Text = reader["credit_balance"].ToString();
                                    txtcash.Focus();
                                }
                                else
                                {
                                    // Clear the supplier ID if no match is found
                                    txtcusname.Clear();
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

        private void label10_Click(object sender, EventArgs e)
        {
            customer cus = new customer();
            cus.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            customer cus = new customer();
            cus.ShowDialog();
        }

        private void txtcash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                combpayment.Focus();
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                butsave.Focus();
                
            }
        }

        private void combpayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (combpayment.Text)
            {
                case "Cash":
                    txtcash.Focus();
                    txtcardno.Enabled = false;
                    lblcard.Enabled = false;

                    break;

                case "Credit":

                    if (txtnic.Text == "")
                    {
                        MessageBox.Show("Fill NIC", "Information Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblcard.Enabled = true;
                        txtcardno.Enabled = true;
                        string nic = txtnic.Text;
                        txtcardno.Text = nic;
                        txtcardno.Focus();
                    }
                    
                    break;

                case "Cheque":
                    lblcard.Enabled= true;
                    txtcardno.Enabled= true;
                    lblpassingdate.Enabled= true;
                    dtppassingdate.Enabled= true;
                    txtcardno.Focus();
                    break;

                case "Card":
                    lblcard.Enabled = true;
                    txtcardno.Enabled = true;
                    txtcardno.Focus();
                    break;

                default:
                    MessageBox.Show("Invalid payment method selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void txtcardno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DialogResult result = MessageBox.Show("Conform Payment",
                                      "Confirmation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    switch (combpayment.Text)
                    {
                        case "Cheque":
                            if (string.IsNullOrWhiteSpace(txtnic.Text) ||
                               string.IsNullOrWhiteSpace(lblbillno.Text) ||
                               string.IsNullOrWhiteSpace(lbltotalamount.Text) ||
                                string.IsNullOrWhiteSpace(txtcardno.Text))
                            {
                                MessageBox.Show("Please fill in all required fields.");
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connstring))
                                {
                                    conn.Open();
                                    string insert = @"INSERT INTO bill_details(bill_number,amount,customer_nic,date,time,payment_method,reference) VALUES(@bill_number,@amount,@customer_nic,@date,@time,@payment_method,@reference)";
                                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                                    {
                                        if (double.TryParse(lblbillno.Text.Trim(), out double billno))
                                        {
                                            cmd.Parameters.AddWithValue("@bill_number", billno);
                                        }

                                        cmd.Parameters.AddWithValue("@amount", lbltotalamount.Text.Trim());
                                        cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());

                                        DateTime currentDateTime = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@date", currentDateTime.Date);
                                        cmd.Parameters.AddWithValue("@time", currentDateTime.TimeOfDay);

                                        cmd.Parameters.AddWithValue("@payment_method", combpayment.Text.Trim());
                                        cmd.Parameters.AddWithValue("@reference", txtcardno.Text.Trim());

                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {

                                            insertcheque();
                                            

                                        }
                                        else
                                        {
                                            MessageBox.Show("Payment Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }

                                }
                            }
                            break;

                        //card process
                        case "Card":

                            if (string.IsNullOrWhiteSpace(lblbillno.Text) ||
                               string.IsNullOrWhiteSpace(lbltotalamount.Text) ||
                                string.IsNullOrWhiteSpace(txtcardno.Text))
                            {
                                MessageBox.Show("Please fill in all required fields.");
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connstring))
                                {
                                    conn.Open();
                                    string insert = @"INSERT INTO bill_details(bill_number,amount,customer_nic,date,time,payment_method,reference) VALUES(@bill_number,@amount,@customer_nic,@date,@time,@payment_method,@reference)";
                                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                                    {
                                        if (double.TryParse(lblbillno.Text.Trim(), out double billno))
                                        {
                                            cmd.Parameters.AddWithValue("@bill_number", billno);
                                        }

                                        cmd.Parameters.AddWithValue("@amount", lbltotalamount.Text.Trim());

                                        if (string.IsNullOrWhiteSpace(txtnic.Text))
                                        {
                                            cmd.Parameters.AddWithValue("@customer_nic", DBNull.Value);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());
                                        }


                                        DateTime currentDateTime = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@date", currentDateTime.Date);
                                        cmd.Parameters.AddWithValue("@time", currentDateTime.TimeOfDay);

                                        cmd.Parameters.AddWithValue("@payment_method", combpayment.Text.Trim());
                                        cmd.Parameters.AddWithValue("@reference", txtcardno.Text.Trim());

                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Payment Successfully.", "Payment Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            finishedbill();
                                            butprint.PerformClick();

                                        }
                                        else
                                        {
                                            MessageBox.Show("Payment Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }

                                }
                            }
                            break;

                        case "Credit":
                            if (string.IsNullOrWhiteSpace(lblbillno.Text) ||
                               string.IsNullOrWhiteSpace(lbltotalamount.Text) ||
                               string.IsNullOrWhiteSpace(txtcardno.Text) ||
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
                                        string insert = @"INSERT INTO bill_details(bill_number,amount,customer_nic,date,time,payment_method,reference) 
                                                          VALUES(@bill_number,@amount,@customer_nic,@date,@time,@payment_method,@reference)";
                                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                                        {
                                            if (double.TryParse(lblbillno.Text.Trim(), out double billno))
                                            {
                                                cmd.Parameters.AddWithValue("@bill_number", billno);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid Bill Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return;
                                            }

                                            cmd.Parameters.AddWithValue("@amount", lbltotalamount.Text.Trim());
                                            cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());

                                            DateTime currentDateTime = DateTime.Now;
                                            cmd.Parameters.AddWithValue("@date", currentDateTime.Date);
                                            cmd.Parameters.AddWithValue("@time", currentDateTime.TimeOfDay);
                                            cmd.Parameters.AddWithValue("@payment_method", combpayment.Text.Trim());
                                            cmd.Parameters.AddWithValue("@reference", txtcardno.Text.Trim());

                                            int rowsAffected = cmd.ExecuteNonQuery();
                                            if (rowsAffected > 0)
                                            {
                                                FetchAndUpdateCustomerCredit(txtnic.Text.Trim(), lbltotalamount.Text.Trim());
                                            }
                                            else
                                            {
                                                MessageBox.Show("Payment Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;


                    }
                }
                else
                {
                    txtcardno.Focus();
                }

            }
        }

        private void panelcashdetails_Paint(object sender, PaintEventArgs e)
        {

        }

        public void insertcheque()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string insert = @"INSERT INTO cheque(cheque_number,customer_nic,amount,receved_date,passing_date) VALUES(@cheque_number,@customer_nic,@amount,@receved_date,@passing_date)";
                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                    {
                        if (double.TryParse(txtcardno.Text.Trim(), out double no))
                        {
                            cmd.Parameters.AddWithValue("@cheque_number", no);
                        }
                        cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());
                        cmd.Parameters.AddWithValue("@amount", lbltotalamount.Text.Trim());

                        cmd.Parameters.AddWithValue("@receved_date", DateTime.Now.Date);

                        cmd.Parameters.AddWithValue("@passing_date", dtppassingdate.Value.Date);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment Successfully.","Payment Status",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            finishedbill();
                            butprint.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Cheque Update Error.\nPlease Enter Cheque Details Using Dashboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrWhiteSpace(txtcash.Text))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }
                else
                {
                    if (!double.TryParse(txtcash.Text.Trim(), out double cash))
                    {
                        MessageBox.Show("Invalid cash amount. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!double.TryParse(lbltotalamount.Text.Trim(), out double total))
                    {
                        MessageBox.Show("Invalid total amount. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cash >= total)
                    {
                        double balance = cash - total;

                        using (SqlConnection conn = new SqlConnection(connstring))
                        {
                            conn.Open();
                            string insert = @"INSERT INTO bill_details(bill_number,amount,customer_nic,date,time,payment_method,reference) VALUES(@bill_number,@amount,@customer_nic,@date,@time,@payment_method,@reference)";
                            using (SqlCommand cmd = new SqlCommand(insert, conn))
                            {
                                if (double.TryParse(lblbillno.Text.Trim(), out double billno))
                                {
                                    cmd.Parameters.AddWithValue("@bill_number", billno);
                                }

                                cmd.Parameters.AddWithValue("@amount", lbltotalamount.Text.Trim());

                                if (string.IsNullOrWhiteSpace(txtnic.Text))
                                {
                                    cmd.Parameters.AddWithValue("@customer_nic", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@customer_nic", txtnic.Text.Trim());
                                }

                                DateTime currentDateTime = DateTime.Now;
                                cmd.Parameters.AddWithValue("@date", currentDateTime.Date);
                                cmd.Parameters.AddWithValue("@time", currentDateTime.TimeOfDay);

                                cmd.Parameters.AddWithValue("@payment_method", combpayment.Text.Trim());
                                if (string.IsNullOrWhiteSpace(txtcardno.Text))
                                {
                                    cmd.Parameters.AddWithValue("@reference", DBNull.Value);
                                }

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    string message = "Payment Successfully.\nBalance Is: ";
                                    MessageBox.Show(message + balance, "Payment Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   
                                    finishedbill();

                                    
                                }
                                else
                                {
                                    MessageBox.Show("Payment Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }


                    }
                    else
                    {
                        txtcash.Text = total.ToString();
                        txtcash.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            butsave.PerformClick();
        }

        public void updatecustomercredit(string customerNic, double newCreditBalance)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string update = @"UPDATE customer 
                             SET credit_balance = @credit_balance
                             WHERE customer_nic = @customer_nic";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_nic", customerNic);
                        cmd.Parameters.AddWithValue("@credit_balance", newCreditBalance);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Payment Successfully.\nTotal Credit Balance Is: {newCreditBalance:F2}",
                                            "Payment Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            finishedbill();
                            butprint.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Payment Error while updating credit balance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating credit balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FetchAndUpdateCustomerCredit(string customerNic, string totalAmountText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string fetchQuery = @"SELECT credit_balance FROM customer WHERE customer_nic=@customer_nic";
                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_nic", customerNic);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double credit = reader["credit_balance"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["credit_balance"]);
                                if (double.TryParse(totalAmountText, out double total))
                                {
                                    double newCreditBalance = credit + total;
                                    lblnewcreditbalance.Text = newCreditBalance.ToString("F2");
                                    updatecustomercredit(customerNic, newCreditBalance);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Total Amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Customer NIC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching credit balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void butsave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                butsave.PerformClick();
            }
        }

        public void finishedbill()
        {
            totalsale();
            saveitems();
            UpdateStock();
            print();
            panelitemsdetails.Enabled = false;
            panelcashdetails.Enabled = false;
            panelcustomer.Enabled = false;
            dataGridViewsearchresult.Enabled = false;

            txtcode.Clear();
            txtname.Clear();
            txtprice.Clear();
            txtcost.Clear();
            txtwarranty.Clear();
            txtquantity.Clear();
            txttotal.Clear();
            lbldiscount.Text = "0";
            lblstock.Text = "-";
            lblminquantity.Text = "-";
            lblminimumprice.Text = "-";
            txtnic.Clear();
            txtcusname.Clear();
            txtcusbalance.Clear();

            lbltotalamount.Text = "-";
            lbltotaldiscount.Text = "-";
            lblitemcount.Text = "-";
            txtcash.Clear();
            combpayment.Text = "Cash";
            txtcardno.Clear();

            dataGridViewsold.Rows.Clear();
            fetchdata();
            






        }

        private void butprint_Click(object sender, EventArgs e)
        {
            print();
        }

        private void print()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 800,
                Height = 600
            };

            printPreviewDialog.ShowDialog();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {


            string shopName = "";
            string address = "";
            string phoneNumber = "";
            string logoUrl = "";
            string user = "";

            // Fetch data from shop_details
            using (SqlConnection conn1 = new SqlConnection(connstring))
            {
                conn1.Open(); // Open the first connection

                // SQL query to fetch shop details
                string query1 = "SELECT shopname, address, phonenumber, logourl FROM shop_details";

                using (SqlCommand cmd1 = new SqlCommand(query1, conn1))
                {
                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                    {
                        if (reader1.Read()) // Assuming the table has only one row
                        {
                            shopName = reader1["shopname"].ToString();
                            address = reader1["address"].ToString();
                            phoneNumber = reader1["phonenumber"].ToString();
                            logoUrl = reader1["logourl"].ToString();
                        }
                    }
                }
            }

            // Fetch data from temp_login_user
            using (SqlConnection conn2 = new SqlConnection(connstring))
            {
                conn2.Open(); // Open the second connection

                // SQL query to fetch user_name
                string query2 = "SELECT user_name FROM temp_login_user";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn2))
                {
                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read()) // Assuming the table has only one row
                        {
                            user = reader2["user_name"].ToString();
                        }
                    }
                }
            }




            Graphics graphics = e.Graphics;

            // Fonts and layout
            Font headerFont = new Font("Arial Rounded MT...", 14, FontStyle.Bold);
            Font subHeaderFont = new Font("Bell MT", 12, FontStyle.Regular);
            Font tableFont = new Font("Calibri", 10, FontStyle.Regular);
            Font footerFont = new Font("Arial Rounded MT...", 10, FontStyle.Italic);

            int startX = 50;
            int startY = 60;
            int offset = 20;

            
            int pageHeight = 1122; 

            
            if (System.IO.File.Exists(logoUrl))
            {
                Image logo = Image.FromFile(logoUrl);
                graphics.DrawImage(logo, new Rectangle(startX, startY, 120, 120));
            }

            // Header Section
            graphics.DrawString(shopName, new Font("Broadway", 20, FontStyle.Bold), Brushes.Black, 200, startY);
            graphics.DrawString(address, subHeaderFont, Brushes.Black, 200, startY + offset+10);
            graphics.DrawString(phoneNumber, subHeaderFont, Brushes.Black, 200, startY + (offset * 2)+10);

            // Bill Info Section
            string billno = lblbillno.Text;
            string customername = txtcusname.Text;

            graphics.DrawString("Bill Number: " + billno, subHeaderFont, Brushes.Black, 200, startY + offset * 4);
            graphics.DrawString($"Date: {DateTime.Now.ToShortDateString()}", subHeaderFont, Brushes.Black, 200, startY + offset * 5);
            graphics.DrawString("Customer Name: " + customername, subHeaderFont, Brushes.Black, 200, startY + offset * 6);

            string payment = combpayment.Text;

            graphics.DrawString("Original Copy", subHeaderFont, Brushes.Black, startX + 500, startY + offset * 4);
            graphics.DrawString("Payment Method: " + payment, subHeaderFont, Brushes.Black, startX + 500, startY + offset * 5);
            graphics.DrawString("Cashier: " + user, subHeaderFont, Brushes.Black, startX + 500, startY + offset * 6);

            // Table Header
            offset = startY + offset * 8;
            graphics.DrawLine(Pens.Black, startX, offset, startX + 700, offset); // Top border of the table
            graphics.DrawString("Item No", tableFont, Brushes.Black, startX, offset + 5);
            graphics.DrawString("Description", tableFont, Brushes.Black, startX + 100, offset + 5);
            graphics.DrawString("Quantity", tableFont, Brushes.Black, startX + 300, offset + 5);
            graphics.DrawString("Rate", tableFont, Brushes.Black, startX + 400, offset + 5);
            graphics.DrawString("Total", tableFont, Brushes.Black, startX + 500, offset + 5);
            graphics.DrawString("Warranty Time", tableFont, Brushes.Black, startX + 600, offset + 5);
            offset += 20;
            graphics.DrawLine(Pens.Black, startX, offset, startX + 700, offset); // Bottom border of the table header

            int i = 1;
            // Table Rows
            foreach (DataGridViewRow row in dataGridViewsold.Rows)
            {
                if (!row.IsNewRow)
                {
                    
                    graphics.DrawString(i.ToString(), tableFont, Brushes.Black, startX, offset + 5);
                    graphics.DrawString(row.Cells[1].Value?.ToString() ?? "", tableFont, Brushes.Black, startX + 100, offset + 5);
                    graphics.DrawString(row.Cells[3].Value?.ToString() ?? "", tableFont, Brushes.Black, startX + 300, offset + 5);
                    graphics.DrawString(row.Cells[2].Value?.ToString() ?? "", tableFont, Brushes.Black, startX + 400, offset + 5);
                    graphics.DrawString(row.Cells[5].Value?.ToString() ?? "", tableFont, Brushes.Black, startX + 500, offset + 5);
                    graphics.DrawString(row.Cells[6].Value?.ToString() ?? "", tableFont, Brushes.Black, startX + 600, offset + 5);
                    offset += 20; // Move to the next row
                    i++;
                }
            }

            // Total Section (Positioned above the footer)
            int totalSectionY = pageHeight - 150; // 150 pixels above the bottom of the page
            graphics.DrawLine(Pens.Black, startX, totalSectionY - 10, startX + 700, totalSectionY - 10); // Top border of total section

            string total = lbltotalamount.Text;
            string itemcount = lblitemcount.Text;
            double discount = 0.0;
            double.TryParse(lbltotaldiscount.Text, out discount);

            graphics.DrawString("Total Price: " + total, new Font("Bell MT", 16, FontStyle.Regular), Brushes.Black, startX, totalSectionY);
            graphics.DrawString("Number of Items: " + itemcount, new Font("Bell MT", 16, FontStyle.Regular), Brushes.Black, startX, totalSectionY + 25);

            if (discount > 0)
            {
                graphics.DrawString("Discount: " + discount, new Font("Bell MT", 16, FontStyle.Regular), Brushes.Black, startX + 400, totalSectionY + 20);
            }

            // Footer Section (At the very bottom of the page)
            int footerY = pageHeight - 50; // Footer position
            graphics.DrawLine(Pens.Black, startX, footerY - 10, startX + 700, footerY - 10); // Footer border
            graphics.DrawString("Software Developed by: Mishad Vihanga                                Contact 077-9861099 0785371087 ", footerFont, Brushes.Black, startX, footerY);
        



    }

        private void butadd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string fetchQuery = @"SELECT * FROM item WHERE item_code=@item_code";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        if (double.TryParse(txtcode.Text.Trim(), out double id))
                        {

                            cmd.Parameters.AddWithValue("@item_code", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtname.Text = reader["description"] == DBNull.Value ? string.Empty : reader["description"].ToString();
                                    txtprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();
                                    txtcost.Text = reader["cost"] == DBNull.Value ? string.Empty : reader["cost"].ToString();
                                    txtwarranty.Text = reader["warranty"] == DBNull.Value ? string.Empty : reader["warranty"].ToString();

                                    lblsalingprice.Text = reader["salling_price"] == DBNull.Value ? string.Empty : reader["salling_price"].ToString();


                                    lblname.Text = reader["item_name"] == DBNull.Value ? string.Empty : reader["item_name"].ToString();
                                    lblminimumprice.Text = reader["minimum_price"] == DBNull.Value ? "0" : reader["minimum_price"].ToString();
                                    lblminquantity.Text = reader["min_quantity"] == DBNull.Value ? "0" : reader["min_quantity"].ToString();

                                    lblstock.Text = reader["stock"] == DBNull.Value ? string.Empty : reader["stock"].ToString();

                                    txtquantity.Focus();

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

        public void saveitems()
        {
            try
            {
                // Ensure bill number is valid
                if (!int.TryParse(lblbillno.Text, out int billNumber))
                {
                    MessageBox.Show("Invalid Bill Number. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if bill number is invalid
                }

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd"); // Format date without time

                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    int rowsInserted = 0; // Counter for inserted rows

                    foreach (DataGridViewRow row in dataGridViewsold.Rows)
                    {
                        if (!row.IsNewRow) // Skip new empty row
                        {
                            try
                            {
                                // Validate data
                                if (row.Cells["dgvcode"].Value == null ||
                                    row.Cells["dgvprice"].Value == null ||
                                    row.Cells["dgvquantity"].Value == null ||
                                    string.IsNullOrWhiteSpace(row.Cells["dgvcode"].Value.ToString()) ||
                                    string.IsNullOrWhiteSpace(row.Cells["dgvprice"].Value.ToString()) ||
                                    string.IsNullOrWhiteSpace(row.Cells["dgvquantity"].Value.ToString()))
                                {
                                    MessageBox.Show($"Row {row.Index + 1}: One or more fields are empty. Please check your entries.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue; // Skip this row if any cell is empty
                                }

                                // Convert values safely
                                string itemCode = row.Cells["dgvcode"].Value.ToString().Trim();
                                string soldPriceStr = row.Cells["dgvprice"].Value.ToString().Trim();
                                string quantityStr = row.Cells["dgvquantity"].Value.ToString().Trim();

                                // Try parsing sold price
                                if (!decimal.TryParse(soldPriceStr, out decimal soldPrice))
                                {
                                    MessageBox.Show($"Row {row.Index + 1}: Invalid price format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }

                                // Try parsing quantity (handle decimal values too)
                                if (!double.TryParse(quantityStr, out double quantityDouble))
                                {
                                    MessageBox.Show($"Row {row.Index + 1}: Invalid quantity format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }

                                int quantity = (int)Math.Floor(quantityDouble); // Convert to int (rounding down)

                                // SQL Insert Query
                                string query = "INSERT INTO salling_item (item_code, bill_number, sold_price, date, quantity) " +
                                               "VALUES (@item_code, @bill_number, @sold_price, @date, @quantity)";

                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@item_code", itemCode);
                                    cmd.Parameters.AddWithValue("@bill_number", billNumber);
                                    cmd.Parameters.AddWithValue("@sold_price", soldPrice);
                                    cmd.Parameters.AddWithValue("@date", currentDate);
                                    cmd.Parameters.AddWithValue("@quantity", quantity);

                                    int result = cmd.ExecuteNonQuery(); // Execute query
                                    if (result > 0) rowsInserted++; // Count successful insertions
                                }
                            }
                            catch (Exception rowEx)
                            {
                                MessageBox.Show($"Error inserting row {row.Index + 1}: " + rowEx.Message, "Row Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // Show success message if at least one row is inserted
                    if (rowsInserted > 0)
                    {
                        MessageBox.Show($"Successfully inserted {rowsInserted} record(s).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No data inserted. Please check your entries.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void totalsale()
        {
            try
            {
                decimal currentTotal = 0;

                // Step 1: Fetch current total from database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string fetchQuery = "SELECT total FROM total_sale"; // Assuming only one row

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            currentTotal = Convert.ToDecimal(result); // Convert fetched value to decimal
                        }
                    }
                }

                // Step 2: Get the new value from lbltotal and add to current total
                if (!decimal.TryParse(lbltotalamount.Text, out decimal newAmount))
                {
                    MessageBox.Show("Invalid total value in label.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal updatedTotal = currentTotal + newAmount; // Add new amount to current total

                // Step 3: Update the total value in the database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string updateQuery = "UPDATE total_sale SET total = @updatedTotal";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@updatedTotal", updatedTotal);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Total updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check your database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fetchdata()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string fetchQuery = "SELECT total FROM total_sale";

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            lbltotalvalue.Text = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {
            cheque chequ = new cheque();
            chequ.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            cheque chequ = new cheque();
            chequ.ShowDialog();
        }

        public void UpdateStock()
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();

                    int rowsUpdated = 0; // Track how many rows are updated

                    foreach (DataGridViewRow row in dataGridViewsold.Rows)
                    {
                        if (row.Cells["dgvcode"].Value != null && row.Cells["dgvquantity"].Value != null)
                        {
                            // Convert item_code and quantity to appropriate types
                            string itemCodeStr = row.Cells["dgvcode"].Value.ToString().Trim();
                            string soldQuantityStr = row.Cells["dgvquantity"].Value.ToString().Trim();

                            if (!int.TryParse(itemCodeStr, out int itemCode) || !double.TryParse(soldQuantityStr, out double soldQuantityDouble))
                            {
                                MessageBox.Show($"Invalid item code or quantity in row {row.Index + 1}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue; // Skip this row
                            }

                            int soldQuantity = (int)Math.Floor(soldQuantityDouble); // Convert double to int by flooring

                            // Check if the item exists in the database
                            string checkQuery = "SELECT stock FROM item WHERE item_code = @itemCode";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@itemCode", itemCode);
                                object stockResult = checkCmd.ExecuteScalar();

                                if (stockResult == null)
                                {
                                    MessageBox.Show($"Item code {itemCode} not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue; // Skip this item
                                }

                                
                            }

                            // Update stock in the database
                            string updateQuery = "UPDATE item SET stock = stock - @quantity WHERE item_code = @itemCode";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@quantity", soldQuantity);
                                updateCmd.Parameters.AddWithValue("@itemCode", itemCode);

                                int affectedRows = updateCmd.ExecuteNonQuery();
                                if (affectedRows > 0)
                                {
                                    rowsUpdated++;
                                }
                                else
                                {
                                    MessageBox.Show($"Stock update failed for item: {itemCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    if (rowsUpdated > 0)
                    {
                        MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No stock was updated. Please check item codes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelitemsdetails.Enabled = false;
            panelcashdetails.Enabled = false;
            panelcustomer.Enabled = false;
            dataGridViewsearchresult.Enabled = false;

            txtcode.Clear();
            txtname.Clear();
            txtprice.Clear();
            txtcost.Clear();
            txtwarranty.Clear();
            txtquantity.Clear();
            txttotal.Clear();
            lbldiscount.Text = "0";
            lblstock.Text = "-";
            lblminquantity.Text = "-";
            lblminimumprice.Text = "-";
            txtnic.Clear();
            txtcusname.Clear();
            txtcusbalance.Clear();

            lbltotalamount.Text = "-";
            lbltotaldiscount.Text = "-";
            lblitemcount.Text = "-";
            txtcash.Clear();
            combpayment.Text = "Cash";
            txtcardno.Clear();

            dataGridViewsold.Rows.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                decimal currentTotal = 0;

                // Step 1: Fetch current total from database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string fetchQuery = "SELECT total FROM total_sale"; // Assuming only one row

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            currentTotal = Convert.ToDecimal(result); // Convert fetched value to decimal
                        }
                    }
                }

                // Step 2: Get the new value from lbltotal and add to current total
                if (!decimal.TryParse(txtcashvalue.Text, out decimal newAmount))
                {
                    MessageBox.Show("Invalid total value in label.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal updatedTotal = currentTotal + newAmount; // Add new amount to current total

                // Step 3: Update the total value in the database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string updateQuery = "UPDATE total_sale SET total = @updatedTotal";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@updatedTotal", updatedTotal);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Total updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fetchdata();
                            txtcashvalue.Clear();
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check your database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                decimal currentTotal = 0;

                // Step 1: Fetch current total from database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string fetchQuery = "SELECT total FROM total_sale"; // Assuming only one row

                    using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            currentTotal = Convert.ToDecimal(result); // Convert fetched value to decimal
                        }
                    }
                }

                // Step 2: Get the new value from lbltotal and add to current total
                if (!decimal.TryParse(txtcashvalue.Text, out decimal newAmount))
                {
                    MessageBox.Show("Invalid total value in label.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal updatedTotal = currentTotal - newAmount; // Add new amount to current total

                // Step 3: Update the total value in the database
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string updateQuery = "UPDATE total_sale SET total = @updatedTotal";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@updatedTotal", updatedTotal);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Total updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fetchdata();
                            txtcashvalue.Clear();
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check your database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
