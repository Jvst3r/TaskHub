using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Api.Attributes
{
    public class FromRouteTaskIdAttribute :IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var guid = bindingContext.ValueProvider.GetValue("id").FirstValue;

            if (guid == null || string.IsNullOrWhiteSpace(guid))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.HttpContext.Response.StatusCode = 400;
                //bindingContext.HttpContext.Response.Body = "Идентификатор задачи не задан";
                await Task.CompletedTask;
            }
            
            if (!IsGuid(guid))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.HttpContext.Response.StatusCode = 400;
            }

            await Task.CompletedTask;
                
        }

        private static bool IsGuid(string value)
        {
            if (value.Length != 32)
            {
                return false;
            }

            for (int i = 0; i < 8; i++)
            {
                if (!(char.IsDigit(value[i]) || char.IsLower(value[i])))
                {
                    return false;
                }
            }

            for (int i = 9; i < 13; i++)
            {
                if (!(char.IsDigit(value[i]) || char.IsLetter(value[i])))
                {
                    return false;
                }
            }

            for (int i = 14; i < 19; i++)
            {
                if (!(char.IsDigit(value[i]) || char.IsLetter(value[i])))
                {
                    return false;
                }
            }

            for (int i = 21; i < 26; i++)
            {
                if (!(char.IsDigit(value[i]) || char.IsLetter(value[i])))
                {
                    return false;
                }
            }

            for (int i = 26; i < 31; i++)
            {
                if (!(char.IsDigit(value[i]) || char.IsLetter(value[i])))
                {
                    return false;
                }
            }

            if (value[9] != '-' ||
                value[13] != '-' ||
                value[18] != '-' ||
                value[23] != '-')
            { 
                return false;
            }

            return true;
        }
    }
}
