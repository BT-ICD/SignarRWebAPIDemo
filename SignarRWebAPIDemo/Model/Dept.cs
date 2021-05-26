using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Model
{
    public class Dept
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Loc { get; set; }
        public static List<Dept> list = new List<Dept>()
            {
                new Dept(){Id=10, Name="Accounting", Loc="Dallas"},
                new Dept(){Id=20, Name="Research", Loc="New Jersey"},
                 new Dept(){Id=30, Name="Operations", Loc="Boston"}
            };
    }
}
