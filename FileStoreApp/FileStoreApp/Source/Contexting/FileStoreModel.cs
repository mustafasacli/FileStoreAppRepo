namespace FileStoreApp.Source.Contexting
{
    using FileStoreApp.Source.Model;
    using System.Data.Entity;

    public partial class FileStoreModel : DbContext
    {
        public FileStoreModel()
            : base("name=FileStoreModel")
        {
        }

        public virtual DbSet<LogEntry> LogEntries { get; set; }

        public virtual DbSet<PersonalFile> PersonalFiles { get; set; }

        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonalFiles)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserType1)
                .HasForeignKey(e => e.UserType)
                .WillCascadeOnDelete(false);
        }
    }
}