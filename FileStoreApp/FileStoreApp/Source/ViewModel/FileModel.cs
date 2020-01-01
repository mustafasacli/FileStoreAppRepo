using System;

namespace FileStoreApp.Source.ViewModel
{
    public class FileModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string CreatedUser { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }
    }
}