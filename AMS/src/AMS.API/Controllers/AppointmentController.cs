using AMS.API.Infrastructure.Base;
using AMS.AppService.Appointment.Commands.AddAppointment;
using AMS.AppService.Appointment.Commands.CancelAppointment;
using AMS.AppService.Appointment.Commands.DeleteAppointment;
using AMS.AppService.Appointment.Commands.EditAppointment;
using AMS.AppService.Appointment.Queries.GetAppointment;
using AMS.Domain.Appointment.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Route = AMS.API.Infrastructure.Base.Route;


namespace AMS.API.Controllers
{
    
    [ApiController]
    [Route(Route.API)]
    
    public class AppointmentController : BaseController
    {
        #region CTRS
        public AppointmentController(IMediator mediator)
            : base(mediator)
        { }
        #endregion

        /// <summary>
        /// Search in appointments
        /// </summary>
        /// <param name="searchAppointmentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> List([FromBody] SearchAppointmentDto searchAppointmentDto)
        {
            return Ok(await Mediator.Send(searchAppointmentDto));
        }
        /// <summary>
        /// Get appointment By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await Mediator.Send(new GetAppointmentQuery(id)));
        }
        /// <summary>
        /// Get appointment status drop down list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAppointmentStatusDDL()
        {
            return Ok(await Mediator.Send(new AppointmentStatusDDLDto()));
        }
        /// <summary>
        /// Add appointment
        /// </summary>
        /// <param name="addAppointmentCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddAppointmentCommand addAppointmentCommand)
        {
            return Ok(await Mediator.Send(addAppointmentCommand));
        }
        /// <summary>
        /// Edit appointment
        /// </summary>
        /// <param name="editAppointmentCommand"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditAppointmentCommand editAppointmentCommand)
        {
            return Ok(await Mediator.Send(editAppointmentCommand));
        }
        /// <summary>
        /// Cancel appointment
        /// </summary>
        /// <param name="cancelAppointmentCommand"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Cancel([FromBody] CancelAppointmentCommand cancelAppointmentCommand)
        {
            return Ok(await Mediator.Send(cancelAppointmentCommand));
        }
        /// <summary>
        /// Delete appointment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await Mediator.Send(new DeleteAppointmentCommand(id)));
        }
    }
}
