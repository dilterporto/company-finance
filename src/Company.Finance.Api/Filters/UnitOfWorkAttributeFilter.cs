using Company.Finance.Persistence;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.Finance.Filters
{
    public class UnitOfWorkAttributeFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkAttributeFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            await _unitOfWork.Commit();
            base.OnActionExecuted(context);
        }
    }
}