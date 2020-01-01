using FileStoreAdmin.Source.Extensions;
using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.MessageUtil;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace FileStoreAdmin.Source.Views.Users
{
    public partial class FrmUserList : Form
    {
        UserManager userMan;
        List<UserViewModel> userList;

        public FrmUserList()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            userMan = new UserManager();
            userList = new List<UserViewModel>();
        }

        private void FrmUserList_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEntity();
        }
        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEntity();
        }

        private void updateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateEntity();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateEntity();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }

        private void recoverUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecoverEntity();
        }

        private void recoverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecoverEntity();
        }
        private void refreshListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        void LoadForm()
        {
            try
            {
                userList = userMan.UserList();
                LoadList2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "LoadForm");
                Messaging.Error("Kullanıcı listesi yüklenirken hata oluştu.");
            }
        }

        void LoadList2Grid()
        {
            try
            {
                grdUsers.DataSource = userList;
                grdUsers.PrepareGrid(Constants.UsersHeaderList, '-');
                grdUsers.Invalidate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        void AddEntity()
        {
            try
            {
                FrmUser frmUsr = new FrmUser();
                frmUsr.UserChanged += this.DelegatingUser;
                frmUsr.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "AddEntity");
                Messaging.Error("Kullanıcı eklenemedi.");
            }
        }

        void UpdateEntity()
        {
            try
            {
                if (grdUsers.SelectedRows.Count == 0)
                {
                    Messaging.Warning("Lütfen bir Kullanıcı seçin.");
                    return;
                }

                int id = grdUsers.SelectedRows[0].Cells["Id"].Value.ToInt();

                bool isDeleted = userMan.IsUserDeleted(id);

                if (isDeleted)
                {
                    Messaging.Warning("Silinen Kullanıcı düzenlenemez.");
                    return;
                }

                FrmUser frmUsr = new FrmUser(id);
                frmUsr.UserChanged += this.DelegatingUser;
                frmUsr.ShowDialog();

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "UpdateEntity");
                Messaging.Error("Kullanıcı güncellenemedi.");
            }
        }

        void DelegatingUser(UserSaveModel user, bool isInsert)
        {
            try
            {
                UserViewModel usr = null;
                if (isInsert)
                {
                    usr = new UserViewModel()
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        CreationDate = user.CreationDate,
                        TypeName = user.TypeName,
                        State = "Aktif",
                    };
                }
                else
                {
                    for (int counter = 0; counter < userList.Count; counter++)
                    {
                        if (userList[counter].Id == user.Id)
                        {
                            usr = userList[counter];
                            usr.FullName = user.FullName;
                            userList[counter] = usr;
                            break;
                        }
                    }
                }
                LoadList2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "DeleteEntity");
                Messaging.Error("Liste yenilenemedi.");
            }
        }

        void DeleteEntity()
        {
            try
            {
                if (grdUsers.SelectedRows.Count == 0)
                {
                    Messaging.Warning("Lütfen bir Kullanıcı seçin.");
                    return;
                }

                int id = grdUsers.SelectedRows[0].Cells["Id"].Value.ToInt();

                bool isDeleted = userMan.IsUserDeleted(id);

                if (isDeleted)
                {
                    Messaging.Warning("Kullanıcı Zaten Silinmiş.");
                    return;
                }

                DialogResult dr = Messaging.Confirm("Kullanıcı silinecektir. Emin misiniz?");

                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                isDeleted = userMan.DeleteUser(id);
                LogManager.LogTransaction("Users", AppVariables.UserId, id, TransactionTypes.Delete);

                if (isDeleted)
                {
                    SetStateOfUser(id, false);
                    Messaging.Info("Silme İşlemi başarılı.");
                }
                else
                {
                    Messaging.Info("Silme İşlemi başarısız.");
                }
                LoadList2Grid();
                return;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "DeleteEntity");
                Messaging.Error("Kullanıcı Silinirken hata oluştu.");
            }
        }

        void RecoverEntity()
        {
            try
            {
                if (grdUsers.SelectedRows.Count == 0)
                {
                    Messaging.Warning("Lütfen bir Kullanıcı seçin.");
                    return;
                }

                int id = grdUsers.SelectedRows[0].Cells["Id"].Value.ToInt();

                bool isDeleted = userMan.IsUserDeleted(id);

                if (isDeleted == false)
                {
                    Messaging.Warning("Kullanıcı Zaten Aktif.");
                    return;
                }

                DialogResult dr = Messaging.Confirm("Kullanıcı aktif edilecektir. Emin misiniz?");

                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                isDeleted = userMan.RecoverUser(id);
                LogManager.LogTransaction("Users", AppVariables.UserId, id, TransactionTypes.Recover);

                if (isDeleted)
                {
                    SetStateOfUser(id, true);
                    Messaging.Info("Geri alma İşlemi başarılı.");
                }
                else
                {
                    Messaging.Info("Geri alma İşlemi başarısız.");
                }
                LoadList2Grid();
                return;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "RecoverEntity");
                Messaging.Error("Kullanıcı geri alınırken hata oluştu.");
            }
        }

        void SetStateOfUser(int id, bool isActive)
        {
            try
            {
                UserViewModel uvm = null;
                for (int counter = 0; counter < userList.Count; counter++)
                {
                    if (userList[counter].Id == id)
                    {
                        uvm = userList[counter];
                        uvm.State = isActive ? "Aktif" : "Silinmiş";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void RefreshList()
        {
            try
            {
                userList = userMan.UserList();
                LoadList2Grid();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "FrmUserList", "RefreshList");
                Messaging.Error("Liste yenilenirken hata oluştu.");
            }
        }

        private void grdUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshList();
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                DeleteEntity();
                return;
            }

            if (e.Control)
            {

                if (e.KeyCode == Keys.E)
                {
                    AddEntity();
                    return;
                }

                if (e.KeyCode == Keys.D)
                {
                    UpdateEntity();
                    return;
                }

                if (e.KeyCode == Keys.Z)
                {
                    RecoverEntity();
                    return;
                }
            }
        }


    }
}