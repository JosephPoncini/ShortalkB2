using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ShortalkB2.Models;
using ShortalkB2.Models.Dtos;
using ShortalkB2.Service.Context;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System;
using System.IO;
using System.Collections.Generic;

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

        public string? ChangeCard(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }

            string json = File.ReadAllText("words.json");
            List<CardWithChoices> data = JsonSerializer.Deserialize<List<CardWithChoices>>(json);

            Random random = new Random();

            int index = random.Next(0, data.Count);

            CardWithChoices randomCardChoice = data[index];

            Random random2 = new Random();
            int indexj = random2.Next(0, randomCardChoice.SecondWords.Count);

            CardDto card = new CardDto();

            int coinFlip = random.Next(0, 2);

            switch (coinFlip)
            {
                case 0:
                    card.FirstWord = randomCardChoice.FirstWord;
                    card.SecondWord = randomCardChoice.FirstWord + " " + randomCardChoice.SecondWords[indexj];
                    break;
                case 1:
                    card.FirstWord = randomCardChoice.SecondWords[indexj];
                    card.SecondWord = randomCardChoice.FirstWord + " " + randomCardChoice.SecondWords[indexj];
                    break;
            }

            if (card.SecondWord.Length + card.FirstWord.Length > 19)
            {
                return ChangeCard(roomName);
            }

            if (card.SecondWord != game.ThreePointWord)
            {
                game.OnePointWord = card.FirstWord;
                game.ThreePointWord = card.SecondWord;
                game.OnePointWordHasBeenSaid = false;
                game.ThreePointWordHasBeenSaid = false;

                _context.Update(game);
                int saveResult = _context.SaveChanges();

                if (saveResult != 0)
                {
                    return "New Card Picked!";
                }
                else
                {
                    return "Failed to pick new card";
                }
            }

            return ChangeCard(roomName);

        }

        public CardDto? GetCard(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return new CardDto
            {
                FirstWord = game.OnePointWord,
                SecondWord = game.ThreePointWord
            };

        }

        public string StartNextTurn(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "Room not found";
            }



            int teamACount = 0;
            int teamBCount = 0;

            // Count players on Team A
            if (!string.IsNullOrEmpty(game.PlayerA1)) teamACount++;
            if (!string.IsNullOrEmpty(game.PlayerA2)) teamACount++;
            if (!string.IsNullOrEmpty(game.PlayerA3)) teamACount++;
            if (!string.IsNullOrEmpty(game.PlayerA4)) teamACount++;
            if (!string.IsNullOrEmpty(game.PlayerA5)) teamACount++;

            // Count players on Team B
            if (!string.IsNullOrEmpty(game.PlayerB1)) teamBCount++;
            if (!string.IsNullOrEmpty(game.PlayerB2)) teamBCount++;
            if (!string.IsNullOrEmpty(game.PlayerB3)) teamBCount++;
            if (!string.IsNullOrEmpty(game.PlayerB4)) teamBCount++;
            if (!string.IsNullOrEmpty(game.PlayerB5)) teamBCount++;

            game.TurnNumber++;

            if (game.NumberOfRounds * Math.Max(teamACount, teamBCount) * 2 == game.TurnNumber)
            {
                game.GamePhase = "lastTurn";
            }



            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The Next turn has begun";
            }
            else
            {
                return "Failed to go to next turn";
            }
        }

        public int? GetTurnNumber(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return null;
            }

            return game.TurnNumber;

        }

        public string AddSkippedWord(SubmitCardRequestDto request)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == request.RoomName);

            if (game == null)
            {
                return "could not find room";
            }

            string card = $"{request.Card.FirstWord}_{request.Card.SecondWord}-";
            game.SkippedWords += card;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The card has been added to Skipped Words";
            }
            else
            {
                return "Failed to add card to Skipped Words";
            }
        }

        public string AddBuzzedWord(SubmitCardRequestDto request)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == request.RoomName);

            if (game == null)
            {
                return "could not find room";
            }

            if (game.TurnNumber % 2 == 1)
            {
                game.TeamAScore -= 1;
            }
            else
            {
                game.TeamBScore -= 1;
            }

            string card = $"{request.Card.FirstWord}_{request.Card.SecondWord}-";
            game.BuzzWords += card;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The card has been added to Buzzed Words";
            }
            else
            {
                return "Failed to add card to Buzzed Words";
            }
        }

        public string AddOnePointWord(SubmitCardRequestDto request)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == request.RoomName);

            if (game == null)
            {
                return "could not find room";
            }

            if (game.TurnNumber % 2 == 1)
            {
                game.TeamAScore += 1;
            }
            else
            {
                game.TeamBScore += 1;
            }

            string card = $"{request.Card.FirstWord}_{request.Card.SecondWord}-";
            game.OnePointWords += card;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The card has been added to One Point Words";
            }
            else
            {
                return "Failed to add card to One Point Words";
            }
        }

        public string AddThreePointWord(SubmitCardRequestDto request)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == request.RoomName);

            if (game == null)
            {
                return "could not find room";
            }

            if (game.TurnNumber % 2 == 1)
            {
                game.TeamAScore += 3;
            }
            else
            {
                game.TeamBScore += 3;
            }

            string card = $"{request.Card.FirstWord}_{request.Card.SecondWord}-";
            game.ThreePointWords += card;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "The card has been added to Three Point Words";
            }
            else
            {
                return "Failed to add card to Three Point Words";
            }
        }

        public AllWordsDto GetAllWords(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            return new AllWordsDto
            {
                SkippedWords = game.SkippedWords,
                OnePointWords = game.OnePointWords,
                ThreePointWords = game.ThreePointWords,
                BuzzWords = game.BuzzWords
            };

        }

        public string CleanSlate(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "could not find room";
            }

            game.BuzzWords = "";
            game.SkippedWords = "";
            game.OnePointWords = "";
            game.ThreePointWords = "";

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Slate has been cleaned";
            }
            else
            {
                return "Failed to clean slate";
            }

        }
        public string CleanLobby(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "could not find room";
            }

            game.TurnNumber = 0;
            game.PlayerA1 = null;
            game.PlayerA2 = null;
            game.PlayerA3 = null;
            game.PlayerA4 = null;
            game.PlayerA5 = null;
            game.PlayerB1 = null;
            game.PlayerB2 = null;
            game.PlayerB3 = null;
            game.PlayerB4 = null;
            game.PlayerB5 = null;
            game.ReadyStatusA1 = false;
            game.ReadyStatusA2 = false;
            game.ReadyStatusA3 = false;
            game.ReadyStatusA4 = false;
            game.ReadyStatusA5 = false;
            game.ReadyStatusB1 = false;
            game.ReadyStatusB2 = false;
            game.ReadyStatusB3 = false;
            game.ReadyStatusB4 = false;
            game.ReadyStatusB5 = false;

            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Lobby has been cleaned";
            }
            else
            {
                return "Failed to clean lobby";
            }

        }

        public string CleanScore(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "could not find room";
            }

            game.TeamAScore = 0;
            game.TeamBScore = 0;


            _context.Update(game);
            int saveResult = _context.SaveChanges();

            if (saveResult != 0)
            {
                return "Score has been cleaned";
            }
            else
            {
                return "Failed to clean score";
            }

        }

        public string CheckGuess(string roomName, string guess)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);

            if (game == null)
            {
                return "dgray";
            }

            if (guess == game.OnePointWord)
            {
                game.OnePointWordHasBeenSaid = true;
                _context.Update(game);
                _context.SaveChanges();
                return "green";
            }

            if (guess == game.ThreePointWord)
            {
                game.ThreePointWordHasBeenSaid = true;
                _context.Update(game);
                _context.SaveChanges();
                return "purple";
            }

            if (IsGuessClose(game.OnePointWord, game.ThreePointWord, guess))
            {
                return "yellow";
            }

            return "black";
        }

        public WordsHaveBeenSaidDto GetWordsHaveBeenSaid(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);
            return new WordsHaveBeenSaidDto
            {
                OnePointWordHasBeenSaid = game.OnePointWordHasBeenSaid,
                ThreePointWordHasBeenSaid = game.ThreePointWordHasBeenSaid
            };
        }

        public ScoresDto GetScores(string roomName)
        {
            var game = _context.GameInfo.FirstOrDefault(g => g.RoomName == roomName);
            return new ScoresDto
            {
                TeamAScore = game.TeamAScore,
                TeamBScore = game.TeamBScore
            };
        }

        public bool IsGuessClose(string? onePointWord, string? threePointWord, string guess)
        {
            bool result = AreStringsOffByOneChar(RemoveSpacesAndLowercase(onePointWord), RemoveSpacesAndLowercase(guess)) || AreStringsOffByOneChar(RemoveSpacesAndLowercase(threePointWord), RemoveSpacesAndLowercase(guess));

            return result;
        }

        public static string RemoveSpacesAndLowercase(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Remove all spaces and convert to lowercase
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        }

        public bool AreStringsOffByOneChar(string str1, string str2)
        {
            if (str1 == null || str2 == null)
            {
                throw new ArgumentNullException("Input strings cannot be null");
            }

            // If lengths differ by more than 1, they cannot be off by just one char
            if (Math.Abs(str1.Length - str2.Length) > 1)
            {
                return false;
            }

            // If the lengths are the same, check for one differing character
            if (str1.Length == str2.Length)
            {
                int mismatchCount = 0;

                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] != str2[i])
                    {
                        mismatchCount++;
                        if (mismatchCount > 1)
                        {
                            return false;
                        }
                    }
                }

                return mismatchCount == 1;
            }

            // If lengths differ by exactly 1, check for insert/remove case
            if (str1.Length > str2.Length)
            {
                return IsOneEditDistance(str2, str1);
            }
            else
            {
                return IsOneEditDistance(str1, str2);
            }
        }

        private static bool IsOneEditDistance(string shorter, string longer)
        {
            int i = 0, j = 0;

            while (i < shorter.Length && j < longer.Length)
            {
                if (shorter[i] != longer[j])
                {
                    // If there is a mismatch, move the pointer of the longer string
                    if (i != j)
                    {
                        return false;
                    }
                    j++;
                }
                else
                {
                    i++;
                    j++;
                }
            }

            return true;
        }
    }



}
