using UnityEngine;

namespace Modules.Grids.Runtime
{
    public readonly struct Cell
    {
        public readonly Vector2Int GridPosition;
        public readonly Vector2 WorldPosition;

        public Cell(Vector2Int gridPosition, Vector2 worldPosition)
        {
            GridPosition = gridPosition;
            WorldPosition = worldPosition;
        }

        public override string ToString()
        {
            return $"Cell {GridPosition} at {WorldPosition}";
        }
    }
}