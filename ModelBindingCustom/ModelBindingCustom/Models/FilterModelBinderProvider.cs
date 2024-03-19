using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinding.Models
{
    public class FilterModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(FilterModel))
            {
                return new FilterModelBinder();
            }

            return null;
        }
    }
}