using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using TwitterLib;

namespace MINTIA_Display
{
    [Serializable]
    public class Settings
    {
        // Twitter //
        public string TwitterID { get; set; }
        public string TwitterTokenValue { get; set; }
        public string TwitterTokenSecret { get; set; }
    }
}
