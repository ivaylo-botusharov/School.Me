namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;

    public class StudentDetailsEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name must not be more than 100 characters.")]
        public string Name { get; set; }

        [Range(5, 100)]
        public int? Age { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }

        public void UploadProfilePhoto(string uploadDirectory)
        {
            if (this.AccountDetailsEditModel.ImageUpload != null && this.AccountDetailsEditModel.ImageUpload.ContentLength > 0)
            {
                //string imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);

                string extension = Path.GetExtension(this.AccountDetailsEditModel.ImageUpload.FileName);

                string imageFileName = string.Format(
                    "{0}-{1}{2}",
                    this.AccountDetailsEditModel.UserName,
                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), extension);

                string fullDirectoryPath = HttpContext.Current.Server.MapPath(uploadDirectory);

                DirectoryInfo directory = Directory.CreateDirectory(fullDirectoryPath);

                string imagePath = Path.Combine(fullDirectoryPath, imageFileName);
                this.AccountDetailsEditModel.ImageUrl = uploadDirectory + "/" + imageFileName;
                this.AccountDetailsEditModel.ImageUpload.SaveAs(imagePath);
            }
        }
    }
}