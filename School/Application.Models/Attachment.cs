using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
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
