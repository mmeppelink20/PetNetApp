using DataAccessLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DataObjects;

namespace DataAccessLayer
{
    public class ImagesAccessor : IImagesAccessor
    {
        private static readonly ImageFormat imageFormat = ImageFormat.Png;
        private static readonly int maxImageWidth = 720;
        private static readonly int maxImageHeight = 720;

        public ImagesAccessor()
        {
            Directory.CreateDirectory(DataPathInformation.ImagePath);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/25
        /// 
        /// Method to resize the selected image to a reasonable size based on the static maxImageWidth and maxImageHeight variables;
        /// Based off of https://www.c-sharpcorner.com/UploadFile/ishbandhu2009/resize-an-image-in-C-Sharp/
        /// </summary>
        /// <param name="image">The image to resize</param>
        /// <returns>The resized image</returns>
        private Image ResizeImage(Image image)
        {
            double imageScale;
            double horizontalImageScale = (double)maxImageWidth / image.Width;
            double verticalImageScale = (double)maxImageHeight / image.Height;
            imageScale = horizontalImageScale < verticalImageScale ? horizontalImageScale : verticalImageScale;
            if (imageScale > 1)
            {
                imageScale = 1;
            }
            int newWidth = (int)(image.Width * imageScale);
            int newHeight = (int)(image.Height * imageScale);
            Image newImage = new Bitmap(newWidth, newHeight);
            Graphics graphics = Graphics.FromImage(newImage);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            graphics.Dispose();

            return newImage;
        }
        public Images InsertImageByUri(string imageUri)
        {
            return InsertImageByUriAndLink(imageUri, null);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/25
        /// 
        /// Takes the image at the selected Uri, resizes it and saves it to the image folder in the base directory,
        /// adds the image Guid and original file name to the database, creates the join record using the additional command
        /// and returns an images object with the file name and Id that was assigned to it
        /// </summary>
        /// <param name="imageUri">The full path where the image is located</param>
        /// <param name="joinTableLambda">This is an action that allows you to use the connection, transaction, and imageId created in this method to extend its functionality</param>
        /// <returns></returns>
        private Images InsertImageByUriAndLink(string imageUri, Action<SqlConnection, SqlTransaction, string> joinTableLambda)
        {
            Images images = null;
            string imageGuid = Guid.NewGuid().ToString();
            string imageName = imageUri.Substring((imageUri.LastIndexOf("\\") > 0 ? imageUri.LastIndexOf("\\") : imageUri.LastIndexOf("/")) + 1);
            SqlConnection conn = new DBConnection().GetConnection();
            SqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("sp_insert_image", conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36).Value = imageGuid;
                cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar, 50).Value = imageName;

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new ApplicationException("Failed to insert the image Id into the database");
                }

                // once the record is saved execute the optional command if its not null
                joinTableLambda?.Invoke(conn, trans, imageGuid);
                LoadResizeAndSaveImage(imageUri, imageGuid);

                images = new Images() { ImageId = imageGuid, ImageFileName = imageName };
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return images;
        }

        private void LoadResizeAndSaveImage(string imageUri, string imageGuid)
        {
            Image image = Image.FromFile(imageUri);
            image = ResizeImage(image);
            image.Save(DataPathInformation.ImagePath + imageGuid + ".png", imageFormat);
        }

        public List<Images> InsertImagesByUris(IEnumerable<string> imageUris)
        {
            return InsertImagesByUrisAndLink(imageUris, null);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Takes the images at the selected Uris, resizes them and saves them to the image folder in the base directory,
        /// adds the images Guids and original file names to the database, creates the join records using the addiotional command
        /// and returns a list of images objects with the file names and Ids that were assigned to them
        /// </summary>
        /// <param name="imageUris"></param>
        /// <param name="joinTableLambda"></param>
        /// <returns>List of images objects</returns>
        private List<Images> InsertImagesByUrisAndLink(IEnumerable<string> imageUris, Action<SqlConnection, SqlTransaction, string> joinTableLambda)
        {
            List<Images> images = new List<Images>();
            SqlConnection conn = new DBConnection().GetConnection();
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                foreach (string imageUri in imageUris)
                {
                    string imageGuid = Guid.NewGuid().ToString();
                    string imageName = imageUri.Substring((imageUri.LastIndexOf("\\") > 0 ? imageUri.LastIndexOf("\\") : imageUri.LastIndexOf("/")) + 1);

                    SqlCommand cmd = new SqlCommand("sp_insert_image", conn, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36).Value = imageGuid;
                    cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar, 50).Value = imageName;

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        throw new ApplicationException("Failed to insert the image Id into the database");
                    }

                    // once the record is saved execute the optional command if its not null
                    joinTableLambda?.Invoke(conn, trans, imageGuid);

                    LoadResizeAndSaveImage(imageUri, imageGuid);
                    images.Add(new Images(){ImageFileName = imageName, ImageId = imageGuid });
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                List<Images> undeletedFiles = new List<Images>();
                foreach (Images image in images)
                {
                    try
                    {
                        DeleteImageByImages(image);
                    }
                    catch 
                    {
                        undeletedFiles.Add(image);
                    }
                }
                throw new ApplicationException("There was an error adding your files" +
                    "\nThe following files were added: " + string.Join(", ", images.Select(image => image.ImageFileName + " - " + image.ImageId).ToArray()) +
                    "\nThe following files failed to be cleaned up: " + string.Join(", ", undeletedFiles.Select(image => image.ImageFileName + " - " + image.ImageId).ToArray()), ex);
            }
            finally
            {
                conn.Close();
            }
            return images;
        }

