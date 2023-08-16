using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessLayerInterfaces
{
    public interface IImagesAccessor
    {

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Takes the image at the selected Uri, resizes it and saves it to the image folder in the base directory,
        /// adds the image Id and original file name to the database and returns an Images with the file name and Id that was assigned to it
        /// </summary>
        /// <param name="imageUri">The full path of the image</param>
        /// <returns></returns>
        Images InsertImageByUri(string imageUri);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Takes a list of image uri, resizes it and saves it to the image folder in the base directory,
        /// adds the image Id and original file name to the database and returns an Images with the file name and Id that was assigned to it
        /// </summary>
        /// <param name="imageUris">The list of each image's absolute path</param>
        /// <returns>A list of Images objects</returns>
        List<Images> InsertImagesByUris(IEnumerable<string> imageUris);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Takes an images object and returns the appropriate Bitmap Image for that object
        /// </summary>
        /// <param name="images">The images object with the ImageId and Image File Name</param>
        /// <returns></returns>
        BitmapImage SelectImageByImages(Images images);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/02/18
        /// 
        /// Selects all of the medical images for the specified animal id
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalId">The animal id to get medical record images for</param>
        /// <returns>List of Images objects</returns>
        List<Images> SelectMedicalImagesByAnimalId(int animalId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Deletes the selected image from the images directory
        /// </summary>
        /// <param name="images">image to delete</param>
        /// <returns>number of successful deletions</returns>
        int DeleteImageByImages(Images images);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/02/18
        /// 
        /// Adds the image to the database, stores it in the images folder and links it in the database to a medical record
        /// </summary>
        /// <param name="animalId">The animal id to get medical record images for</param>
        /// <returns>List of Images objects</returns>
        int InsertMedicalImageByAnimalId(int animalId, string imageFileName);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/02/18
        /// 
        /// Adds the image to the database, stores it in the images folder and links it in the database to a medical record
        /// </summary>
        /// <param name="animalId">The animal id to get medical record images for</param>
        /// <returns>List of Images objects</returns>
        int InsertMedicalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Selects all the profile images for the specified animal
        /// </summary>
        /// <param name="animalId">The id of the animal whose images will be selected</param>
        /// <returns>A list of Images objects</returns>
        List<Images> SelectAnimalImagesByAnimalId(int animalId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Inserts the selected image into the database
        /// </summary>
        /// <param name="animalId">The id of the animal to whose record an image is being added</param>
        /// <param name="imageFileName">The Uri where the image is located</param>
        /// <returns>Rows affected</returns>
        int InsertAnimalImageByAnimalId(int animalId, string imageFileName);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Inserts the selected images into the database
        /// </summary>
        /// <param name="animalId">The id of the animal to whose record images are being added</param>
        /// <param name="imageFileNames">The list of Uri where the images are located</param>
        /// <returns>Rows affected</returns>
        int InsertAnimalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames);

        List<Images> SelectAnimalImageByAnimalId(int animalId);
    }
}
