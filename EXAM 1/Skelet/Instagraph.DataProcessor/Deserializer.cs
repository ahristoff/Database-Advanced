using System.Collections.Generic;
using Instagraph.Data;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {        
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {            
            return null;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            return null;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            return null;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {           
            return null;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
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
