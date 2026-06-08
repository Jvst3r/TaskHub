using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes.ModelBinders
{
    public class FromRouteTaskIdAttribute : ModelBinderAttribute<TaskIdBinder>
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return new TaskIdBinder();
        }
    }
}
