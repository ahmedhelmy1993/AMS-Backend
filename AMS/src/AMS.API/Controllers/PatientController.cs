using AMS.API.Infrastructure.Base;
using AMS.AppService.Patient.Commands.AddPatient;
using AMS.AppService.Patient.Commands.DeletePatient;
using AMS.AppService.Patient.Commands.EditPatient;
using AMS.AppService.Patient.Queries.GetPatient;
using AMS.Domain.Patient.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Route = AMS.API.Infrastructure.Base.Route;


namespace AMS.API.Controllers
{
    [ApiController]
    [Route(Route.API)]
    public class PatientController : BaseController
    {
        #region CTRS
        public PatientController(IMediator mediator)
            : base(mediator)
        { }
        #endregion

        /// <summary>
        /// Search in patient list
        /// </summary>
        /// <param name="searchPatientDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> List([FromBody] SearchPatientDto searchPatientDto)
        {
            return Ok(await Mediator.Send(searchPatientDto));
        }
        /// <summary>
        /// Get patient by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await Mediator.Send(new GetPatientQuery(id)));
        }
        /// <summary>
        /// Get patient drop down list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDDL()
        {
            return Ok(await Mediator.Send(new PatientDDLDto()));
        }
        /// <summary>
        /// Add patient
        /// </summary>
        /// <param name="addPatientCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddPatientCommand addPatientCommand)
        {
            return Ok(await Mediator.Send(addPatientCommand));
        }
        /// <summary>
        /// Edit patient
        /// </summary>
        /// <param name="editPatientCommand"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditPatientCommand editPatientCommand)
        {
            return Ok(await Mediator.Send(editPatientCommand));
        }
        /// <summary>
        /// Delete patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await Mediator.Send(new DeletePatientCommand(id)));
        }
    }
}
