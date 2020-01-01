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
    public partial class FrmTransactions : Form
    {
        private LogWatchManager logMan;
        int totalPage;
        int currentPage;
        int totalRows;

        string columnHeaderList;
        string cmbxOpList;
        string searchType;

        List<TransactionViewModel> allLogs = null;
        List<TransactionViewModel> filteredLogs = null;

        public FrmTransactions()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();
                logMan = new LogWatchManager();
                columnHeaderList = Constants.TransactionHeaderList;
                cmbxOpList = Constants.LogComboResource;
                searchType = "NONE";
                dtpStart.Value = DateTime.Now.Date;
                dtpEnd.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "Ctor");
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
                allLogs = logMan.TransactionList();
                filteredLogs = allLogs;
                totalRows = filteredLogs.Count;
                totalPage = MathUtility.ComputeTotalPage(totalRows, AppVariables.PageItemCount);
                cmbxPage.LoadInt2Combo(totalPage);
                ChangePage(0);
                cmbxSearchType.LoadString2ComboBox(cmbxOpList, '-', '*');
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "GoToPage");
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
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "GoToPage");
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
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "Previous");
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
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "Next");
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
                        filteredLogs = allLogs.AsQueryable().Where(l => l.LogTime < dtpEnd.Value.LastTimeOfDay()).ToList<TransactionViewModel>();
                        break;

                    case "AFTR":
                        filteredLogs = allLogs.AsQueryable().Where(l => l.LogTime > dtpStart.Value.FirstTimeOfDay()).ToList<TransactionViewModel>();
                        break;

                    case "BTWN":
                        filteredLogs = allLogs.AsQueryable().Where(
                            l => l.LogTime > dtpStart.Value.FirstTimeOfDay() && l.LogTime < dtpEnd.Value.LastTimeOfDay()
                            ).ToList<TransactionViewModel>();
                        break;
                }

                totalRows = filteredLogs.Count;
                totalPage = MathUtility.ComputeTotalPage(totalRows, AppVariables.PageItemCount);
                cmbxPage.LoadInt2Combo(totalPage);
                ChangePage(0);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "Search");
            }
        }

        private void grdLogs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //   OpenStackTrace();
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
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "grdLogs_MouseDoubleClick");
            }
        }

        void ChangePage(int page)
        {
            try
            {
                currentPage = page;
                grdLogs.DataSource = filteredLogs.Page<TransactionViewModel>(page, AppVariables.PageItemCount);
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
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "cmbxSearchType_SelectedIndexChanged");
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
                allLogs = logMan.TransactionList();
                filteredLogs = allLogs;
                totalRows = filteredLogs.Count;
                totalPage = MathUtility.ComputeTotalPage(totalRows, AppVariables.PageItemCount);
                cmbxPage.LoadInt2Combo(totalPage);
                ChangePage(0);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmTransactions", "RefreshList");
                Messaging.Error("Liste yenilenemedi");
            }
        }
    }
}