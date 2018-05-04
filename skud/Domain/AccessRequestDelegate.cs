using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skud.Domain
{
    public delegate bool AccessRequestDelegate(ulong uid, Direction direction);
}
