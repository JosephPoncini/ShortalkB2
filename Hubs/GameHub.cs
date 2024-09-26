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

    public async Task RefreshRounds(UserConnection conn, int round)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshRounds", "admin", $"The game has been set to {round} rounds.");
    }
    
    public async Task RefreshGamePhase(UserConnection conn, string gamePhase)
    {
        await Clients.Group(conn.RoomName)
            .SendAsync("RefreshGamePhase", "admin", $"The game has been set the game {gamePhase} mode.");
    }
    

}
