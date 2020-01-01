using System;

namespace FileStoreAdmin.Source.ViewModel
{
    class TransactionViewModel
    {
        public string UserFullName { get; set; }

        public DateTime LogTime { get; set; }

        public string TableName { get; set; }

        public string Entity { get; set; }

        public string TransactionType { get; set; }
    }
}
