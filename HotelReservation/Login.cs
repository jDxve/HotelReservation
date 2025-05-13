using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HotelReservation
{
    public partial class Login : Form
    {
        // Connection string for MySQL database
        private string connectionString = "Server=localhost;Database=hotel_reservation;Uid=root;Pwd=root;";

        public Login()
        {
            InitializeComponent();

            // Set focus to username textbox on form load
            this.ActiveControl = usernameTextBox;

            // Optional: Add a hotel logo if available
            try
            {
                // If you have a logo resource in your project
                // logoBox.Image = Properties.Resources.hotel_logo;
            }
            catch (Exception)
            {
                // Logo not available, no action needed
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // You can do additional initialization here
            // Example: Check if database is available on startup
            // Uncomment the line below if you want to test connection on form load
            // TestDatabaseConnection();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Please enter your username!", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter your password!", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return;
            }

            // Attempt to login
            AttemptLogin(usernameTextBox.Text.Trim(), passwordTextBox.Text.Trim());
        }

        private void AttemptLogin(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM admin WHERE username = @username AND password = @password";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Login successful
                                MakeReservation makeReservationForm = new MakeReservation();

                                // Hide the current form
                                this.Hide();

                                // Show the MakeReservation form
                                makeReservationForm.Show();
                            }
                            else
                            {
                                // Login failed
                                MessageBox.Show("Invalid username or password!", "Login Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                passwordTextBox.Clear(); // Clear password
                                passwordTextBox.Focus(); // Focus on password field
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}\n\nMake sure your MySQL server is running and the database is properly set up.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void forgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Validate that username is provided
            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Please enter your username first.", "Forgot Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                usernameTextBox.Focus();
                return;
            }

            // First check if the username exists in the database
            bool userExists = false;
            string username = usernameTextBox.Text.Trim();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM admin WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        userExists = (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!userExists)
            {
                MessageBox.Show("Username not found. Please check your username and try again.",
                    "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return;
            }

            // Open the password reset form
            PasswordReset resetForm = new PasswordReset(username);
            if (resetForm.ShowDialog() == DialogResult.OK)
            {
                // Password reset was successful
                MessageBox.Show("Your password has been reset successfully. You can now log in with your new password.",
                    "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Optional: Add a key event handler for the form to enable Escape key to close
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}