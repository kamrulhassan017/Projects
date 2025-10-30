namespace PIzzaShop
{
    partial class Login
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
            this.txtBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowHide = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.radioOwner = new System.Windows.Forms.RadioButton();
            this.radioCSO = new System.Windows.Forms.RadioButton();
            this.radioManager = new System.Windows.Forms.RadioButton();
            this.radioChef = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(674, 25);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(183, 52);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(354, 156);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(214, 40);
            this.txtUsername.TabIndex = 1;
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPassword.Location = new System.Drawing.Point(354, 228);
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.Size = new System.Drawing.Size(214, 40);
            this.txtBoxPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(180, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 16.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(180, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // btnShowHide
            // 
            this.btnShowHide.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHide.Location = new System.Drawing.Point(590, 231);
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(102, 37);
            this.btnShowHide.TabIndex = 5;
            this.btnShowHide.UseVisualStyleBackColor = true;
            this.btnShowHide.Click += new System.EventHandler(this.btnShowHide_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("MingLiU_MSCS-ExtB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(406, 388);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(162, 52);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // radioOwner
            // 
            this.radioOwner.AutoSize = true;
            this.radioOwner.Location = new System.Drawing.Point(352, 292);
            this.radioOwner.Name = "radioOwner";
            this.radioOwner.Size = new System.Drawing.Size(66, 20);
            this.radioOwner.TabIndex = 7;
            this.radioOwner.TabStop = true;
            this.radioOwner.Text = "Owner";
            this.radioOwner.UseVisualStyleBackColor = true;
            // 
            // radioCSO
            // 
            this.radioCSO.AutoSize = true;
            this.radioCSO.Location = new System.Drawing.Point(474, 292);
            this.radioCSO.Name = "radioCSO";
            this.radioCSO.Size = new System.Drawing.Size(56, 20);
            this.radioCSO.TabIndex = 8;
            this.radioCSO.TabStop = true;
            this.radioCSO.Text = "CSO";
            this.radioCSO.UseVisualStyleBackColor = true;
            // 
            // radioManager
            // 
            this.radioManager.AutoSize = true;
            this.radioManager.Location = new System.Drawing.Point(352, 335);
            this.radioManager.Name = "radioManager";
            this.radioManager.Size = new System.Drawing.Size(82, 20);
            this.radioManager.TabIndex = 9;
            this.radioManager.TabStop = true;
            this.radioManager.Text = "Manager";
            this.radioManager.UseVisualStyleBackColor = true;
            // 
            // radioChef
            // 
            this.radioChef.AutoSize = true;
            this.radioChef.Location = new System.Drawing.Point(474, 335);
            this.radioChef.Name = "radioChef";
            this.radioChef.Size = new System.Drawing.Size(55, 20);
            this.radioChef.TabIndex = 10;
            this.radioChef.TabStop = true;
            this.radioChef.Text = "Chef";
            this.radioChef.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.radioChef);
            this.Controls.Add(this.radioManager);
            this.Controls.Add(this.radioCSO);
            this.Controls.Add(this.radioOwner);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnShowHide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnBack);
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowHide;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.RadioButton radioOwner;
        private System.Windows.Forms.RadioButton radioCSO;
        private System.Windows.Forms.RadioButton radioManager;
        private System.Windows.Forms.RadioButton radioChef;
    }
}

