using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GenerateVideoRequest
    {
        public string Topic { get; set; }
        public string Style { get; set; }
        public int LengthSec { get; set; }
        public string Voice { get; set; }
    }
}
