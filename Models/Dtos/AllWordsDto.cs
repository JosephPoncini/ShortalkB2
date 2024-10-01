using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortalkB2.Models.Dtos
{
    public class AllWordsDto
    {
        public string SkippedWords {get; set;}
        public string OnePointWords {get; set;}
        public string ThreePointWords {get; set;}
        public string BuzzWords {get; set;}
    }
}