namespace ShoppingCart.Web.Helper
{
    public static class FileUpload
    {
        public static string UploadFile(this IWebHostEnvironment environment, IFormFile file)
        {
            string fileName = null;

            if (file != null)
            {
                string uploadDir = Path.Combine(environment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        public static void DeleteFile(this IWebHostEnvironment environment, string fileName)
        {
            if (fileName != null)
            {
                string filePath = Path.Combine(environment.WebRootPath, "images", fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        public static string UpdateFile(this IWebHostEnvironment environment, string oldFileName, IFormFile newFile)
        {
            // First: Delete the old file!
            DeleteFile(environment, oldFileName);

            // Then: Add the new file!
            return UploadFile(environment, newFile);
        }
    }
}
