using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Placement
{
    public interface IOccupyingSeveralCells : IOccupyingСells
    {
        List<Vector3Int> GetOccupyingCells();
    }
}
