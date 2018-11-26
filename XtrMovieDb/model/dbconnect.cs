using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace XtrMovieDb.model
{
    class dbconnect
    {

        static string nmdatabase = "movie.db";
        static string conexao = "Data Source=" + nmdatabase + ";";
        public SQLiteConnection _conn;

        public bool DropDB()
        {
            try
            {
                System.IO.File.Delete(nmdatabase);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreateDB()
        {
            //banco de dados nao existe
            if (!System.IO.File.Exists(nmdatabase))
            {
                //Conexao Temporaria criacao senha;
                SQLiteConnection.CreateFile(nmdatabase);
                                

                try
                {

                    var sql = "CREATE TABLE IF NOT EXISTS jbfile (idFile integer primary key,";
                    sql += " hash integer,";
                    sql += " fullpath text,";
                    sql += " name text,";
                    sql += " lock integer,";
                    sql += " timestamp integer); ";

                    var c = QueryExecute(sql);

                    sql = "CREATE TABLE IF NOT EXISTS jbgenre (idGenre integer primary key,";
                    sql += " genre text unique); ";

                    c = QueryExecute(sql);

                    sql = "CREATE TABLE IF NOT EXISTS jbmovie (idMovie integer primary key,";
                    sql += " idFile integer,";
                    sql += " year integer,";
                    sql += " rate integer,";
                    sql += " genres text,";
                    sql += " idDirector integer,";
                    sql += " idActors text,";
                    sql += " playtimes integer,";
                    sql += " myrate integer ); ";

                    c = QueryExecute(sql);

                    sql = "CREATE TABLE IF NOT EXISTS jbperson (idPerson integer primary key,";
                    sql += " name text unique); ";

                    c = QueryExecute(sql);

                    sql = "CREATE TABLE IF NOT EXISTS jbtvepisode (idEpisode integer primary key,";
                    sql += " idFile integer,";
                    sql += " idSeries integer,";
                    sql += " season integar,";
                    sql += " rate integer,";
                    sql += " idActors text ,";
                    sql += " myrate integer,";
                    sql += " playtimes integer);";

                    c = QueryExecute(sql);

                    sql = " CREATE TABLE IF NOT EXISTS jbtvseries (idSeries integer primary key,";
                    sql += " idFile integer,";
                    sql += " genres text,";
                    sql += " rate integer,";
                    sql += " idActors text ,";
                    sql += " myrate integer); ";

                    c = QueryExecute(sql);

                    sql = "CREATE TABLE IF NOT EXISTS jbversion (version integer,";
                    sql += " name string,";
                    sql += " accesses integer);";

                    c = QueryExecute(sql);

                    c = QueryExecute("CREATE INDEX jbhash_idx ON jbfile (hash ASC)");
                    c = QueryExecute("CREATE INDEX jbmactor_idx ON jbmovie (idActors ASC)");
                    c = QueryExecute("CREATE INDEX jbmgenre_idx ON jbmovie (genres ASC)");
                    c = QueryExecute("CREATE INDEX jbperson_idx ON jbperson (name ASC)");
                    c = QueryExecute("CREATE INDEX jbtve_idx ON jbtvepisode (idActors ASC)");
                    c = QueryExecute("INSERT INTO jbversion VALUES (1, 'mljb52', 1263552501)");

                }
                catch (Exception ex)
                {

                    return false;
                }

                return true;

            }
           

            return true;
        }

        /// <summary>
        /// Executa comandos de Insert, Update, Delete
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        public int QueryExecute(string _sql, List<SQLiteParameter> _parameter = null, string _conexao = "", SQLiteConnection _conn = null)
        {
            int ret = 0;

            //Se com Transacao
            if (_conn != null)
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                    _conn.Open();

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, _conn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    ret = _cmd.ExecuteNonQuery();
                }

                return ret;
            }

            //Execução Direta
            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            using (SQLiteConnection tmpconn = new SQLiteConnection(_conexao))
            {

                tmpconn.Open();

                if (String.IsNullOrEmpty(_sql))
                    return 0;

                if (tmpconn == null)
                    return 0;

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    ret = _cmd.ExecuteNonQuery();
                }

                tmpconn.Close();

            }

            return ret;
        }

        /// <summary>
        /// Executa leitura de dados SELECT
        /// </summary>
        /// <param name="_sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public SQLiteDataReader QueryReader(string _sql, out SQLiteConnection tmpconn, List<SQLiteParameter> _parameter = null, string _conexao = "")
        {
            tmpconn = null;

            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            tmpconn = new SQLiteConnection(_conexao);
            tmpconn.Open();

            if (String.IsNullOrEmpty(_sql))
                return null;

            if (tmpconn == null)
                return null;

            SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn);

            if (_parameter != null)
                _cmd.Parameters.AddRange(_parameter.ToArray());

            SQLiteDataReader _r = _cmd.ExecuteReader();

            return _r;

        }

        public List<Dictionary<string, object>> QueryReader(string _sql, List<SQLiteParameter> _parameter = null, string _conexao = "")
        {
            List<Dictionary<string, object>> r = new List<Dictionary<string, object>>();
            var dict = new Dictionary<string, object>();


            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            using (SQLiteConnection tmpconn = new SQLiteConnection(_conexao))
            {
                tmpconn.Open();

                if (String.IsNullOrEmpty(_sql))
                    return null;

                if (tmpconn == null)
                    return null;

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    SQLiteDataReader _r = _cmd.ExecuteReader();

                    while (_r.Read())
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();

                        for (int i = 0; i < _r.FieldCount; i++)
                        {
                            d[_r.GetName(i)] = _r[i];
                            //nada

                        }

                        r.Add(d);
                        d = null;
                    }

                }
            }

            return r;
        }

        /// <summary>
        /// Obtem uma conexao atual
        /// </summary>
        /// <param name="_conexao"></param>
        /// <returns></returns>
        public SQLiteConnection GetConn(string _conexao = "")
        {
            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            SQLiteConnection tmpconn = new SQLiteConnection(_conexao);

            tmpconn.Open();

            return tmpconn;

        }


        public string BackupDatabase()
        {
            string _newfile = "";
            try
            {
                System.IO.Directory.CreateDirectory("backup");
                _newfile = "backup/" + "backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_") + nmdatabase;
                System.IO.File.Copy(nmdatabase, _newfile);

                return _newfile;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
