using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinding.Models
{
    public class FilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Retrieve the query string
            var queryString = bindingContext.HttpContext.Request.Query;

            // Instantiate the target object
            var model = new FilterModel();

            // Bind properties
            if (queryString.TryGetValue("Name", out var name))
            {
                model.Name = name;
            }

            if (queryString.TryGetValue("Age", out var age) && int.TryParse(age, out var ageValue))
            {
                model.Age = ageValue;
            }

            if (queryString.TryGetValue("Interests", out var interests))
            {
                model.Interests = interests.ToString().Split(",").ToList();
            }

            // Set the result
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}