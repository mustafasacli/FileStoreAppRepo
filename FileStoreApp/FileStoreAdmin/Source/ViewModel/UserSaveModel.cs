using System;

namespace FileStoreAdmin.Source.ViewModel
{
    public class UserSaveModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int UserTypeId { get; set; }

        public string Pass { get; set; }

        public string PassAgain { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string TypeName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}