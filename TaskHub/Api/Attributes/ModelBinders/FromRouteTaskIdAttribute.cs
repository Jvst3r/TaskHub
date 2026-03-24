using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes.ModelBinders
{
    public class FromRouteTaskIdAttribute : Attribute, IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return new TaskIdBinder();
        }
    }
}
