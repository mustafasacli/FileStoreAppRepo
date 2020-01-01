using FileStoreAdmin.Source.Extensions;
using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.MessageUtil;
using FileStoreAdmin.Source.Util;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.Views.Logs
{
    public partial class FrmLogs : Form
    {
        private LogWatchManager logMan;
        int totalPage;
        int currentPage;
        int totalRows;

        string columnHeaderList;
        string cmbxOpList;
        string searchType;

        List<LogViewModel> allLogs = null;
        List<LogViewModel> filteredLogs = null;

        public FrmLogs()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();
                logMan = new LogWatchManager();
                columnHeaderList = Constants.LogColumnList;
                cmbxOpList = Constants.LogComboResource;
                searchType = "NONE";
                dtpStart.Value = DateTime.Now.Date;
                dtpEnd.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "Ctor");
            }
        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnGoToPage_Click(object sender, EventArgs e)
        {
            GoToPage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        void LoadForm()
        {
            try
            {
                allLogs = logMan.LogList();
                filteredLogs = allLogs;
                totalRows = filteredLogs.Count;
                totalPage = MathUtility.ComputeTotalPage(totalRows, AppVariables.PageItemCount);
                cmbxPage.LoadInt2Combo(totalPage);
                ChangePage(0);
                cmbxSearchType.LoadString2ComboBox(cmbxOpList, '-', '*');
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "GoToPage");
            }
        }

        private void GoToPage()
        {
            try
            {
                if (cmbxPage.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen Sayfa Seçin.");
                    return;
                }

                string pageStr = cmbxPage.GetItemText(cmbxPage.SelectedItem);
                int pg = pageStr.ToInt();
                ChangePage(pg - 1);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "GoToPage");
            }
        }

        private void Previous()
        {
            try
            {
                ChangePage(currentPage - 1);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "Previous");
            }
        }

        private void Next()
        {
            try
            {
                ChangePage(currentPage + 1);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "Next");
            }
        }

        private void Search()
        {
            try
            {
                switch (searchType)
                {
                    case "NONE":
                    default:
                        filteredLogs = allLogs;
                        break;

                    case "BFR":
                        filteredLogs = allLogs.AsQueryable().Where(l => l.LogTime < dtpEnd.Value.LastTimeOfDay()).ToList<LogViewModel>();
                        break;

                    case "AFTR":
                        filteredLogs = allLogs.AsQueryable().Where(l => l.LogTime > dtpStart.Value.FirstTimeOfDay()).ToList<LogViewModel>();
                        break;

                    case "BTWN":
                        filteredLogs = allLogs.AsQueryable().Where(
                            l => l.LogTime > dtpStart.Value.FirstTimeOfDay() && l.LogTime < dtpEnd.Value.LastTimeOfDay()
                            ).ToList<LogViewModel>();
                        break;
                }

                ChangePage(0);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "Search");
            }
        }

        private void grdLogs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenStackTrace();
        }

        void OpenStackTrace()
        {
            try
            {
                if (grdLogs.SelectedRows.Count == 0)
                {
                    return;
                }

                StringBuilder strBuilder = new StringBuilder();
                foreach (DataGridViewColumn colmn in grdLogs.Columns)
                {
                    strBuilder.AppendLine(string.Format("{0} : {1}", colmn.Name, grdLogs.SelectedRows[0].Cells[colmn.Name].Value));
                }
                FrmStackTrace frmStck = new FrmStackTrace(strBuilder.ToString());
                frmStck.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "grdLogs_MouseDoubleClick");
            }
        }

        void ChangePage(int page)
        {
            try
            {
                currentPage = page;
                grdLogs.DataSource = filteredLogs.Page<LogViewModel>(page, AppVariables.PageItemCount);
                grdLogs.PrepareGrid(columnHeaderList, '-');
                grdLogs.Refresh();
                btnNext.Enabled = currentPage != totalPage;
                btnPrevious.Enabled = currentPage != 0;
                txtPage.Text = string.Format("{0} / {1}", currentPage + 1, totalPage + 1);
                grdLogs.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmbxSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtpStart.Visible = false;
                dtpEnd.Visible = false;
                lblT.Visible = false;
                string strVal = cmbxSearchType.SelectedValue.ToStr();
                searchType = strVal;

                if (string.Equals(strVal, "BFR"))
                {
                    dtpEnd.Visible = true;
                    return;
                }

                if (string.Equals(strVal, "AFTR"))
                {
                    dtpStart.Visible = true;
                    return;
                }

                if (string.Equals(strVal, "BTWN"))
                {
                    dtpStart.Visible = true;
                    dtpEnd.Visible = true;
                    lblT.Visible = true;
                    return;
                }

                strVal = "NONE";
                searchType = strVal;
                return;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "cmbxSearchType_SelectedIndexChanged");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        void RefreshList()
        {
            try
            {
                allLogs = logMan.LogList();
                filteredLogs = allLogs;
                totalRows = filteredLogs.Count;
                totalPage = MathUtility.ComputeTotalPage(totalRows, AppVariables.PageItemCount);
                cmbxPage.LoadInt2Combo(totalPage);
                ChangePage(0);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmLogs", "RefreshList");
                Messaging.Error("Liste yenilenemedi");
            }
        }
    }
}