using FileStoreAdmin.Source.Contexting;
using FileStoreAdmin.Source.Management;
using FileStoreAdmin.Source.Model;
using FileStoreAdmin.Source.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FileStoreAdmin.Source.Initiliazing
{
    public class FileStoreModelInitiliazer : DropCreateDatabaseAlways<FileStoreModel>
    {
        protected override void Seed(FileStoreModel context)
        {
            //base.Seed(context);
            try
            {
                List<UserType> usrtypeList = new List<UserType>()
                {
                    new UserType{Id=1,TypeName="Yonetici",IsActive=true},
                    new UserType{Id=2,TypeName="Avukat",IsActive=true},
                    new UserType{Id=3,TypeName="Stajyer",IsActive=true},
                    new UserType{Id=4,TypeName="Katip",IsActive=true}
                };

                usrtypeList.ForEach(u => context.UserTypes.Add(u));
                context.SaveChanges();

                User usr = new User()
                {
                    Id = 1,
                    FirstName = "Mustafa",
                    LastName = "Saçlı",
                    UserName = "mustiscl",
                    Pass = SecurityHelper.EncodeString("mst123"),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    UserType = 1
                };

                context.Users.Add(usr);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, 0, "FileStoreModelInitiliazer", "Seed");
            }
        }
    }
}