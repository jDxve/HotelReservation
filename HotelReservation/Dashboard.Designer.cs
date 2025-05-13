namespace HotelReservation
{
    partial class Dashboard
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
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnRoomTypes = new System.Windows.Forms.Button();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnRoomBookingLogs = new System.Windows.Forms.Button();
            this.btnReviews = new System.Windows.Forms.Button();
            this.btnMakeReservation = new System.Windows.Forms.Button();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnGuests = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.mainPanel.SuspendLayout();
            this.menuPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.menuPanel);
            this.mainPanel.Controls.Add(this.headerPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(900, 550);
            this.mainPanel.TabIndex = 0;
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.menuPanel.BackgroundImage = global::HotelReservation.Properties.Resources.hotels1;
            this.menuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuPanel.Controls.Add(this.btnLogout);
            this.menuPanel.Controls.Add(this.btnRoomTypes);
            this.menuPanel.Controls.Add(this.btnRooms);
            this.menuPanel.Controls.Add(this.btnRoomBookingLogs);
            this.menuPanel.Controls.Add(this.btnReviews);
            this.menuPanel.Controls.Add(this.btnMakeReservation);
            this.menuPanel.Controls.Add(this.btnPayments);
            this.menuPanel.Controls.Add(this.btnGuests);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(0, 80);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(900, 470);
            this.menuPanel.TabIndex = 1;
            this.menuPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.menuPanel_Paint);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(783, 415);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(105, 40);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnRoomTypes
            // 
            this.btnRoomTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRoomTypes.FlatAppearance.BorderSize = 0;
            this.btnRoomTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomTypes.ForeColor = System.Drawing.Color.White;
            this.btnRoomTypes.Location = new System.Drawing.Point(520, 260);
            this.btnRoomTypes.Name = "btnRoomTypes";
            this.btnRoomTypes.Size = new System.Drawing.Size(200, 60);
            this.btnRoomTypes.TabIndex = 6;
            this.btnRoomTypes.Text = "Room Types";
            this.btnRoomTypes.UseVisualStyleBackColor = false;
            this.btnRoomTypes.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRooms.FlatAppearance.BorderSize = 0;
            this.btnRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRooms.ForeColor = System.Drawing.Color.White;
            this.btnRooms.Location = new System.Drawing.Point(520, 170);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(200, 60);
            this.btnRooms.TabIndex = 5;
            this.btnRooms.Text = "Rooms";
            this.btnRooms.UseVisualStyleBackColor = false;
            this.btnRooms.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnRoomBookingLogs
            // 
            this.btnRoomBookingLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRoomBookingLogs.FlatAppearance.BorderSize = 0;
            this.btnRoomBookingLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomBookingLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomBookingLogs.ForeColor = System.Drawing.Color.White;
            this.btnRoomBookingLogs.Location = new System.Drawing.Point(520, 80);
            this.btnRoomBookingLogs.Name = "btnRoomBookingLogs";
            this.btnRoomBookingLogs.Size = new System.Drawing.Size(200, 60);
            this.btnRoomBookingLogs.TabIndex = 4;
            this.btnRoomBookingLogs.Text = "Room Booking Logs";
            this.btnRoomBookingLogs.UseVisualStyleBackColor = false;
            this.btnRoomBookingLogs.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnReviews
            // 
            this.btnReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnReviews.FlatAppearance.BorderSize = 0;
            this.btnReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviews.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReviews.ForeColor = System.Drawing.Color.White;
            this.btnReviews.Location = new System.Drawing.Point(170, 260);
            this.btnReviews.Name = "btnReviews";
            this.btnReviews.Size = new System.Drawing.Size(200, 60);
            this.btnReviews.TabIndex = 3;
            this.btnReviews.Text = "Reviews";
            this.btnReviews.UseVisualStyleBackColor = false;
            this.btnReviews.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnMakeReservation
            // 
            this.btnMakeReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMakeReservation.FlatAppearance.BorderSize = 0;
            this.btnMakeReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeReservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeReservation.ForeColor = System.Drawing.Color.White;
            this.btnMakeReservation.Location = new System.Drawing.Point(170, 170);
            this.btnMakeReservation.Name = "btnMakeReservation";
            this.btnMakeReservation.Size = new System.Drawing.Size(200, 60);
            this.btnMakeReservation.TabIndex = 2;
            this.btnMakeReservation.Text = "Make a Reservation";
            this.btnMakeReservation.UseVisualStyleBackColor = false;
            this.btnMakeReservation.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnPayments
            // 
            this.btnPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayments.ForeColor = System.Drawing.Color.White;
            this.btnPayments.Location = new System.Drawing.Point(170, 80);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Size = new System.Drawing.Size(200, 60);
            this.btnPayments.TabIndex = 1;
            this.btnPayments.Text = "Payments";
            this.btnPayments.UseVisualStyleBackColor = false;
            this.btnPayments.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGuests
            // 
            this.btnGuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGuests.FlatAppearance.BorderSize = 0;
            this.btnGuests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuests.ForeColor = System.Drawing.Color.White;
            this.btnGuests.Location = new System.Drawing.Point(345, 350);
            this.btnGuests.Name = "btnGuests";
            this.btnGuests.Size = new System.Drawing.Size(200, 60);
            this.btnGuests.TabIndex = 0;
            this.btnGuests.Text = "Guests";
            this.btnGuests.UseVisualStyleBackColor = false;
            this.btnGuests.Click += new System.EventHandler(this.button1_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.headerPanel.Controls.Add(this.lblWelcome);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.logoBox);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 80);
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.headerPanel_Paint);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(615, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(273, 23);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Welcome, Administrator";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(85, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(355, 29);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Hotel Reservation Dashboard";
            // 
            // logoBox
            // 
            this.logoBox.Location = new System.Drawing.Point(20, 15);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(50, 50);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoBox.TabIndex = 0;
            this.logoBox.TabStop = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Management - Dashboard";
            this.mainPanel.ResumeLayout(false);
            this.menuPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Button btnGuests;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnMakeReservation;
        private System.Windows.Forms.Button btnReviews;
        private System.Windows.Forms.Button btnRoomBookingLogs;
        private System.Windows.Forms.Button btnRooms;
        private System.Windows.Forms.Button btnRoomTypes;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
    }
}