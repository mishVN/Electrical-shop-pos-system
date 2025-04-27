namespace Seya
{
    partial class items
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelsearch = new System.Windows.Forms.Panel();
            this.panelsearchresult = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelsearchbar = new System.Windows.Forms.Panel();
            this.txtsearchbar = new System.Windows.Forms.TextBox();
            this.panelcontent = new System.Windows.Forms.Panel();
            this.panelstock = new System.Windows.Forms.Panel();
            this.butupdatestock = new System.Windows.Forms.Button();
            this.lblavailable = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtstockadd = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.butnew = new System.Windows.Forms.Button();
            this.txtminquantity = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtminimumprice = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtsallingprice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtcost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtsupplierid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtsupplier = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtdescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbarcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.paneladd = new System.Windows.Forms.Panel();
            this.txtwarranty = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.butupdateitem = new System.Windows.Forms.Button();
            this.lblprofit = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panelsearch.SuspendLayout();
            this.panelsearchresult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelsearchbar.SuspendLayout();
            this.panelcontent.SuspendLayout();
            this.panelstock.SuspendLayout();
            this.paneladd.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelsearch
            // 
            this.panelsearch.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelsearch.Controls.Add(this.panelsearchresult);
            this.panelsearch.Controls.Add(this.panelsearchbar);
            this.panelsearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelsearch.Location = new System.Drawing.Point(0, 0);
            this.panelsearch.Name = "panelsearch";
            this.panelsearch.Size = new System.Drawing.Size(258, 485);
            this.panelsearch.TabIndex = 0;
            // 
            // panelsearchresult
            // 
            this.panelsearchresult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelsearchresult.Controls.Add(this.dataGridView1);
            this.panelsearchresult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelsearchresult.Location = new System.Drawing.Point(0, 25);
            this.panelsearchresult.Name = "panelsearchresult";
            this.panelsearchresult.Size = new System.Drawing.Size(258, 460);
            this.panelsearchresult.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(254, 456);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panelsearchbar
            // 
            this.panelsearchbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelsearchbar.Controls.Add(this.txtsearchbar);
            this.panelsearchbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelsearchbar.Location = new System.Drawing.Point(0, 0);
            this.panelsearchbar.Name = "panelsearchbar";
            this.panelsearchbar.Size = new System.Drawing.Size(258, 25);
            this.panelsearchbar.TabIndex = 0;
            // 
            // txtsearchbar
            // 
            this.txtsearchbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsearchbar.Location = new System.Drawing.Point(0, 0);
            this.txtsearchbar.Name = "txtsearchbar";
            this.txtsearchbar.Size = new System.Drawing.Size(254, 20);
            this.txtsearchbar.TabIndex = 0;
            this.txtsearchbar.TextChanged += new System.EventHandler(this.txtsearchbar_TextChanged);
            this.txtsearchbar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearchbar_KeyPress);
            // 
            // panelcontent
            // 
            this.panelcontent.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelcontent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelcontent.Controls.Add(this.panelstock);
            this.panelcontent.Controls.Add(this.butnew);
            this.panelcontent.Controls.Add(this.txtminquantity);
            this.panelcontent.Controls.Add(this.label10);
            this.panelcontent.Controls.Add(this.txtminimumprice);
            this.panelcontent.Controls.Add(this.label9);
            this.panelcontent.Controls.Add(this.txtsallingprice);
            this.panelcontent.Controls.Add(this.label8);
            this.panelcontent.Controls.Add(this.txtcost);
            this.panelcontent.Controls.Add(this.label7);
            this.panelcontent.Controls.Add(this.txtsupplierid);
            this.panelcontent.Controls.Add(this.label6);
            this.panelcontent.Controls.Add(this.txtsupplier);
            this.panelcontent.Controls.Add(this.label5);
            this.panelcontent.Controls.Add(this.txtname);
            this.panelcontent.Controls.Add(this.label4);
            this.panelcontent.Controls.Add(this.txtdescription);
            this.panelcontent.Controls.Add(this.label3);
            this.panelcontent.Controls.Add(this.txtbarcode);
            this.panelcontent.Controls.Add(this.label2);
            this.panelcontent.Controls.Add(this.txtcode);
            this.panelcontent.Controls.Add(this.label1);
            this.panelcontent.Controls.Add(this.paneladd);
            this.panelcontent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelcontent.Location = new System.Drawing.Point(258, 0);
            this.panelcontent.Name = "panelcontent";
            this.panelcontent.Size = new System.Drawing.Size(571, 485);
            this.panelcontent.TabIndex = 1;
            // 
            // panelstock
            // 
            this.panelstock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelstock.Controls.Add(this.butupdatestock);
            this.panelstock.Controls.Add(this.lblavailable);
            this.panelstock.Controls.Add(this.label14);
            this.panelstock.Controls.Add(this.txtstockadd);
            this.panelstock.Controls.Add(this.label13);
            this.panelstock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelstock.Location = new System.Drawing.Point(0, 343);
            this.panelstock.Name = "panelstock";
            this.panelstock.Size = new System.Drawing.Size(567, 138);
            this.panelstock.TabIndex = 26;
            // 
            // butupdatestock
            // 
            this.butupdatestock.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butupdatestock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butupdatestock.Location = new System.Drawing.Point(17, 53);
            this.butupdatestock.Name = "butupdatestock";
            this.butupdatestock.Size = new System.Drawing.Size(175, 31);
            this.butupdatestock.TabIndex = 22;
            this.butupdatestock.Text = "ADD";
            this.butupdatestock.UseVisualStyleBackColor = false;
            this.butupdatestock.Click += new System.EventHandler(this.butupdatestock_Click);
            // 
            // lblavailable
            // 
            this.lblavailable.AutoSize = true;
            this.lblavailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblavailable.Location = new System.Drawing.Point(343, 24);
            this.lblavailable.Name = "lblavailable";
            this.lblavailable.Size = new System.Drawing.Size(11, 15);
            this.lblavailable.TabIndex = 30;
            this.lblavailable.Text = "-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(234, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "Available Quantity";
            // 
            // txtstockadd
            // 
            this.txtstockadd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstockadd.Location = new System.Drawing.Point(17, 26);
            this.txtstockadd.Name = "txtstockadd";
            this.txtstockadd.Size = new System.Drawing.Size(175, 21);
            this.txtstockadd.TabIndex = 28;
            this.txtstockadd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstockadd_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 15);
            this.label13.TabIndex = 27;
            this.label13.Text = "Quantity";
            // 
            // butnew
            // 
            this.butnew.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butnew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butnew.Location = new System.Drawing.Point(125, 29);
            this.butnew.Name = "butnew";
            this.butnew.Size = new System.Drawing.Size(78, 22);
            this.butnew.TabIndex = 24;
            this.butnew.Text = "NEW";
            this.butnew.UseVisualStyleBackColor = false;
            this.butnew.Click += new System.EventHandler(this.butnew_Click);
            // 
            // txtminquantity
            // 
            this.txtminquantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtminquantity.Location = new System.Drawing.Point(226, 302);
            this.txtminquantity.Name = "txtminquantity";
            this.txtminquantity.Size = new System.Drawing.Size(175, 21);
            this.txtminquantity.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(225, 286);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Quantity";
            // 
            // txtminimumprice
            // 
            this.txtminimumprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtminimumprice.Location = new System.Drawing.Point(21, 302);
            this.txtminimumprice.Name = "txtminimumprice";
            this.txtminimumprice.Size = new System.Drawing.Size(175, 21);
            this.txtminimumprice.TabIndex = 17;
            this.txtminimumprice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtminimumprice_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 286);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "Minimum Price";
            // 
            // txtsallingprice
            // 
            this.txtsallingprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsallingprice.Location = new System.Drawing.Point(21, 255);
            this.txtsallingprice.Name = "txtsallingprice";
            this.txtsallingprice.Size = new System.Drawing.Size(175, 21);
            this.txtsallingprice.TabIndex = 15;
            this.txtsallingprice.TextChanged += new System.EventHandler(this.txtsallingprice_TextChanged);
            this.txtsallingprice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsallingprice_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Selling Price";
            // 
            // txtcost
            // 
            this.txtcost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcost.Location = new System.Drawing.Point(21, 208);
            this.txtcost.Name = "txtcost";
            this.txtcost.Size = new System.Drawing.Size(175, 21);
            this.txtcost.TabIndex = 13;
            this.txtcost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcost_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cost";
            // 
            // txtsupplierid
            // 
            this.txtsupplierid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsupplierid.Location = new System.Drawing.Point(228, 164);
            this.txtsupplierid.Name = "txtsupplierid";
            this.txtsupplierid.Size = new System.Drawing.Size(175, 21);
            this.txtsupplierid.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(227, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Supplier id";
            // 
            // txtsupplier
            // 
            this.txtsupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsupplier.Location = new System.Drawing.Point(21, 164);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Size = new System.Drawing.Size(175, 21);
            this.txtsupplier.TabIndex = 9;
            this.txtsupplier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsupplier_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Supplier Name";
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.Location = new System.Drawing.Point(21, 120);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(175, 21);
            this.txtname.TabIndex = 7;
            this.txtname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtname_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name";
            // 
            // txtdescription
            // 
            this.txtdescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescription.Location = new System.Drawing.Point(21, 77);
            this.txtdescription.Name = "txtdescription";
            this.txtdescription.Size = new System.Drawing.Size(175, 21);
            this.txtdescription.TabIndex = 5;
            this.txtdescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdescription_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description";
            // 
            // txtbarcode
            // 
            this.txtbarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbarcode.Location = new System.Drawing.Point(226, 29);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Size = new System.Drawing.Size(175, 21);
            this.txtbarcode.TabIndex = 3;
            this.txtbarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbarcode_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(225, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Barcode\\Serial Number";
            // 
            // txtcode
            // 
            this.txtcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(19, 29);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(100, 21);
            this.txtcode.TabIndex = 1;
            this.txtcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcode_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code";
            // 
            // paneladd
            // 
            this.paneladd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.paneladd.Controls.Add(this.button2);
            this.paneladd.Controls.Add(this.txtwarranty);
            this.paneladd.Controls.Add(this.label16);
            this.paneladd.Controls.Add(this.button1);
            this.paneladd.Controls.Add(this.butupdateitem);
            this.paneladd.Controls.Add(this.lblprofit);
            this.paneladd.Controls.Add(this.label11);
            this.paneladd.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneladd.Location = new System.Drawing.Point(0, 0);
            this.paneladd.Name = "paneladd";
            this.paneladd.Size = new System.Drawing.Size(567, 343);
            this.paneladd.TabIndex = 25;
            // 
            // txtwarranty
            // 
            this.txtwarranty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwarranty.Location = new System.Drawing.Point(226, 206);
            this.txtwarranty.Name = "txtwarranty";
            this.txtwarranty.Size = new System.Drawing.Size(175, 21);
            this.txtwarranty.TabIndex = 28;
            this.txtwarranty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtwarranty_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(225, 190);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 15);
            this.label16.TabIndex = 27;
            this.label16.Text = "Warranty";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(427, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 31);
            this.button1.TabIndex = 20;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butupdateitem
            // 
            this.butupdateitem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butupdateitem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butupdateitem.Location = new System.Drawing.Point(427, 65);
            this.butupdateitem.Name = "butupdateitem";
            this.butupdateitem.Size = new System.Drawing.Size(96, 31);
            this.butupdateitem.TabIndex = 21;
            this.butupdateitem.Text = "UPDATE";
            this.butupdateitem.UseVisualStyleBackColor = false;
            this.butupdateitem.Click += new System.EventHandler(this.butupdateitem_Click);
            // 
            // lblprofit
            // 
            this.lblprofit.AutoSize = true;
            this.lblprofit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprofit.Location = new System.Drawing.Point(325, 253);
            this.lblprofit.Name = "lblprofit";
            this.lblprofit.Size = new System.Drawing.Size(11, 15);
            this.lblprofit.TabIndex = 23;
            this.lblprofit.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(288, 253);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 15);
            this.label11.TabIndex = 22;
            this.label11.Text = "Profit";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(427, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 31);
            this.button2.TabIndex = 29;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 485);
            this.Controls.Add(this.panelcontent);
            this.Controls.Add(this.panelsearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "items";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ITEMS";
            this.panelsearch.ResumeLayout(false);
            this.panelsearchresult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelsearchbar.ResumeLayout(false);
            this.panelsearchbar.PerformLayout();
            this.panelcontent.ResumeLayout(false);
            this.panelcontent.PerformLayout();
            this.panelstock.ResumeLayout(false);
            this.panelstock.PerformLayout();
            this.paneladd.ResumeLayout(false);
            this.paneladd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelsearch;
        private System.Windows.Forms.Panel panelsearchresult;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelsearchbar;
        private System.Windows.Forms.TextBox txtsearchbar;
        private System.Windows.Forms.Panel panelcontent;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtminquantity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtminimumprice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtsallingprice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtcost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtsupplierid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtsupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtdescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel paneladd;
        private System.Windows.Forms.Button butnew;
        private System.Windows.Forms.Label lblprofit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button butupdateitem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelstock;
        private System.Windows.Forms.Button butupdatestock;
        private System.Windows.Forms.Label lblavailable;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtstockadd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtwarranty;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button2;
    }
}