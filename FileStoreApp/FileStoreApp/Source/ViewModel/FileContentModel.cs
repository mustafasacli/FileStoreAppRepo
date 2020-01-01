using System;
namespace FileStoreApp.Source.ViewModel
{
    public class FileContentModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }


        public string FileExtension
        {
            get
            {
                string result = string.Empty;

                try
                {
                    string fileName = string.Format("{0}", this.FileName);
                    int indx = fileName.LastIndexOf('.');

                    if (indx != -1)
                    {
                        result = fileName.Substring(indx, fileName.Length - indx);
                    }

                }
                catch (Exception)
                {
                }

                return result;
            }
        }

    }
}