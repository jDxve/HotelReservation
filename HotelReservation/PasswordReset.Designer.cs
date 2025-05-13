namespace HotelReservation
{
    partial class PasswordReset
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.pinLabel = new System.Windows.Forms.Label();
            this.pinTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.actionButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.mainPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.contentPanel);
            this.mainPanel.Controls.Add(this.headerPanel);
            this.mainPanel.Controls.Add(this.buttonPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(400, 350);
            this.mainPanel.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(400, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(118, 19);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(164, 24);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Password Reset";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.usernameLabel);
            this.contentPanel.Controls.Add(this.pinLabel);
            this.contentPanel.Controls.Add(this.pinTextBox);
            this.contentPanel.Controls.Add(this.newPasswordLabel);
            this.contentPanel.Controls.Add(this.newPasswordTextBox);
            this.contentPanel.Controls.Add(this.confirmPasswordLabel);
            this.contentPanel.Controls.Add(this.confirmPasswordTextBox);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(400, 230);
            this.contentPanel.TabIndex = 1;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(20, 20);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(77, 17);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // pinLabel
            // 
            this.pinLabel.AutoSize = true;
            this.pinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pinLabel.Location = new System.Drawing.Point(20, 60);
            this.pinLabel.Name = "pinLabel";
            this.pinLabel.Size = new System.Drawing.Size(92, 17);
            this.pinLabel.TabIndex = 1;
            this.pinLabel.Text = "Security PIN:";
            // 
            // pinTextBox
            // 
            this.pinTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pinTextBox.Location = new System.Drawing.Point(120, 60);
            this.pinTextBox.Name = "pinTextBox";
            this.pinTextBox.PasswordChar = '*';
            this.pinTextBox.Size = new System.Drawing.Size(230, 23);
            this.pinTextBox.TabIndex = 2;
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPasswordLabel.Location = new System.Drawing.Point(20, 100);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(104, 17);
            this.newPasswordLabel.TabIndex = 3;
            this.newPasswordLabel.Text = "New Password:";
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPasswordTextBox.Location = new System.Drawing.Point(130, 100);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.PasswordChar = '•';
            this.newPasswordTextBox.Size = new System.Drawing.Size(220, 23);
            this.newPasswordTextBox.TabIndex = 4;
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPasswordLabel.Location = new System.Drawing.Point(20, 140);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(125, 17);
            this.confirmPasswordLabel.TabIndex = 5;
            this.confirmPasswordLabel.Text = "Confirm Password:";
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(150, 140);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.PasswordChar = '•';
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(200, 23);
            this.confirmPasswordTextBox.TabIndex = 6;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonPanel.Controls.Add(this.actionButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 290);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(400, 60);
            this.buttonPanel.TabIndex = 2;
            // 
            // actionButton
            // 
            this.actionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.actionButton.FlatAppearance.BorderSize = 0;
            this.actionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionButton.ForeColor = System.Drawing.Color.White;
            this.actionButton.Location = new System.Drawing.Point(210, 15);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(160, 30);
            this.actionButton.TabIndex = 0;
            this.actionButton.Text = "Verify PIN";
            this.actionButton.UseVisualStyleBackColor = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Silver;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(30, 15);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(160, 30);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PasswordReset
            // 
            this.AcceptButton = this.actionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordReset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password Reset";
            this.mainPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label pinLabel;
        private System.Windows.Forms.TextBox pinTextBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.Label confirmPasswordLabel;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Button cancelButton;
    }
}