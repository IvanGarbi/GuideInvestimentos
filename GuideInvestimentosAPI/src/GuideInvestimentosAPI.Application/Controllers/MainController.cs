using GuideInvestimentosAPI.Business.Interfaces.Notifications;
using GuideInvestimentosAPI.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GuideInvestimentosAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected INotificator _notificator;

        public MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool IsValid()
        {
            return !_notificator.EmptyNotifications();
        }

        protected ActionResult ApiResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotifyErrorsModelState(modelState);
            }

            return ApiResponse();
        }

        protected ActionResult ApiResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificator.ReturnNotifications().Select(n => n.Error)
            });
        }

        private void NotifyErrorsModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);

            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        private void NotifyError(string error)
        {
            _notificator.AddNotification(new Notification(error));
        }
    }
}
