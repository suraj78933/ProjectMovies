using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectMovies
{
    public partial class ProjectMoviesForm : Form
    {
        //create an instance of the Database Class
        Database myDatabase = new Database();

        public ProjectMoviesForm()
        {
            InitializeComponent();
            loadDB();
            TransparentBackgrounds();
        }

        #region Transparent Background
        private void TransparentBackgrounds()
        {
            // Makes Background of the panel transparent in front of a picture box
            var pos1 = this.PointToScreen(panel3.Location);
            pos1 = pbBackgroundImage.PointToClient(pos1);
            panel3.Parent = pbBackgroundImage;
            panel3.Location = pos1;
            panel3.BackColor = Color.Transparent;
        }
        #endregion

        #region Load Database
        public void loadDB()
        {
            //load the customer dgv
            DisplayDataGridViewCustomer();

            //load the movies dgv
            DisplayDataGridViewMovies();

            //load the rentals dgv
            DisplayDataGridViewRentals();

            //load the top customers dgv
            DisplayDataGridViewTopCustomer();

            //load the top movies dgv
            DisplayDataGridViewTopMovie();

        }
        #endregion

        #region All Tabs
        #region Tab Customer
        private void DisplayDataGridViewCustomer()
        {
            dgvCustomer.DataSource = null;
            try
            {
                // display data on dgvCustomer
                dgvCustomer.DataSource = myDatabase.FillDGVCustomerWithCustomer();
                // make all columns the same size and to fit inside the box
                dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Tab Movies
        private void DisplayDataGridViewMovies()
        {
            dgvMovies.DataSource = null;
            try
            {
                dgvMovies.DataSource = myDatabase.FillDGVMovieWithMovie();
                //dgvMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DgvMovies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region DGV Movies Columns
        private void DgvMovies()
        {
            // display columns to fit within the box
            dgvMovies.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //id
            dgvMovies.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //rating
            dgvMovies.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //title
            dgvMovies.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //year
            dgvMovies.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //rental cost
            dgvMovies.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; //copies
            dgvMovies.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //plot
            dgvMovies.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //genre
        }
        #endregion 
        #endregion

        #region Tab Rentals
        private void DisplayDataGridViewRentals()
        {
            dgvRentals.DataSource = null;
            try
            {
                dgvRentals.DataSource = myDatabase.FillDGVRentalsWithRentals();
                //dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DgvRentals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewRentalsNotReturned()
        {
            dgvRentals.DataSource = null;
            try
            {
                dgvRentals.DataSource = myDatabase.FillDGVRentalsWithRentalsNotReturned();
                //dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DgvRentals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region DGV Rentals Columns
        private void DgvRentals()
        {
            dgvRentals.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRentals.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRentals.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRentals.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRentals.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRentals.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRentals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        #endregion
        #endregion 

        #region Tab Top Customer
        private void DisplayDataGridViewTopCustomer()
        {
            dgvTopCust.DataSource = null;
            try
            {
                // display data on dgvCustomer
                dgvTopCust.DataSource = myDatabase.FillDGVTopCustomerWithTopCustomer();
                // make all columns the same size and to fit inside the box
                dgvTopCust.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Tab Top Movies
        private void DisplayDataGridViewTopMovie()
        {
            dgvTopMovies.DataSource = null;
            try
            {
                // display data on dgvCustomer
                dgvTopMovies.DataSource = myDatabase.FillDGVTopMovieWithTopMovie();
                // make all columns the same size and to fit inside the box
                dgvTopMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion

        #region DGV Content Changed - Customer, Movie, Rentals
        #region Customer - Content Clicked
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int CustomerID = 0;
            try
            {
                // displays customer information in textboxes when a cell is clicked
                myDatabase.CustomerID = (int)dgvCustomer.Rows[e.RowIndex].Cells[0].Value;
                lblCustID.Text = myDatabase.CustomerID.ToString();
                txtCustomerFirstName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCustomerLastName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCustomerAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();


                txtRentalsFN.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRentalsLN.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Movies - Content Clicked
        private void dgvMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // displays movie information in textboxes when a cell is clicked
            myDatabase.MovieID = Convert.ToInt32(dgvMovies.Rows[e.RowIndex].Cells[0].Value.ToString());
            lblMovieID.Text = myDatabase.MovieID.ToString();
            txtMovieRating.Text = dgvMovies.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMovieName.Text = dgvMovies.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMovieYear.Text = dgvMovies.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMovieCopies.Text = dgvMovies.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtMoviePlot.Text = dgvMovies.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtMovieGenre.Text = dgvMovies.Rows[e.RowIndex].Cells[7].Value.ToString();

            myDatabase.MovieReleaseYear = (dgvMovies.Rows[e.RowIndex].Cells[3].Value).ToString();
            txtRentalCost.Text = myDatabase.RentalCost().ToString();
            //txtRentalCost.Text = dgvMovies.Rows[e.RowIndex].Cells[4].Value.ToString();


            txtRentalsMovieTitle.Text = dgvMovies.Rows[e.RowIndex].Cells[2].Value.ToString();

        }
        #endregion

        #region Rentals - Content Clicked
        private void dgvRentals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // displays rental information in label when a cell is clicked
            myDatabase.RMID = Convert.ToInt32(dgvRentals.Rows[e.RowIndex].Cells[0].Value.ToString());
            lblRMID.Text = myDatabase.RMID.ToString();
            myDatabase.FN = dgvRentals.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblRentalFN.Text = myDatabase.FN;
            myDatabase.LN = dgvRentals.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblRentalLN.Text = myDatabase.LN;
            myDatabase.DateRented = dgvRentals.Rows[e.RowIndex].Cells[5].Value.ToString();
            lblDateRented.Text = myDatabase.DateRented;
            myDatabase.DateReturned = dgvRentals.Rows[e.RowIndex].Cells[6].Value.ToString();
            lblDateReturned.Text = myDatabase.DateReturned;
        }
        #endregion
        #endregion

        #region Radio Button Checked Changed - All Rented, Out Rented
        private void rdbAllRented_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Movies that have been Returned");

            //load the rentals dgv
            DisplayDataGridViewRentals();
        }

        private void rdbOutRented_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Movies that have not been Returned");

            // load the rentals that have not been returned yet
            DisplayDataGridViewRentalsNotReturned();
        }
        #endregion

        #region Clear Section
        private void btnClearCustomer_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void btnClearMovie_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void clearText()
        {
            // clears text from Customer Tab
            txtCustomerFirstName.Text = "";
            txtCustomerLastName.Text = "";
            txtCustomerAddress.Text = "";
            txtCustomerPhone.Text = "";
            lblCustID.Text = "";

            // clears text from Movies Tab
            txtMovieYear.Text = "";
            txtMovieCopies.Text = "";
            txtMovieGenre.Text = "";
            txtMovieName.Text = "";
            txtMoviePlot.Text = "";
            txtMovieRating.Text = "";
            lblMovieID.Text = "";
            txtRentalCost.Text = "";

            // clears text from Rental Tab
            txtRentalsFN.Text = "";
            txtRentalsLN.Text = "";
            txtRentalsMovieTitle.Text = "";
            lblRMID.Text = "";
            lblRentalFN.Text = "";
            lblRentalLN.Text = "";
            lblDateReturned.Text = "";
            lblDateRented.Text = "";
        }
        #endregion

        #region Buttons - Customer
        #region Button Add Customer
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            myDatabase.Result = null;

            // hold the success or failure result
            // only run if there is something in the textboxes
            if (txtCustomerFirstName.Text != string.Empty && txtCustomerLastName.Text != string.Empty && txtCustomerAddress.Text != string.Empty && txtCustomerPhone.Text != string.Empty)
            {
                try
                {
                    myDatabase.Result = myDatabase.InsertOrUpdateCustomer(txtCustomerFirstName.Text,
                        txtCustomerLastName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, lblCustID.Text, "Add");
                    MessageBox.Show(txtCustomerFirstName.Text + " " + txtCustomerLastName.Text + " Adding " +
                                    myDatabase.Result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // update the datagrid view to see new entries
                DisplayDataGridViewCustomer();
                clearText();
            }
            else
            {
                MessageBox.Show("Please fill in all fields and then try again");
            }
        }
        #endregion

        #region Button Update Customer
        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            myDatabase.Result = null;
            try
            {
                myDatabase.Result = myDatabase.InsertOrUpdateCustomer(txtCustomerFirstName.Text,
                    txtCustomerLastName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, lblCustID.Text, "Update");
                MessageBox.Show(txtCustomerFirstName.Text + " " + txtCustomerLastName.Text + " Updating " +
                                myDatabase.Result);

                // update the datagrid view to see new entries
                DisplayDataGridViewCustomer();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Customer was not Updated " + ex.Message);
            }
        }
        #endregion

        #region Button Delete Customer
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DeleteCustomerOrMovie(sender);
        }
        #endregion
        #endregion

        #region Buttons - Movie
        #region Button Add Movie
        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            myDatabase.MovieReleaseYear = txtMovieYear.Text;
            myDatabase.Result = null;


            // hold the success or failure result
            // only run if there is something in the textboxes
            if (txtMovieYear.Text != string.Empty && txtMovieCopies.Text != string.Empty && txtMovieGenre.Text != string.Empty && txtMovieName.Text != string.Empty && txtMoviePlot.Text != string.Empty && txtMovieRating.Text != string.Empty)
            {
                try
                {

                    myDatabase.Result = myDatabase.InsertOrUpdateMovie(txtMovieRating.Text, txtMovieName.Text, txtMovieYear.Text, txtMovieCopies.Text, txtMoviePlot.Text, txtMovieGenre.Text, myDatabase.RentalCost(), lblMovieID.Text, "Add");
                    MessageBox.Show(txtMovieName.Text + " Adding " +
                                    myDatabase.Result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // update the datagrid view to see new entries
                DisplayDataGridViewMovies();
                clearText();
            }
            else
            {
                MessageBox.Show("Please fill in all fields and then try again");
            }
        }
        #endregion

        #region Button Update Movie
        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            myDatabase.Result = null;
            try
            {
                myDatabase.Result = myDatabase.InsertOrUpdateMovie(txtMovieRating.Text, txtMovieName.Text, txtMovieYear.Text, txtMovieCopies.Text, txtMoviePlot.Text, txtMovieGenre.Text, myDatabase.RentalCost(), lblMovieID.Text, "Update");
                MessageBox.Show(txtMovieName.Text + " Updating " + myDatabase.Result);

                // update the datagrid view to see new entries
                DisplayDataGridViewMovies();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Movie was not Updated " + ex.Message);
            }
        }
        #endregion

        #region Button Delete Movie
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            DeleteCustomerOrMovie(sender);
        }
        #endregion
        #endregion

        #region Buttons - Rentals
        #region Issue Movie
        private void btnIssueMovie_Click(object sender, EventArgs e)
        {
            myDatabase.Result = null;
            myDatabase.Today = System.DateTime.Now; //(DateTime.Now.ToString());

            // hold the success or failure result
            // only run if there is something in the textboxes
            if (txtRentalsFN.Text != string.Empty && txtRentalsLN.Text != string.Empty && txtRentalsMovieTitle.Text != string.Empty)
            {
                try
                {
                    myDatabase.Result = myDatabase.IssueMovie(lblMovieID.Text,
                        lblCustID.Text, myDatabase.Today);
                    MessageBox.Show(txtCustomerFirstName.Text + " " + txtCustomerLastName.Text + " Movie Successfully Issued \n" +
                                    myDatabase.Result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // update the datagrid view to see new entries
                DisplayDataGridViewRentals();
                DisplayDataGridViewTopCustomer();
                DisplayDataGridViewTopMovie();
                clearText();
            }
            else
            {
                MessageBox.Show("Please fill in all fields and then try again");
            }
        }
        #endregion

        #region Return Movie
        private void btnReturnMovie_Click(object sender, EventArgs e)
        {
            if (lblDateReturned.Text == string.Empty)
            {
                //MessageBox.Show("Returned");

                myDatabase.Today = System.DateTime.Now; //(DateTime.Now.ToString());

                try
                {
                    myDatabase.Result = myDatabase.ReturnMovie(myDatabase.Today, lblRMID.Text);
                    MessageBox.Show(lblRentalFN.Text + " " + lblRentalLN.Text + " Movie Successfully Returned \n" +
                                    myDatabase.Result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // update the datagrid view to see new entries
                DisplayDataGridViewRentals();
                DisplayDataGridViewTopCustomer();
                DisplayDataGridViewTopMovie();
                clearText();
            }
            else
            {
                MessageBox.Show("You cannot return a movie which has already been returned");
            }
        }
        #endregion
        #endregion

        #region Delete Customer or Movie
        private void DeleteCustomerOrMovie(object sender)
        {
            // hold the ID of the Customer or Movie
            myDatabase.InputID = string.Empty;

            myDatabase.Result = null;
            myDatabase.fakebutton = null;
            myDatabase.fakebutton = (Button)sender;

            try
            {
                switch (myDatabase.fakebutton.Name)
                {
                    case "btnDeleteCustomer":
                        myDatabase.InputID = lblCustID.Text;
                        break;
                    case "btnDeleteMovie":
                        myDatabase.InputID = lblMovieID.Text;
                        break;
                }

                // delete here and return back success or failure
                myDatabase.Result =
                    myDatabase.DeleteCustomerOrMovie(myDatabase.InputID, myDatabase.fakebutton.Tag.ToString());
                MessageBox.Show(myDatabase.fakebutton.Tag + " delete " + myDatabase.Result);

                // refresh everything
                loadDB();
                clearText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("First click on the Customer or Movie you want to delete " + ex.Message);
            }
        }

        #endregion
    }
}
