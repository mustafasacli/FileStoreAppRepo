using System;

namespace FileStoreAdmin.Source.ViewModel
{
    public class LogViewModel
    {
        public string UserFullName { get; set; }

        public DateTime LogTime { get; set; }

        public string Title { get; set; }

        public string LogCode { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}