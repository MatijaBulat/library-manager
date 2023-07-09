using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Author : BaseEntity
    {
        public override string ToString() => Name;
    }
}
