using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackATL_Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository AgendaRepository;

        public AgendaController(IAgendaRepository agendaRepository)
        {
            AgendaRepository = agendaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Agenda_Item>> GetAgendaList()
        {
            return AgendaRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Agenda_Item> GetAgenda(string id)
        {
            Agenda_Item agenda = AgendaRepository.Get(id);

            if (agenda == null)
                return NotFound();

            return agenda;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Agenda_Item> CreateAgenda([FromBody]Agenda_Item agenda)
        {
            AgendaRepository.Add(agenda);
            return CreatedAtAction(nameof(GetAgenda), new { agenda.ID }, agenda);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult EditAgenda([FromBody] Agenda_Item agenda)
        {
            try
            {
                AgendaRepository.Update(agenda);
            }
            catch (Exception)
            {
                return BadRequest("Error while editing item");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteAgenda(string id)
        {
            Agenda_Item agenda = AgendaRepository.Remove(id);

            if (agenda == null)
                return NotFound();

            return Ok();
        }


    }
}
