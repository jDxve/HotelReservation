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
        public Dashboard()
        {
            InitializeComponent();
            this.FormClosing += Dashboard_FormClosing; // Add form closing event handler
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
            OpenForm(new Reserations());
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

        // Add this method and button for logging out
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            this.Hide();
            loginForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            this.Hide();
            loginForm.Show();
        }
    }
}
