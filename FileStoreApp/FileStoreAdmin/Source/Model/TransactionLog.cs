namespace FileStoreAdmin.Source.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TransactionLog")]
    public partial class TransactionLog
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime LogTime { get; set; }

        [Required]
        [StringLength(50)]
        public string TableName { get; set; }

        public int? EntityId { get; set; }

        [StringLength(50)]
        public string TransactionType { get; set; }

        public virtual User User { get; set; }
    }
}