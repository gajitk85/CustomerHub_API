namespace CustomerHub.API.Utilities
{
    public class ModelValidation
    {
        public static Dictionary<string, string[]> Validate(Customer model)
        {
            var validationErrors = new Dictionary<string, string[]>();

            if (String.IsNullOrWhiteSpace(model.FullName))
            {
                validationErrors.Add("FullName", new string[] { "Name is required." });
            }

            // Validate the date of birth
            if (model.DateOfBirth.Date > DateTime.Today.Date)
            {
                validationErrors.Add("DateOfBirth", new string[] { "The date of birth cannot be greater than today." });
            }

            return validationErrors;
        }
    }
}
