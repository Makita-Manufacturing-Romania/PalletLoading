using Microsoft.AspNetCore.Http;
using PalletLoading.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using System.Linq;

namespace PalletLoading.Services
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public async Task<bool> UploadFile(IFormFile file, string path)
        {
            bool useImageConvert = true;

            try
            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = file.FileName;


                    string[] array = fileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                    string fileType = (array.Last()).ToLower();

                    if ((fileType == "png" || fileType == "jpeg" || fileType == "jpg") && useImageConvert)
                    {
                        try
                        {
                            using (var stream = file.OpenReadStream())
                            using (MagickImage image = new MagickImage(stream))
                            {
                                double newPercentage = 1;

                                // Get the file size in bytes
                                long fileSizeBytes = file.Length;

                                // Determine the new percentage based on your conditions
                                if ((image.Width + image.Height) > 8000 || (fileSizeBytes > 10000000))
                                {
                                    newPercentage = 0.125;
                                }
                                else if ((image.Width + image.Height) > 4000 || (fileSizeBytes > 1000000))
                                {
                                    newPercentage = 0.25;
                                }
                                else if ((image.Width + image.Height) > 3000 || (fileSizeBytes > 600000))
                                {
                                    newPercentage = 0.5;
                                }

                                if (newPercentage < 1)
                                {
                                    int newWidth = (int)(image.Width * newPercentage);
                                    int newHeight = (int)(image.Height * newPercentage);

                                    image.Resize(newWidth, newHeight);
                                }

                                //// Combine the destination folder path and the original file name
                                string destinationFilePath = Path.Combine(path, Path.GetFileNameWithoutExtension(file.FileName)) + "." + fileType;

                                // Set the format to WebP explicitly
                                //image.Format = MagickFormat.WebP;

                                //// Save the image in WebP format
                                try
                                {
                                    image.Write(destinationFilePath);
                                }
                                catch (Exception ex)
                                {
                                    //jsonData["Error2"] = "Eroare " + ex.ToString();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            //badImagesList.Add(Path.GetFileNameWithoutExtension(formFile.FileName));
                            //jsonData["Error3"] = "Eroare " + ex.ToString();
                            ////LEAVE MESSAGE WITH DAMAGED IMAGES
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
