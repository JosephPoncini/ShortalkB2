using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models
{
    public class LobbyRoomModel
    {
        public int ID { get; set; }
        public string LobbyName { get; set; }
        public string Host {get; set;}
        public int NumberOfRounds {get; set;}
        public int TimeLimit {get; set;}
        public string TeamMemberA1 { get; set; }
        public string TeamMemberA2 { get; set; }
        public string TeamMemberA3 { get; set; }
        public string TeamMemberA4 { get; set; }
        public string TeamMemberA5 { get; set; }
        public string TeamMemberB1 { get; set; }
        public string TeamMemberB2 { get; set; }
        public string TeamMemberB3 { get; set; }
        public string TeamMemberB4 { get; set; }
        public string TeamMemberB5 { get; set; }
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
        public LobbyRoomModel()
        {
            
        }
    }
}