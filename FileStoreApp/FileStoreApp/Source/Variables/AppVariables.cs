namespace FileStoreApp.Source.Variables
{
    internal static class AppVariables
    {
        private static int _userId = 0;

        /// <summary>
        /// Gets, Sets User Id.
        /// </summary>
        internal static int UserId
        {
            get { return AppVariables._userId; }
            set { AppVariables._userId = value; }
        }

        private static string _userName = string.Empty;

        internal static string UserName
        {
            get { return AppVariables._userName; }
            set { AppVariables._userName = value; }
        }
    }
}