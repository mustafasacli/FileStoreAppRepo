using FileStoreApp.Source.Management;
using FileStoreApp.Source.Model;
using FileStoreApp.Source.Variables;
using FileStoreApp.Source.ViewModel;
using System;
using System.IO;
using System.Windows.Forms;

namespace FileStoreApp.Source.Views
{
    internal partial class FrmFile : Form
    {

        #region [ Private Fields ]

        int _fileId = -1;
        FileManager fileMan;
        PersonalFile personFile;
        bool isFileGet = false;

        #endregion


        #region [ Public Delegates ]

        public delegate void Saved(FileModel fileModel, bool isInserted);
        public Saved SaveEvent;

        #endregion


        #region [ FrmFile Ctors ]

        public FrmFile()
            : this(-1)
        { }

        public FrmFile(int fileId)
        {
            try
            {
                InitializeComponent();
                _fileId = fileId;
                fileMan = new FileManager();
                personFile = new PersonalFile();
                isFileGet = false;
                this.Text = _fileId == -1 ? "Dosya Ekle" : "Dosya Güncelle";
                if (_fileId > 0)
                {
                    personFile = fileMan.GetPersonalFileById(_fileId);
                    if (personFile != null)
                    {
                        lblFileName.Text = string.Format("Dosya : {0}", personFile.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFile", "Ctor");
            }
        }

        #endregion


        #region [ btnBrowse_Click method ]

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //Browse...
                using (OpenFileDialog opfldlg = new OpenFileDialog())
                {
                    opfldlg.Title = "Dosya Seçin";
                    DialogResult dr = opfldlg.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        personFile.FileName = opfldlg.SafeFileName;
                        personFile.FileContent = File.ReadAllBytes(opfldlg.FileName);
                        lblFileName.Text = string.Format("Dosya : {0}", personFile.FileName);
                        isFileGet = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFile", "Browse");
            }
        }

        #endregion


        #region [ btnSave_Click method ]

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion


        #region [ btnCancel_Click method ]

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        #endregion


        #region [ Save method ]

        void Save()
        {
            try
            {
                bool isProcessSuccessed = false;
                if (_fileId == -1)
                {
                    if (isFileGet)
                    {
                        personFile.CreatedBy = AppVariables.UserId;
                        personFile.CreationDate = DateTime.Now;
                        personFile.IsActive = true;

                        isProcessSuccessed = fileMan.AddFile(personFile);

                        if (isProcessSuccessed)
                        {
                            if (SaveEvent != null)
                            {
                                SaveEvent(new FileModel()
                                {
                                    Id = personFile.Id,
                                    FileName = personFile.FileName,
                                    CreationDate = personFile.CreationDate,
                                    CreatedUser = AppVariables.UserName,
                                    CreatedBy = AppVariables.UserId
                                }, true);
                            }
                            MessageBox.Show("Dosya Ekleme tamamlandı.");
                            btnSave.Enabled = false;
                            btnCancel.Text = "Kapat";
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Dosya Ekleme tamamlanamadı.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Dosya Seçin.");
                        return;
                    }
                }
                else
                {
                    if (isFileGet)
                    {
                        isProcessSuccessed = fileMan.UpdateFile(personFile);
                        if (isProcessSuccessed)
                        {
                            if (SaveEvent != null)
                            {
                                SaveEvent(new FileModel()
                                {
                                    Id = personFile.Id,
                                    FileName = personFile.FileName,
                                    CreationDate = personFile.CreationDate,
                                    CreatedUser = AppVariables.UserName,
                                    CreatedBy = AppVariables.UserId
                                }, false);
                            }
                            MessageBox.Show("Dosya Güncelleme tamamlandı.");
                            btnSave.Enabled = false;
                            btnCancel.Text = "Kapat";
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Dosya Güncelleme tamamlanamadı.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme tamamlandı.");
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmFile", "Save");
            }
        }

        #endregion


        #region [ Cancel method ]

        void Cancel() { this.Close(); }

        #endregion

    }
}
