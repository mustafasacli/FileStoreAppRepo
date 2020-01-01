namespace FileStoreAdmin.Source.Views.Logs
{
    partial class FrmLogs
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
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytSub = new System.Windows.Forms.TableLayoutPanel();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblT = new System.Windows.Forms.Label();
            this.cmbxPage = new System.Windows.Forms.ComboBox();
            this.btnGoToPage = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxSearchType = new System.Windows.Forms.ComboBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdLogs = new System.Windows.Forms.DataGridView();
            this.tblLytMain.SuspendLayout();
            this.tblLytSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblLytMain.Controls.Add(this.tblLytSub, 0, 1);
            this.tblLytMain.Controls.Add(this.grdLogs, 0, 0);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Location = new System.Drawing.Point(0, 0);
            this.tblLytMain.Margin = new System.Windows.Forms.Padding(4);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 2;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tblLytMain.Size = new System.Drawing.Size(1181, 548);
            this.tblLytMain.TabIndex = 0;
            // 
            // tblLytSub
            // 
            this.tblLytSub.ColumnCount = 15;
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tblLytSub.Controls.Add(this.lblPage, 0, 0);
            this.tblLytSub.Controls.Add(this.lblT, 11, 0);
            this.tblLytSub.Controls.Add(this.cmbxPage, 1, 0);
            this.tblLytSub.Controls.Add(this.btnGoToPage, 2, 0);
            this.tblLytSub.Controls.Add(this.btnPrevious, 4, 0);
            this.tblLytSub.Controls.Add(this.btnSearch, 13, 0);
            this.tblLytSub.Controls.Add(this.btnNext, 6, 0);
            this.tblLytSub.Controls.Add(this.label1, 8, 0);
            this.tblLytSub.Controls.Add(this.cmbxSearchType, 9, 0);
            this.tblLytSub.Controls.Add(this.dtpStart, 10, 0);
            this.tblLytSub.Controls.Add(this.dtpEnd, 12, 0);
            this.tblLytSub.Controls.Add(this.txtPage, 5, 0);
            this.tblLytSub.Controls.Add(this.btnRefresh, 14, 0);
            this.tblLytSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytSub.Location = new System.Drawing.Point(4, 478);
            this.tblLytSub.Margin = new System.Windows.Forms.Padding(4);
            this.tblLytSub.Name = "tblLytSub";
            this.tblLytSub.RowCount = 1;
            this.tblLytSub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytSub.Size = new System.Drawing.Size(1173, 66);
            this.tblLytSub.TabIndex = 0;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(4, 24);
            this.lblPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(52, 17);
            this.lblPage.TabIndex = 0;
            this.lblPage.Text = "Sayfa :";
            // 
            // lblT
            // 
            this.lblT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT.AutoSize = true;
            this.lblT.Location = new System.Drawing.Point(905, 24);
            this.lblT.Name = "lblT";
            this.lblT.Size = new System.Drawing.Size(13, 17);
            this.lblT.TabIndex = 1;
            this.lblT.Text = "-";
            this.lblT.Visible = false;
            // 
            // cmbxPage
            // 
            this.cmbxPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbxPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxPage.FormattingEnabled = true;
            this.cmbxPage.Location = new System.Drawing.Point(88, 21);
            this.cmbxPage.Name = "cmbxPage";
            this.cmbxPage.Size = new System.Drawing.Size(63, 24);
            this.cmbxPage.TabIndex = 2;
            // 
            // btnGoToPage
            // 
            this.btnGoToPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGoToPage.Location = new System.Drawing.Point(163, 13);
            this.btnGoToPage.Name = "btnGoToPage";
            this.btnGoToPage.Size = new System.Drawing.Size(74, 40);
            this.btnGoToPage.TabIndex = 3;
            this.btnGoToPage.Text = "Git";
            this.btnGoToPage.UseVisualStyleBackColor = true;
            this.btnGoToPage.Click += new System.EventHandler(this.btnGoToPage_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrevious.Location = new System.Drawing.Point(334, 13);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(44, 40);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Text = "|<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(1035, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 40);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Ara";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.Location = new System.Drawing.Point(454, 13);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(44, 40);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = ">|";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(596, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filtre Tipi :";
            // 
            // cmbxSearchType
            // 
            this.cmbxSearchType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbxSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSearchType.FormattingEnabled = true;
            this.cmbxSearchType.Location = new System.Drawing.Point(695, 22);
            this.cmbxSearchType.Name = "cmbxSearchType";
            this.cmbxSearchType.Size = new System.Drawing.Size(94, 24);
            this.cmbxSearchType.TabIndex = 5;
            this.cmbxSearchType.SelectedIndexChanged += new System.EventHandler(this.cmbxSearchType_SelectedIndexChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(795, 21);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(104, 23);
            this.dtpStart.TabIndex = 6;
            this.dtpStart.Visible = false;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(925, 21);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(104, 23);
            this.dtpEnd.TabIndex = 6;
            this.dtpEnd.Visible = false;
            // 
            // txtPage
            // 
            this.txtPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPage.Location = new System.Drawing.Point(389, 21);
            this.txtPage.Name = "txtPage";
            this.txtPage.ReadOnly = true;
            this.txtPage.Size = new System.Drawing.Size(54, 23);
            this.txtPage.TabIndex = 7;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefresh.Location = new System.Drawing.Point(1095, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 40);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grdLogs
            // 
            this.grdLogs.AllowUserToAddRows = false;
            this.grdLogs.AllowUserToDeleteRows = false;
            this.grdLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLogs.Location = new System.Drawing.Point(4, 4);
            this.grdLogs.Margin = new System.Windows.Forms.Padding(4);
            this.grdLogs.MultiSelect = false;
            this.grdLogs.Name = "grdLogs";
            this.grdLogs.ReadOnly = true;
            this.grdLogs.RowHeadersVisible = false;
            this.grdLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdLogs.Size = new System.Drawing.Size(1173, 466);
            this.grdLogs.TabIndex = 1;
            this.grdLogs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdLogs_MouseDoubleClick);
            // 
            // FrmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 548);
            this.Controls.Add(this.tblLytMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Olaylar";
            this.Load += new System.EventHandler(this.FrmLogs_Load);
            this.tblLytMain.ResumeLayout(false);
            this.tblLytSub.ResumeLayout(false);
            this.tblLytSub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLytMain;
        private System.Windows.Forms.TableLayoutPanel tblLytSub;
        private System.Windows.Forms.DataGridView grdLogs;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblT;
        private System.Windows.Forms.ComboBox cmbxPage;
        private System.Windows.Forms.Button btnGoToPage;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxSearchType;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Button btnRefresh;
    }
}