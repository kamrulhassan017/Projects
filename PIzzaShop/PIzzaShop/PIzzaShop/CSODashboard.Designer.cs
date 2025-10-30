namespace PIzzaShop
{
    partial class CSODashboard

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
            this.btnBack = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddPizza = new System.Windows.Forms.Button();
            this.comboPIzza = new System.Windows.Forms.ComboBox();
            this.comboSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboIngredients = new System.Windows.Forms.ComboBox();
            this.btnAddIngre = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(787, 21);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(183, 52);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Logout";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(145, 21);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(214, 40);
            this.txtUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(507, 155);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(454, 326);
            this.dataGridView1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(25, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pizza:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(25, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Size:";
            // 
            // btnAddPizza
            // 
            this.btnAddPizza.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPizza.Location = new System.Drawing.Point(353, 258);
            this.btnAddPizza.Name = "btnAddPizza";
            this.btnAddPizza.Size = new System.Drawing.Size(115, 52);
            this.btnAddPizza.TabIndex = 8;
            this.btnAddPizza.Text = "Add";
            this.btnAddPizza.UseVisualStyleBackColor = true;
            this.btnAddPizza.Click += new System.EventHandler(this.btnAddPizza_Click);
            // 
            // comboPIzza
            // 
            this.comboPIzza.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.comboPIzza.FormattingEnabled = true;
            this.comboPIzza.Location = new System.Drawing.Point(238, 155);
            this.comboPIzza.Name = "comboPIzza";
            this.comboPIzza.Size = new System.Drawing.Size(230, 35);
            this.comboPIzza.TabIndex = 9;
            // 
            // comboSize
            // 
            this.comboSize.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.comboSize.FormattingEnabled = true;
            this.comboSize.Location = new System.Drawing.Point(238, 205);
            this.comboSize.Name = "comboSize";
            this.comboSize.Size = new System.Drawing.Size(230, 35);
            this.comboSize.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(22, 381);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ingredents:";
            // 
            // comboIngredients
            // 
            this.comboIngredients.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.comboIngredients.FormattingEnabled = true;
            this.comboIngredients.Location = new System.Drawing.Point(238, 378);
            this.comboIngredients.Name = "comboIngredients";
            this.comboIngredients.Size = new System.Drawing.Size(230, 35);
            this.comboIngredients.TabIndex = 11;
            // 
            // btnAddIngre
            // 
            this.btnAddIngre.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIngre.Location = new System.Drawing.Point(353, 419);
            this.btnAddIngre.Name = "btnAddIngre";
            this.btnAddIngre.Size = new System.Drawing.Size(115, 52);
            this.btnAddIngre.TabIndex = 12;
            this.btnAddIngre.Text = "Add";
            this.btnAddIngre.UseVisualStyleBackColor = true;
            this.btnAddIngre.Click += new System.EventHandler(this.btnAddIngre_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(502, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 28);
            this.label5.TabIndex = 13;
            this.label5.Text = "Order Menu:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(25, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 28);
            this.label6.TabIndex = 14;
            this.label6.Text = "Pizza Selection:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(22, 346);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(327, 28);
            this.label7.TabIndex = 15;
            this.label7.Text = "Ingredents Selection:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(735, 489);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(226, 52);
            this.btnConfirm.TabIndex = 16;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(515, 499);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(164, 40);
            this.txtTotal.TabIndex = 17;
            this.txtTotal.Text = "total";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(12, 499);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(156, 40);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CSODashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddIngre);
            this.Controls.Add(this.comboIngredients);
            this.Controls.Add(this.comboSize);
            this.Controls.Add(this.comboPIzza);
            this.Controls.Add(this.btnAddPizza);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnBack);
            this.MaximizeBox = false;
            this.Name = "CSODashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSO Dashboard";
            this.Load += new System.EventHandler(this.CSODashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddPizza;
        private System.Windows.Forms.ComboBox comboPIzza;
        private System.Windows.Forms.ComboBox comboSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboIngredients;
        private System.Windows.Forms.Button btnAddIngre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnClear;
    }
}

