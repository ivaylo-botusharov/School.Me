namespace School.Web.Areas.Students.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;
    using School.Models;
    using School.Web.Areas.Students.Models.AccountViewModels;

    public class StudentRegisterSubmitModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not be longer than {1} symbols.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }

        public void UploadProfilePhoto(string uploadDirectory)
        {
            if (this.RegisterViewModel.ImageUpload != null && this.RegisterViewModel.ImageUpload.ContentLength > 0)
            {
                //string imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);

                string extension = Path.GetExtension(this.RegisterViewModel.ImageUpload.FileName);

                string imageFileName = string.Format(
                    "{0}-{1}{2}",
                    this.RegisterViewModel.UserName,
                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), extension);

                string fullDirectoryPath = HttpContext.Current.Server.MapPath(uploadDirectory);

                DirectoryInfo directory = Directory.CreateDirectory(fullDirectoryPath);

                string imagePath = Path.Combine(fullDirectoryPath, imageFileName);
                this.RegisterViewModel.ImageUrl = uploadDirectory + "/" + imageFileName;
                this.RegisterViewModel.ImageUpload.SaveAs(imagePath);
            }
        }
    }
}