using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace RealWorldApp.Data
{
    [Preserve(AllMembers = true)]
    public interface ISQLiteInterface
    {
       
      SQLiteConnection GetSQLiteConnection();
    }

    [Preserve(AllMembers = true)]
    public interface ISQLConnection
    {

        SQLiteAsyncConnection GetSQLiteConnection();
    }
}
