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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Seya
{
    public partial class Dashboard : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public Dashboard()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            fetchdata();
            LoadChartData();
            displayCheque();
            CalculateSum();
        }

        private void buticon_Click(object sender, EventArgs e)
        {
            items item = new items();
            item.ShowDialog();
        }

        private void butsupplier_Click(object sender, EventArgs e)
        {
            supplier supplier = new supplier();
            supplier.ShowDialog();
        }

        private void butcustomer_Click(object sender, EventArgs e)
        {
            customer cus = new customer();
            cus.ShowDialog();
        }

        private void butcheque_Click(object sender, EventArgs e)
        {
            cheque che = new cheque();
            che.ShowDialog();
        }

        private void butuseraccount_Click(object sender, EventArgs e)
        {
            useraccount user = new useraccount();
            user.Show();

        }

        private void button2_Click(object sender, EventArgs e)
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
        public void CalculateAndInsertDailySales()
        {
            try
            {
                decimal totalSales = 0;
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd"); // Get today's date in YYYY-MM-DD format

                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    // Step 1: Calculate the sum of sold_price where date = today
                    string sumQuery = "SELECT SUM(sold_price) FROM salling_item WHERE CONVERT(date, date) = @currentDate";

                    using (SqlCommand cmd = new SqlCommand(sumQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@currentDate", currentDate);
                        object result = cmd.ExecuteScalar(); // Get sum of sold_price

                        if (result != null && result != DBNull.Value)
                        {
                            totalSales = Convert.ToDecimal(result);
                        }
                    }

                    // Step 2: Insert total_sales into daily_sales table
                    string insertQuery = "INSERT INTO daily_sales (date, total_sale) VALUES (@date, @totalSale)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", currentDate);
                        cmd.Parameters.AddWithValue("@totalSale", totalSales);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Daily sales recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert daily sales.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure, Do the day End Process",
                                         "Confirm Process",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If user clicks 'Yes', proceed with the process
            if (result == DialogResult.Yes)
            {
                try
                {
                    CalculateAndInsertDailySales(); // Call your function

                    // Show success message after function execution
                    MessageBox.Show("Day End Process successfully!",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Show error message if an exception occurs
                    MessageBox.Show("Error occurred: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void LoadChartData()
        {
            string query = "SELECT TOP 7 date, total_sale FROM daily_sales ORDER BY date ASC"; // Change ORDER to ASC

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Clear existing data in the chart
                chart1.Series.Clear();

                // Create a new series for the chart
                var series = chart1.Series.Add("Daily Sales");
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // Use Line Chart

                // Loop through the DataTable to add data to the chart
                foreach (DataRow row in dt.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["date"]);
                    decimal totalSale = Convert.ToDecimal(row["total_sale"]);

                    // Add the data point to the chart series
                    series.Points.AddXY(date.ToString("yyyy-MM-dd"), totalSale);
                }

                // Format X-Axis to display dates properly
                chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Rotate labels for better visibility
                chart1.ChartAreas[0].AxisX.Interval = 1; // Ensure each date is shown
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Hide grid lines for clarity
            }
        }

        public void displayCheque()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd"); // Current date formatted as yyyy-MM-dd
            string query = "SELECT * FROM outgoing_cheque WHERE passing_date = @currentDate";

            // Create a new SqlConnection object
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the current date parameter to the query
                        cmd.Parameters.AddWithValue("@currentDate", currentDate);

                        // Execute the command and fetch the data into a DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Create a DataTable to hold the results
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            displayCheque();
            fetchdata();
            LoadChartData();
            CalculateSum();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void CalculateSum()
        {
            string query = "SELECT SUM(sold_price) FROM salling_item WHERE CAST(date AS DATE) = @today";

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Use DateTime.Today instead of a string
                        cmd.Parameters.Add("@today", SqlDbType.Date).Value = DateTime.Today;

                        object result = cmd.ExecuteScalar();
                        decimal totalSum = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;

                        lbltodaysale.Text = totalSum.ToString("N2"); // Format as currency (optional)
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(txtusername.Text==""||
                txtpassword.Text == "")
            {
                MessageBox.Show("Fill Blanks.","Information Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string insert = @"DELETE FROM [temp_login_user] WHERE user_name=@user_name";

                    using (SqlCommand cmd1 = new SqlCommand(insert, conn))
                    {

                        cmd1.Parameters.AddWithValue("@user_name", txtusername.Text.Trim());


                        int rowsAffected = cmd1.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("SuccessFull Logoff", "Logoff", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        else
                        {
                            MessageBox.Show("No rows were inserted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            poslogin poslogin = new poslogin();
            poslogin.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            poslogin poslogin = new poslogin();
            poslogin.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }

}
