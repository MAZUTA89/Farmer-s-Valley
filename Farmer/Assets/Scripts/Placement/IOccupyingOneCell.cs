using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Placement
{
    public interface IOccupyingOneCell
    {
        Vector3Int GetOccupyingCell();
    }
}
