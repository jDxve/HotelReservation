using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HotelReservation
{
    public partial class PasswordReset : Form
    {
        // Hardcoded security PIN for password reset
        private const string SECURITY_PIN = "907039";
        private string username;
        private string connectionString = "Server=localhost;Database=hotel_reservation;Uid=root;Pwd=root;";

        // Track the current state of the form
        private enum ResetState
        {
            EnterPin,
            EnterNewPassword
        }
        private ResetState currentState = ResetState.EnterPin;

        public PasswordReset(string username)
        {
            InitializeComponent();
            this.username = username;
            SetupInitialState();
        }

        private void SetupInitialState()
        {
            // Set form title and instructions
            this.Text = "Password Reset";

            // Set initial state to PIN verification
            usernameLabel.Text = $"Username: {username}";
            pinLabel.Visible = true;
            pinTextBox.Visible = true;
            newPasswordLabel.Visible = false;
            newPasswordTextBox.Visible = false;
            confirmPasswordLabel.Visible = false;
            confirmPasswordTextBox.Visible = false;

            // Set focus to PIN textbox
            pinTextBox.Focus();

            // Set button text
            actionButton.Text = "Verify PIN";

            // Set current state
            currentState = ResetState.EnterPin;
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case ResetState.EnterPin:
                    VerifyPin();
                    break;
                case ResetState.EnterNewPassword:
                    ResetPassword();
                    break;
            }
        }

        private void VerifyPin()
        {
            string enteredPin = pinTextBox.Text.Trim();

            if (string.IsNullOrEmpty(enteredPin))
            {
                MessageBox.Show("Please enter the security PIN.", "Input Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (enteredPin == SECURITY_PIN)
            {
                // PIN is correct, move to password reset state
                currentState = ResetState.EnterNewPassword;

                // Hide PIN controls
                pinLabel.Visible = false;
                pinTextBox.Visible = false;

                // Show password controls
                newPasswordLabel.Visible = true;
                newPasswordTextBox.Visible = true;
                confirmPasswordLabel.Visible = true;
                confirmPasswordTextBox.Visible = true;

                // Update button text
                actionButton.Text = "Reset Password";

                // Focus on new password field
                newPasswordTextBox.Focus();
            }
            else
            {
                MessageBox.Show("Invalid security PIN. Please try again.", "PIN Verification Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                pinTextBox.Clear();
                pinTextBox.Focus();
            }
        }

        private void ResetPassword()
        {
            string newPassword = newPasswordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;

            // Validate password fields
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please enter a new password.", "Input Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newPasswordTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please confirm your new password.", "Input Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                confirmPasswordTextBox.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                confirmPasswordTextBox.Clear();
                confirmPasswordTextBox.Focus();
                return;
            }

            // All validations passed, update the password in the database
            if (UpdatePasswordInDatabase(newPassword))
            {
                MessageBox.Show("Password has been reset successfully.", "Password Reset Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool UpdatePasswordInDatabase(string newPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE admin SET password = @password WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", newPassword);
                        int result = cmd.ExecuteNonQuery();

                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PasswordReset_Load(object sender, EventArgs e)
        {

        }
    }
}