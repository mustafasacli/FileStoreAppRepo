using FileStoreAdmin.Source.Contexting;
using FileStoreAdmin.Source.Model;
using FileStoreAdmin.Source.Util;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStoreAdmin.Source.Management
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

        internal List<UserViewModel> UserList()
        {
            List<UserViewModel> result = null;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from u in db.Users
                              where (u.UserType != 1)
                              select new UserViewModel()
                              {
                                  Id = u.Id,
                                  CreationDate = u.CreationDate,
                                  TypeName = u.UserType1 != null ? u.UserType1.TypeName : "Null",
                                  FullName = u.FirstName + " " + u.LastName,
                                  State = u.IsActive ? "Aktif" : "Silinmiş"
                              }).ToList<UserViewModel>();
                }

                if (result == null)
                {
                    result = new List<UserViewModel>();
                }
            }
            catch (Exception ex)
            {
                result = null;
                LogManager.LogException(ex, AppVariables.UserId, "UserManager", "Login");
            }
            return result;
        }

        public UserSaveModel GetUser(int id)
        {
            UserSaveModel user = null;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    user = (from u in db.Users
                            where (u.IsActive == true && u.Id == id)
                            select new UserSaveModel()
                              {
                                  Id = u.Id,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  UserName = u.UserName,
                                  UserTypeId = u.UserType,
                                  CreationDate = u.CreationDate,
                                  TypeName = u.UserType1 != null ? u.UserType1.TypeName : "Null"
                              }).FirstOrDefault<UserSaveModel>();
                }

                if (user.Id == 0)
                {
                    user = null;
                }

            }
            catch (Exception ex)
            {
                user = null;
                LogManager.LogException(ex, AppVariables.UserId, "UserManager", "GetUser");
            }
            return user;
        }

        public int AddUser(UserSaveModel user)
        {
            int result = -1;
            try
            {
                User usr = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Pass = SecurityHelper.EncodeString(user.Pass),
                    UserName = user.UserName,
                    UserType = user.UserTypeId,
                    IsActive = true
                };
                usr.CreationDate = user.CreationDate == (new DateTime()) ? DateTime.Now : user.CreationDate;

                using (FileStoreModel db = new FileStoreModel())
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                    result = usr.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool UpdateUser(UserSaveModel user)
        {
            bool result = false;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    User usr = null;
                    usr = db.Users.Find(user.Id);
                    if (usr != null)
                    {
                        usr.FirstName = user.FirstName;
                        usr.LastName = user.LastName;
                        if (string.Format("{0}", user.Pass).Length > 0)
                        {
                            usr.Pass = SecurityHelper.EncodeString(user.Pass);
                        }

                        db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public List<KeyValueObject> GetUserTypes()
        {
            List<KeyValueObject> result = null;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from ut in db.UserTypes
                              where (ut.IsActive && ut.Id > 1)
                              select new KeyValueObject()
                              {
                                  Value = ut.Id.ToString(),
                                  Text = ut.TypeName
                              }
                              ).ToList<KeyValueObject>();
                }

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, AppVariables.UserId, "UserManager", "GetUserTypes");
            }

            if (result == null)
                result = new List<KeyValueObject>();

            return result;
        }

        public bool IsUserDeleted(int id)
        {
            bool result = false;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    int count = db.Users.AsQueryable().Where(u => u.Id == id && u.IsActive == false).Count();
                    result = count > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool DeleteUser(int id)
        {
            bool result = false;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    User usr = null;
                    usr = db.Users.Find(id);
                    if (usr != null)
                    {
                        usr.IsActive = false;
                        db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool RecoverUser(int id)
        {
            bool result = false;
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    User usr = null;
                    usr = db.Users.Find(id);
                    if (usr != null)
                    {
                        usr.IsActive = true;
                        db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}