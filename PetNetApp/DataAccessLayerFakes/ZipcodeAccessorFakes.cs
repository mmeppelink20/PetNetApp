using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class ZipcodeAccessorFakes : IZipcodeAccessor
    {
        private List<Zipcode> fakeZipcodeObjects = null;
        private Zipcode zipcodeObj = null;

        public ZipcodeAccessorFakes()
        {

        }

        public Zipcode SelectCityStateLatLongByZipcode(string zipcode)
        {
            //throw new NotImplementedException();

            fakeZipcodeObjects = new List<Zipcode>()
            {
                new Zipcode(){ ZipcodeId="50207", City="New Sharon", State="IA", Latitude=41.47000m, Longitude=-92.65000m},
                new Zipcode(){ ZipcodeId="61111", City="Loves Park", State="IL", Latitude=42.33000m, Longitude=-89.01000m},
                new Zipcode(){ ZipcodeId="43560", City="Sylvania", State="OH", Latitude=41.71000m, Longitude=-83.70000m},
                new Zipcode(){ ZipcodeId="78675", City="Willow City", State="TX", Latitude=30.40000m, Longitude=-98.70000m},
                new Zipcode(){ ZipcodeId="93412", City="Los Osos", State="CA", Latitude=35.31000m, Longitude=-120.83000m}
            };

            zipcodeObj = fakeZipcodeObjects.Where(z => z.ZipcodeId == zipcode).FirstOrDefault();

            return zipcodeObj;

        }
    }
}
