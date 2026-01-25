using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Video
{
    using Application.Interfaces;
    using Domain;
    using Domain.Models;
    using System.Text.RegularExpressions;
    using static System.Formats.Asn1.AsnWriter;

    public class SceneParser : ISceneParser
    {
        public List<Scene> Parse(string script)
        {
            var scenes = new List<Scene>();

            var matches = Regex.Matches(
                script,
                @"Scene\s*(\d+)\s*:\s*(.+)",
                RegexOptions.IgnoreCase
            );

            foreach (Match match in matches)
            {
                scenes.Add(new Scene
                {
                    Order = int.Parse(match.Groups[1].Value),
                    Description = match.Groups[2].Value.Trim()
                });
            }

            // fallback if LLM didn’t label scenes
            if (!scenes.Any())
            {
                var lines = script
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries);

                int i = 1;
                foreach (var line in lines)
                {
                    scenes.Add(new Scene
                    {
                        Order = i++,
                        Description = line.Trim()
                    });
                }
            }

            return scenes;
        }
    }

}
