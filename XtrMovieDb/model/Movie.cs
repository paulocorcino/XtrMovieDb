using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XtrMovieDb.model
{
    class Movie
    {
        /*
         * idFile integer primary key,";
           hash integer,";
           fullpath text,";
           name text,";           
           timestamp integer
           idMovie integer primary key,";                   
           year integer
           rate integer, 2 digits
           genres text 
         */

        public int idMovie { get; set; }
        public int idFile { get; set; }
        public long hash { get; set; }
        public string fullpath  { get; set; } 
        public string name { get; set; }
        public long timestamp { get; set; }        
        public int year  { get; set; }
        public int rate  { get; set; }
        public string genres { get; set; }

    }
}
