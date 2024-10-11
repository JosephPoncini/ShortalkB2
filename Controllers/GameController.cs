using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using ShortalkB2.Models;
using ShortalkB2.Models.Dtos;
using ShortalkB2.Service;
using System.Collections.Generic;

namespace ShortalkB2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _data;

        public GameController(GameService data)
        {
            _data = data;
        }

        // Endpoint to create a new lobby room
        [HttpPost]
        [Route("createGame")]
        public IActionResult CreateRoom([FromBody] LobbyRoomRequestDto request)
        {
            var newGame = new GameModel();
            _data.CreateRoom(newGame, request.Username, request.RoomName);
            return Ok("Game room created successfully.");
        }

        // Endpoint to delete a lobby room by ID
        [HttpDelete]
        [Route("deleteGame/{id}")]
        public IActionResult DeleteRoom(int id)
        {
            bool result = _data.DeleteRoom(id);
            if (result)
            {
                return Ok("Room deleted successfully.");
            }
            return NotFound("Room not found.");
        }

        // Endpoint to get all existing lobby rooms
        [HttpGet]
        [Route("GetAllRooms")]
        public ActionResult<List<GameModel>> GetAllRooms()
        {
            var lobbies = _data.GetAllRooms();
            return Ok(lobbies);
        }

        [HttpGet]
        [Route("CheckIfRoomExists/{roomName}")]
        public ActionResult<List<GameModel>> CheckIfGameExists(string roomName)
        {
            bool result = _data.CheckIfGameExists(roomName);
            return Ok(result);
        }

        [HttpGet]
        [Route("CheckIfNameExistsInGame/{roomName}/{username}")]
        public ActionResult<List<GameModel>> CheckIfNameExistsInGame(string roomName, string username)
        {
            bool result = _data.CheckIfNameExistsInGame(roomName, username);
            return Ok(result);
        }

        [HttpGet]
        [Route("getTeamMembersByRoom/{roomName}")]
        public ActionResult<TeamMembersDto> GetTeamMembersByRoom(string roomName)
        {
            var teamMembers = _data.GetTeamMembersByRoom(roomName);

            if (teamMembers == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(teamMembers);
        }

        [HttpGet]
        [Route("getTimeLimitByRoom/{roomName}")]
        public ActionResult<int?> GetTimeLimitByRoom(string roomName)
        {
            var timeLimit = _data.GetTimeLimitByRoom(roomName);

            if (timeLimit == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(timeLimit);
        }

        [HttpGet]
        [Route("getNumOfRoundsByRoom/{roomName}")]
        public ActionResult<int?> GetNumOfRoundsByRoom(string roomName)
        {
            var timeLimit = _data.GetNumOfRoundsByRoom(roomName);

            if (timeLimit == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(timeLimit);
        }

        [HttpGet]
        [Route("getHostByRoom/{roomName}")]
        public ActionResult<string?> GetHostByRoom(string roomName)
        {
            var timeLimit = _data.GetHostByRoom(roomName);

            if (timeLimit == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(timeLimit);
        }

        [HttpGet]
        [Route("getGamePhaseByRoom/{roomName}")]
        public ActionResult<string?> GetGamePhaseByRoom(string roomName)
        {
            var gamePhase = _data.GamePhaseByRoom(roomName);

            if (gamePhase == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(gamePhase);
        }


        [HttpPost]
        [Route("joinRoom")]
        public ActionResult JoinRoom([FromBody] LobbyRoomRequestDto request)
        {
            var result = _data.JoinRoom(request.RoomName, request.Username);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }
            else if (result == "Room is full")
            {
                return BadRequest("Room is full.");
            }

            return Ok($"{request.Username} joined the room {request.RoomName} successfully.");
        }

        [HttpPost]
        [Route("toggleTeam")]
        public ActionResult ToggleTeam([FromBody] LobbyRoomRequestDto request)
        {
            var result = _data.ToggleTeam(request.RoomName, request.Username);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }

            return Ok($"{request.Username} has toggled successfully.");
        }

        [HttpPost]
        [Route("shuffleTeams")]
        public ActionResult ShuffleTeams([FromBody] LobbyRoomRequestDto request)
        {
            var result = _data.ShuffleTeams(request.RoomName);

            return Ok(result);
        }

        [HttpPost]
        [Route("removePlayer")]
        public ActionResult RemovePlayer([FromBody] RemovePlayerRequestDto request)
        {
            var result = _data.RemovePlayer(request.RoomName, request.PlayerName);

            return Ok(result);
        }


        [HttpPost]
        [Route("setReadyStatus")]
        public ActionResult SetReadyStatus([FromBody] SetReadyStatusDto request)
        {
            var result = _data.SetReadyStatus(request.RoomName, request.Username, request.IsReady);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }
            else if (result == "Player not found")
            {
                return BadRequest("Player not found in the room.");
            }

            return Ok($"Ready status for {request.Username} has been set to {request.IsReady}.");
        }

        [HttpPost]
        [Route("changeTimeLimit")]
        public ActionResult ChangeTimeLimit([FromBody] ChangeTimeLimitRequestDto request)
        {
            var result = _data.ChangeTimeLimit(request.RoomName, request.TimeLimit);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }
            else if (result == "Player not found")
            {
                return BadRequest("Player not found in the room.");
            }

            return Ok($"The Time Limit has been set to {request.TimeLimit}.");
        }

        [HttpPost]
        [Route("changeNumberOfRounds")]
        public ActionResult ChangeNumberOfRounds([FromBody] ChangeNumOfRoundsRequestDto request)
        {
            var result = _data.ChangeNumberOfRounds(request.RoomName, request.NumberOfRounds);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }
            else if (result =="Failed to update the number of rounds")
            {
                return BadRequest("Failed to update the number of rounds");
            }

            return Ok($"The number of rounds has successfully updated to {request.NumberOfRounds}.");
        }

        [HttpGet]
        [Route("getRoomByName/{roomName}")]
        public ActionResult<GameModel> GetRoomByName(string roomName)
        {
            var room = _data.GetRoomByName(roomName);

            if (room == null)
            {
                return NotFound($"Room with name '{roomName}' not found.");
            }

            return Ok(room);
        }

        [HttpDelete]
        [Route("deleteAllGames")]
        public IActionResult DeleteAllRooms()
        {
            bool result = _data.DeleteAllRooms();
            if (result)
            {
                return Ok("All rooms deleted successfully.");
            }
            return StatusCode(500, "An error occurred while deleting the rooms.");
        }

        [HttpPost]
        [Route("changeGamePhase")]
        public ActionResult ChangeGamePhase([FromBody] ChangeGamePhaseRequestDto request)
        {
            var result = _data.ChangeGamePhase(request.RoomName, request.GamePhase);

            if (result == "Room not found")
            {
                return NotFound("Room not found.");
            }

            return Ok($"The Game has been set to {request.GamePhase}.");
        }

        [HttpGet]
        [Route("getTime")]
        public IActionResult GetCurrentTime()
        {
            var currentTime = _data.GetTime();
            return Ok(currentTime);
        }

        [HttpPost]
        [Route("setStartTimeForRound/{roomName}")]
        public ActionResult SetStartTimeForRound(string roomName)
        {
            var result = _data.SetStartTimeForRound(roomName);

            return Ok(result);
        }

        [HttpGet]
        [Route("getStartTimeByRoom/{roomName}")]
        public ActionResult<int?> GetStartTime(string roomName)
        {
            var timeLimit = _data.GetStartTime(roomName);

            if (timeLimit == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(timeLimit);
        }
        
        [HttpGet]
        [Route("getCard/{roomName}")]
        public ActionResult<CardDto> GetCard(string roomName)
        {
            CardDto card = _data.GetCard(roomName);

            return Ok(card);
        }
        
        [HttpGet]
        [Route("getTurnNumber/{roomName}")]
        public ActionResult<int> GetTurnNumber(string roomName)
        {
            int? turnNumber = _data.GetTurnNumber(roomName);

            return Ok(turnNumber);
        }
        
        [HttpGet]
        [Route("getAllWords/{roomName}")]
        public ActionResult<AllWordsDto> GetAllWords(string roomName)
        {
            AllWordsDto words = _data.GetAllWords(roomName);

            return Ok(words);
        }

        [HttpPost]
        [Route("addSkippedWord")]
        public IActionResult AddSkippedWord([FromBody] SubmitCardRequestDto request)
        {
            string msg = _data.AddSkippedWord(request);
            return Ok(msg);
        }

        [HttpPost]
        [Route("addBuzzedWord")]
        public IActionResult AddBuzzedWord([FromBody] SubmitCardRequestDto request)
        {
            string msg = _data.AddBuzzedWord(request);
            return Ok(msg);
        }

        [HttpPost]
        [Route("addOnePointWord")]
        public IActionResult AddOnePointWord([FromBody] SubmitCardRequestDto request)
        {
            string msg = _data.AddOnePointWord(request);
            return Ok(msg);
        }

        [HttpPost]
        [Route("addThreePointWord")]
        public IActionResult AddThreePointWord([FromBody] SubmitCardRequestDto request)
        {
            string msg = _data.AddThreePointWord(request);
            return Ok(msg);
        }

        [HttpPost]
        [Route("cleanSlate/{roomName}")]
        public IActionResult CleanSlate(string roomName)
        {
            string msg = _data.CleanSlate(roomName);
            return Ok(msg);
        }

        [HttpGet]
        [Route("getWordsBeenSaid/{roomName}")]
        public WordsHaveBeenSaidDto GetWordsHaveBeenSaid( string roomName)
        {
            return _data.GetWordsHaveBeenSaid(roomName);
        }

        [HttpGet]
        [Route("getScores/{roomName}")]
        public ScoresDto GetScores( string roomName)
        {
            return _data.GetScores(roomName);
        }

    }
}
