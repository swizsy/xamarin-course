using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
        // Commented code was required for the local SQLite database
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        public string Id;

        public string UserId;

        //[MaxLength(250)]
        public string Experience { get; set; }

        #region Venue Properties
        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        #endregion
    }
}
