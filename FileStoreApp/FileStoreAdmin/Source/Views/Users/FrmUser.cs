using FileStoreAdmin.Source.Extensions;
using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.MessageUtil;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.Views.Users
{
    public partial class FrmUser : Form
    {

        private int _id = -1;
        private UserManager userMan;
        private UserSaveModel user;
        private bool isFormLoaded = false;
        private bool isSaved = false;

        public delegate void UserAddOrUpdated(UserSaveModel user, bool isInsert);
        public UserAddOrUpdated UserChanged;

        public FrmUser()
            : this(-1)
        { }

        public FrmUser(int id)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            isFormLoaded = false;
            _id = id;
            user = new UserSaveModel();
            userMan = new UserManager();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void LoadForm()
        {
            try
            {
                btnSave.Enabled = _id > 0;

                cmbxUserType.LoadKeyValueList2Combo(userMan.GetUserTypes());

                if (_id > 0)
                {
                    user = userMan.GetUser(_id);
                    if (user != null)
                    {
                        txtFirstName.Text = user.FirstName;
                        txtLastName.Text = user.LastName;
                        //txtUserName.Text = user.UserName;
                        cmbxUserType.SelectedValue = user.UserTypeId;
                    }
                }
            }
            catch (Exception ex)
            {
                Messaging.Error("Kullanıcı ekranı yüklenirken hata oluştu.");
                LogManager.LogException(ex, AppVariables.UserId, "FrmUser", "LoadForm");
            }
            finally
            {
                if (_id > 0)
                {
                    txtUserName.Enabled = false;
                    cmbxUserType.Enabled = false;
                }

                isFormLoaded = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_id == -1)
                {
                    if (!txtPass.Text.IsEqual(txtPassAgain.Text))// && (txtPass.Text.Length > 0 || txtPassAgain.Text.Length > 0))
                    {
                        Messaging.Warning("Şifreler aynı değil.");
                        return;
                    }
                    else
                    {
                        if (user.UserTypeId < 1)
                        {
                            Messaging.Warning("Lütfen kullanıcı tipi seçiniz.");
                            return;
                        }

                        if (user.UserName.LengthOfString() < 1)
                        {
                            Messaging.Warning("Lütfen kullanıcı adı giriniz.");
                            return;
                        }

                        if (txtPass.Text.LengthOfString() < 1)
                        {
                            Messaging.Warning("Lütfen Şifre giriniz.");
                            return;
                        }

                        user.Pass = txtPass.Text;
                        user.CreationDate = DateTime.Now;
                        int insertId = userMan.AddUser(user);
                        isSaved = true;
                        btnSave.Disable();
                        LogManager.LogTransaction("Users", AppVariables.UserId, insertId, TransactionTypes.Insert);
                        if (UserChanged != null)
                        {
                            UserChanged(user, true);
                        }
                    }
                }
                else
                {
                    if (!txtPass.Text.IsEqual(txtPassAgain.Text))// && (txtPass.Text.Length > 0 || txtPassAgain.Text.Length > 0))
                    {
                        Messaging.Warning("Şifreler aynı değil.");
                        return;
                    }
                    else
                    {
                        if (txtPass.Text.LengthOfString() > 0)
                        {
                            user.Pass = txtPass.Text;
                        }

                        userMan.UpdateUser(user);
                        isSaved = true;
                        btnSave.Disable();
                        LogManager.LogTransaction("Users", AppVariables.UserId, user.Id, TransactionTypes.Update);
                        if (UserChanged != null)
                        {
                            UserChanged(user, false);
                        }
                    }
                }
                btnCancel.Text = "Kapat";
            }
            catch (Exception ex)
            {
                Messaging.Error("Kullanıcı kaydedilirken hata oluştu.");
                LogManager.LogException(ex, AppVariables.UserId, "FrmUser", "LoadForm");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                this.Close();
            }
            else
            {
                DialogResult dr = Messaging.Confirm("İşlemi iptal etmek istediğinizden emin misiniz?");

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {
                user.FirstName = txtFirstName.Text;
                SetEnableStateOfSaveButton();
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {
                user.LastName = txtLastName.Text;
                SetEnableStateOfSaveButton();
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {
                user.UserName = txtUserName.Text;
                SetEnableStateOfSaveButton();
            }
        }

        private void cmbxUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {
                user.UserTypeId = cmbxUserType.SelectedValue.ToInt();
                user.TypeName = cmbxUserType.GetSelectedText();
            }
        }

        void SetEnableStateOfSaveButton()
        {
            btnSave.Enabled = (!isSaved) && txtFirstName.Text.Length > 0 && txtLastName.Text.Length > 0;
        }
    }
}
