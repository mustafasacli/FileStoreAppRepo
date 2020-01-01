using FileStoreApp.Source.Extensions;
using FileStoreApp.Source.Management;
using FileStoreApp.Source.Variables;
using FileStoreApp.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileStoreApp.Source.Views
{
    internal partial class FrmFileList : Form
    {
        #region [ Private Fields ]

        private FileManager fileMan = null;
        private List<FileModel> allFiles = null;

        #endregion [ Private Fields ]

        #region [ FrmFileList Ctor ]

        public FrmFileList()
        {
            try
            {
                InitializeComponent();
                fileMan = new FileManager();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "Ctor");
            }
        }

        #endregion [ FrmFileList Ctor ]

        #region [ KeyDownMethod method ]

        private void KeyDownMethod(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshList();
                return;
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                return;
            }

            if (e.Control && e.KeyCode == Keys.E)
            {
                Add();
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                Delete();
                return;
            }

            if (e.Control && e.KeyCode == Keys.G)
            {
                UpdateEntity();
                return;
            }
        }

        #endregion [ KeyDownMethod method ]

        #region [ LoadFiles2Grid method ]

        private void LoadFiles2Grid()
        {
            try
            {
                if (allFiles == null)
                {
                    allFiles = new List<FileModel>();
                }

                grdFiles.DataSource = allFiles;
                grdFiles.Refresh();
                grdFiles.PrepareGrid(Constants.FileList_ColumnList, '-');
                grdFiles.Refresh();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [ LoadFiles2Grid method ]

        #region [ btnAdd_Click method ]

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        #endregion [ btnAdd_Click method ]

        #region [ btnUpdate_Click method ]

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEntity();
        }

        #endregion [ btnUpdate_Click method ]

        #region [ btnDelete_Click method ]

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        #endregion [ btnDelete_Click method ]

        #region [ btnSave_Click method ]

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion [ btnSave_Click method ]

        #region [ grdFiles_MouseDoubleClick method ]

        private void grdFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Save();
        }

        #endregion [ grdFiles_MouseDoubleClick method ]

        #region [ LoadForm method ]

        private void LoadForm()
        {
            try
            {
                allFiles = fileMan.Files();
                LoadFiles2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "LoadForm");
            }
        }

        #endregion [ LoadForm method ]

        #region [ Add method ]

        private void Add()
        {
            FrmFile frmFl = new FrmFile();
            frmFl.SaveEvent += this.UpdateListRemote;
            frmFl.ShowDialog();
        }

        #endregion [ Add method ]

        #region [ UpdateListRemote method ]

        private void UpdateListRemote(FileModel fileModel, bool isInsert)
        {
            try
            {
                if (isInsert)
                {
                    allFiles.Add(fileModel);
                }
                else
                {
                    if (allFiles != null)
                    {
                        for (int counter = 0; counter < allFiles.Count; counter++)
                        {
                            if (allFiles[counter].Id == fileModel.Id)
                            {
                                allFiles[counter].FileName = fileModel.FileName;
                                allFiles[counter].CreationDate = fileModel.CreationDate;
                                allFiles[counter].CreatedUser = fileModel.CreatedUser;
                                allFiles[counter].CreatedBy = fileModel.CreatedBy;
                            }
                        }
                    }
                }

                LoadFiles2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "UpdateListRemote");
            }
        }

        #endregion [ UpdateListRemote method ]

        #region [ Update method ]

        private void UpdateEntity()
        {
            try
            {
                if (grdFiles.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Dosya Seçiniz.");
                    return;
                }

                int fileId = grdFiles.SelectedRows[0].Cells["Id"].Value.ToInt();

                if (fileId < 1)
                {
                    MessageBox.Show("Geçersiz Dosya!..");
                    return;
                }

                int userId = grdFiles.SelectedRows[0].Cells["CreatedBy"].Value.ToInt();
                if (userId != AppVariables.UserId)
                {
                    MessageBox.Show("Dosyayı sadece yükleyen güncelleyebilir.");
                    return;
                }

                FrmFile frmFl = new FrmFile(fileId);
                frmFl.SaveEvent += this.UpdateListRemote;
                frmFl.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "Update");
            }
        }

        #endregion [ Update method ]

        #region [ Delete method ]

        private void Delete()
        {
            try
            {
                if (grdFiles.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Dosya Seçiniz.");
                    return;
                }

                int fileId = grdFiles.SelectedRows[0].Cells["Id"].Value.ToInt();

                if (fileId < 1)
                {
                    MessageBox.Show("Geçersiz Dosya!..");
                    return;
                }

                int userId = grdFiles.SelectedRows[0].Cells["CreatedBy"].Value.ToInt();
                if (userId != AppVariables.UserId)
                {
                    MessageBox.Show("Dosyayı sadece yükleyen silebilir.");
                    return;
                }
                DialogResult dr = MessageBox.Show("Dosya Silinecektir. Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool result = fileMan.DeleteFile(fileId);

                    if (result)
                    {
                        allFiles = allFiles.AsEnumerable().Where(f => f.Id != fileId).ToList<FileModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "Delete");
            }
        }

        #endregion [ Delete method ]

        #region [ Save method ]

        private void Save()
        {
            try
            {
                if (grdFiles.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Dosya Seçiniz.");
                    return;
                }

                int fileId = grdFiles.SelectedRows[0].Cells["Id"].Value.ToInt();

                if (fileId < 1)
                {
                    MessageBox.Show("Geçersiz Dosya!..");
                    return;
                }

                using (SaveFileDialog svFlDialog = new SaveFileDialog())
                {
                    svFlDialog.Title = "Dosya Kaydetme yeri seçiniz.";

                    DialogResult dr = svFlDialog.ShowDialog();

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        FileContentModel fcm = fileMan.GetFileById(fileId);
                        if (fcm != null)
                        {
                            string fileName = svFlDialog.FileName;

                            if (fileName.EndsWith(fcm.FileExtension) == false)
                            {
                                fileName = string.Format("{0}{1}", fileName, fcm.FileExtension);
                            }

                            File.WriteAllBytes(fileName, fcm.Content);
                            // Logging File Saving
                            LogManager.LogTransaction("Files", AppVariables.UserId, fileId, TransactionTypes.FileSave);
                            MessageBox.Show("Dosya Kaydedildi.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "Save");
                MessageBox.Show("Dosya Kaydedilemedi.");
            }
        }

        #endregion [ Save method ]

        #region [ FrmFileList_Load method ]

        private void FrmFileList_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        #endregion [ FrmFileList_Load method ]

        #region [ btnRefresh_Click method ]

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        #endregion [ btnRefresh_Click method ]

        #region [ RefreshList method ]

        private void RefreshList()
        {
            try
            {
                allFiles = fileMan.Files();
                LoadFiles2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFileList", "RefreshList");
            }
        }

        #endregion [ RefreshList method ]
    }
}