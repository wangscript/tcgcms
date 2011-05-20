using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Entity
{
    public class CategorieProperties : EntityBase
    {
        public string CategorieId { get; set; }
        public string ProertieName { get; set; }
        public string Type { get; set; }
        public string Values { get; set; }
        public int width { get; set; }
        public int height { get; set; }

    }
}
