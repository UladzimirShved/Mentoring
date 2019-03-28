using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2.Utils
{
    public class Cars
    {
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
    
      public static List<Cars> DeserializeCar()
      {
          List<Cars> list = new List<Cars>();
          list.Add(JsonConvert.DeserializeObject<Cars>(File.ReadAllText(TestData.InputFilePath)));
          return list;
      }
    }
}
