using ShortalkB2.Models;
using ShortalkB2.Service.Context;
using System.Collections.Generic;
using System.Linq;

namespace ShortalkB2.Service
{
    public class LobbyService
    {
        private readonly DataContext _context;

        // Inject DataContext through the constructor
        public LobbyService(DataContext context)
        {
            _context = context;
        }

        // Create a new lobby room
        public void CreateLobbyRoom(LobbyRoomModel newLobby)
        {
            _context.LobbyInfo.Add(newLobby);
            _context.SaveChanges();
        }

        

        // Delete a lobby room by ID
        public bool DeleteLobbyRoom(int id)
        {
            LobbyRoomModel lobby = _context.LobbyInfo.SingleOrDefault(l => l.ID == id);
            if (lobby != null)
            {
                _context.LobbyInfo.Remove(lobby);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // Get all existing lobby rooms
        public List<LobbyRoomModel> GetAllLobbies()
        {
            return _context.LobbyInfo.ToList();
        }
    }
}
