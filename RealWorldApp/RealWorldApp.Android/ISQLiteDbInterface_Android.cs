using Xamarin.Forms;
using System.IO;
using RealWorldApp.Data;
using SQLite;
using Android.Telecom;
using RealWorldApp.Droid;
using RealWorldApp.Droid.Dependancies;

[assembly: Dependency(typeof(GetSQLiteConnnection))]
namespace RealWorldApp.Droid.Dependancies
{
    public class GetSQLiteConnnection : ISQLiteInterface
    {
       
        public SQLite.SQLiteConnection GetSQLiteConnection()
        {
            var fileName = "Ecommerce.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
            
        }

        //SQLiteConnection ISQLiteInterface.GetSQLiteConnection()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}