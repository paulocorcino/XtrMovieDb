using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XtrMovieDb
{
    public partial class Form1 : Form
    {
        controller.GetInfoMovie gtm = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("save.inf"))
            {
                var s = File.ReadAllLines("save.inf", Encoding.Default);
                txtRoot.Text = s[0];
                txtMovie.Text = s[1];
                txtTvSeries.Text = s[2];

                if (txtRoot.Text != "")
                    fbdRoot.SelectedPath = txtRoot.Text;

                if (txtMovie.Text != "")
                    fbdMovie.SelectedPath = txtMovie.Text;

                if (txtTvSeries.Text != "")
                    fbdTvSerie.SelectedPath = txtTvSeries.Text;
            }
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {
            if (fbdRoot.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtRoot.Text = fbdRoot.SelectedPath;
                SaveSelection();
            }
        }

        private void btnChMovie_Click(object sender, EventArgs e)
        {
            if (fbdMovie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMovie.Text = fbdMovie.SelectedPath;
                SaveSelection();
            }
        }

        private void btnChTv_Click(object sender, EventArgs e)
        {
            if (fbdTvSerie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTvSeries.Text = fbdTvSerie.SelectedPath;
                SaveSelection();
            }
        }

        private void SaveSelection()
        {
            string[] cfg = new string[3];
            cfg[0] = txtRoot.Text;
            cfg[1] = txtMovie.Text;
            cfg[2] = txtTvSeries.Text;

            File.Delete("save.inf");
            File.WriteAllLines("save.inf", cfg, Encoding.Default);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (txtRoot.Text == "")
            {
                MessageBox.Show("Please select a root folder.", Form1.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMovie.Text == "" && txtTvSeries.Text == "")
            {
                MessageBox.Show("Please select a folder.", Form1.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gtm = new controller.GetInfoMovie();

            gtm.NewDb(txtMovie.Text, txtRoot.Text, txtTvSeries.Text, chkdbtoroot.Checked);
            if (gtm.isAlive)
            {
                tmrProc.Enabled = true;
                btnRun.Enabled = false;
            }
        }

        private void tmrProc_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = String.Format("Movies Found.: {0} / Movie Process.: {1}", gtm.NumFilms.ToString(), gtm.NumFilmsProc.ToString());
            
            if (!gtm.isAlive)
            {
                btnRun.Enabled = true;
                tmrProc.Enabled = false;
                txtErros.Clear();
                txtErros.Lines = gtm.Erros.ToArray();                
                gtm = null;
            }
        }

        private void txtRoot_TextChanged(object sender, EventArgs e)
        {
            fbdRoot.SelectedPath = txtRoot.Text;
            SaveSelection();
        }

        private void txtMovie_TextChanged(object sender, EventArgs e)
        {
            fbdMovie.SelectedPath = txtMovie.Text;
            SaveSelection();
        }

        private void txtTvSeries_TextChanged(object sender, EventArgs e)
        {
            fbdTvSerie.SelectedPath = txtTvSeries.Text;
            SaveSelection();
        }
    }
}
