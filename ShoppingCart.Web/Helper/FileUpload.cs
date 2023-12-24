﻿namespace ShoppingCart.Web.Helper
{
    public class FileUpload
    {
        private readonly IWebHostEnvironment _environment;

        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string UploadFile(IFormFile file)
        {
            string fileName = null;

            if (file != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}