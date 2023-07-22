using ControllersExample.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControllersExample.CustomModelBinder
{
    public class NameModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            emp emp=new emp();
            //combine firstname n lastname
            if (bindingContext.ValueProvider.GetValue("firstname").Count() > 0)
            {
               emp.PetName= bindingContext.ValueProvider.GetValue("firstname").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("lastname").Count() > 0)
            {
                emp.PetName += " "+bindingContext.ValueProvider.GetValue("lastname").FirstValue;
            }
           bindingContext.Result=ModelBindingResult.Success(emp);
            return Task.CompletedTask;
        }
    }
}
