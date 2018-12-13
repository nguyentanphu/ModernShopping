using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ModernShopping.Presentation.ModelBinders
{
    public class CustomArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.IsEnumerableType && context.Metadata.ModelType.GenericTypeArguments[0].IsPrimitive)
                return new ArrayModelBinder();

            return null;
        }
    }
}
