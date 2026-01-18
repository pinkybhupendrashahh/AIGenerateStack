using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class OllamaOptions
    {
        public string BaseUrl { get; set; }
        public string Model { get; set; }
    }

}
