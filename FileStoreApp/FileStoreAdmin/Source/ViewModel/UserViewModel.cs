using System;

namespace FileStoreAdmin.Source.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TypeName { get; set; }

        public DateTime CreationDate { get; set; }

        public string State { get; set; }
    }
}