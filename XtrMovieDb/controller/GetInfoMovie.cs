using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading;

namespace XtrMovieDb.controller
{
    class GetInfoMovie
    {
        bool _isalive = false;
        Hashtable randomnumber = new Hashtable();
        string _tsrcmovie;
        string _tsrcroot;
        string _tsrctvseries;
        List<string> _terros = new List<string>();
        bool _tmovetoroot = false;
        int _tnumfilmes = 1;
        int _tnumfilmesproc = 0;        
        Thread t = null;
        
        public bool isAlive { get { return _isalive; } }
        public int NumFilms { get { return _tnumfilmes; } }
        public int NumFilmsProc { get { return _tnumfilmesproc; } }
        public List<string> Erros { get { return _terros; } }

        public void NewDb(string _srcmovie, string _srcroot, string _srctvseries, bool _movetoroot)
        {
            model.dbconnect dbconn = new model.dbconnect();

            //Cria os bancos
            dbconn.DropDB();
            dbconn.CreateDB();

            dbconn = null;

            _tsrcmovie = _srcmovie;
            _tsrcroot = _srcroot;
            _tsrctvseries = _srctvseries;
            _tmovetoroot = _movetoroot;

            ThreadStart ts = new ThreadStart(ProcFiles);
            t = new Thread(ts);
            t.IsBackground = true;
            t.Start();

            _isalive = true;

            return;

        }

