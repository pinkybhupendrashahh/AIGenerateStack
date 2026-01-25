using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Interfaces
{
    public interface ISceneParser
    {
        List<Scene> Parse(string script);
    }
}
