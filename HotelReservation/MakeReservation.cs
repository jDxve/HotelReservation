using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using ClosedXML.Excel; // For Excel export without Excel installed
using System.Globalization; // For phCulture if used

namespace HotelReservation
{
    public partial class MakeReservation : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=hotel_reservation;Uid=root;Pwd=root;";

        private DataTable allReservationsTable;
        private DataTable allGuestsTable;
        private DataTable allPaymentsTable;
        private DataTable allRoomBookingLogsTable;

        private int currentSelectedReservationIdForPaymentsView = -1;
        private int _selectedGuestIdForEdit = -1;

        // New fields for Reviews Tab
        private DataTable dtReviewGuestSearchResults;
        private DataTable dtReviewGuestPastStays;
        private int selectedReviewGuestId = -1;
        private int selectedReviewRoomId = -1;
        private int selectedReviewReservationId = -1; // To ensure review is for a specific stay

        // For Philippine Peso formatting (if needed)
        private CultureInfo phCulture = new CultureInfo("en-PH");
        // If you don't need specific PHP formatting, you can set phCulture = null;
        // and adjust the currency formatting in ExportToExcel and DGV loading.


        public MakeReservation()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            // Initialize new DataTables
            dtReviewGuestSearchResults = new DataTable();
            dtReviewGuestPastStays = new DataTable();
        }

        private void InitializeDatabaseConnection()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database connection: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void LogRoomBookingEvent(int roomId, int reservationId, string action, MySqlTransaction transaction = null)
        {
            bool manageConnection = (transaction == null);
            try
            {
                if (manageConnection) OpenConnection();
                string query = "INSERT INTO Room_Booking_Logs (room_id, reservation_id, action, timestamp) " +
                               "VALUES (@room_id, @reservation_id, @action, NOW())";
                MySqlCommand cmd;
                if (transaction != null)
                {
                    cmd = new MySqlCommand(query, connection, transaction);
                }
                else
                {
                    cmd = new MySqlCommand(query, connection);
                }
                cmd.Parameters.AddWithValue("@room_id", roomId);
                cmd.Parameters.AddWithValue("@reservation_id", reservationId);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log room booking event: RoomID {roomId}, ResID {reservationId}, Action {action}. Error: {ex.Message}");
            }
            finally
            {
                if (manageConnection) CloseConnection();
            }
        }

        private void MakeReservation_Load(object sender, EventArgs e)
        {
            LoadRoomTypesForNewReservation();
            if (cmbNewReservationRoomType.Items.Count > 0)
            {
                cmbNewReservationRoomType.SelectedIndex = -1; // No initial selection
            }
            else
            {
                LoadAvailableRoomsForNewReservation();
            }

            cmbNewReservationStatus.Items.Clear();
            cmbNewReservationStatus.Items.AddRange(new string[] { "Pending", "Confirmed" });
            if (cmbNewReservationStatus.Items.Count > 0) cmbNewReservationStatus.SelectedIndex = 0;

            cmbNewPaymentMethod.Items.Clear();
            cmbNewPaymentMethod.Items.AddRange(new string[] { "Cash", "Credit Card", "Debit Card", "Online" });
            if (cmbNewPaymentMethod.Items.Count > 0) cmbNewPaymentMethod.SelectedIndex = 0;

            cmbNewPaymentStatus.Items.Clear();
            cmbNewPaymentStatus.Items.AddRange(new string[] { "Pending", "Completed" });
            if (cmbNewPaymentStatus.Items.Count > 0) cmbNewPaymentStatus.SelectedIndex = 1;

            dtpNewReservationCheckIn.MinDate = DateTime.Today;
            dtpNewReservationCheckOut.MinDate = DateTime.Today.AddDays(1);
            dtpNewReservationCheckIn.Value = DateTime.Today;
            dtpNewReservationCheckOut.Value = DateTime.Today.AddDays(1);

            LoadAllReservations();
            btnMarkPaymentCompleted.Enabled = false;
            btnConfirmReservation.Enabled = false;

            LoadAllGuests();
            btnSaveGuestChanges.Enabled = false;
            btnDeleteGuest.Enabled = false;
            ClearGuestEditFields();

            LoadAllPayments();
            InitializeReviewsTab(); // Initialize the new tab
        }

        private void LoadRoomTypesForNewReservation()
        {
            try
            {
                OpenConnection();
                string query = "SELECT type_id, type_name FROM Room_Types ORDER BY type_name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbNewReservationRoomType.DataSource = dt;
                cmbNewReservationRoomType.DisplayMember = "type_name";
                cmbNewReservationRoomType.ValueMember = "type_id";
                if (cmbNewReservationRoomType.Items.Count > 0) cmbNewReservationRoomType.SelectedIndex = -1;
                else if (cmbNewReservationRoom != null) { cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear(); cmbNewReservationRoom.Text = "No Room Types Defined"; }
            }
            catch (Exception ex) { MessageBox.Show("Error loading room types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { CloseConnection(); }
        }

        private void cmbNewReservationRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableRoomsForNewReservation();
        }

        private void dtpNewReservationCheckIn_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNewReservationCheckOut.Value.Date <= dtpNewReservationCheckIn.Value.Date)
            {
                dtpNewReservationCheckOut.Value = dtpNewReservationCheckIn.Value.AddDays(1);
            }
            dtpNewReservationCheckOut.MinDate = dtpNewReservationCheckIn.Value.AddDays(1);
            LoadAvailableRoomsForNewReservation();
        }

        private void dtpNewReservationCheckOut_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNewReservationCheckOut.Value.Date <= dtpNewReservationCheckIn.Value.Date)
            {
                MessageBox.Show("Check-out date must be after Check-in date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Optionally revert or prevent change
            }
            LoadAvailableRoomsForNewReservation();
        }

        private void LoadAvailableRoomsForNewReservation()
        {
            // (Existing robust logic for loading available rooms - keep as is)
            // Ensure cmbNewReservationRoom.Text updates correctly on errors or no availability
            if (cmbNewReservationRoomType == null || dtpNewReservationCheckIn == null || dtpNewReservationCheckOut == null || cmbNewReservationRoom == null)
            {
                if (cmbNewReservationRoom != null) cmbNewReservationRoom.Text = "Control Init Error";
                return;
            }
            if (cmbNewReservationRoomType.SelectedValue == null)
            {
                if (cmbNewReservationRoomType.Items.Count > 0 && cmbNewReservationRoomType.SelectedIndex == -1)
                {
                    cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear(); cmbNewReservationRoom.Text = "Select Room Type";
                }
                else if (cmbNewReservationRoomType.Items.Count == 0)
                {
                    cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear(); cmbNewReservationRoom.Text = "No Room Types";
                }
                return;
            }

            int roomTypeId;
            try
            {
                if (cmbNewReservationRoomType.SelectedValue == null || !(cmbNewReservationRoomType.SelectedValue is int || cmbNewReservationRoomType.SelectedValue is long))
                {
                    cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear();
                    if (cmbNewReservationRoomType.Items.Count == 0) cmbNewReservationRoom.Text = "No Room Types";
                    else cmbNewReservationRoom.Text = "Select Room Type";
                    return;
                }
                roomTypeId = Convert.ToInt32(cmbNewReservationRoomType.SelectedValue);
            }
            catch (Exception)
            {
                cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear(); cmbNewReservationRoom.Text = "Invalid Room Type ID";
                return;
            }

            DateTime checkIn = dtpNewReservationCheckIn.Value.Date;
            DateTime checkOut = dtpNewReservationCheckOut.Value.Date;

            if (checkOut <= checkIn)
            {
                cmbNewReservationRoom.DataSource = null; cmbNewReservationRoom.Items.Clear(); cmbNewReservationRoom.Text = "Invalid Date Range";
                return;
            }

            try
            {
                OpenConnection();
                string query = @"
                    SELECT R.room_id, R.room_number
                    FROM Rooms R
                    WHERE R.room_type = @roomTypeId 
                    AND R.status NOT IN ('Out of Order', 'Maintenance') 
                    AND R.room_id NOT IN (
                        SELECT Res.room_id FROM Reservations Res
                        WHERE Res.room_id IS NOT NULL AND Res.status NOT IN ('Cancelled', 'Checked Out', 'No Show')
                        AND (Res.check_in_date < @checkOutDate AND Res.check_out_date > @checkInDate)
                    ) ORDER BY R.room_number;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@roomTypeId", roomTypeId);
                cmd.Parameters.AddWithValue("@checkInDate", checkIn);
                cmd.Parameters.AddWithValue("@checkOutDate", checkOut);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cmbNewReservationRoom.DataSource = dt;
                    cmbNewReservationRoom.DisplayMember = "room_number";
                    cmbNewReservationRoom.ValueMember = "room_id";
                    cmbNewReservationRoom.SelectedIndex = 0;
                }
                else
                {
                    cmbNewReservationRoom.DataSource = null;
                    cmbNewReservationRoom.Items.Clear();
                    cmbNewReservationRoom.Text = "No Rooms Available";
                    cmbNewReservationRoom.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available rooms: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbNewReservationRoom.DataSource = null;
                cmbNewReservationRoom.Items.Clear();
                cmbNewReservationRoom.Text = "Error Loading Rooms";
            }
            finally
            {
                CloseConnection();
            }
        }

        private bool ValidateNewReservationInputs()
        {
            // (Existing validation logic - keep as is)
            // Add robust parsing for txtNewPaymentAmount if it contains currency symbols
            if (string.IsNullOrWhiteSpace(txtNewGuestFirstName.Text)) { MessageBox.Show("Guest First name is required."); txtNewGuestFirstName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtNewGuestLastName.Text)) { MessageBox.Show("Guest Last name is required."); txtNewGuestLastName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtNewGuestEmail.Text)) { MessageBox.Show("Guest Email is required."); txtNewGuestEmail.Focus(); return false; }
            try { var _ = new System.Net.Mail.MailAddress(txtNewGuestEmail.Text); }
            catch { MessageBox.Show("Invalid Guest Email format."); txtNewGuestEmail.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtNewGuestPhone.Text)) { MessageBox.Show("Guest Phone number is required."); txtNewGuestPhone.Focus(); return false; }

            if (cmbNewReservationRoomType.SelectedValue == null) { MessageBox.Show("Please select a room type."); cmbNewReservationRoomType.Focus(); return false; }
            if (cmbNewReservationRoom.SelectedValue == null || cmbNewReservationRoom.Text == "No Rooms Available" || cmbNewReservationRoom.Text == "Invalid Date Range" || cmbNewReservationRoom.Text == "Select Room Type" || cmbNewReservationRoom.Text == "Invalid Room Type Selection" || cmbNewReservationRoom.Text == "No Room Types Defined" || cmbNewReservationRoom.Text == "Invalid Room Type ID" || cmbNewReservationRoom.Text == "Error Loading Rooms" || cmbNewReservationRoom.Text == "Select Room Type First")
            { MessageBox.Show("No available room selected or invalid configuration. Please check dates or room type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbNewReservationRoom.Focus(); return false; }
            if (cmbNewReservationStatus.SelectedItem == null) { MessageBox.Show("Please select a reservation status."); cmbNewReservationStatus.Focus(); return false; }

            decimal paymentAmountValue;
            if (string.IsNullOrWhiteSpace(txtNewPaymentAmount.Text) ||
                !decimal.TryParse(txtNewPaymentAmount.Text, NumberStyles.Any, phCulture ?? CultureInfo.CurrentCulture, out paymentAmountValue) ||
                paymentAmountValue <= 0)
            {
                MessageBox.Show("Valid positive payment amount is required.");
                txtNewPaymentAmount.Focus(); return false;
            }

            if (cmbNewPaymentMethod.SelectedItem == null) { MessageBox.Show("Please select a payment method."); cmbNewPaymentMethod.Focus(); return false; }
            if (cmbNewPaymentStatus.SelectedItem == null) { MessageBox.Show("Please select a payment status."); cmbNewPaymentStatus.Focus(); return false; }
            return true;
        }

        private void btnCreateReservationAndPayment_Click(object sender, EventArgs e)
        {
            // (Existing logic, ensure decimal.Parse(txtNewPaymentAmount.Text) uses robust parsing if needed)
            if (!ValidateNewReservationInputs()) return;

            decimal paymentAmountValue = decimal.Parse(txtNewPaymentAmount.Text, NumberStyles.Any, phCulture ?? CultureInfo.CurrentCulture);


            MySqlTransaction sqlTransaction = null;
            int guestId = -1;
            long reservationId = -1;

            try
            {
                OpenConnection();
                sqlTransaction = connection.BeginTransaction();

                string guestQuery;
                MySqlCommand guestCmd;

                string checkEmailQuery = "SELECT guest_id FROM Guests WHERE email = @p_email";
                MySqlCommand checkEmailCmd = new MySqlCommand(checkEmailQuery, connection, sqlTransaction);
                checkEmailCmd.Parameters.AddWithValue("@p_email", txtNewGuestEmail.Text.Trim());
                object existingGuestIdObj = checkEmailCmd.ExecuteScalar();

                if (existingGuestIdObj != null && existingGuestIdObj != DBNull.Value)
                {
                    DialogResult useExisting = MessageBox.Show($"A guest with email '{txtNewGuestEmail.Text.Trim()}' already exists (ID: {existingGuestIdObj}). Do you want to proceed with this existing guest for the new reservation?",
                                                               "Existing Guest Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (useExisting == DialogResult.Yes)
                    {
                        guestId = Convert.ToInt32(existingGuestIdObj);
                    }
                    else
                    {
                        if (sqlTransaction != null) sqlTransaction.Rollback();
                        MessageBox.Show("Reservation cancelled. Please use a different email or manage the existing guest.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    guestQuery = "INSERT INTO Guests (first_name, last_name, email, phone, address, created_at) " +
                                 "VALUES (@p_first_name, @p_last_name, @p_email, @p_phone, @p_address, NOW()); SELECT LAST_INSERT_ID();";
                    guestCmd = new MySqlCommand(guestQuery, connection, sqlTransaction);
                    guestCmd.Parameters.AddWithValue("@p_first_name", txtNewGuestFirstName.Text.Trim());
                    guestCmd.Parameters.AddWithValue("@p_last_name", txtNewGuestLastName.Text.Trim());
                    guestCmd.Parameters.AddWithValue("@p_email", txtNewGuestEmail.Text.Trim());
                    guestCmd.Parameters.AddWithValue("@p_phone", txtNewGuestPhone.Text.Trim());
                    guestCmd.Parameters.AddWithValue("@p_address", txtNewGuestAddress.Text.Trim());

                    object guestIdResult = guestCmd.ExecuteScalar();
                    if (guestIdResult == null || guestIdResult == DBNull.Value) throw new Exception("Failed to retrieve new Guest ID after insert.");
                    guestId = Convert.ToInt32(guestIdResult);
                }

                string reservationQuery = "INSERT INTO Reservations (guest_id, room_id, check_in_date, check_out_date, status, created_at) " +
                                          "VALUES (@guest_id, @room_id, @check_in_date, @check_out_date, @status, NOW()); SELECT LAST_INSERT_ID();";
                MySqlCommand reservationCmd = new MySqlCommand(reservationQuery, connection, sqlTransaction);
                reservationCmd.Parameters.AddWithValue("@guest_id", guestId);
                reservationCmd.Parameters.AddWithValue("@room_id", (int)cmbNewReservationRoom.SelectedValue);
                reservationCmd.Parameters.AddWithValue("@check_in_date", dtpNewReservationCheckIn.Value.Date);
                reservationCmd.Parameters.AddWithValue("@check_out_date", dtpNewReservationCheckOut.Value.Date);
                string reservationStatus = cmbNewReservationStatus.SelectedItem.ToString();
                reservationCmd.Parameters.AddWithValue("@status", reservationStatus);
                object reservationIdResult = reservationCmd.ExecuteScalar();
                if (reservationIdResult == null || reservationIdResult == DBNull.Value) throw new Exception("Failed to create reservation or retrieve reservation ID.");
                reservationId = Convert.ToInt64(reservationIdResult);

                LogRoomBookingEvent((int)cmbNewReservationRoom.SelectedValue, (int)reservationId, reservationStatus, sqlTransaction);

                string paymentQuery = "INSERT INTO Payments (reservation_id, amount, payment_date, payment_method, status) " +
                                      "VALUES (@reservation_id, @amount, @payment_date, @payment_method, @status);";
                MySqlCommand paymentCmd = new MySqlCommand(paymentQuery, connection, sqlTransaction);
                paymentCmd.Parameters.AddWithValue("@reservation_id", reservationId);
                paymentCmd.Parameters.AddWithValue("@amount", paymentAmountValue); // Use parsed value
                paymentCmd.Parameters.AddWithValue("@payment_date", DateTime.Now);
                paymentCmd.Parameters.AddWithValue("@payment_method", cmbNewPaymentMethod.SelectedItem.ToString());
                paymentCmd.Parameters.AddWithValue("@status", cmbNewPaymentStatus.SelectedItem.ToString());
                paymentCmd.ExecuteNonQuery();

                sqlTransaction.Commit();
                MessageBox.Show("Reservation, Guest, and Payment created successfully! Booking logged.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearNewReservationFields();
                LoadAllGuests();
                LoadAllReservations();
                LoadAllPayments();
                if (tabControlMain.SelectedTab == tabPageRoomBookingLogs) LoadRoomBookingLogs();
            }
            catch (MySqlException mex)
            {
                if (sqlTransaction != null) sqlTransaction.Rollback();
                string errorMsg = "Database Error: " + mex.Message;
                if (mex.Number == 1062 && mex.Message.ToLower().Contains("guests.email"))
                    errorMsg = "A guest with this email already exists. Please use a different email.";
                else if (mex.Message.Contains("Check-out date must be after check-in date"))
                    errorMsg = "Database Error (from trigger): Check-out date must be after check-in date.";

                MessageBox.Show(errorMsg, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException fex)
            {
                if (sqlTransaction != null) sqlTransaction.Rollback();
                MessageBox.Show("Invalid payment amount format: " + fex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null) sqlTransaction.Rollback();
                MessageBox.Show("An error occurred: " + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { CloseConnection(); }
        }


        private void btnClearNewReservationFields_Click(object sender, EventArgs e)
        {
            ClearNewReservationFields();
        }

        private void ClearNewReservationFields()
        {
            // (Existing logic - keep as is)
            txtNewGuestFirstName.Clear();
            txtNewGuestLastName.Clear();
            txtNewGuestEmail.Clear();
            txtNewGuestPhone.Clear();
            txtNewGuestAddress.Clear();

            if (cmbNewReservationRoomType.Items.Count > 0)
            {
                cmbNewReservationRoomType.SelectedIndex = -1; // Or 0 for first item default
            }
            else
            {
                cmbNewReservationRoomType.DataSource = null;
                LoadAvailableRoomsForNewReservation();
            }

            dtpNewReservationCheckIn.Value = DateTime.Today;
            dtpNewReservationCheckOut.Value = DateTime.Today.AddDays(1);
            if (cmbNewReservationStatus.Items.Count > 0) cmbNewReservationStatus.SelectedIndex = 0;
            txtNewPaymentAmount.Clear();
            if (cmbNewPaymentMethod.Items.Count > 0) cmbNewPaymentMethod.SelectedIndex = 0;
            if (cmbNewPaymentStatus.Items.Count > 0) cmbNewPaymentStatus.SelectedIndex = 1;
        }

        private void LoadAllReservations()
        {
            // (Existing logic - keep as is)
            try
            {
                OpenConnection();
                string query = @"
                    SELECT R.reservation_id AS ID, CONCAT(G.first_name, ' ', G.last_name) AS Guest,
                           RM.room_number AS 'Room#', RT.type_name AS 'Room Type',
                           R.check_in_date AS 'Check-in', R.check_out_date AS 'Check-out',
                           R.status AS Status, R.created_at AS 'Booked On'
                    FROM Reservations R
                    JOIN Guests G ON R.guest_id = G.guest_id
                    JOIN Rooms RM ON R.room_id = RM.room_id
                    JOIN Room_Types RT ON RM.room_type = RT.type_id
                    ORDER BY R.created_at DESC;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                allReservationsTable = new DataTable();
                adapter.Fill(allReservationsTable);
                dgvReservations.DataSource = allReservationsTable;

                dgvReservations.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dgvReservations.Columns.Contains("Check-in")) dgvReservations.Columns["Check-in"].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (dgvReservations.Columns.Contains("Check-out")) dgvReservations.Columns["Check-out"].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (dgvReservations.Columns.Contains("Booked On")) dgvReservations.Columns["Booked On"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

                dgvReservationPayments.DataSource = null;
                btnMarkPaymentCompleted.Enabled = false;
                btnConfirmReservation.Enabled = false;
                txtSearchReservations.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reservations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { CloseConnection(); }
        }

        private void dgvReservations_SelectionChanged(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            currentSelectedReservationIdForPaymentsView = -1;
            btnMarkPaymentCompleted.Enabled = false;
            btnConfirmReservation.Enabled = false;

            if (dgvReservations.SelectedRows.Count > 0)
            {
                DataRowView drv = dgvReservations.SelectedRows[0].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("ID") && drv["ID"] != DBNull.Value)
                {
                    currentSelectedReservationIdForPaymentsView = Convert.ToInt32(drv["ID"]);
                    LoadPaymentsForSelectedReservation(currentSelectedReservationIdForPaymentsView);

                    if (drv.Row.Table.Columns.Contains("Status"))
                    {
                        string status = drv["Status"].ToString();
                        btnConfirmReservation.Enabled = status.Equals("Pending", StringComparison.OrdinalIgnoreCase);
                    }
                }
                else
                {
                    dgvReservationPayments.DataSource = null;
                }
            }
            else
            {
                dgvReservationPayments.DataSource = null;
            }
        }

        private void dgvReservationPayments_SelectionChanged(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (dgvReservationPayments.SelectedRows.Count > 0)
            {
                DataRowView drv = dgvReservationPayments.SelectedRows[0].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("Status") &&
                    drv["Status"].ToString().Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    btnMarkPaymentCompleted.Enabled = true;
                }
                else
                {
                    btnMarkPaymentCompleted.Enabled = false;
                }
            }
            else
            {
                btnMarkPaymentCompleted.Enabled = false;
            }
        }

        private void btnConfirmReservation_Click(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (dgvReservations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a reservation to confirm.", "No Reservation Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvReservations.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null || !drv.Row.Table.Columns.Contains("ID") || !drv.Row.Table.Columns.Contains("Status"))
            {
                MessageBox.Show("Invalid reservation data selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int reservationId = Convert.ToInt32(drv["ID"]);
            string currentStatus = drv["Status"].ToString();
            int roomId = -1;


            if (!currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only 'Pending' reservations can be confirmed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to confirm reservation ID {reservationId}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlTransaction transaction = null;
                try
                {
                    OpenConnection();
                    transaction = connection.BeginTransaction();

                    string roomQuery = "SELECT room_id FROM Reservations WHERE reservation_id = @reservation_id";
                    MySqlCommand roomCmd = new MySqlCommand(roomQuery, connection, transaction);
                    roomCmd.Parameters.AddWithValue("@reservation_id", reservationId);
                    object roomIdObj = roomCmd.ExecuteScalar();
                    if (roomIdObj != null && roomIdObj != DBNull.Value)
                    {
                        roomId = Convert.ToInt32(roomIdObj);
                    }
                    else
                    {
                        throw new Exception("Could not retrieve room_id for the selected reservation.");
                    }


                    string updateQuery = "UPDATE Reservations SET status = 'Confirmed' WHERE reservation_id = @reservation_id";
                    MySqlCommand cmd = new MySqlCommand(updateQuery, connection, transaction);
                    cmd.Parameters.AddWithValue("@reservation_id", reservationId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        if (roomId != -1)
                        {
                            LogRoomBookingEvent(roomId, reservationId, "Confirmed", transaction);
                        }

                        transaction.Commit();

                        MessageBox.Show($"Reservation ID {reservationId} has been confirmed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadAllReservations();
                        if (tabControlMain.SelectedTab == tabPageRoomBookingLogs)
                        {
                            LoadRoomBookingLogs();
                        }
                    }
                    else
                    {
                        if (transaction != null) transaction.Rollback();
                        MessageBox.Show("Could not confirm the reservation. It might have been modified or deleted.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    MessageBox.Show("Error confirming reservation: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        private void LoadPaymentsForSelectedReservation(int reservationId)
        {
            // (Existing logic - ensure DGV "Amount" column formatting uses phCulture if set)
            try
            {
                OpenConnection();
                string query = "SELECT payment_id AS 'Payment ID', amount AS Amount, payment_date AS Date, " +
                               "payment_method AS Method, status AS Status " +
                               "FROM Payments WHERE reservation_id = @reservation_id ORDER BY payment_date DESC;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@reservation_id", reservationId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvReservationPayments.DataSource = dt;

                dgvReservationPayments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dgvReservationPayments.Columns.Contains("Date")) dgvReservationPayments.Columns["Date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                if (dgvReservationPayments.Columns.Contains("Amount"))
                {
                    dgvReservationPayments.Columns["Amount"].DefaultCellStyle.FormatProvider = phCulture ?? CultureInfo.CurrentCulture;
                    dgvReservationPayments.Columns["Amount"].DefaultCellStyle.Format = "C2";
                }

                dgvReservationPayments_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payments for reservation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { CloseConnection(); }
        }

        private void txtSearchReservations_TextChanged(object sender, EventArgs e)
        {
            FilterDataTable(allReservationsTable, txtSearchReservations.Text, new[] { "Guest", "Room#", "Room Type", "Status" });
        }

        private void btnMarkPaymentCompleted_Click(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (dgvReservationPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to mark as completed.", "No Payment Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvReservationPayments.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null || !drv.Row.Table.Columns.Contains("Payment ID") || !drv.Row.Table.Columns.Contains("Status"))
            {
                MessageBox.Show("Invalid payment data selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int paymentId = Convert.ToInt32(drv["Payment ID"]);
            string currentStatus = drv["Status"].ToString();

            if (!currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only 'Pending' payments can be marked as completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Mark payment ID {paymentId} as 'Completed'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    OpenConnection();
                    string query = "UPDATE Payments SET status = 'Completed', payment_date = NOW() WHERE payment_id = @payment_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@payment_id", paymentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Payment status updated to 'Completed'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPaymentsForSelectedReservation(currentSelectedReservationIdForPaymentsView);
                        LoadAllPayments();
                    }
                    else
                    {
                        MessageBox.Show("Could not update payment status.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating payment status: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        private void LoadAllGuests()
        {
            // (Existing logic - keep as is)
            try
            {
                OpenConnection();
                string query = "SELECT guest_id AS ID, first_name AS 'First Name', last_name AS 'Last Name', " +
                               "email AS Email, phone AS Phone, address AS Address, created_at AS 'Joined Date' " +
                               "FROM Guests ORDER BY last_name, first_name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                allGuestsTable = new DataTable();
                adapter.Fill(allGuestsTable);
                dgvAllGuests.DataSource = allGuestsTable;
                dgvAllGuests.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dgvAllGuests.Columns.Contains("Joined Date")) dgvAllGuests.Columns["Joined Date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

                txtSearchGuests.Clear();
                ClearGuestEditFields();
                _selectedGuestIdForEdit = -1;
                btnSaveGuestChanges.Enabled = false;
                btnDeleteGuest.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { CloseConnection(); }
        }

        private void dgvAllGuests_SelectionChanged(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (dgvAllGuests.SelectedRows.Count > 0)
            {
                DataRowView drv = dgvAllGuests.SelectedRows[0].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("ID"))
                {
                    _selectedGuestIdForEdit = Convert.ToInt32(drv["ID"]);
                    txtEditGuestFirstName.Text = drv["First Name"].ToString();
                    txtEditGuestLastName.Text = drv["Last Name"].ToString();
                    txtEditGuestEmail.Text = drv["Email"].ToString();
                    txtEditGuestPhone.Text = drv["Phone"].ToString();
                    txtEditGuestAddress.Text = drv["Address"].ToString();

                    btnSaveGuestChanges.Enabled = true;
                    btnDeleteGuest.Enabled = true;
                }
                else
                {
                    ClearGuestEditFields();
                }
            }
            else
            {
                ClearGuestEditFields();
            }
        }

        private void ClearGuestEditFields()
        {
            // (Existing logic - keep as is)
            txtEditGuestFirstName.Clear();
            txtEditGuestLastName.Clear();
            txtEditGuestEmail.Clear();
            txtEditGuestPhone.Clear();
            txtEditGuestAddress.Clear();
            _selectedGuestIdForEdit = -1;
            btnSaveGuestChanges.Enabled = false;
            btnDeleteGuest.Enabled = false;
        }

        private bool ValidateGuestEditInputs()
        {
            // (Existing logic - keep as is)
            if (string.IsNullOrWhiteSpace(txtEditGuestFirstName.Text)) { MessageBox.Show("First name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEditGuestFirstName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEditGuestLastName.Text)) { MessageBox.Show("Last name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEditGuestLastName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEditGuestEmail.Text)) { MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEditGuestEmail.Focus(); return false; }
            try { var _ = new System.Net.Mail.MailAddress(txtEditGuestEmail.Text); }
            catch { MessageBox.Show("Invalid Email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEditGuestEmail.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEditGuestPhone.Text)) { MessageBox.Show("Phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEditGuestPhone.Focus(); return false; }
            return true;
        }

        private void btnSaveGuestChanges_Click(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (_selectedGuestIdForEdit == -1)
            {
                MessageBox.Show("Please select a guest to update.", "No Guest Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateGuestEditInputs()) return;

            try
            {
                OpenConnection();
                string emailCheckQuery = "SELECT COUNT(*) FROM Guests WHERE email = @email AND guest_id != @guest_id";
                MySqlCommand emailCheckCmd = new MySqlCommand(emailCheckQuery, connection);
                emailCheckCmd.Parameters.AddWithValue("@email", txtEditGuestEmail.Text.Trim());
                emailCheckCmd.Parameters.AddWithValue("@guest_id", _selectedGuestIdForEdit);
                long emailExistsCount = (long)emailCheckCmd.ExecuteScalar();

                if (emailExistsCount > 0)
                {
                    MessageBox.Show("This email address is already in use by another guest. Please choose a different email.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "UPDATE Guests SET first_name = @first_name, last_name = @last_name, email = @email, " +
                               "phone = @phone, address = @address WHERE guest_id = @guest_id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@first_name", txtEditGuestFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@last_name", txtEditGuestLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEditGuestEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtEditGuestPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtEditGuestAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@guest_id", _selectedGuestIdForEdit);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Guest information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllGuests();
                    LoadAllReservations();
                    LoadAllPayments();
                }
                else
                {
                    MessageBox.Show("Could not update guest information. The data might be unchanged or the guest not found.", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (MySqlException mex)
            {
                if (mex.Number == 1062 && mex.Message.ToLower().Contains("email"))
                    MessageBox.Show("Error: This email address is already in use. " + mex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Database Error updating guest: " + mex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating guest: " + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void btnDeleteGuest_Click(object sender, EventArgs e)
        {
            // (Existing logic - keep as is)
            if (_selectedGuestIdForEdit == -1)
            {
                MessageBox.Show("Please select a guest to delete.", "No Guest Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string guestNameToLog = $"{txtEditGuestFirstName.Text} {txtEditGuestLastName.Text}";

            try
            {
                OpenConnection();
                string checkReservationsQuery = "SELECT COUNT(*) FROM Reservations WHERE guest_id = @guest_id AND status NOT IN ('Cancelled', 'Checked Out', 'No Show')";
                MySqlCommand checkCmd = new MySqlCommand(checkReservationsQuery, connection);
                checkCmd.Parameters.AddWithValue("@guest_id", _selectedGuestIdForEdit);
                long activeReservationCount = (long)checkCmd.ExecuteScalar();

                if (activeReservationCount > 0)
                {
                    MessageBox.Show($"This guest has {activeReservationCount} active reservation(s) and cannot be deleted. Please cancel or complete their reservations first.", "Cannot Delete Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for guest reservations: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) CloseConnection();
            }


            if (MessageBox.Show($"Are you sure you want to delete guest '{guestNameToLog}' (ID: {_selectedGuestIdForEdit})? This action cannot be undone and might affect historical reservation data if not handled by database constraints (e.g. SET NULL).", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlTransaction transaction = null;
                try
                {
                    OpenConnection();
                    transaction = connection.BeginTransaction();

                    string deleteGuestQuery = "DELETE FROM Guests WHERE guest_id = @guest_id";
                    MySqlCommand cmd = new MySqlCommand(deleteGuestQuery, connection, transaction);
                    cmd.Parameters.AddWithValue("@guest_id", _selectedGuestIdForEdit);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        MessageBox.Show($"Guest '{guestNameToLog}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllGuests();
                        LoadAllReservations();
                        LoadAllPayments();
                        ClearGuestEditFields();
                    }
                    else
                    {
                        if (transaction != null) transaction.Rollback();
                        MessageBox.Show("Could not delete guest. Guest may have already been deleted.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (MySqlException mex)
                {
                    if (transaction != null) transaction.Rollback();
                    if (mex.Number == 1451)
                    {
                        MessageBox.Show($"Cannot delete guest '{guestNameToLog}'. They are still referenced in other records (e.g., reservations). Please ensure all related records are handled first. Error: {mex.Message}", "Delete Error (Reference Constraint)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error deleting guest '{guestNameToLog}': {mex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    MessageBox.Show($"Error deleting guest '{guestNameToLog}': {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        private void txtSearchGuests_TextChanged(object sender, EventArgs e)
        {
            FilterDataTable(allGuestsTable, txtSearchGuests.Text, new[] { "First Name", "Last Name", "Email", "Phone", "Address" });
        }

        private void btnExportAllGuests_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvAllGuests, "AllGuestsList");
        }

        private void LoadAllPayments()
        {
            // (Existing logic - ensure DGV "Amount" column formatting uses phCulture if set)
            try
            {
                OpenConnection();
                string query = @"
                    SELECT P.payment_id AS 'Payment ID', R.reservation_id AS 'Reservation ID',
                           CONCAT(G.first_name, ' ', G.last_name) AS 'Guest Name',
                           P.amount AS Amount, P.payment_date AS 'Payment Date',
                           P.payment_method AS Method, P.status AS Status
                    FROM Payments P
                    JOIN Reservations R ON P.reservation_id = R.reservation_id
                    JOIN Guests G ON R.guest_id = G.guest_id
                    ORDER BY P.payment_date DESC;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                allPaymentsTable = new DataTable();
                adapter.Fill(allPaymentsTable);
                dgvAllPayments.DataSource = allPaymentsTable;
                dgvAllPayments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dgvAllPayments.Columns.Contains("Payment Date")) dgvAllPayments.Columns["Payment Date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                if (dgvAllPayments.Columns.Contains("Amount"))
                {
                    dgvAllPayments.Columns["Amount"].DefaultCellStyle.FormatProvider = phCulture ?? CultureInfo.CurrentCulture;
                    dgvAllPayments.Columns["Amount"].DefaultCellStyle.Format = "C2";
                }
                txtSearchPayments.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading all payments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { CloseConnection(); }
        }

        private void txtSearchPayments_TextChanged(object sender, EventArgs e)
        {
            FilterDataTable(allPaymentsTable, txtSearchPayments.Text, new[] { "Guest Name", "Reservation ID", "Method", "Status", "Payment ID" });
        }

        private void btnExportAllPayments_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvAllPayments, "AllPaymentsList");
        }

        private void LoadRoomBookingLogs()
        {
            // (Existing logic - keep as is)
            try
            {
                OpenConnection();
                string query = @"
                    SELECT
                        RBL.log_id AS 'Log ID',
                        RBL.timestamp AS 'Timestamp',
                        RM.room_number AS 'Room Number',
                        RBL.reservation_id AS 'Reservation ID', 
                        CONCAT(G.first_name, ' ', G.last_name) AS 'Guest Name',
                        RBL.action AS 'Action'
                    FROM Room_Booking_Logs RBL
                    JOIN Rooms RM ON RBL.room_id = RM.room_id
                    JOIN Reservations RES ON RBL.reservation_id = RES.reservation_id
                    JOIN Guests G ON RES.guest_id = G.guest_id
                    ORDER BY RBL.timestamp DESC LIMIT 1000;"; // Added LIMIT for performance

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                allRoomBookingLogsTable = new DataTable();
                adapter.Fill(allRoomBookingLogsTable);
                dgvRoomBookingLogs.DataSource = allRoomBookingLogsTable;

                dgvRoomBookingLogs.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                if (dgvRoomBookingLogs.Columns.Contains("Timestamp"))
                {
                    dgvRoomBookingLogs.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    dgvRoomBookingLogs.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvRoomBookingLogs.Columns.Contains("Guest Name"))
                {
                    dgvRoomBookingLogs.Columns["Guest Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvRoomBookingLogs.Columns.Contains("Action"))
                {
                    dgvRoomBookingLogs.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room booking logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading room booking logs: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        private void txtSearchRoomBookingLogs_TextChanged(object sender, EventArgs e)
        {
            FilterDataTable(allRoomBookingLogsTable, txtSearchRoomBookingLogs.Text,
                new[] { "Room Number", "Reservation ID", "Guest Name", "Action" });
        }

        private void btnRefreshRoomBookingLogs_Click(object sender, EventArgs e)
        {
            LoadRoomBookingLogs();
            if (txtSearchRoomBookingLogs != null) txtSearchRoomBookingLogs.Clear();
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageViewReservations) LoadAllReservations();
            else if (tabControlMain.SelectedTab == tabPageViewGuests) LoadAllGuests();
            else if (tabControlMain.SelectedTab == tabPageViewPayments) LoadAllPayments(); // Use tabPageViewPayments
            else if (tabControlMain.SelectedTab == tabPageRoomBookingLogs) { LoadRoomBookingLogs(); if (txtSearchRoomBookingLogs != null) txtSearchRoomBookingLogs.Clear(); }
            else if (tabControlMain.SelectedTab == tabPageReviews) InitializeReviewsTab();
        }

        private void FilterDataTable(DataTable sourceTable, string filterText, string[] columnNamesToFilter)
        {
            // (Existing logic - keep as is)
            if (sourceTable == null)
            {
                Console.WriteLine("FilterDataTable Error: sourceTable is null.");
                return;
            }
            DataView dv = sourceTable.DefaultView;
            if (string.IsNullOrWhiteSpace(filterText))
            {
                dv.RowFilter = string.Empty;
            }
            else
            {
                string filterExpression = "";
                string safeFilterText = filterText.Replace("'", "''");

                for (int i = 0; i < columnNamesToFilter.Length; i++)
                {
                    string dataTableColumnName = columnNamesToFilter[i];

                    if (!sourceTable.Columns.Contains(dataTableColumnName))
                    {
                        Console.WriteLine($"FilterDataTable Warning: Column '{dataTableColumnName}' not found in the DataTable. Skipping this column for filter.");
                        continue;
                    }

                    string rowFilterColumnName = dataTableColumnName;
                    if (dataTableColumnName.Contains(" ") || dataTableColumnName.Contains("#") || dataTableColumnName.Contains("/") || dataTableColumnName.Contains("-") || dataTableColumnName.Contains("."))
                    {
                        rowFilterColumnName = $"[{dataTableColumnName}]";
                    }

                    string filterPart;
                    Type columnType = sourceTable.Columns[dataTableColumnName].DataType;

                    if (columnType == typeof(string))
                    {
                        filterPart = $"{rowFilterColumnName} LIKE '%{safeFilterText}%'";
                    }
                    else
                    {
                        filterPart = $"Convert({rowFilterColumnName}, 'System.String') LIKE '%{safeFilterText}%'";
                    }

                    if (filterExpression.Length > 0)
                    {
                        filterExpression += " OR ";
                    }
                    filterExpression += filterPart;
                }

                if (string.IsNullOrEmpty(filterExpression))
                {
                    dv.RowFilter = string.Empty;
                    return;
                }

                try
                {
                    dv.RowFilter = filterExpression;
                }
                catch (EvaluateException evalEx)
                {
                    Console.WriteLine($"RowFilter EvaluateException: {evalEx.Message} on expression: {filterExpression}");
                    dv.RowFilter = string.Empty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FilterDataTable General Exception: {ex.Message} on expression: {filterExpression}");
                    dv.RowFilter = string.Empty;
                }
            }
        }

        private void MakeReservation_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
        }

        private void ExportToExcel(DataGridView dgv, string fileNameWithoutExtension)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string targetDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "HotelReservationExports");
            try
            {
                if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating/accessing directory {targetDirectory}: {ex.Message}\nExporting to My Documents instead.", "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                targetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx", // Unique filename
                InitialDirectory = targetDirectory
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        string sheetName = fileNameWithoutExtension.Length > 31 ? fileNameWithoutExtension.Substring(0, 31) : fileNameWithoutExtension;
                        var worksheet = workbook.Worksheets.Add(sheetName);

                        int currentColumn = 1;
                        foreach (DataGridViewColumn column in dgv.Columns)
                        {
                            if (column.Visible)
                            {
                                worksheet.Cell(1, currentColumn).Value = column.HeaderText; // SetValue not needed for simple string header
                                currentColumn++;
                            }
                        }
                        worksheet.Row(1).Style.Font.Bold = true;

                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            currentColumn = 1;
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                if (dgv.Columns[j].Visible)
                                {
                                    object cellValueFromDGV = dgv.Rows[i].Cells[j].Value; // Renamed for clarity
                                    var cell = worksheet.Cell(i + 2, currentColumn);

                                    if (cellValueFromDGV == DBNull.Value || cellValueFromDGV == null)
                                    {
                                        cell.Value = string.Empty; // FIX for CS0308: Use .Value or SetValue without type args
                                    }
                                    else
                                    {
                                        // FIX for CS1503:
                                        // ClosedXML's .Value property or .SetValue(object) overload is generally smart enough.
                                        // If a specific type conversion is absolutely necessary, do it before calling SetValue.
                                        // However, for most common DGV cell types, direct assignment to .Value is fine.

                                        if (cellValueFromDGV is DateTime dtValue) // Use type pattern (C# 7.0+) for clarity
                                        {
                                            cell.SetValue(dtValue); // Pass the strongly-typed DateTime
                                            string formatString = dgv.Columns[j].DefaultCellStyle.Format;
                                            if (string.IsNullOrWhiteSpace(formatString))
                                            {
                                                formatString = "yyyy-MM-dd HH:mm:ss";
                                            }
                                            cell.Style.NumberFormat.Format = formatString;
                                        }
                                        else if (cellValueFromDGV is decimal decValue)
                                        {
                                            cell.SetValue(decValue); // Pass the strongly-typed decimal
                                            if (dgv.Columns[j].Name == "Amount")
                                            {
                                                if (phCulture != null)
                                                {
                                                    cell.Style.NumberFormat.Format = "₱#,##0.00";
                                                }
                                                else
                                                {
                                                    cell.Style.NumberFormat.Format = "#,##0.00";
                                                }
                                            }
                                        }
                                        else if (cellValueFromDGV is double dblValue)
                                        {
                                            cell.SetValue(dblValue);
                                            if (dgv.Columns[j].Name == "Amount") // Assuming Amount could also be double
                                            {
                                                if (phCulture != null) { cell.Style.NumberFormat.Format = "₱#,##0.00"; }
                                                else { cell.Style.NumberFormat.Format = "#,##0.00"; }
                                            }
                                        }
                                        else if (cellValueFromDGV is float fltValue)
                                        {
                                            cell.SetValue(fltValue);
                                            if (dgv.Columns[j].Name == "Amount") // Assuming Amount could also be float
                                            {
                                                if (phCulture != null) { cell.Style.NumberFormat.Format = "₱#,##0.00"; }
                                                else { cell.Style.NumberFormat.Format = "#,##0.00"; }
                                            }
                                        }
                                        else if (cellValueFromDGV is bool bValue)
                                        {
                                            cell.SetValue(bValue);
                                        }
                                        else // Fallback for other types, treat as string
                                        {
                                            cell.SetValue(cellValueFromDGV.ToString());
                                        }
                                    }
                                    currentColumn++;
                                }
                            }
                        }
                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Data exported successfully to " + sfd.FileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"Error saving the file '{sfd.FileName}'. It might be open in another program. Please close it and try again.\nDetails: {ioEx.Message}", "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during export: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtNewGuestFirstName_TextChanged(object sender, EventArgs e)
        {
            // Intentionally empty if no specific logic is needed on text change
        }

        // --- NEW METHODS FOR REVIEWS TAB ---
        private void InitializeReviewsTab()
        {
            dgvReviewGuestSearchResults.DataSource = null;
            dgvReviewGuestPastStays.DataSource = null;
            lblSelectedGuestNameForReview.Text = "(None)";
            lblSelectedRoomForReview.Text = "(None)";
            if (numReviewRating.IsHandleCreated) numReviewRating.Value = 5; // Default rating, check if handle created
            txtReviewComment.Clear();
            btnSubmitReview.Enabled = false;
            selectedReviewGuestId = -1;
            selectedReviewRoomId = -1;
            selectedReviewReservationId = -1;
        }

        private void btnSearchReviewGuest_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchReviewGuest.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a guest name or email to search.", "Search Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtReviewGuestSearchResults.Clear();
                dgvReviewGuestSearchResults.DataSource = dtReviewGuestSearchResults;
                return;
            }

            try
            {
                OpenConnection();
                string query = "SELECT guest_id AS ID, first_name AS 'First Name', last_name AS 'Last Name', email AS Email " +
                               "FROM Guests " +
                               "WHERE first_name LIKE @searchText OR last_name LIKE @searchText OR email LIKE @searchText " +
                               "ORDER BY last_name, first_name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                dtReviewGuestSearchResults.Clear();
                adapter.Fill(dtReviewGuestSearchResults);
                dgvReviewGuestSearchResults.DataSource = dtReviewGuestSearchResults;
                dgvReviewGuestSearchResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dtReviewGuestSearchResults.Rows.Count == 0)
                {
                    MessageBox.Show("No guests found matching your search.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching for guests: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void dgvReviewGuestSearchResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReviewGuestSearchResults.SelectedRows.Count > 0)
            {
                DataRowView drv = dgvReviewGuestSearchResults.SelectedRows[0].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("ID") && drv["ID"] != DBNull.Value)
                {
                    selectedReviewGuestId = Convert.ToInt32(drv["ID"]);
                    string guestName = $"{drv["First Name"]} {drv["Last Name"]}";
                    lblSelectedGuestNameForReview.Text = guestName;
                    LoadGuestPastStaysForReview(selectedReviewGuestId);
                }
            }
            else
            {
                selectedReviewGuestId = -1;
                lblSelectedGuestNameForReview.Text = "(None)";
                dtReviewGuestPastStays.Clear();
                dgvReviewGuestPastStays.DataSource = dtReviewGuestPastStays;
                lblSelectedRoomForReview.Text = "(None)";
                btnSubmitReview.Enabled = false;
            }
        }

        private void LoadGuestPastStaysForReview(int guestId)
        {
            dtReviewGuestPastStays.Clear();
            if (guestId == -1)
            {
                dgvReviewGuestPastStays.DataSource = dtReviewGuestPastStays;
                return;
            }

            try
            {
                OpenConnection();
                string query = @"
                    SELECT 
                        R.reservation_id AS ReservationID, 
                        RM.room_id AS RoomID, 
                        RM.room_number AS 'Room Number', 
                        RT.type_name AS 'Room Type',
                        R.check_out_date AS 'Checked Out Date'
                    FROM Reservations R
                    JOIN Rooms RM ON R.room_id = RM.room_id
                    JOIN Room_Types RT ON RM.room_type = RT.type_id
                    WHERE R.guest_id = @guestId 
                      AND R.status = 'Checked Out'
                      AND NOT EXISTS (
                          SELECT 1 FROM Reviews REV 
                          WHERE REV.guest_id = R.guest_id 
                            AND REV.room_id = R.room_id 
                            -- AND REV.reservation_id = R.reservation_id -- Add this if Reviews table has reservation_id
                      )
                    ORDER BY R.check_out_date DESC;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@guestId", guestId);

                adapter.Fill(dtReviewGuestPastStays);
                dgvReviewGuestPastStays.DataSource = dtReviewGuestPastStays;
                dgvReviewGuestPastStays.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (dgvReviewGuestPastStays.Columns.Contains("Checked Out Date"))
                {
                    dgvReviewGuestPastStays.Columns["Checked Out Date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guest's past stays: " + ex.Message, "Load Stays Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void dgvReviewGuestPastStays_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReviewGuestPastStays.SelectedRows.Count > 0)
            {
                DataRowView drv = dgvReviewGuestPastStays.SelectedRows[0].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("RoomID") && drv["RoomID"] != DBNull.Value &&
                    drv.Row.Table.Columns.Contains("ReservationID") && drv["ReservationID"] != DBNull.Value)
                {
                    selectedReviewRoomId = Convert.ToInt32(drv["RoomID"]);
                    selectedReviewReservationId = Convert.ToInt32(drv["ReservationID"]);
                    string roomNumber = drv["Room Number"].ToString();
                    lblSelectedRoomForReview.Text = roomNumber;
                    if (numReviewRating.IsHandleCreated) numReviewRating.Value = 5;
                    txtReviewComment.Clear();
                    btnSubmitReview.Enabled = true;
                }
            }
            else
            {
                selectedReviewRoomId = -1;
                selectedReviewReservationId = -1;
                lblSelectedRoomForReview.Text = "(None)";
                btnSubmitReview.Enabled = false;
            }
        }

        private void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (selectedReviewGuestId == -1)
            {
                MessageBox.Show("Please select a guest first.", "No Guest Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedReviewRoomId == -1 || selectedReviewReservationId == -1)
            {
                MessageBox.Show("Please select a past stay/room to review.", "No Room Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rating = (int)numReviewRating.Value;
            string comment = txtReviewComment.Text.Trim();

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Please enter a comment for your review.", "Comment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReviewComment.Focus();
                return;
            }

            try
            {
                OpenConnection();
                string query = "INSERT INTO Reviews (guest_id, room_id, rating, comment, created_at) " + // Add reservation_id here if your table has it
                               "VALUES (@guest_id, @room_id, @rating, @comment, NOW())"; // And add @reservation_id if needed
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@guest_id", selectedReviewGuestId);
                cmd.Parameters.AddWithValue("@room_id", selectedReviewRoomId);
                // cmd.Parameters.AddWithValue("@reservation_id", selectedReviewReservationId); // Uncomment if Reviews table has reservation_id
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@comment", comment);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Review submitted successfully!", "Review Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGuestPastStaysForReview(selectedReviewGuestId);
                    lblSelectedRoomForReview.Text = "(None)";
                    if (numReviewRating.IsHandleCreated) numReviewRating.Value = 5;
                    txtReviewComment.Clear();
                    btnSubmitReview.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to submit review.", "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException mex)
            {
                if (mex.Number == 1062)
                {
                    MessageBox.Show("A review for this guest and room (and possibly reservation) already exists.", "Duplicate Review", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Database error submitting review: " + mex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}