using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using RealWorldApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLiteDbInterface_iOS))]
namespace RealWorldApp.iOS
{
   public class ISQLiteDbInterface_iOS : ISite
    {
        public IComponent Component => throw new NotImplementedException();

        public IContainer Container => throw new NotImplementedException();

        public bool DesignMode => throw new NotImplementedException();

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var fileName = "Ecommerce.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}