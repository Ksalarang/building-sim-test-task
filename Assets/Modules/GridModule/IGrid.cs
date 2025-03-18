using System.Collections.Generic;
using UnityEngine;

namespace Modules.GridModule
{
    //todo: delete
    public interface IGrid
    {
        Cell GetClosestCell(Vector2 position);

        List<Cell> GetCellList();

        Cell GetCell(int x, int y);
    }
}