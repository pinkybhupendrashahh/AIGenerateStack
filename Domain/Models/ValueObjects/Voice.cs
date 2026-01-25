using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.ValueObjects
{
   
        public class Voice
        {
            public string Name { get; }

            [JsonConstructor]
            public Voice(string name)
            {
                Name = name;
            }
        

        public static Voice Create(string name)
            => new Voice(name);

        public override string ToString() => Name;
    }
}
