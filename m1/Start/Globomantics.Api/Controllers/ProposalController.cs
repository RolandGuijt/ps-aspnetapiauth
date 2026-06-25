using Globomantics.Client.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Server.Controllers
{
    [ApiController]
    [Route("proposal")]
    public class ProposalController(IProposalRepository repo) : Controller
    {
        [HttpGet("all/{conferenceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll(int conferenceId)
        {
            var proposals = repo.GetAllForConference(conferenceId);
            if (proposals == null || !proposals.Any())
            {
                return NoContent();
            }
            return Ok(proposals);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var proposal = repo.GetOne(id);
            if (proposal == null)
                return NotFound();
            return Ok(proposal);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add(ProposalModel model)
        {
            var id = repo.Add(model);
            return CreatedAtAction(nameof(GetById), new { id }, model);
        }

        [HttpPut("approve/{proposalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Approve(int proposalId)
        {
            var prop = repo.Approve(proposalId);
            if (prop == null)
                return NotFound();
            return Ok(prop);
        }
    }
}
