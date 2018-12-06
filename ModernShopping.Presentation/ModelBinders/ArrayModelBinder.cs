using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModernShopping.Presentation.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (!bindingContext.ModelMetadata.IsEnumerableType)
                return Task.CompletedTask;

            // Fetch the value of argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelMetadata.Name);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var elementType = bindingContext.ModelType.GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);

            var valueString = valueProviderResult.ToString();
            var values = valueString.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x))
                .ToArray();

            var typeValues = Array.CreateInstance(elementType, values.Length);

            values.CopyTo(typeValues, 0);

            bindingContext.Result = ModelBindingResult.Success(typeValues);
            return Task.CompletedTask;
        }
    }
}
