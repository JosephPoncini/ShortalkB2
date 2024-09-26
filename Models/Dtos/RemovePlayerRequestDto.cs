using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models.Dtos
{
    public class RemovePlayerRequestDto
    {
        public string RoomName { get; set; }
        public string PlayerName {get; set;}       
    }
}