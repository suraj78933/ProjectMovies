using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectMovies
{
    public class Database
    {
        //create connection and command and an adapter
        private SqlConnection Connection = new SqlConnection();
        private SqlCommand Command = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();

        private string year;
        private int currentYear;
        private int fiveYearsAgo;

        #region Database
        public Database()
        {
            // add the connection string to run db
            string connectionString = @"Data Source=DESKTOP-6JPRMU5;Initial Catalog=VBMoviesFullData;Integrated Security=True";
            Connection.ConnectionString = connectionString;
            Command.Connection = Connection;
        }
        #endregion

        public int CustomerID { get; set; }
        public int MovieID { get; set; }
        public int RMID { get; set; }
        public string FN { get; set; }
        public string LN { get; set; }
        public string DateRented { get; set; }
        public string DateReturned { get; set; }
        public DateTime Today { get; set; }
        public string Result { get; set; }
        public string InputID { get; set; }
        public Button fakebutton { get; set; }
        public int MovieCost { get; set; }

        public string MovieReleaseYear { get; set; }

        #region SQL Connection
        private void SqlConnection(DataTable dt)
        {
            // connect in to the DB and get the SQL

            // open a connection to the DB
            Connection.Open();
            // fill the datatable from the SQL
            da.Fill(dt);
            // close the connection
            Connection.Close();
        }
        #endregion

        #region Fill DGV's - Customers, Movies, Rentals, Top Customers, Top Movies
        #region Fill DGV Customers
        public DataTable FillDGVCustomerWithCustomer()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select*from CustomerData ORDER by 'Last Name'", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }
        #endregion

        #region Fill DGV Movies
        public DataTable FillDGVMovieWithMovie()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from MoviesInfoData Order by Title, Year DESC", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }
        #endregion

        #region Fill DGV Rentals
        public DataTable FillDGVRentalsWithRentals()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select*from CustomerAndMoviesRented Order by 'Last Name', Title", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }

        public DataTable FillDGVRentalsWithRentalsNotReturned()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select*from CustomerAndMoviesNotReturned Order by 'Last Name', Title", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }
        #endregion

        #region Fill DGV Top Customers
        public DataTable FillDGVTopCustomerWithTopCustomer()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select*from TopCustomers ORDER by 'Customer Count' DESC, 'Last Name'", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }
        #endregion

        #region Fill DGV Top Movies
        public DataTable FillDGVTopMovieWithTopMovie()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from TopMovies ORDER by 'Movie Count' DESC, Title", Connection))
            {
                SqlConnection(dt);
            }
            //pass the datatable data to the DataGridView;
            return dt;
        }
        #endregion

        #endregion

        #region Buttons - Insert or Update
        #region Insert or Update Customer
        public string InsertOrUpdateCustomer(string firstname, string lastname, string address, string phone, string id,
            string addOrUpdate)
        {
            try
            {
                if (addOrUpdate == "Add")
                {
                    // Create a Command Object  //Create a Query
                    var myCommand = new SqlCommand("INSERT INTO Customer (FirstName, LastName, Address, Phone)" + "VALUES(@firstname, @lastname, @address, @phone)", Connection);

                    // create params
                    myCommand.Parameters.AddWithValue("firstname", firstname);
                    myCommand.Parameters.AddWithValue("lastname", lastname);
                    myCommand.Parameters.AddWithValue("address", address);
                    myCommand.Parameters.AddWithValue("phone", phone);

                    //Create and open a connection to SQL Server
                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                else if (addOrUpdate == "Update")
                {
                    var myCommand = new SqlCommand("UPDATE Customer SET FirstName=@firstname, LastName=@lastname, Address=@address, Phone=@phone WHERE CustID=@id", Connection);

                    // create params
                    myCommand.Parameters.AddWithValue("firstname", firstname);
                    myCommand.Parameters.AddWithValue("lastname", lastname);
                    myCommand.Parameters.AddWithValue("address", address);
                    myCommand.Parameters.AddWithValue("phone", phone);
                    myCommand.Parameters.AddWithValue("id", id);

                    //Create and open a connection to SQL Server
                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                return " is Successful";
            }
            catch (Exception ex)
            {
                // need to get it to close a second time as it jumps the first connection.close when ExecuteNonQuery fails.
                Connection.Close();
                return " has Failed with " + ex;
            }


        }
        #endregion

        #region Insert or Update Movie
        public string InsertOrUpdateMovie(string rating, string title, string year, string copies, string plot, string genre, int cost, string id, string addOrUpdate)
        {
            try
            {
                if (addOrUpdate == "Add")
                {
                    // Create a Command Object  //Create a Query
                    var myCommand = new SqlCommand("INSERT INTO Movies (Rating, Title, Year, Copies, Plot, Genre, Rental_Cost)" + "VALUES(@rating, @title, @year, @copies, @plot, @genre, @cost)", Connection);

                    // create params
                    myCommand.Parameters.AddWithValue("rating", rating);
                    myCommand.Parameters.AddWithValue("title", title);
                    myCommand.Parameters.AddWithValue("year", year);
                    myCommand.Parameters.AddWithValue("copies", copies);
                    myCommand.Parameters.AddWithValue("plot", plot);
                    myCommand.Parameters.AddWithValue("genre", genre);
                    myCommand.Parameters.AddWithValue("cost", cost);

                    //Create and open a connection to SQL Server
                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                else if (addOrUpdate == "Update")
                {
                    var myCommand = new SqlCommand("UPDATE Movies SET Rating=@rating, Title=@title, Year=@year, Copies=@copies,Plot=@plot,Genre=@genre, Rental_Cost=@cost WHERE MovieID=@id", Connection);

                    // create params
                    myCommand.Parameters.AddWithValue("rating", rating);
                    myCommand.Parameters.AddWithValue("title", title);
                    myCommand.Parameters.AddWithValue("year", year);
                    myCommand.Parameters.AddWithValue("copies", copies);
                    myCommand.Parameters.AddWithValue("plot", plot);
                    myCommand.Parameters.AddWithValue("genre", genre);
                    myCommand.Parameters.AddWithValue("cost", cost);
                    myCommand.Parameters.AddWithValue("id", id);

                    //Create and open a connection to SQL Server
                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                return " is Successful";
            }
            catch (Exception ex)
            {
                // need to get it to close a second time as it jumps the first connection.close when ExecuteNonQuery fails.
                Connection.Close();
                return " has Failed with " + ex;
            }


        }
        #endregion
        #endregion

        #region Buttons - Issue or Return Movie
        #region Issue Movie
        public string IssueMovie(string movieID, string custID, DateTime dateRented)
        {
            try
            {
                // Create a Command Object  //Create a Query
                var myCommand = new SqlCommand("INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented)" + "VALUES(@movieID, @custID, @dateRented)", Connection);

                // create params
                myCommand.Parameters.AddWithValue("movieID", movieID);
                myCommand.Parameters.AddWithValue("custID", custID);
                myCommand.Parameters.AddWithValue("dateRented", dateRented);

                //Create and open a connection to SQL Server
                Connection.Open();
                myCommand.ExecuteNonQuery();
                Connection.Close();
                return " is Successful";
            }



            catch (Exception ex)
            {
                // need to get it to close a second time as it jumps the first connection.close when ExecuteNonQuery fails.
                Connection.Close();
                return " has Failed with " + ex;
            }


        }
        #endregion

        #region Return Movie
        public string ReturnMovie(DateTime dateReturned, string id)
        {
            try
            {
                // Create a Command Object  //Create a Query
                var myCommand = new SqlCommand("UPDATE RentedMovies SET DateReturned=@dateReturned WHERE RMID=@id", Connection);

                // create params
                myCommand.Parameters.AddWithValue("dateReturned", dateReturned);
                myCommand.Parameters.AddWithValue("id", id);

                //Create and open a connection to SQL Server
                Connection.Open();
                myCommand.ExecuteNonQuery();
                Connection.Close();
                return " is Successful";
            }



            catch (Exception ex)
            {
                // need to get it to close a second time as it jumps the first connection.close when ExecuteNonQuery fails.
                Connection.Close();
                return " has Failed with " + ex;
            }


        }
        #endregion
        #endregion

        #region Rental Cost
        public int RentalCost()
        {
            year = MovieReleaseYear;
            currentYear = DateTime.Now.Year;
            fiveYearsAgo = currentYear - 5;

            if (fiveYearsAgo <= Convert.ToInt32(year))
            {
                return MovieCost = 5;
            }
            else if (fiveYearsAgo > Convert.ToInt32(year))
            {
                return MovieCost = 2;
            }
            else
            {
                return MovieCost = 0;
            }

        }
        #endregion

        #region Delete Customer or Movie
        public string DeleteCustomerOrMovie(string id, string table)
        {
            if (!object.ReferenceEquals(id, string.Empty))
            {
                var myCommand = new SqlCommand();
                switch (table)
                {
                    case "Customer":
                        myCommand = new SqlCommand("DELETE FROM Customer WHERE CustID=@id");
                        break;
                    case "Movie":
                        myCommand = new SqlCommand("DELETE FROM Movies WHERE MovieID=@id");
                        break;
                }

                myCommand.Connection = Connection;

                // use parameters to prevent SQL injections
                myCommand.Parameters.AddWithValue("id", id);

                // open connection add in the SQL
                Connection.Open();
                myCommand.ExecuteNonQuery();
                Connection.Close();
                return "Success";
            }
            else
            {
                Connection.Close();
                return "Failed";
            }

        }
        #endregion



    }
}
