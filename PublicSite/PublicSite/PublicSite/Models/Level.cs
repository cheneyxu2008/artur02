using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public partial class Level
    {
        public Level()
        {}

        public Level(CriterionLevel level)
        {
            Name = level.ToString();
            LevelNumber = (int) level;
        }
    }
}