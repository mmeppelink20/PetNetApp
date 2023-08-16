using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicLayer
{
    public class ImagesManager : IImagesManager
    {
        private IImagesAccessor _imagesAccessor = null;
        public ImagesManager()
        {
            _imagesAccessor = new ImagesAccessor();
        }
        public ImagesManager(IImagesAccessor imagesAccessor)
        {
            _imagesAccessor = imagesAccessor;
        }

        public List<Images> AddImagesByUris(IEnumerable<string> imageUris)
        {
            List<Images> images = null;
            try
            {
                images = _imagesAccessor.InsertImagesByUris(imageUris);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the selected images", ex);
            }
            return images;
        }
        public Images AddImageByUri(string imageUri)
        {
            Images image = null;
            try
            {
                image = _imagesAccessor.InsertImageByUri(imageUri);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The selected image cannot be added", ex);
            }
            return image;
        }

        public BitmapImage RetrieveImageByImages(Images images)
        {
            BitmapImage image = null;
            try
            {
                image = _imagesAccessor.SelectImageByImages(images);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load the image", ex);
            }
            return image;
        }
        public bool AddMedicalImageByAnimalId(int animalId, string imageFileName)
        {
            try
            {
                return 0 != _imagesAccessor.InsertMedicalImageByAnimalId(animalId, imageFileName);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to add image.", up);
            }
        }

        public bool AddMedicalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            try
            {
                return 0 != _imagesAccessor.InsertMedicalImagesByAnimalId(animalId, imageFileNames);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to add images.", up);
            }
        }

        public List<Images> RetrieveMedicalImagesByAnimalId(int animalId)
        {
            List<Images> images;
            try
            {
                images = _imagesAccessor.SelectMedicalImagesByAnimalId(animalId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to retrieve images", up);
            }

            return images;
        }
        public bool AddAnimalImageByAnimalId(int animalId, string imageFileName)
        {
            try
            {
                return 0 != _imagesAccessor.InsertAnimalImageByAnimalId(animalId, imageFileName);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to add image.", up);
            }
        }

        public bool AddAnimalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            try
            {
                return 0 != _imagesAccessor.InsertAnimalImagesByAnimalId(animalId, imageFileNames);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to add images.", up);
            }
        }

        public List<Images> RetrieveAnimalImagesByAnimalId(int animalId)
        {
            List<Images> images;
            try
            {
                images = _imagesAccessor.SelectAnimalImagesByAnimalId(animalId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to retrieve images", up);
            }

            return images;
        }

        public List<Images> RetriveImageByAnimalId(int animalId)
        {
            List<Images> images;

            try
            {
                images = _imagesAccessor.SelectAnimalImageByAnimalId(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the images", ex);
            }

            return images;
        }
    }
}
