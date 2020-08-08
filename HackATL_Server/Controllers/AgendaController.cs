using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Agenda;
using HackATL_Server.Models.Model_General;
using HackATL_Server.Models.Model_Http.Agenda;
using HackATL_Server.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackATL_Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AgendaController : ControllerBase
    {
        private IAgendaService _AgendaService;
        public AgendaController(IAgendaService AgendaService)
        {
            _AgendaService = AgendaService;
        }

        [HttpPost("getagendas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Agenda>> GetAgenda_All()
        {
            List<Agenda> list = new List<Agenda>();
            list = _AgendaService.GetAgenda_All();
            if (list == null)
                return BadRequest();
            return Ok(list);
           
        }

        [HttpPost("getagenda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Agenda> GetAgenda_Single([FromBody]StringClass model)
        {
            Agenda agenda = new Agenda();
           
            agenda = _AgendaService.GetAgenda(model.str);
            if (agenda == null)
                return BadRequest();
            return Ok(agenda);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Agenda> CreateAgenda([FromBody] Agenda_Create create)
        {
            Agenda agenda = new Agenda();

            agenda = _AgendaService.CreateAgenda(create);
            if (agenda == null)
                return BadRequest();
            return Ok(agenda);
        }

        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Agenda> DeleteAgenda([FromBody] StringClass delete)
        {
      

            var status = _AgendaService.DeleteAgenda(delete.str);
            if (status)
                return BadRequest();
            return Ok(status);
        }

        [HttpPost("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Agenda> EditAgenda([FromBody] Agenda update)
        {
            var status = _AgendaService.EditAgenda(update);
            if (status)
                return BadRequest();
            return Ok(status);
        }


    }
}
