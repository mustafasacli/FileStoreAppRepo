using FileStoreApp.Source.Management;
using FileStoreApp.Source.Model;
using FileStoreApp.Source.Util;
using FileStoreApp.Source.Variables;
using FileStoreApp.Source.ViewModel;
using System;
using System.Windows.Forms;

namespace FileStoreApp.Source.Views
{
    internal partial class FrmLogin : Form
    {

        #region [ Private Fields ]

        private readonly UserManager userMan;

        #endregion


        #region [ FrmLogin Ctors ]

        public FrmLogin()
        {
            InitializeComponent();
            userMan = new UserManager();
        }

        #endregion


        #region [ btnLogin_Click method ]

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginMethod();
        }

        #endregion


        #region [ LoginMethod method ]

        private void LoginMethod()
        {
            if (txtUser.Text.Length > 0 && txtPass.Text.Length > 0)
            {
                LoginModel loginMdl = new LoginModel
                {
                    UserName = txtUser.Text,
                    Pass = SecurityHelper.EncodeString(txtPass.Text)
                };
                User usr = userMan.Login(loginMdl);

                if (usr == null)
                {
                    txtUser.ResetText();
                    txtPass.ResetText();
                    lblLoginInfo.Visible = true;
                    return;
                }
                else
                {
                    LogManager.LogTransaction("Users", AppVariables.UserId, -1, TransactionTypes.Login);

                    AppVariables.UserId = usr.Id;
                    AppVariables.UserName = usr.UserName;
                    Program.isLogin = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı ve Şifreyi doğru giriniz.");
            }
        }

        #endregion


        #region [ btnCancel_Click method ]

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}