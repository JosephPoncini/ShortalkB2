using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models.Dtos
{
    public class ChangeGamePhaseRequestDto
    {
        public string RoomName { get; set; }
        public string GamePhase {get; set;}        
    }
}