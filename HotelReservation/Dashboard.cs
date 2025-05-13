using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservation
{
    public partial class Dashboard : Form
    {
        private string username;

        public Dashboard()
        {
            InitializeComponent();
            this.FormClosing += Dashboard_FormClosing; // Add form closing event handler
        }

        // Optional: Constructor that accepts username for personalized welcome
        public Dashboard(string username)
        {
            InitializeComponent();
            this.username = username;
            this.FormClosing += Dashboard_FormClosing;

            // Set welcome message if username is provided
            if (!string.IsNullOrEmpty(username))
            {
                lblWelcome.Text = $"Welcome, {username}";
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Initialization code when dashboard loads
            SetButtonHoverEffects();
        }

        // Add hover effects to all menu buttons
        private void SetButtonHoverEffects()
        {
            foreach (Control c in menuPanel.Controls)
            {
                if (c is Button btn && btn != btnLogout)
                {
                    // Original color is stored in the Tag property
                    btn.Tag = btn.BackColor;

                    // Mouse enter - lighten the button
                    btn.MouseEnter += (s, args) => {
                        Button b = (Button)s;
                        b.BackColor = ControlPaint.Light((Color)b.Tag, 0.2f);
                    };

                    // Mouse leave - restore original color
                    btn.MouseLeave += (s, args) => {
                        Button b = (Button)s;
                        b.BackColor = (Color)b.Tag;
                    };
                }
            }
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Properly exit the application when dashboard is closed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm(new Guests());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(new Payments());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenForm(new MakeReservation());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenForm(new Reviews());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenForm(new RoomBookingLogs());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenForm(new Rooms());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenForm(new Room_Types());
        }

        // Helper method to handle form navigation
        private void OpenForm(Form form)
        {
            form.FormClosed += (s, args) => this.Show(); // Show dashboard when child form is closed
            this.Hide();
            form.Show();
        }

        // Updated logout method
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Confirm before logout
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Login loginForm = new Login();
                this.Hide();
                loginForm.Show();
            }
        }

        // Keep compatibility with previous button8_Click
        private void button8_Click(object sender, EventArgs e)
        {
            btnLogout_Click(sender, e);
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}