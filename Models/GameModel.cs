using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models
{
    public class GameModel
    {
        public int ID { get; set; }
        public string? RoomName { get; set; }
        public string? GamePhase { get; set; } = "lobby";
        public string? Host {get; set;}
        public int Round {get; set;} = 1;
        public int NumberOfRounds {get; set;} = 1;
        public int TimeLimit {get; set;} = 90;
        public int Time {get; set;}
        public int TeamAScore {get; set;} = 0;
        public int TeamBScore {get; set;} = 0;
        public string? Speaker {get; set;}
        public int TurnNumber {get; set;} = 0;
        public string? OnePointWord { get; set; } = string.Empty;
        public string? ThreePointWord { get; set; } = string.Empty;
        public string? PlayerA1 { get; set; }
        public string? PlayerA2 { get; set; }
        public string? PlayerA3 { get; set; }
        public string? PlayerA4 { get; set; }
        public string? PlayerA5 { get; set; }
        public string? PlayerB1 { get; set; }
        public string? PlayerB2 { get; set; }
        public string? PlayerB3 { get; set; }
        public string? PlayerB4 { get; set; }
        public string? PlayerB5 { get; set; }
        public bool ReadyStatusA1 {get; set;}
        public bool ReadyStatusA2 {get; set;}
        public bool ReadyStatusA3 {get; set;}
        public bool ReadyStatusA4 {get; set;}
        public bool ReadyStatusA5 {get; set;}
        public bool ReadyStatusB1 {get; set;}
        public bool ReadyStatusB2 {get; set;}
        public bool ReadyStatusB3 {get; set;}
        public bool ReadyStatusB4 {get; set;}
        public bool ReadyStatusB5 {get; set;}
        public bool OnePointWordHasBeenSaid { get; set; }
        public bool ThreePointWordHasBeenSaid { get; set; }
        public string? BuzzWords {get; set;} = string.Empty;
        public string? SkippedWords {get; set;} = string.Empty;
        public string? OnePointWords {get; set;} = string.Empty;
        public string? ThreePointWords {get; set;} = string.Empty;
        public GameModel()
        {
            
        }
    }
}