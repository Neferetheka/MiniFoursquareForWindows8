using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MiniFoursquare.API
{
    public abstract class FoursquareManager
    {
        public const string URLAPI = "https://api.foursquare.com/v2/venues/search?";
        public const String CLIENTID = "";
        public const String CLIENTSECRET = "";

        public static List<Venue> LoadVenuesFromJson(string result)
        {
            List<Venue> listVenues = new List<Venue>();

            //On parse le json et on sélectionne toutes les venues
            JObject json = JObject.Parse(@result);
            var venues = json["response"].SelectToken("venues");

            foreach (JToken venue in venues)
            {
                try
                {
                    //On passe le noeud location et le noeud categories, conformément au constructeur
                    listVenues.Add(new Venue(venue["location"], venue["categories"])
                    {
                        Name = venue["name"].ToString(),
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return listVenues;
        }
    }
}