        public void ProcFiles()
        {
            _isalive = true;

            //define o root;
            Uri root = new Uri(_tsrcroot + "\\");


            //Movie //////////////////////////////////////////////////////////////////////////////////////
            if (_tsrcmovie != "")
            {
                var d = Directory.GetDirectories(_tsrcmovie);

                var l = new List<model.Movie>();

                //pastas movie
                foreach (string _d in d)
                {
                    var m = new model.Movie();
                    
                  
                    m.timestamp = UnixTimestampFromDateTime(Directory.GetCreationTime(_d));
                    m.hash = hash();


                    var f = Directory.GetFiles(_d);

                    foreach (string _f in f)
                    {
                        var x = Path.GetFileNameWithoutExtension(_f);
                        var _dirname = Path.GetDirectoryName(_f).Split('\\');

                        if (isMovie(_f) && Path.GetFileNameWithoutExtension(_f).Trim().ToLower() == _dirname[_dirname.Length - 1].Trim().ToLower()) //Filme
                        {
                            Uri _foldermovie = new Uri(_f);
                            m.fullpath = "/" + Uri.UnescapeDataString(root.MakeRelativeUri(_foldermovie).ToString());
                            m.fullpath = m.fullpath.Replace("/..", "");


                            //preenche dados prevendo uma ausencia de informaçao
                            m.name = Path.GetFileNameWithoutExtension(_f);
                            m.year = File.GetCreationTime(_f).Year;
                            m.rate = 0;
                            m.genres = "/other/";
                            
                            
                        }



                        //informacoes do filme //////////////////////////////////////////////////////////////////////////////
                        if (Path.GetExtension(_f).ToLower() == ".nfo")
                        {
                            var nfo = File.ReadAllLines(_f, Encoding.UTF8);
                            bool _genrecap = false;
                            List<string> _genre = new List<string>();

                            //lendo todo conteudo
                            foreach (string _nfo in nfo)
                            {

                                if (_nfo.Contains("<title>") && _nfo.Contains("</title>"))
                                    m.name = _nfo.Replace("<title>", "").Replace("</title>", "").Replace("<![CDATA[", "").Replace("]]>", "").Trim();

                                if (_nfo.Contains("<year>") && _nfo.Contains("</year>"))
                                {
                                    try
                                    {
                                        m.year = int.Parse(_nfo.Replace("<year>", "").Replace("</year>", "").Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        m.year = File.GetCreationTime(_f).Year;
                                    }
                                }

                                if (_nfo.Contains("<rating>") && _nfo.Contains("</rating>"))
                                {
                                    try
                                    {
                                        m.rate = int.Parse(_nfo.Replace("<rating>", "").Replace("</rating>", "").Replace(".", "").Trim());

                                        if (m.rate < 10)
                                            m.rate = m.rate * 10;
                                    }
                                    catch (Exception ex)
                                    {
                                        m.rate = 0;
                                    }
                                }

                                //para a captura generos
                                if (_nfo.Contains("</genre>") && _genrecap)
                                    _genrecap = false;


                                if (_genrecap)
                                {
                                    _genre.Add(_nfo.Replace("<name><![CDATA[", "").Replace("]]></name>", "").Trim());
                                }

                                if (_nfo.Contains("<genre>") && _nfo.Contains("</genre>"))
                                {
                                    //tipo 1
                                    _genre.Add(_nfo.Replace("<genre>", "").Replace("</genre>", "").Trim());
                                }
                                else if (_nfo.Contains("<genre>"))
                                {
                                    //tipo 2
                                    _genrecap = true;
                                }


                            }

                            if (_genre.Count > 0)
                            {
                                m.genres = "/";

                                foreach (string _gcat in _genre)
                                {
                                    m.genres += _gcat + "/";
                                }
                            }

                            _genre = null;

                        }

                    }


                    if (!String.IsNullOrEmpty(m.name) && !String.IsNullOrEmpty(m.fullpath))
                        l.Add(m);
                    else
                        _terros.Add(_d);

                    _tnumfilmes = l.Count();
                    m = null;
                }

                l = l.OrderByDescending(x => x.timestamp).ToList();
                
                XtrMovieDb.model.dbconnect dbconn = new XtrMovieDb.model.dbconnect();
                
                dbconn._conn = dbconn.GetConn();

              

                using (System.Data.SQLite.SQLiteTransaction transac = dbconn._conn.BeginTransaction())
                {
                    //adiciona no banco de dados
                    foreach (model.Movie _m in l)
                    {
                        _tnumfilmesproc++;
                        model.dtoMovie.Insert(_m, dbconn._conn);

                    }

                    transac.Commit();
                }

                dbconn._conn.Close();
                dbconn = null;

               l = null;
                
            }

           
            // Movie ////////////////////////////////////////////////////////////////////////////////////////

            if (_tmovetoroot)
            {
                try
                {
                    if (Directory.Exists(_tsrcroot + "\\.jukebox"))
                        Directory.Delete(_tsrcroot + "\\.jukebox");

                    Directory.CreateDirectory(_tsrcroot + "\\.jukebox");
                }
                catch (Exception ex)
                {
                }

                try
                {
                    File.Delete(_tsrcroot + "\\.jukebox\\movie.db");
                    File.Copy("movie.db", _tsrcroot + "\\.jukebox\\movie.db");
                }
                catch (Exception ex)
                {
                }
            }

            _isalive = false;
        }

        private long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        private long hash()
        {
            Random r = new Random();
            long iNum;
            bool repetir = true;

            do {          

                iNum = r.Next(-999999999, 999999999); //put whatever range you want in here from negative to positive 

                if (!randomnumber.ContainsKey(iNum))
                {
                    randomnumber.Add(iNum, iNum);
                    repetir = false;
                }

            } while(repetir);

            return iNum;
        }

        private bool isMovie(string _file)
        {
            if (_file.ToLower().Contains(".mkv"))
                return true;

            if (_file.ToLower().Contains(".avi"))
                return true;

            if (_file.ToLower().Contains(".rmvb"))
                return true;

            if (_file.ToLower().Contains(".mov"))
                return true;

            if (_file.ToLower().Contains(".mpg"))
                return true;

            if (_file.ToLower().Contains(".mp4"))
                return true;

            if (_file.ToLower().Contains(".divx"))
                return true;

            if (_file.ToLower().Contains(".mpeg"))
                return true;

            if (_file.ToLower().Contains(".3gp"))
                return true;

            if (_file.ToLower().Contains(".iso"))
                return true;

            if (_file.ToLower().Contains(".m4v"))
                return true;

            return false;
        }
    }
}
