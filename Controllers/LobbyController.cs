using Microsoft.AspNetCore.Mvc;
using ShortalkB2.Models;
using ShortalkB2.Service;
using System.Collections.Generic;

namespace ShortalkB2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly LobbyService _data;

        public LobbyController(LobbyService data)
        {
            _data = data;
        }

        // Endpoint to create a new lobby room
        [HttpPost]
        [Route("createLobby")]
        public IActionResult CreateLobbyRoom([FromBody] LobbyRoomModel newLobby)
        {
            _data.CreateLobbyRoom(newLobby);
            return Ok("Lobby room created successfully.");
        }

        // Endpoint to delete a lobby room by ID
        [HttpDelete]
        [Route("deleteLobby/{id}")]
        public IActionResult DeleteLobbyRoom(int id)
        {
            bool result = _data.DeleteLobbyRoom(id);
            if (result)
            {
                return Ok("Lobby room deleted successfully.");
            }
            return NotFound("Lobby room not found.");
        }

        // Endpoint to get all existing lobby rooms
        [HttpGet]
        [Route("GetAllLobbies")]
        public ActionResult<List<LobbyRoomModel>> GetAllLobbies()
        {
            var lobbies = _data.GetAllLobbies();
            return Ok(lobbies);
        }
    }
}
