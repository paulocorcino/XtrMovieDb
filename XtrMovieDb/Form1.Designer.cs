namespace XtrMovieDb
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.txtMovie = new System.Windows.Forms.TextBox();
            this.txtTvSeries = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnChTv = new System.Windows.Forms.Button();
            this.btnChMovie = new System.Windows.Forms.Button();
            this.btnRoot = new System.Windows.Forms.Button();
            this.fbdRoot = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdMovie = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdTvSerie = new System.Windows.Forms.FolderBrowserDialog();
            this.tmrProc = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtErros = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkdbtoroot = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xtreamer Root Device";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder Movie";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Folder Tv Series";
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(15, 28);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(348, 20);
            this.txtRoot.TabIndex = 3;
            this.txtRoot.TextChanged += new System.EventHandler(this.txtRoot_TextChanged);
            // 
            // txtMovie
            // 
            this.txtMovie.Location = new System.Drawing.Point(15, 74);
            this.txtMovie.Name = "txtMovie";
            this.txtMovie.Size = new System.Drawing.Size(348, 20);
            this.txtMovie.TabIndex = 4;
            this.txtMovie.TextChanged += new System.EventHandler(this.txtMovie_TextChanged);
            // 
            // txtTvSeries
            // 
            this.txtTvSeries.Location = new System.Drawing.Point(12, 126);
            this.txtTvSeries.Name = "txtTvSeries";
            this.txtTvSeries.Size = new System.Drawing.Size(351, 20);
            this.txtTvSeries.TabIndex = 5;
            this.txtTvSeries.TextChanged += new System.EventHandler(this.txtTvSeries_TextChanged);
            // 
            // btnRun
            // 
            this.btnRun.Image = global::XtrMovieDb.Properties.Resources.control_play;
            this.btnRun.Location = new System.Drawing.Point(369, 162);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(102, 47);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnChTv
            // 
            this.btnChTv.Image = global::XtrMovieDb.Properties.Resources.folder_star;
            this.btnChTv.Location = new System.Drawing.Point(369, 120);
            this.btnChTv.Name = "btnChTv";
            this.btnChTv.Size = new System.Drawing.Size(75, 31);
            this.btnChTv.TabIndex = 8;
            this.btnChTv.Text = "Choice";
            this.btnChTv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChTv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChTv.UseVisualStyleBackColor = true;
            this.btnChTv.Click += new System.EventHandler(this.btnChTv_Click);
            // 
            // btnChMovie
            // 
            this.btnChMovie.Image = global::XtrMovieDb.Properties.Resources.folder_star;
            this.btnChMovie.Location = new System.Drawing.Point(369, 68);
            this.btnChMovie.Name = "btnChMovie";
            this.btnChMovie.Size = new System.Drawing.Size(75, 31);
            this.btnChMovie.TabIndex = 7;
            this.btnChMovie.Text = "Choice";
            this.btnChMovie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChMovie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChMovie.UseVisualStyleBackColor = true;
            this.btnChMovie.Click += new System.EventHandler(this.btnChMovie_Click);
            // 
            // btnRoot
            // 
            this.btnRoot.Image = global::XtrMovieDb.Properties.Resources.folder;
            this.btnRoot.Location = new System.Drawing.Point(369, 22);
            this.btnRoot.Name = "btnRoot";
            this.btnRoot.Size = new System.Drawing.Size(75, 31);
            this.btnRoot.TabIndex = 6;
            this.btnRoot.Text = "Choice";
            this.btnRoot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRoot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRoot.UseVisualStyleBackColor = true;
            this.btnRoot.Click += new System.EventHandler(this.btnRoot_Click);
            // 
            // fbdRoot
            // 
            this.fbdRoot.ShowNewFolderButton = false;
            // 
            // fbdMovie
            // 
            this.fbdMovie.ShowNewFolderButton = false;
            // 
            // fbdTvSerie
            // 
            this.fbdTvSerie.ShowNewFolderButton = false;
            // 
            // tmrProc
            // 
            this.tmrProc.Interval = 500;
            this.tmrProc.Tick += new System.EventHandler(this.tmrProc_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 380);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // txtErros
            // 
            this.txtErros.BackColor = System.Drawing.Color.White;
            this.txtErros.Location = new System.Drawing.Point(15, 174);
            this.txtErros.Multiline = true;
            this.txtErros.Name = "txtErros";
            this.txtErros.ReadOnly = true;
            this.txtErros.Size = new System.Drawing.Size(348, 203);
            this.txtErros.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Erros";
            // 
            // chkdbtoroot
            // 
            this.chkdbtoroot.AutoSize = true;
            this.chkdbtoroot.Location = new System.Drawing.Point(366, 215);
            this.chkdbtoroot.Name = "chkdbtoroot";
            this.chkdbtoroot.Size = new System.Drawing.Size(117, 17);
            this.chkdbtoroot.TabIndex = 14;
            this.chkdbtoroot.Text = "Move db file to root";
            this.chkdbtoroot.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 402);
            this.Controls.Add(this.chkdbtoroot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtErros);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnChTv);
            this.Controls.Add(this.btnChMovie);
            this.Controls.Add(this.btnRoot);
            this.Controls.Add(this.txtTvSeries);
            this.Controls.Add(this.txtMovie);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XtrMovieDb 1.0.0 Beta";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.TextBox txtMovie;
        private System.Windows.Forms.TextBox txtTvSeries;
        private System.Windows.Forms.Button btnRoot;
        private System.Windows.Forms.Button btnChMovie;
        private System.Windows.Forms.Button btnChTv;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.FolderBrowserDialog fbdRoot;
        private System.Windows.Forms.FolderBrowserDialog fbdMovie;
        private System.Windows.Forms.FolderBrowserDialog fbdTvSerie;
        private System.Windows.Forms.Timer tmrProc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox txtErros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkdbtoroot;
    }
}

