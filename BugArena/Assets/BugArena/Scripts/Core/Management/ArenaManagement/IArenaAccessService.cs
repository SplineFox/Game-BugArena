using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugArena
{
    public interface IArenaAccessService
    {
        Arena Arena { get; set; }
        PlayerSpawner PlayerSpawner { get; set; }
        ScoreCounter ScoreCounter { get; set; }
    }
}
