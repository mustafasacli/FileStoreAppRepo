using FileStoreApp.Source.Contexting;
using FileStoreApp.Source.Model;
using FileStoreApp.Source.Variables;
using FileStoreApp.Source.ViewModel;
using System;
using System.Linq;

namespace FileStoreApp.Source.Management
{
    internal class UserManager
    {
        #region [ Login method ]

        internal User Login(LoginModel model)
        {
            User result = null;

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from u in db.Users
                              where (string.Equals(u.UserName, model.UserName) &&
                                  string.Equals(u.Pass, model.Pass) && u.IsActive == true)
                              select u).FirstOrDefault<User>();
                }

                if (result != null)
                {
                    result.Pass = string.Empty;
                    if (result.Id == 0)
                    {
                        result = null;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                LogManager.LogException(ex, AppVariables.UserId, "UserManager", "Login");
            }

            return result;
        }

        #endregion [ Login method ]
    }
}