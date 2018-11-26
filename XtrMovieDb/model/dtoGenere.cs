using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace XtrMovieDb.model
{
    class dtoGenere
    {
        public static List<Genere> Get(string _genre = "")
        {
            var g = new List<Genere>();
            dbconnect dbconn = new dbconnect();
            List<SQLiteParameter> l = null;

            var sql = "Select";
            sql += " idGenre,";
            sql += " genre from jbgenre ";

            if (_genre != "")
            {
                
                sql += (sql.Contains("where") ? " and " : " where ") + " genre like @genre";

                if (l == null)
                    l = new List<SQLiteParameter>();

                l.Add(new SQLiteParameter("genre", _genre));

            }

            //Order by Nome
            sql = "SELECT * FROM (" + sql + ") tmp Order By tmp.idGenre";

            using (var r = dbconn.QueryReader(sql, out dbconn._conn, l))
            {
                while (r.Read())
                {

                    var gg = new Genere();
                    gg.idGenre = int.Parse(r["idGenre"].ToString());
                    gg.genre = r["genre"].ToString();

                   g.Add(gg);
                }

                dbconn._conn.Close();
            }

            dbconn = null;

            return g;
        }

        public static int Insert(string _g, SQLiteConnection _conn = null) {
            
            int ret = -1;
            string[] _gs = null;

            if (_g == "")
                return ret;

            //Sempre que adiciona um genero devo verificar se existe
            if (_g.Contains("/"))
                _gs = _g.Split('/');
            else
            {
                _gs = new string[1];
                _gs[0] = _g;
            }

            foreach (string g in _gs)
            {
                if (g != "")
                {
                    try
                    {
                        dbconnect dbconn = new dbconnect();

                        var sql = "insert into jbgenre (";
                        sql += " idGenre,";
                        sql += " genre) values ( ";
                        sql += " (Select IfNull((Max(a.idGenre) + 1), 1) From jbgenre a),";
                        sql += " @genre) ";

                        var p = new List<System.Data.SQLite.SQLiteParameter>();
                        p.Add(new SQLiteParameter("genre", g));

                        ret = dbconn.QueryExecute(sql, p, "", _conn);

                        dbconn = null;

                    }
                    catch (Exception ex)
                    {
                        ret = 0;
                    }
                }
            }


            return ret;

        }

       
    }

}