        public BitmapImage SelectImageByImages(Images images)
        {
            BitmapImage image = null;
            try
            {
                Uri imageUri = new Uri(DataPathInformation.ImagePath + images.ImageId + ".png");
                image = new BitmapImage(imageUri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return image;
        }

        public int InsertMedicalImageByAnimalId(int animalId, string imageFileName)
        {
            int rows = 0;

            Images image = InsertImageByUriAndLink(imageFileName, (conn, trans, imageId) =>
            {
                var cmdText = "sp_insert_animal_medical_images_by_animal_id";
                var cmd = new SqlCommand(cmdText, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
                cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36);

                cmd.Parameters["@AnimalId"].Value = animalId;
                cmd.Parameters["@ImageId"].Value = imageId;

                rows = cmd.ExecuteNonQuery();
            });

            return rows;
        }

        public List<Images> SelectMedicalImagesByAnimalId(int animalId)
        {
            List<Images> images = new List<Images>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_images_by_animal_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = animalId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var image = new Images();
                        image.ImageId = reader.GetString(0);   // pulling med record id currently in SP
                        image.ImageFileName = reader.GetString(1);
                        images.Add(image);
                    }
                }
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return images;
        }

        public int InsertMedicalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            int rows = 0;

            List<Images> images = InsertImagesByUrisAndLink(imageFileNames, (conn, trans, imageId) =>
            {
                var cmdText = "sp_insert_animal_medical_images_by_animal_id";
                var cmd = new SqlCommand(cmdText, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
                cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36);

                cmd.Parameters["@AnimalId"].Value = animalId;
                cmd.Parameters["@ImageId"].Value = imageId;

                rows += cmd.ExecuteNonQuery();
            });

            return rows;
        }

        public int DeleteImageByImages(Images images)
        {
            try
            {
                File.Delete(DataPathInformation.ImagePath + images.ImageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }

        public List<Images> SelectAnimalImagesByAnimalId(int animalId)
        {
            List<Images> images = new List<Images>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_animal_profile_images_by_animal_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = animalId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var image = new Images();
                        image.ImageId = reader.GetString(0);
                        image.ImageFileName = reader.GetString(1);
                        if(image.ImageId != "")
                        {
                            images.Add(image);
                        }
                    }
                }
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return images;
        }

        public int InsertAnimalImageByAnimalId(int animalId, string imageFileName)
        {
            int rows = 0;

            Images image = InsertImageByUriAndLink(imageFileName, (conn, trans, imageId) =>
            {
                var cmdText = "sp_insert_animal_profile_images_by_animal_id";
                var cmd = new SqlCommand(cmdText, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AnimalId", SqlDbType.Int).Value = animalId;
                cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36).Value = imageId;

                rows = cmd.ExecuteNonQuery();
            });

            return rows;
        }

        public int InsertAnimalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            int rows = 0;

            List<Images> images = InsertImagesByUrisAndLink(imageFileNames, (conn, trans, imageId) =>
            {
                var cmdText = "sp_insert_animal_profile_images_by_animal_id";
                var cmd = new SqlCommand(cmdText, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AnimalId", SqlDbType.Int).Value = animalId;
                cmd.Parameters.Add("@ImageId", SqlDbType.NVarChar, 36).Value = imageId;

                rows += cmd.ExecuteNonQuery();
            });

            return rows;
        }

        public List<Images> SelectAnimalImageByAnimalId(int animalId)
        {
            List<Images> images = new List<Images>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_animal_image_by_animalId";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnimalId", animalId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Images image = new Images();
                        image.ImageId = reader.GetString(0);
                        image.ImageFileName = reader.GetString(1);
                        images.Add(image);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return images;
        }
    }
}
