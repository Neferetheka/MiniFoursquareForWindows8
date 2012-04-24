using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MiniFoursquare.API
{
    public class Venue
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Distance { get; set; }
        public string Image { get; set; }

        public Venue(JToken location, JToken categories)
        {
            if (location.SelectToken("address") != null)
                Address = location["address"].ToString();
            else
                Address = string.Empty;
            if (location.SelectToken("city") != null)
                City = location["city"].ToString();
            else
                City = string.Empty;
            if (location.SelectToken("distance") != null)
                Distance = Convert.ToInt32(location["distance"].ToString());
            else
                Distance = 0;

            if (categories.HasValues)
            {
                Image = categories.First["icon"]["prefix"].ToString() + "64.png";
            }
            else
                Image = string.Empty;
        }
    }
}
