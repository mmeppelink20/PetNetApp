using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicLayerInterfaces
{
    public interface IImagesManager
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Calls the accessor method to store images and return their unique Id's
        /// </summary>
        /// <param name="imageUris">The list of images to store</param>
        /// <returns>List of Images Objects</returns>
        List<Images> AddImagesByUris(IEnumerable<string> imageUris);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Calls the accessor method to store the image and return its unique Id
        /// </summary>
        /// <param name="imageUri">The full path where the image is located</param>
        /// <returns>An Images object with the ImageId and Image File Name</returns>
        Images AddImageByUri(string imageUri);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// Uses the Accessor method to retrieve the actual file from its storage location
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="images">The images object to fetch the displayable bitmap for</param>
        /// <returns>A displayable bitmap image</returns>
        BitmapImage RetrieveImageByImages(Images images);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/02/18
        /// 
        /// Uses the accessors method to retrieve a list of images objects for the specified animals medical records
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalId">The id of the animal to get the medical record images for</param>
        /// <returns>A list of Images objects</returns>
        List<Images> RetrieveMedicalImagesByAnimalId(int animalId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/02/18
        /// 
        /// Adds the selected image to a medical record for the selected animal
        /// </summary>
        /// <param name="animalId">The id of the animal to get the medical record images for</param>
        /// <returns>A list of Images objects</returns>
        bool AddMedicalImageByAnimalId(int animalId, string imageFileName);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/26
        /// 
        /// 
        /// </summary>
        /// <param name="animalId">The id of the animal to get the medical record images for</param>
        /// <param name="imageFileNames">The list of Uri where the images are located</param>
        /// <returns></returns>
        bool AddMedicalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Retrieves a list of image objects for the specified animal's profile record
        /// </summary>
        /// <param name="animalId">The id of the animal whose images will be returned</param>
        /// <returns>A list of Images objects</returns>
        List<Images> RetrieveAnimalImagesByAnimalId(int animalId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Adds the selected image to the animal profile record
        /// </summary>
        /// <param name="animalId">The id of the animal to whose record an image is being added</param>
        /// <param name="imageFileName">The Uri where the image is located</param>
        /// <returns>A boolean signifying success or failure</returns>
        bool AddAnimalImageByAnimalId(int animalId, string imageFileName);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/05
        /// 
        /// Adds the selected image to the animal profile record
        /// </summary>
        /// <param name="animalId">The id of the animal to whose record images are being added</param>
        /// <param name="imageFileNames">The list of Uri where the images are located</param>
        /// <returns>A boolean signifying success or failure</returns>
        bool AddAnimalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames);

        List<Images> RetriveImageByAnimalId(int animalId);
    }
}
