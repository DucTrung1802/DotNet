using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinding.Models
{
    public class HeaderModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var headers = bindingContext.HttpContext.Request.Headers;
            var model = new UserPreferences
            {
                Language = headers["Language"],
                Theme = headers["Theme"]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}