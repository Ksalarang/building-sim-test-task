using System.Collections.Generic;
using UnityEngine;

namespace Modules.GridModule
{
    public interface IGrid
    {
        Cell GetClosestCell(Vector2 position);

        List<Cell> GetCellList();
    }
}