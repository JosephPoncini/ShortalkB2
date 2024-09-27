using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ShortalkB2.Models;
using ShortalkB2.Models.Dtos;
using ShortalkB2.Service.Context;
using System.Collections.Generic;
using System.Linq;

namespace ShortalkB2.Service
{
    public class GameService
    {
        private readonly DataContext _context;

        // Inject DataContext through the constructor
        public GameService(DataContext context)
        {
            _context = context;
        }

        // Create a new game room
        public void CreateRoom(GameModel newGame, string username, string roomname)
        {
            newGame.Host = username;
            newGame.PlayerA1 = username;
            newGame.RoomName = roomname;

            _context.GameInfo.Add(newGame);
            _context.SaveChanges();
        }

        public bool DeleteRoom(int id)
        {
            GameModel game = _context.GameInfo.SingleOrDefault(l => l.ID == id);
            if (game != null)
            {
                _context.GameInfo.Remove(game);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // Get all existing game rooms
        public List<GameModel> GetAllRooms()
        {
            return _context.GameInfo.ToList();
        }

        public GameModel? GetRoomByName(string roomName)
        {
            return _context.GameInfo.FirstOrDefault(game => game.RoomName == roomName);
        }

        public bool CheckIfGameExists(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfNameExistsInGame(string roomName, string username)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return true;
            }

            bool isInGame = game.PlayerA1 == username ||
                             game.PlayerA2 == username ||
                             game.PlayerA3 == username ||
                             game.PlayerA4 == username ||
                             game.PlayerA5 == username ||
                             game.PlayerB1 == username ||
                             game.PlayerB2 == username ||
                             game.PlayerB3 == username ||
                             game.PlayerB4 == username ||
                             game.PlayerB5 == username;

            return isInGame;
        }

        public TeamMembersDto GetTeamMembersByRoom(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return new TeamMembersDto
            {
                PlayerA1 = game.PlayerA1,
                PlayerA2 = game.PlayerA2,
                PlayerA3 = game.PlayerA3,
                PlayerA4 = game.PlayerA4,
                PlayerA5 = game.PlayerA5,
                PlayerB1 = game.PlayerB1,
                PlayerB2 = game.PlayerB2,
                PlayerB3 = game.PlayerB3,
                PlayerB4 = game.PlayerB4,
                PlayerB5 = game.PlayerB5,
                ReadyStatusA1 = game.ReadyStatusA1,
                ReadyStatusA2 = game.ReadyStatusA2,
                ReadyStatusA3 = game.ReadyStatusA3,
                ReadyStatusA4 = game.ReadyStatusA4,
                ReadyStatusA5 = game.ReadyStatusA5,
                ReadyStatusB1 = game.ReadyStatusB1,
                ReadyStatusB2 = game.ReadyStatusB2,
                ReadyStatusB3 = game.ReadyStatusB3,
                ReadyStatusB4 = game.ReadyStatusB4,
                ReadyStatusB5 = game.ReadyStatusB5
            };
        }

        public int? GetTimeLimitByRoom(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.TimeLimit;
        }

        public int? GetNumOfRoundsByRoom(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.NumberOfRounds;
        }

        public string? GetHostByRoom(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.Host;
        }

        public string? GamePhaseByRoom(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.GamePhase;
        }

        public string JoinRoom(string roomName, string username)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            // Check if PlayerA1 is taken, if not, assign to PlayerA1
            if (string.IsNullOrEmpty(game.PlayerA1))
            {
                game.PlayerA1 = username;
            }
            // If PlayerA1 is taken, assign to PlayerB1
            else if (string.IsNullOrEmpty(game.PlayerB1))
            {
                game.PlayerB1 = username;
            }
            // If PlayerB1 is taken, assign to PlayerA2
            else if (string.IsNullOrEmpty(game.PlayerA2))
            {
                game.PlayerA2 = username;
            }
            // If PlayerA2 is taken, assign to PlayerB2
            else if (string.IsNullOrEmpty(game.PlayerB2))
            {
                game.PlayerB2 = username;
            }
            // Continue for the rest of the players...
            else if (string.IsNullOrEmpty(game.PlayerA3))
            {
                game.PlayerA3 = username;
            }
            else if (string.IsNullOrEmpty(game.PlayerB3))
            {
                game.PlayerB3 = username;
            }
            else if (string.IsNullOrEmpty(game.PlayerA4))
            {
                game.PlayerA4 = username;
            }
            else if (string.IsNullOrEmpty(game.PlayerB4))
            {
                game.PlayerB4 = username;
            }
            else if (string.IsNullOrEmpty(game.PlayerA5))
            {
                game.PlayerA5 = username;
            }
            else if (string.IsNullOrEmpty(game.PlayerB5))
            {
                game.PlayerB5 = username;
            }
            else
            {
                // All player spots are filled
                return "Room is full";
            }

            // Save the changes to the database
            _context.SaveChanges();

            return "Success";
        }

