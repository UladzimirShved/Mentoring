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
    //public class Cars
    //{
    //    [JsonProperty(PropertyName = "manufacture")]
    //    public string Manufacture { get; set; }
    //    [JsonProperty(PropertyName = "model")]
    //    public string Model { get; set; }
    //    [JsonProperty(PropertyName = "minyear")]
    //    public int MinYear { get; set; }
    //    [JsonProperty(PropertyName = "maxyear")]
    //    public int MaxYear { get; set; }

    //    public static List<Cars> DeserializeCar()
    //    {
    //        return JsonConvert.DeserializeObject<List<Cars>>(File.ReadAllText("Task2/carData.json"));
    //    }
    //}
    public class Cars
    {
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
    
      public static List<Cars> DeserializeCar()
      {
          List<Cars> list = new List<Cars>();
          list.Add(JsonConvert.DeserializeObject<Cars>(File.ReadAllText("Task2/carData.json")));
          return list;
      }
    }
}
