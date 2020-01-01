using System;
using System.Configuration;
using FileStoreAdmin.Source.Extensions;

namespace FileStoreAdmin.Source.Variables
{
    internal static class AppVariables
    {
        private static int _userId = 0;

        /// <summary>
        /// Gets, Sets User Id.
        /// </summary>
        internal static int UserId
        {
            get { return AppVariables._userId; }
            set { AppVariables._userId = value; }
        }

        private static string _userName = string.Empty;

        internal static string UserName
        {
            get { return AppVariables._userName; }
            set { AppVariables._userName = value; }
        }

        public static int PageItemCount
        {
            get
            {
                int pageItemCount = 20;

                try
                {
                    pageItemCount = ConfigurationManager.AppSettings["pageItemCount"].ToInt();

                    if (pageItemCount < 20)
                        pageItemCount = 20;


                    if (pageItemCount > 100)
                        pageItemCount = 100;

                }
                catch (Exception)
                {
                    pageItemCount = 20;
                }

                return pageItemCount;
            }
        }

        public static int TimerInterval
        {
            get
            {
                //timerInterval
                int timerInterval = 180;

                try
                {
                    timerInterval = ConfigurationManager.AppSettings["timerInterval"].ToInt();

                    if (timerInterval < 180)
                        timerInterval = 180;

                    if (timerInterval > 1800)
                        timerInterval = 1800;

                }
                catch (Exception)
                {
                    timerInterval = 180;
                }

                return timerInterval;
            }
        }
    }
}