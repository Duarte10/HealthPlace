using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPlace.Core.Models
{
    public class DbVersionModel : ModelBase
    {
        public string Version { get; set; }

        public override string ToString()
        {
            return "DbVersionModel";
        }
    }
}
