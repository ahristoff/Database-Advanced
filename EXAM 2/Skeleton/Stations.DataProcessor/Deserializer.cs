using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stations.Data;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{		

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            return null;
		}

		public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            return null;    
        }

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            return null;
		}

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            return null;
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
		{

            return null;
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            return null;
        }


        private static bool IsValid(object obj)
		{
			var validationContext = new ValidationContext(obj);
			var validationResults = new List<ValidationResult>();

			var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
			return isValid;
		}
	}
}