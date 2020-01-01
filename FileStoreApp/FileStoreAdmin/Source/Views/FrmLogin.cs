using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.Model;
using FileStoreAdmin.Source.Util;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.Views
{
    public partial class FrmLogin : Form
    {
        #region [ Private Fields ]

        private readonly UserManager userMan;

        #endregion [ Private Fields ]


        #region [ FrmLogin Ctors ]

        public FrmLogin()
        {
            InitializeComponent();
            userMan = new UserManager();
        }

        #endregion [ FrmLogin Ctors ]


        #region [ btnLogin_Click method ]

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginMethod();
        }

        #endregion [ btnLogin_Click method ]


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
                    txtUser.Focus();
                    return;
                }
                else
                {
                    AppVariables.UserId = usr.Id;
                    AppVariables.UserName = usr.UserName;

                    LogManager.LogTransaction("Users", AppVariables.UserId, -1, TransactionTypes.Login);
                    Program.isLogin = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı ve Şifreyi doğru giriniz.");
            }
        }

        #endregion [ LoginMethod method ]


        #region [ btnCancel_Click method ]

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion [ btnCancel_Click method ]

    }
}