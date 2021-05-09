using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectMovies;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Database myDatabase = new Database();


        [TestMethod]
        public void MovieCost()
        {
            myDatabase.MovieReleaseYear = 2016.ToString();

            int result = myDatabase.RentalCost();
            Assert.AreEqual(result, 5);
        }

    }
}
