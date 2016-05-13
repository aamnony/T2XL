namespace T2XL
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mtxtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.lblOwnPhone = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnSendCode = new System.Windows.Forms.Button();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.lblWrongCode = new System.Windows.Forms.Label();
            this.mtxtCode = new System.Windows.Forms.MaskedTextBox();
            this.lblWorking = new System.Windows.Forms.Label();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxtPhoneNumber
            // 
            this.mtxtPhoneNumber.Location = new System.Drawing.Point(53, 29);
            this.mtxtPhoneNumber.Mask = "000000000000";
            this.mtxtPhoneNumber.Name = "mtxtPhoneNumber";
            this.mtxtPhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.mtxtPhoneNumber.TabIndex = 1;
            // 
            // lblOwnPhone
            // 
            this.lblOwnPhone.AutoSize = true;
            this.lblOwnPhone.Location = new System.Drawing.Point(6, 32);
            this.lblOwnPhone.Name = "lblOwnPhone";
            this.lblOwnPhone.Size = new System.Drawing.Size(41, 13);
            this.lblOwnPhone.TabIndex = 2;
            this.lblOwnPhone.Text = "Phone:";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(12, 56);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(35, 13);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "Code:";
            // 
            // btnSendCode
            // 
            this.btnSendCode.Location = new System.Drawing.Point(159, 27);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(75, 23);
            this.btnSendCode.TabIndex = 5;
            this.btnSendCode.Text = "Send Code";
            this.btnSendCode.UseVisualStyleBackColor = true;
            this.btnSendCode.Click += new System.EventHandler(this.btnSendCode_Click);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.lblWrongCode);
            this.grpLogin.Controls.Add(this.mtxtCode);
            this.grpLogin.Controls.Add(this.mtxtPhoneNumber);
            this.grpLogin.Controls.Add(this.lblOwnPhone);
            this.grpLogin.Controls.Add(this.lblCode);
            this.grpLogin.Controls.Add(this.btnSendCode);
            this.grpLogin.Location = new System.Drawing.Point(9, 9);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(242, 88);
            this.grpLogin.TabIndex = 20;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Login";
            // 
            // lblWrongCode
            // 
            this.lblWrongCode.AutoSize = true;
            this.lblWrongCode.Location = new System.Drawing.Point(160, 58);
            this.lblWrongCode.Name = "lblWrongCode";
            this.lblWrongCode.Size = new System.Drawing.Size(66, 13);
            this.lblWrongCode.TabIndex = 7;
            this.lblWrongCode.Text = "Wrong code";
            this.lblWrongCode.Visible = false;
            // 
            // mtxtCode
            // 
            this.mtxtCode.Location = new System.Drawing.Point(53, 55);
            this.mtxtCode.Mask = "00000";
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.Size = new System.Drawing.Size(100, 20);
            this.mtxtCode.TabIndex = 6;
            this.mtxtCode.ValidatingType = typeof(int);
            this.mtxtCode.TextChanged += new System.EventHandler(this.mtxtCode_TextChanged);
            // 
            // lblWorking
            // 
            this.lblWorking.AutoSize = true;
            this.lblWorking.Location = new System.Drawing.Point(84, 49);
            this.lblWorking.Name = "lblWorking";
            this.lblWorking.Size = new System.Drawing.Size(99, 13);
            this.lblWorking.TabIndex = 8;
            this.lblWorking.Text = "Adding messages...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 107);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.lblWorking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox mtxtPhoneNumber;
        private System.Windows.Forms.Label lblOwnPhone;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Button btnSendCode;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.MaskedTextBox mtxtCode;
        private System.Windows.Forms.Label lblWrongCode;
        private System.Windows.Forms.Label lblWorking;
    }
}

