using FileStoreAdmin.Source.Contexting;
using FileStoreAdmin.Source.Variables;
using FileStoreAdmin.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStoreAdmin.Source.Management
{
    class LogWatchManager
    {
        public List<LogViewModel> LogList()
        {
            List<LogViewModel> logs = new List<LogViewModel>();
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    logs = (from l in db.LogEntries
                            select
                                new LogViewModel()
                                {
                                    UserFullName = l.User != null ? l.User.FirstName + " " + l.User.LastName : "NULL",
                                    LogTime = l.LogTime,
                                    Title = l.Title,
                                    LogCode = l.ErrorCode,
                                    Message = l.Message,
                                    StackTrace = l.StackTrace
                                }).ToList<LogViewModel>();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return logs;
        }


        public List<TransactionViewModel> TransactionList()
        {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    transactions = (from l in db.TransactionLogs
                                    orderby l.LogTime descending
                                    select
                                        new TransactionViewModel()
                                        {
                                            UserFullName = l.User != null ? l.User.FirstName + " " + l.User.LastName : "NULL",
                                            LogTime = l.LogTime,
                                            TableName = l.TableName,
                                            TransactionType = l.TransactionType,
                                            Entity = l.EntityId.ToString()
                                        }).ToList<TransactionViewModel>();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return transactions;
        }

        public List<TransactionViewModel> LastTransactionList()
        {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            try
            {
                using (FileStoreModel db = new FileStoreModel())
                {
                    transactions = (from l in db.TransactionLogs
                                    orderby l.LogTime descending
                                    select
                                        new TransactionViewModel()
                                        {
                                            UserFullName = l.User != null ? l.User.FirstName + " " + l.User.LastName : "NULL",
                                            LogTime = l.LogTime,
                                            TableName = l.TableName,
                                            TransactionType = l.TransactionType,
                                            Entity = l.EntityId.ToString()
                                        }).Take(AppVariables.PageItemCount).ToList<TransactionViewModel>();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return transactions;
        }
    }
}
