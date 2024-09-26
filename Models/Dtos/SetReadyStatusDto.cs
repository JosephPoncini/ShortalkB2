using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models.Dtos
{
    public class SetReadyStatusDto
    {
        public string Username { get; set; }
        public string RoomName { get; set; }
        public bool IsReady { get; set; }
    }
}