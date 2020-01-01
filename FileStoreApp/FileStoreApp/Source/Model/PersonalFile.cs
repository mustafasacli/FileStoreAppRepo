namespace FileStoreApp.Source.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class PersonalFile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }

    }
}