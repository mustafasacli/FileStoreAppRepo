using FileStoreAdmin.Source.Extensions;
using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using FileStoreAdmin.Source.Views.Logs;
using FileStoreAdmin.Source.Views.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.Views.Main
{
    public partial class FrmMain : Form
    {

        #region [ Private Fields ]

        private LogWatchManager logwMan;
        private List<TransactionViewModel> _transactions;
        private bool _isFormWorking = false;

        #endregion


        #region [ FrmMain method ]

        public FrmMain()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();
                _isFormWorking = true;
                logwMan = new LogWatchManager();
                LoadTransactions();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmMain", "Ctor");
            }
        }

        #endregion


        #region [ userListToolStripMenuItem_Click method ]

        private void userListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserList frmUsers = new FrmUserList();
            frmUsers.Show();
        }

        #endregion


        #region [ addUserToolStripMenuItem_Click method ]

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUser frmUsr = new FrmUser();
            frmUsr.Show();
        }

        #endregion


        #region [ errorListToolStripMenuItem_Click method ]

        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogs frmLgs = new FrmLogs();
            frmLgs.Show();
        }

        #endregion


        #region [ transactionListToolStripMenuItem_Click method ]

        private void transactionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransactions frmTrans = new FrmTransactions();
            frmTrans.Show();
        }

        #endregion


        #region [ LoadTransactions method ]

        private void LoadTransactions()
        {
            try
            {
                _transactions = logwMan.LastTransactionList();
                grdMain.DataSource = _transactions;
                grdMain.PrepareGrid(Constants.TransactionHeaderList, '-');
                grdMain.Invalidate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ FrmMain_FormClosing method ]

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogManager.LogTransaction("Users", AppVariables.UserId, -1, TransactionTypes.LogOut);

            try
            {
                bckWorkerMain.CancelAsync();
            }
            catch (Exception)
            {
            }
        }

        #endregion


        #region [ exitToolStripMenuItem_Click method ]

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region [ bckWorkerMain_DoWork method ]

        private void bckWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_isFormWorking == true)
            {
                if (bckWorkerMain.CancellationPending == false)
                {
                    try
                    {
                        Thread.Sleep(AppVariables.TimerInterval * 1000);
                        LoadTransactions();
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogException(ex, AppVariables.UserId, "FrmMain", "bckWorkerMain_DoWork_If");
                    }
                }
                else
                {
                    try
                    {
                        _isFormWorking = false;
                        if (bckWorkerMain.IsBusy)
                        {
                            bckWorkerMain.CancelAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogException(ex, AppVariables.UserId, "FrmMain", "bckWorkerMain_DoWork_Else");
                    }
                    finally
                    {
                        bckWorkerMain.Dispose();
                    }
                }
            }
        }

        #endregion


        #region [ FrmMain_Load method ]

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        #endregion


        #region [ LoadForm method ]

        private void LoadForm()
        {
            try
            {
                bckWorkerMain.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmMain", "LoadForm");
            }
        }

        #endregion

    }
}
