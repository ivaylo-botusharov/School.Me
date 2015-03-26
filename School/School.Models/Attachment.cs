namespace School.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string RelativeFilePath { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public int FileSize { get; set; }
    }
}
