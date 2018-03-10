using Google.Maps;
using Google.Maps.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomTrip
{
    public static class Utils
    {
        public static Evenement GetCoordonnees(String adresse)
        {
            var request = new GeocodingRequest();
            request.Address = adresse;
            var response = new GeocodingService().GetResponse(request);

            //The GeocodingService class submits the request to the API web service, and returns the
            //response strongly typed as a GeocodeResponse object which may contain zero, one or more results.

            Evenement depart = new Evenement();
            depart.EVENT_NAME = "Point de départ";

            //Assuming we received at least one result, let's get some of its properties:
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();

                depart.LATITUDE = result.Geometry.Location.Latitude;
                depart.LONGITUDE = result.Geometry.Location.Longitude;
                depart.ADRESSE = result.FormattedAddress;

            }
            else
            {
                depart.ADRESSE = String.Format("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
            }

            return depart;
        }

            public static void MajCoordonnees()
        {
            using (var db = new masterEntities())
            {
                var query = from even in db.Evenement
                            orderby even.EVENT_NAME
                            select even;
                int max = 2000;
                foreach (Evenement even in query)
                {
                    var request = new GeocodingRequest();
                    request.Address = even.ADRESSE;
                    var response = new GeocodingService().GetResponse(request);

                    //The GeocodingService class submits the request to the API web service, and returns the
                    //response strongly typed as a GeocodeResponse object which may contain zero, one or more results.

                    //Assuming we received at least one result, let's get some of its properties:
                    if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
                    {
                        var result = response.Results.First();

                        even.LATITUDE = result.Geometry.Location.Latitude;
                        even.LONGITUDE = result.Geometry.Location.Longitude;
                        
                    }
                    else
                    {
                        Console.WriteLine("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
                    }
                    max -= 1;
                    if (max == 0)
                    {
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}