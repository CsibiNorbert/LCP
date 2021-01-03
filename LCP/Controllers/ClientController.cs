using AutoMapper;
using LCP.ApiResponses;
using LCP.Core.Entities;
using LCP.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCP.Core.Interfaces;

namespace LCP.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientRepository _clientRepo;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper autoMapper)
        {
            _clientRepo = clientRepository;
            _mapper = autoMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Client>>> GetClients()
        {
            var clients = await _clientRepo.GetClientsAsync();
            return Ok(_mapper.Map<IReadOnlyList<ReturnClientDto>>(clients));
        }

        [HttpGet("{clientId}", Name = "getclient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClient(int clientId)
        {
            var client = await _clientRepo.GetClientByIdAsync(clientId);

            if (client == null)
            {
                // 404: Not Found
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<ReturnClientDto>(client));
        }

        [HttpPost("insertclient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClient([FromBody] ClientToBeAdded clientToAdd)
        {

            var createClient = _mapper.Map<Client>(clientToAdd);

            var result = await _clientRepo.AddClientAsync(createClient);

            if (result != null)
            {
                return CreatedAtRoute("getclient", new { controller = "client", clientId = result.Id }, result);
            }

            return BadRequest(new ApiResponse(400, "Something went wrong while adding the client"));
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientToBeAdded client)
        {
            // Re-using the same DTO for speed purposes
            // Retrieve client
            var clientFromRepo = await _clientRepo.GetClientByIdAsync(id);

            // Map client with Dto
            _mapper.Map(client, clientFromRepo);

            if (await _clientRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest(new ApiResponse(400, "Something went wrong while updating the client"));
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientRepo.GetClientByIdAsync(id);

            if (client != null)
            {
                _clientRepo.Delete(client);
            }

            if (await _clientRepo.SaveAll())
            {
                return Ok();
            }

            return BadRequest(new ApiResponse(400, "Failed to delete the Client"));
        }
    }
}
