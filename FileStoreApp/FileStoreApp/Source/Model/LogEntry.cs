namespace FileStoreApp.Source.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LogEntry")]
    public partial class LogEntry
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime LogTime { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(50)]
        public string ErrorCode { get; set; }

        [StringLength(100)]
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public virtual User User { get; set; }
    }
}