        public string ToggleTeam(string roomName, string username)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            bool ShiftTeamUp(GameModel game, string username, bool isInTeamA)
            {
                if (isInTeamA)
                {
                    if (game.PlayerA1 == username)
                    {
                        game.PlayerA1 = game.PlayerA2;
                        game.PlayerA2 = game.PlayerA3;
                        game.PlayerA3 = game.PlayerA4;
                        game.PlayerA4 = game.PlayerA5;
                        game.PlayerA5 = null;
                        game.ReadyStatusA1 = game.ReadyStatusA2;
                        game.ReadyStatusA2 = game.ReadyStatusA3;
                        game.ReadyStatusA3 = game.ReadyStatusA4;
                        game.ReadyStatusA4 = game.ReadyStatusA5;
                        game.ReadyStatusA5 = false;
                    }
                    else if (game.PlayerA2 == username)
                    {
                        game.PlayerA2 = game.PlayerA3;
                        game.PlayerA3 = game.PlayerA4;
                        game.PlayerA4 = game.PlayerA5;
                        game.PlayerA5 = null;
                        game.ReadyStatusA2 = game.ReadyStatusA3;
                        game.ReadyStatusA3 = game.ReadyStatusA4;
                        game.ReadyStatusA4 = game.ReadyStatusA5;
                        game.ReadyStatusA5 = false;
                    }
                    else if (game.PlayerA3 == username)
                    {
                        game.PlayerA3 = game.PlayerA4;
                        game.PlayerA4 = game.PlayerA5;
                        game.PlayerA5 = null;
                        game.ReadyStatusA3 = game.ReadyStatusA4;
                        game.ReadyStatusA4 = game.ReadyStatusA5;
                        game.ReadyStatusA5 = false;
                    }
                    else if (game.PlayerA4 == username)
                    {
                        game.PlayerA4 = game.PlayerA5;
                        game.PlayerA5 = null;
                        game.ReadyStatusA4 = game.ReadyStatusA5;
                        game.ReadyStatusA5 = false;
                    }
                    else if (game.PlayerA4 == username)
                    {
                        game.PlayerA5 = null;
                        game.ReadyStatusA5 = false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (game.PlayerB1 == username)
                    {
                        game.PlayerB1 = game.PlayerB2;
                        game.PlayerB2 = game.PlayerB3;
                        game.PlayerB3 = game.PlayerB4;
                        game.PlayerB4 = game.PlayerB5;
                        game.PlayerB5 = null;
                        game.ReadyStatusB1 = game.ReadyStatusB2;
                        game.ReadyStatusB2 = game.ReadyStatusB3;
                        game.ReadyStatusB3 = game.ReadyStatusB4;
                        game.ReadyStatusB4 = game.ReadyStatusB5;
                        game.ReadyStatusB5 = false;
                    }
                    else if (game.PlayerB2 == username)
                    {
                        game.PlayerB2 = game.PlayerB3;
                        game.PlayerB3 = game.PlayerB4;
                        game.PlayerB4 = game.PlayerB5;
                        game.PlayerB5 = null;
                        game.ReadyStatusB2 = game.ReadyStatusB3;
                        game.ReadyStatusB3 = game.ReadyStatusB4;
                        game.ReadyStatusB4 = game.ReadyStatusB5;
                        game.ReadyStatusB5 = false;
                    }
                    else if (game.PlayerB3 == username)
                    {
                        game.PlayerB3 = game.PlayerB4;
                        game.PlayerB4 = game.PlayerB5;
                        game.PlayerB5 = null;
                        game.ReadyStatusB3 = game.ReadyStatusB4;
                        game.ReadyStatusB4 = game.ReadyStatusB5;
                        game.ReadyStatusB5 = false;
                    }
                    else if (game.PlayerB4 == username)
                    {
                        game.PlayerB4 = game.PlayerB5;
                        game.PlayerB5 = null;
                        game.ReadyStatusB4 = game.ReadyStatusB5;
                        game.ReadyStatusB5 = false;
                    }
                    else if (game.PlayerB4 == username)
                    {
                        game.PlayerB5 = null;
                        game.ReadyStatusB5 = false;
                    }
                    else
                    {
                        return false;
                    }
                }

                // Save the changes to the database
                _context.SaveChanges();

                return true;
            }

            // Check if the username is in Team A
            bool isInTeamA = game.PlayerA1 == username ||
                             game.PlayerA2 == username ||
                             game.PlayerA3 == username ||
                             game.PlayerA4 == username ||
                             game.PlayerA5 == username;

            bool isInTeamB = game.PlayerB1 == username ||
                             game.PlayerB2 == username ||
                             game.PlayerB3 == username ||
                             game.PlayerB4 == username ||
                             game.PlayerB5 == username;

            if (isInTeamA)
            {
                if (string.IsNullOrEmpty(game.PlayerB1))
                {
                    game.PlayerB1 = username;

                }
                else if (string.IsNullOrEmpty(game.PlayerB2))
                {
                    game.PlayerB2 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerB3))
                {
                    game.PlayerB3 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerB4))
                {
                    game.PlayerB4 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerB5))
                {
                    game.PlayerB5 = username;
                }
                else
                {
                    // All player spots are filled
                    return "Cannot Toggle";
                }
            }
            else if (isInTeamB)
            {
                if (string.IsNullOrEmpty(game.PlayerA1))
                {
                    game.PlayerA1 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerA2))
                {
                    game.PlayerA2 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerA3))
                {
                    game.PlayerA3 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerA4))
                {
                    game.PlayerA4 = username;
                }
                else if (string.IsNullOrEmpty(game.PlayerA5))
                {
                    game.PlayerA5 = username;
                }
                else
                {
                    return "Unable to Toggle";
                }
            }
            else
            {
                return "Player not found";
            }

            // Save the changes to the database
            if (ShiftTeamUp(game, username, isInTeamA))
            {
                _context.SaveChanges();
            }
            else
            {
                return "Fail";
            }

            return "Success";
        }

        public string SetReadyStatus(string roomName, string username, bool isReady)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            if (game.PlayerA1 == username)
            {
                game.ReadyStatusA1 = isReady;
            }
            else if (game.PlayerA2 == username)
            {
                game.ReadyStatusA2 = isReady;
            }
            else if (game.PlayerA3 == username)
            {
                game.ReadyStatusA3 = isReady;
            }
            else if (game.PlayerA4 == username)
            {
                game.ReadyStatusA4 = isReady;
            }
            else if (game.PlayerA5 == username)
            {
                game.ReadyStatusA5 = isReady;
            }
            else if (game.PlayerB1 == username)
            {
                game.ReadyStatusB1 = isReady;
            }
            else if (game.PlayerB2 == username)
            {
                game.ReadyStatusB2 = isReady;
            }
            else if (game.PlayerB3 == username)
            {
                game.ReadyStatusB3 = isReady;
            }
            else if (game.PlayerB4 == username)
            {
                game.ReadyStatusB4 = isReady;
            }
            else if (game.PlayerB5 == username)
            {
                game.ReadyStatusB5 = isReady;
            }
            else
            {
                // Username does not match any players
                return "Player not found";
            }

            // Save the changes to the database
            _context.SaveChanges();

            return "Success";
        }

        public bool DeleteAllRooms()
        {
            try
            {
                var allGames = _context.GameInfo.ToList();
                if (allGames.Any())
                {
                    _context.GameInfo.RemoveRange(allGames);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ShuffleTeams(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            string?[] players =
            [
                game.PlayerA1,
                game.PlayerA2,
                game.PlayerA3,
                game.PlayerA4,
                game.PlayerA5,
                game.PlayerB1,
                game.PlayerB2,
                game.PlayerB3,
                game.PlayerB4,
                game.PlayerB5,
            ];
            bool[] readyStatuses =
            [
                game.ReadyStatusA1,
                game.ReadyStatusA2,
                game.ReadyStatusA3,
                game.ReadyStatusA4,
                game.ReadyStatusA5,
                game.ReadyStatusB1,
                game.ReadyStatusB2,
                game.ReadyStatusB3,
                game.ReadyStatusB4,
                game.ReadyStatusB5,
            ];

            // Shuffle the players array
            Random rnd = new Random();
            for (int i = players.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                string temp = players[i];
                bool temp2 = readyStatuses[i];
                players[i] = players[j];
                readyStatuses[i] = readyStatuses[j];
                players[j] = temp;
                readyStatuses[j] = temp2;
            }

            string?[] playersRandomized = new string?[10];
            bool[] statusesRandomized = new bool[10];
            int ii = 0;

            for (int i = 0; i < players.Length; i++)
            {
                playersRandomized[i] = null;
                statusesRandomized[i] = false;
                if (!string.IsNullOrEmpty(players[i]))
                {
                    playersRandomized[ii] = players[i];
                    statusesRandomized[ii] = readyStatuses[i];
                    ii++;
                }
            }

            int coinFlip = rnd.Next(0, 2);

            switch (coinFlip)
            {
                case 0:
                    game.PlayerA1 = playersRandomized[0];
                    game.PlayerB1 = playersRandomized[1];
                    game.PlayerA2 = playersRandomized[2];
                    game.PlayerB2 = playersRandomized[3];
                    game.PlayerA3 = playersRandomized[4];
                    game.PlayerB3 = playersRandomized[5];
                    game.PlayerA4 = playersRandomized[6];
                    game.PlayerB4 = playersRandomized[7];
                    game.PlayerA5 = playersRandomized[8];
                    game.PlayerB5 = playersRandomized[9];

                    game.ReadyStatusA1 = statusesRandomized[0];
                    game.ReadyStatusB1 = statusesRandomized[1];
                    game.ReadyStatusA2 = statusesRandomized[2];
                    game.ReadyStatusB2 = statusesRandomized[3];
                    game.ReadyStatusA3 = statusesRandomized[4];
                    game.ReadyStatusB3 = statusesRandomized[5];
                    game.ReadyStatusA4 = statusesRandomized[6];
                    game.ReadyStatusB4 = statusesRandomized[7];
                    game.ReadyStatusA5 = statusesRandomized[8];
                    game.ReadyStatusB5 = statusesRandomized[9];
                    break;
                case 1:
                    game.PlayerB1 = playersRandomized[0];
                    game.PlayerA1 = playersRandomized[1];
                    game.PlayerB2 = playersRandomized[2];
                    game.PlayerA2 = playersRandomized[3];
                    game.PlayerB3 = playersRandomized[4];
                    game.PlayerA3 = playersRandomized[5];
                    game.PlayerB4 = playersRandomized[6];
                    game.PlayerA4 = playersRandomized[7];
                    game.PlayerB5 = playersRandomized[8];
                    game.PlayerA5 = playersRandomized[9];

                    game.ReadyStatusB1 = statusesRandomized[0];
                    game.ReadyStatusA1 = statusesRandomized[1];
                    game.ReadyStatusB2 = statusesRandomized[2];
                    game.ReadyStatusA2 = statusesRandomized[3];
                    game.ReadyStatusB3 = statusesRandomized[4];
                    game.ReadyStatusA3 = statusesRandomized[5];
                    game.ReadyStatusB4 = statusesRandomized[6];
                    game.ReadyStatusA4 = statusesRandomized[7];
                    game.ReadyStatusB5 = statusesRandomized[8];
                    game.ReadyStatusA5 = statusesRandomized[9];
                    break;
            }

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Teams shuffled and saved successfully.";
            }
            else
            {
                return "Failed to shuffle teams.";
            }
        }

        public string ChangeTimeLimit(string roomName, int timeLimit)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            game.TimeLimit = timeLimit;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Time Limit has successfully updated";
            }
            else
            {
                return "Failed to update Time Limit";
            }
        }

        public string ChangeNumberOfRounds(string roomName, int numberOfRounds)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            game.NumberOfRounds = numberOfRounds;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The number of rounds has successfully updated";
            }
            else
            {
                return "Failed to update the number of rounds";
            }
        }

        public string RemovePlayer(string roomName, string playerName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            switch (playerName)
            {
                case string p when p == game.PlayerA1:
                    game.PlayerA1 = game.PlayerA2;
                    game.PlayerA2 = game.PlayerA3;
                    game.PlayerA3 = game.PlayerA4;
                    game.PlayerA4 = game.PlayerA5;
                    game.PlayerA5 = null;
                    break;
                case string p when p == game.PlayerA2:
                    game.PlayerA2 = game.PlayerA3;
                    game.PlayerA3 = game.PlayerA4;
                    game.PlayerA4 = game.PlayerA5;
                    game.PlayerA5 = null;
                    break;
                case string p when p == game.PlayerA3:
                    game.PlayerA3 = game.PlayerA4;
                    game.PlayerA4 = game.PlayerA5;
                    game.PlayerA5 = null;
                    break;
                case string p when p == game.PlayerA4:
                    game.PlayerA4 = game.PlayerA5;
                    game.PlayerA5 = null;
                    break;
                case string p when p == game.PlayerA5:
                    game.PlayerA5 = null;
                    break;
                case string p when p == game.PlayerB1:
                    game.PlayerB1 = game.PlayerB2;
                    game.PlayerB2 = game.PlayerB3;
                    game.PlayerB3 = game.PlayerB4;
                    game.PlayerB4 = game.PlayerB5;
                    game.PlayerB5 = null;
                    break;
                case string p when p == game.PlayerB2:
                    game.PlayerB2 = game.PlayerB3;
                    game.PlayerB3 = game.PlayerB4;
                    game.PlayerB4 = game.PlayerB5;
                    game.PlayerB5 = null;
                    break;
                case string p when p == game.PlayerB3:
                    game.PlayerB3 = game.PlayerB4;
                    game.PlayerB4 = game.PlayerB5;
                    game.PlayerB5 = null;
                    break;
                case string p when p == game.PlayerB4:
                    game.PlayerB4 = game.PlayerB5;
                    game.PlayerB5 = null;
                    break;
                case string p when p == game.PlayerB5:
                    game.PlayerB5 = null;
                    break;
                default:
                    return $"{playerName} was not found";
            }

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return $"{playerName} has successfully been remove";
            }
            else
            {
                return $"{playerName} has failed to be remove";
            }
        }

        public string ChangeGamePhase(string roomName, string gamePhase)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            game.GamePhase = gamePhase;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Game Phase has successfully updated";
            }
            else
            {
                return "Failed to update Game Phase";
            }
        }

        public DateTime GetTime()
        {
            var currentTime = DateTime.UtcNow;
            return currentTime;
        }

        public string SetStartTimeForRound(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            DateTime currentTime = GetTime();

            TimeSpan timeSinceEpoch = currentTime - epoch;

            game.Time = (int)timeSinceEpoch.TotalSeconds;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Start Time successfully implemented";
            }
            else
            {
                return "Failed to implement start time";
            }

        }

        public int? GetStartTime(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.Time;
        }
    }



}
