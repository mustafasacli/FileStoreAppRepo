using FileStoreApp.Source.Contexting;
using FileStoreApp.Source.Model;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Text;

namespace FileStoreApp.Source.Management
{
    class LogManager
    {

        #region [ LogException method ]

        public void LogException(Exception ex, string className, string methodName)
        {
            LogException(ex, 0, className, methodName);
        }

        #endregion


        #region [ LogException method ]

        public static void LogException(Exception ex, int userId, string className, string methodName)
        {
            LogEntry log = null;
            try
            {
                if (ex is DbEntityValidationException)
                {
                    DbEntityValidationException dex = ex as DbEntityValidationException;
                    foreach (DbEntityValidationResult error in dex.EntityValidationErrors)
                    {
                        foreach (DbValidationError validationError in error.ValidationErrors)
                        {
                            log = new LogEntry
                            {
                                LogTime = DateTime.Now,
                                Title = validationError.PropertyName,
                                Message = validationError.ErrorMessage,
                                ErrorCode = string.Format("ERR_{0}_{1}", className, methodName)
                            };
                            if (userId > 0)
                            {
                                log.UserId = userId;
                            }
                            LogException2Db(log);
                        }
                    }
                }
                else
                {
                    log = new LogEntry()
                    {
                        LogTime = DateTime.Now,
                        ErrorCode = string.Format("ERR_{0}_{1}", className, methodName),
                        Title = string.Format("An Exception handled at {1} method in {0}.", className, methodName)
                    };

                    if (userId > 0)
                    {
                        log.UserId = userId;
                    }

                    if (ex != null)
                    {
                        log.Message = ex.Message;
                        log.StackTrace = ex.StackTrace;
                    }

                    using (FileStoreModel db = new FileStoreModel())
                    {
                        db.LogEntries.Add(log);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException dex)
            {
                try
                {
                    log = new LogEntry
                    {
                        LogTime = DateTime.Now,
                        Title = "DbEntity",
                        ErrorCode = "Validation_Error",
                        Message = dex.Message,
                        StackTrace = dex.StackTrace
                    };
                    LogException2File(log);

                    foreach (DbEntityValidationResult error
                        in dex.EntityValidationErrors)
                    {
                        foreach (DbValidationError validationError
                            in error.ValidationErrors)
                        {
                            log = new LogEntry
                            {
                                LogTime = DateTime.Now,
                                Title = validationError.PropertyName,
                                Message = validationError.ErrorMessage
                            };
                            LogException2File(log);
                        }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception exc)
            {
                LogException2File(log);
                LogException2File(exc);
            }
        }

        #endregion


        #region [ LogException2Db method ]

        static void LogException2Db(LogEntry lg)
        {
            try
            {
                LogEntry log = new LogEntry()
                {
                    ErrorCode = lg.ErrorCode,
                    LogTime = lg.LogTime,
                    Message = lg.Message,
                    StackTrace = lg.StackTrace,
                    Title = lg.Title
                };
                if (lg.UserId > 0)
                {
                    log.UserId = lg.UserId;
                }

                using (FileStoreModel db = new FileStoreModel())
                {
                    db.LogEntries.Add(log);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LogException2File(lg);
                LogException2File(ex);
            }

        }

        #endregion


        #region [ LogTransaction method ]

        public static void LogTransaction(string tableName, int userId, int entityId, string transactionType)
        {
            try
            {
                TransactionLog transLog = new TransactionLog()
                {
                    LogTime = DateTime.Now,
                    TableName = tableName,
                    EntityId = entityId,
                    TransactionType = transactionType
                };

                if (userId > 0)
                {
                    transLog.UserId = userId;
                }

                using (FileStoreModel db = new FileStoreModel())
                {
                    db.TransactionLogs.Add(transLog);
                    db.SaveChanges();
                }

            }
            catch (DbEntityValidationException dex)
            {
                LogException2File(new LogEntry { Title = "DbEntity", ErrorCode = "Validation_Error", Message = dex.Message, StackTrace = dex.StackTrace });
                foreach (DbEntityValidationResult error
                    in dex.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError
                        in error.ValidationErrors)
                    {
                        LogException2File(new LogEntry
                        {
                            Title = validationError.PropertyName,
                            ErrorCode = validationError.ErrorMessage
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                LogException2File(exc);
            }
        }

        #endregion


        #region [ LogTransaction method ]

        /// <summary>
        /// Logs Transaction to Db.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="entityId"></param>
        /// <param name="transactionType"></param>
        public void LogTransaction(string tableName, int entityId, string transactionType)
        {
            try
            {
                TransactionLog transLog = new TransactionLog()
                {
                    LogTime = DateTime.Now,
                    TableName = tableName,
                    EntityId = entityId,
                    TransactionType = transactionType
                };

                using (FileStoreModel db = new FileStoreModel())
                {
                    db.TransactionLogs.Add(transLog);
                    db.SaveChanges();
                }

            }
            catch (DbEntityValidationException dex)
            {
                LogException2File(new LogEntry
                {
                    LogTime = DateTime.Now,
                    Title = "DbEntity",
                    ErrorCode = "Validation_Error",
                    Message = dex.Message,
                    StackTrace = dex.StackTrace
                });
                foreach (DbEntityValidationResult error
                    in dex.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError
                        in error.ValidationErrors)
                    {
                        LogException2File(new LogEntry
                        {
                            LogTime = DateTime.Now,
                            Title = validationError.PropertyName,
                            ErrorCode = validationError.ErrorMessage
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                LogException2File(exc);
            }
        }

        #endregion


        #region [ LogException2File method ]

        internal static void LogException2File(LogEntry log)
        {
            StringBuilder strBilder = new StringBuilder();
            try
            {
                if (log != null)
                {
                    string fullFilePath = getLogFile();
                    strBilder.AppendLine(string.Format("Exception Time: {0}", log.LogTime.ToString("yyyy.MM.dd HH:mm:ss fff")));
                    strBilder.AppendLine(string.Format("Title: {0}", log.Title));
                    strBilder.AppendLine(string.Format("Error Code: {0}", log.ErrorCode));
                    strBilder.AppendLine(string.Format("Message: {0}", log.Message));
                    strBilder.AppendLine(string.Format("StackTrace: {0}", log.StackTrace));
                    strBilder.AppendLine("*****************************************************");
                    using (StreamWriter sw = new StreamWriter(new FileStream(fullFilePath, FileMode.OpenOrCreate)))
                    {
                        sw.Write(strBilder.ToString());
                    }
                }
            }
            catch (Exception)
            {
            }
            strBilder = null;
        }

        #endregion


        #region [ LogException2File method ]

        internal static void LogException2File(Exception exc)
        {
            StringBuilder strBilder = new StringBuilder();
            try
            {
                string fullFilePath = getLogFile();
                strBilder.AppendLine(string.Format("Exception Time: {0}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss fff")));
                strBilder.AppendLine(string.Format("Message: {0}", exc.Message));
                strBilder.AppendLine(string.Format("StackTrace: {0}", exc.StackTrace));
                strBilder.AppendLine("*****************************************************");
                FileMode fMode = File.Exists(fullFilePath) ? FileMode.Append : FileMode.OpenOrCreate;
                using (StreamWriter sw = new StreamWriter(new FileStream(fullFilePath, fMode)))
                {
                    sw.Write(strBilder.ToString());
                }

                if (exc.InnerException != null)
                {
                    LogException2File(exc.InnerException);
                }

            }
            catch (Exception)
            {
            }
            strBilder = null;
        }

        #endregion


        #region [ getLogFile method ]

        static string getLogFile()
        {

            string fullLogFilePath = string.Empty;
            try
            {
                string errorFile = string.Format("error_{0}.log", DateTime.Now.Date.ToString("yyyy_MM_dd"));
                string logFilePath = AppDomain.CurrentDomain.BaseDirectory;

                fullLogFilePath = string.Format("{0}\\{1}", logFilePath, errorFile);
            }
            catch (Exception)
            {
            }

            return fullLogFilePath;
        }

        #endregion

    }
}