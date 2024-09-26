using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models.Dtos
{
    public class TeamMembersDto
    {
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
        public bool ReadyStatusA1 { get; set; }
        public bool ReadyStatusA2 { get; set; }
        public bool ReadyStatusA3 { get; set; }
        public bool ReadyStatusA4 { get; set; }
        public bool ReadyStatusA5 { get; set; }
        public bool ReadyStatusB1 { get; set; }
        public bool ReadyStatusB2 { get; set; }
        public bool ReadyStatusB3 { get; set; }
        public bool ReadyStatusB4 { get; set; }
        public bool ReadyStatusB5 { get; set; }
    }
}