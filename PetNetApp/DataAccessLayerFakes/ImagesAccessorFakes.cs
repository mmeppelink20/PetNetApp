using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessLayerFakes
{
    public class ImagesAccessorFakes : IImagesAccessor
    {
        List<Images> fakeImages = new List<Images>();
        List<MedicalRecordVM> fakeMedicalRecords = new List<MedicalRecordVM>();
        List<Images> stephenFakeImages = new List<Images>();
        List<AnimalVM> fakeAnimals = new List<AnimalVM>();

        public ImagesAccessorFakes()
        {
            fakeImages.Add(new Images 
            {
               ImageId = "unique-1",
               ImageFileName = "oreo_arm_scratch.png"
            });

            fakeImages.Add(new Images
            {
                ImageId = "unique-2",
                ImageFileName = "orea_scratch_healing.png"
            });

            fakeImages.Add(new Images
            {
                ImageId = "unique-3",
                ImageFileName = "frank_mites.jpeg"
            });

            fakeImages.Add(new Images
            {
                ImageId = "unique-4",
                ImageFileName = "frank_mites2.jpeg"
            });

            fakeMedicalRecords.Add(new MedicalRecordVM
            {
                MedicalRecordId = 1,
                AnimalId = 1,
                Date = DateTime.Now,
                MedicalNotes = "Oreo scratched their arm.",
                IsProcedure = false,
                IsTest = false,
                IsVaccination = false,
                IsPrescription = false,
                Images = true,
                QuarantineStatus = false,
                Diagnosis = "Tis but a flesh wound",
                AnimalImages = new List<Images>
                {
                    fakeImages[0]
                }

            });

            fakeMedicalRecords.Add(new MedicalRecordVM
            {
                MedicalRecordId = 3,
                AnimalId = 2,
                Date = DateTime.Now,
                MedicalNotes = "Oreo's scratch is healing",
                IsProcedure = false,
                IsTest = false,
                IsVaccination = false,
                IsPrescription = false,
                Images = true,
                QuarantineStatus = false,
                Diagnosis = "Tis but even less than a flesh wound",
                AnimalImages = new List<Images>
                {
                    fakeImages[1]
                }
            });

            fakeMedicalRecords.Add(new MedicalRecordVM
            {
                MedicalRecordId = 10,
                AnimalId = 5,
                Date = DateTime.Now,
                MedicalNotes = "pictures of skin condition",
                IsProcedure = false,
                IsTest = false,
                IsVaccination = false,
                IsPrescription = true,
                Images = true,
                QuarantineStatus = false,
                Diagnosis = "lice",
                AnimalImages = new List<Images>
                {
                    fakeImages[2],
                    fakeImages[3]
                }
            });

            fakeAnimals.Add(new AnimalVM
            {
                AnimalId = 1,
                AnimalName = "Test name 1",
                AnimalGender = "Test gender 1",
                AnimalTypeId = "Test type 1",
                AnimalBreedId = "Test breed 1",
                KennelName = "Test kennel 1",
                Personality = "Test personality 1",
                Description = "Test description 1",
                AnimalStatusId = "Test status 1",
                AnimalStatusDescription = "Test status description 1",
                BroughtIn = DateTime.Parse("2023-06-01"),
                MicrochipNumber = "Test SN",
                Aggressive = false,
                AggressiveDescription = "Not aggressive",
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = "N/A",
                AnimalImages = new List<Images>
                {
                    fakeImages[1]
                }
            });

            fakeAnimals.Add(new AnimalVM
            {
                AnimalId = 2,
                AnimalShelterId = 100000,
                AnimalName = "Test name 2",
                AnimalGender = "Test gender 2",
                AnimalTypeId = "Test type 2",
                AnimalBreedId = "Test breed 2",
                KennelName = "Test kennel 2",
                Personality = "Test personality 2",
                Description = "Test description 2",
                AnimalStatusId = "Test status 2",
                AnimalStatusDescription = "Test status description 2",
                BroughtIn = DateTime.Parse("2023-06-02"),
                MicrochipNumber = "Test SN",
                Aggressive = true,
                AggressiveDescription = "Bites",
                ChildFriendly = false,
                NeuterStatus = false,
                Notes = "N/A",
                AnimalImages = new List<Images>
                {
                    fakeImages[1]
                }
            });

        }

        public int DeleteImageByImages(Images images)
        {
            return stephenFakeImages.Remove(images) ? 1 : 0;
        }

        public Images InsertImageByUri(string imageUri)
        {
            string fileName = imageUri.Substring((imageUri.LastIndexOf("/") > 0 ? imageUri.LastIndexOf("/") : imageUri.LastIndexOf("\\")) + 1);
            Images image = new Images() { ImageFileName = fileName, ImageId = Guid.NewGuid().ToString() };
            stephenFakeImages.Add(image);
            return image;
        }

        public List<Images> InsertImagesByUris(IEnumerable<string> imageUris)
        {
            List<Images> newImages = new List<Images>();
            foreach (string imageUri in imageUris)
            {
                string fileName = imageUri.Substring((imageUri.LastIndexOf("/") > 0 ? imageUri.LastIndexOf("/") : imageUri.LastIndexOf("\\")) + 1);
                Images image = new Images() { ImageFileName = fileName, ImageId = Guid.NewGuid().ToString() };
                stephenFakeImages.Add(image);
                newImages.Add(image);
            }
            return newImages;
        }

        public int InsertMedicalImageByAnimalId(int animalId, string imageFileName)
        {
            int rows = 0;
            Images _image = new Images();
            _image.ImageId = "unique-15";
            _image.ImageFileName = imageFileName;

            for (int i = 0; i < fakeMedicalRecords.Count; i++)
            {
                if(fakeMedicalRecords[i].AnimalId == animalId)
                {
                    fakeMedicalRecords[i].AnimalImages.Add(_image);
                    rows = 3;
                }
            }
            
            return rows;
        }

        public int InsertMedicalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            int rows = 0;
            List<Images> newImages = new List<Images>();
            foreach (string imageFileName in imageFileNames)
            {
                Images _image = new Images();
                _image.ImageId = "unique-15";
                _image.ImageFileName = imageFileName;
                newImages.Add(_image);
                stephenFakeImages.Add(_image);
                fakeMedicalRecords.Where(rec => rec.AnimalId == animalId).ToList().ForEach(rec => rec.AnimalImages.Add(_image));
                rows++;
            }
            fakeMedicalRecords.First(record => record.AnimalId == animalId).AnimalImages.Concat(newImages);
            return rows;
        }

        public List<Images> SelectAnimalImageByAnimalId(int animalId)
        {
            throw new NotImplementedException();
        }

        public BitmapImage SelectImageByImages(Images images)
        {
            return new BitmapImage();
        }

        public List<Images> SelectMedicalImagesByAnimalId(int animalId)
        {
            //var animalImages = fakeMedicalRecords.Where((x) => x.AnimalId == animalId && x.Images).Select((x) => x.AnimalImages);
            //return fakeImages.Where((x) => animalImages

            List<Images> _images = new List<Images>();
            foreach (var record in fakeMedicalRecords)
            {
                if(record.AnimalId == animalId)
                {
                    foreach(var image in record.AnimalImages)
                    {
                        _images.Add(image);
                    }
                }
            }
            return _images;
        }

        public int InsertAnimalImageByAnimalId(int animalId, string imageFileName)
        {
            int rows = 0;
            Images _image = new Images();
            _image.ImageId = "unique-15";
            _image.ImageFileName = imageFileName;

            for (int i = 0; i < fakeAnimals.Count; i++)
            {
                if (fakeAnimals[i].AnimalId == animalId)
                {
                    fakeAnimals[i].AnimalImages.Add(_image);
                    rows = 3;
                }
            }

            return rows;
        }

        public int InsertAnimalImagesByAnimalId(int animalId, IEnumerable<string> imageFileNames)
        {
            int rows = 0;
            List<Images> newImages = new List<Images>();
            foreach (string imageFileName in imageFileNames)
            {
                Images _image = new Images();
                _image.ImageId = "unique-15";
                _image.ImageFileName = imageFileName;
                newImages.Add(_image);
                stephenFakeImages.Add(_image);
                fakeAnimals.Where(rec => rec.AnimalId == animalId).ToList().ForEach(rec => rec.AnimalImages.Add(_image));
                rows++;
            }
            fakeAnimals.First(record => record.AnimalId == animalId).AnimalImages.Concat(newImages);
            return rows;
        }

        public List<Images> SelectAnimalImagesByAnimalId(int animalId)
        {
            List<Images> _images = new List<Images>();
            foreach (var animal in fakeAnimals)
            {
                if (animal.AnimalId == animalId)
                {
                    foreach (var image in animal.AnimalImages)
                    {
                        _images.Add(image);
                    }
                }
            }
            return _images;
        }
    }
}
