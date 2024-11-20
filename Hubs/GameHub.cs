using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ShortalkB2.Models;
using ShortalkB2.Service;
using ShortalkB2.Service.Context;
using SQLitePCL;

namespace ShortalkB2.Hubs;

public class GameHub : Hub
{
    private readonly DataContext _context;
    private readonly GameService _data;
    public GameHub(DataContext context, GameService data)
    {
        _context = context;
        _data = data;

    }

    public async Task JoinSpecificGame(UserConnection conn)
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, conn.RoomName);

        _context.connections[Context.ConnectionId] = conn;

        await Clients.Group(conn.RoomName)
            .SendAsync("JoinSpecificGame", "admin", $"{conn.Username} has joined the game");

    }

    public async Task SendMessage(UserConnection conn, string msg)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("SendMessage", conn.Username, msg);
    }

    public async Task RefreshTeams(UserConnection conn, string msg)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshTeams", conn.Username, msg);
    }

    public async Task RefreshTime(UserConnection conn, int time)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshTime", "admin", $"The time has been set to {time/60}:{time%60}.");
    }

    public async Task BanPlayer(UserConnection conn, string player)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("BanPlayer", "admin", player);
    }

    public async Task RefreshRounds(UserConnection conn, int round)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshRounds", "admin", $"The game has been set to {round} rounds.");
    }
    
    public async Task RefreshGamePhase(UserConnection conn, string gamePhase)
    {
        if(gamePhase == "game")
        {
            _data.SetStartTimeForRound(conn.RoomName);
        }
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshGamePhase", "admin", $"The game has been set the game {gamePhase} mode.");
    }

    public async Task RefreshCard (UserConnection conn, string msg)
    {
        _data.ChangeCard(conn.RoomName);

        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshCard", conn.Username, msg);
    }

    public async Task GoToNextTurn (UserConnection conn)
    {
        string msg = _data.StartNextTurn(conn.RoomName);

        await Clients.Group(conn.RoomName)
            .SendAsync("GoToNextTurn", "admin", msg);
    }
    
    public async Task TypeDescription (UserConnection conn, string msg)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("TypeDescription", "admin", msg);
    }
    
    public async Task SendGuess(UserConnection conn, string guess)
    {
        string color = _data.CheckGuess(conn.RoomName, guess);

        await Clients.Group(conn.RoomName)
            .SendAsync("SendGuess", conn.Username, guess, color);
    }

}
