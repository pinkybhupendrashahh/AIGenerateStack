using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Git
{
    public class GitHubOptions
    {
        public string Owner { get; set; } = "";
        public string Repo { get; set; } = "";
        public string Branch { get; set; } = "main";
        public string Token { get; set; } = "";
        public string AudioFolder { get; set; } = "audio";
    }

}
