using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public static class DataPathInformation
    {
        public static string BaseDirectory { get; set; } = Environment.CurrentDirectory;
        private static readonly string imageFolder = @"\Images\";
        public static string ImagePath => BaseDirectory + imageFolder;
    }
}
