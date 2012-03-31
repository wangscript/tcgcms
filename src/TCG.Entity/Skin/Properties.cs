using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class Properties : EntityBase
    {
        public string PropertiesCategorieId { get; set; }
        public string ProertieName { get; set; }
        public string Type { get; set; }
        public string Values { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int iOrder { get; set; }

    }
}
