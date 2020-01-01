namespace FileStoreAdmin.Source.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        public User()
        {
            LogEntries = new HashSet<LogEntry>();
            PersonalFiles = new HashSet<PersonalFile>();
            TransactionLogs = new HashSet<TransactionLog>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Pass { get; set; }

        public int UserType { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }

        public virtual ICollection<PersonalFile> PersonalFiles { get; set; }

        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }

        public virtual UserType UserType1 { get; set; }
    }
}