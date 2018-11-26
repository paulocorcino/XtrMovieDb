using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace XtrMovieDb.model
{
    class dtoMovie
    {
        public static List<Movie> Get(int _fileid = 0, string _namemovie = "")
        {
            var m = new List<Movie>();
            dbconnect dbconn = new dbconnect();
            List<SQLiteParameter> l = null;

            var sql = "Select";
                sql += " jbmovie.idMovie,";
                sql += " jbfile.idFile,";
                sql += " jbfile.hash,";
                sql += " jbfile.fullpath,";
                sql += " jbfile.name,";
                sql += " jbfile.timestamp,";
                sql += " jbmovie.year,";
                sql += " jbmovie.rate,";
                sql += " jbmovie.genres";
                sql += " From";
                sql += " jbfile Inner Join";
                sql += " jbmovie On jbfile.idFile = jbmovie.idFile";

                if (_fileid > 0)
                {
                    sql += (sql.Contains("where") ? " and " : " where ") + " idFile = @idFile";

                    if (l == null)
                        l = new List<SQLiteParameter>();

                    l.Add(new SQLiteParameter("idFile", _fileid));

                }

                if (_namemovie != "")
                {

                    sql += (sql.Contains("where") ? " and " : " where ") + " name like @name";

                    if (l == null)
                        l = new List<SQLiteParameter>();

                    l.Add(new SQLiteParameter("name", _namemovie));

                }

                //Order by Nome
                sql = "SELECT * FROM (" + sql + ") tmp Order By tmp.timestamp";

                using (var r = dbconn.QueryReader(sql, out dbconn._conn, l))
                {
                    while (r.Read())
                    {                  

                        var mm = new Movie();
                        mm.idMovie   = int.Parse(r["idMovie"].ToString());
                        mm.idFile    = int.Parse(r["idFile"].ToString());
                        mm.hash      = long.Parse(r["hash"].ToString());
                        mm.fullpath  = r["fullpath"].ToString();
                        mm.name      = r["name"].ToString();
                        mm.timestamp = long.Parse(r["timestamp"].ToString());
                        mm.year      = int.Parse(r["year"].ToString());
                        mm.rate      = int.Parse(r["rate"].ToString());
                        mm.genres    = r["genres"].ToString();

                        m.Add(mm);
                    }

                    dbconn._conn.Close();
                }

                dbconn = null;

                return m;

        }

        public static int Insert(Movie _m, SQLiteConnection _conn = null)
        {
            int ret = -1;

            if (_m == null)
                return ret;

            try
            {

                dbconnect dbconn = new dbconnect();

                var sql = "insert into jbfile (";
                sql += "idFile,";
                sql += " hash,";
                sql += " fullpath,";
                sql += " name,";
                sql += " lock,";
                sql += " timestamp ) values (";
                sql += " (Select IfNull((Max(a.idFile) + 1), 1) From jbfile a) ,";
                sql += " @hash,";
                sql += " @fullpath,";
                sql += " @name,";
                sql += " 0,";
                sql += " @timestamp )";

                var p = new List<System.Data.SQLite.SQLiteParameter>();
                p.Add(new SQLiteParameter("hash", _m.hash));
                p.Add(new SQLiteParameter("fullpath", _m.fullpath));
                p.Add(new SQLiteParameter("name", _m.name));
                p.Add(new SQLiteParameter("timestamp", _m.timestamp));

                ret = dbconn.QueryExecute(sql, p,"",_conn);


                sql = " insert into jbmovie (";
                sql += " idMovie,";
                sql += " idFile,";
                sql += " year,";
                sql += " rate,";
                sql += " genres,";
                sql += " idDirector,";
                sql += " idActors,";
                sql += " playtimes,";
                sql += " myrate ) values (";
                sql += " (Select IfNull((Max(a.idMovie) + 1), 1) From jbmovie a),";
                sql += " (Select IfNull((Max(a.idMovie) + 1), 1) From jbmovie a),";
                sql += " @year,";
                sql += " @rate,";
                sql += " @genres,";
                sql += " null,";
                sql += " null,";
                sql += " 0,";
                sql += " null )";

                p.Clear();
                p.Add(new SQLiteParameter("year", _m.year));
                p.Add(new SQLiteParameter("rate", _m.rate));
                p.Add(new SQLiteParameter("genres", _m.genres));

                ret = dbconn.QueryExecute(sql, p, "", _conn);

                //adicionar genero
                model.dtoGenere.Insert(_m.genres, _conn);

                dbconn = null;
            }
            catch (Exception ex)
            {
                //nada
            }

            return ret;
        }

        public static int Update(Movie _m)
        {
            int ret = -1;

            if (_m == null)
                return ret;

            try
            {
                dbconnect dbconn = new dbconnect();

                var sql = "update jbfile set ";
                sql += " fullpath = @fullpath,";
                sql += " name = @name,";
                sql += " timestamp = @timestamp where idFile = @idfile";

                var p = new List<System.Data.SQLite.SQLiteParameter>();
                p.Add(new SQLiteParameter("fullpath", _m.fullpath));
                p.Add(new SQLiteParameter("name", _m.name));
                p.Add(new SQLiteParameter("timestamp", _m.timestamp));
                p.Add(new SQLiteParameter("idfile", _m.idFile));

                ret = dbconn.QueryExecute(sql, p);


                sql = " update jbmovie set ";
                sql += " year = @year,";
                sql += " rate = @rate,";
                sql += " genres = @genres where idMovie = @idMovie";

                p.Clear();
                p.Add(new SQLiteParameter("year", _m.year));
                p.Add(new SQLiteParameter("rate", _m.rate));
                p.Add(new SQLiteParameter("genres", _m.genres));
                p.Add(new SQLiteParameter("idMovie", _m.idMovie));

                ret = dbconn.QueryExecute(sql, p);

                //adicionar genero

                dbconn = null;
            }
            catch (Exception ex)
            {
                //nada
            }

            return ret;
        }

        public static int Delete(int _idfile)
        {
            int ret = -1;

            dbconnect dbconn = new dbconnect();
            string sql = "Delete from jbfile  where idFile = @idfile";

            var p = new List<System.Data.SQLite.SQLiteParameter>();
            p.Add(new SQLiteParameter("idFile", _idfile));
            ret = dbconn.QueryExecute(sql, p);

            p.Clear();

            sql = "Delete from jbmovie  where idMovie = @idMovie";
            p.Add(new SQLiteParameter("idMovie", _idfile));
            ret = dbconn.QueryExecute(sql, p);


            dbconn = null;

            return ret;
        }

    }
}
