using FileStoreApp.Source.Contexting;
using FileStoreApp.Source.Model;
using FileStoreApp.Source.Variables;
using FileStoreApp.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStoreApp.Source.Management
{
    internal class FileManager
    {
        #region [ Private Fields ]

        //  private FileStoreModel db = new FileStoreModel();

        #endregion [ Private Fields ]


        #region [ Files method ]

        internal List<FileModel> Files()
        {
            List<FileModel> result = new List<FileModel>();

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from p in db.PersonalFiles
                              join u in db.Users on p.CreatedBy equals u.Id
                              where p.IsActive == true
                              select new FileModel
                              {
                                  Id = p.Id,
                                  FileName = p.FileName,
                                  CreationDate = p.CreationDate,
                                  CreatedUser = u.FirstName + " " + u.LastName,
                                  //CreatedUser = p.User == null ? "NULL" : string.Format("{0} {1}", p.User.FirstName, p.User.LastName),
                                  CreatedBy = p.CreatedBy
                              }).ToList<FileModel>();
                }

                if (result == null)
                    result = new List<FileModel>();

                result = result.OrderByDescending(r => r.CreationDate).ToList<FileModel>();
            }
            catch (Exception ex)
            {
                result = null;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "Files");
            }

            return result;
        }

        #endregion [ Files method ]


        #region [ GetFileById method ]

        internal FileContentModel GetFileById(int fileId)
        {
            FileContentModel result = new FileContentModel();

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from p in db.PersonalFiles
                              where p.IsActive == true && p.Id == fileId
                              select new FileContentModel { Id = fileId, FileName = p.FileName, Content = p.FileContent }).FirstOrDefault<FileContentModel>();
                }

                if (result == null)
                {
                    result = new FileContentModel();
                }

                if (result.Content == null)
                {
                    result.Content = new byte[] { };
                }
            }
            catch (Exception ex)
            {
                result = null;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "GetFileById");
            }

            return result;
        }

        #endregion [ GetFileById method ]


        #region [ GetPersonalFileById method ]

        internal PersonalFile GetPersonalFileById(int fileId)
        {
            PersonalFile result = new PersonalFile();

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    result = (from p in db.PersonalFiles
                              where p.IsActive == true && p.Id == fileId
                              select p).FirstOrDefault<PersonalFile>();
                }

                if (result == null)
                {
                    result = new PersonalFile();
                }

                if (result.FileContent == null)
                {
                    result.FileContent = new byte[] { };
                }
            }
            catch (Exception ex)
            {
                result = null;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "GetPersonalFileById");
            }

            return result;
        }

        #endregion [ GetPersonalFileById method ]


        #region [ AddFile method ]

        internal bool AddFile(PersonalFile file)
        {
            bool result = false;

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    db.PersonalFiles.Add(file);
                    db.SaveChanges();
                }
                LogManager.LogTransaction("PersonalFile", AppVariables.UserId, file.Id, TransactionTypes.Insert);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "AddFile");
            }

            return result;
        }

        #endregion [ AddFile method ]


        #region [ UpdateFile method ]

        internal bool UpdateFile(PersonalFile file)
        {
            bool result = false;

            try
            {
                if (file != null)
                {
                    using (FileStoreModel db = new FileStoreModel())
                    {
                        PersonalFile pf = null;
                        pf = db.PersonalFiles.Find(file.Id);
                        if (pf != null)
                        {
                            pf.FileName = file.FileName;
                            pf.FileContent = file.FileContent;

                            db.Entry(pf).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();
                            result = true;
                        }
                    }
                }

                if (result)
                {
                    LogManager.LogTransaction("PersonalFile", AppVariables.UserId, file.Id, TransactionTypes.Update);
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "UpdateFile");
            }

            return result;
        }

        #endregion [ UpdateFile method ]


        #region [ DeleteFile method ]

        internal bool DeleteFile(int fileId)
        {
            bool result = false;

            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    PersonalFile pf = db.PersonalFiles.Find(fileId);
                    if (pf != null)
                    {
                        pf.IsActive = false;
                        db.Entry(pf).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                LogManager.LogTransaction("PersonalFile", AppVariables.UserId, fileId, TransactionTypes.Delete);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                LogManager.LogException(ex, AppVariables.UserId, "FileManager", "DeleteFile");
            }

            return result;
        }

        #endregion [ DeleteFile method ]

    }
}