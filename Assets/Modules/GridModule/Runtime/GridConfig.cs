using UnityEngine;

namespace Modules.GridModule.Runtime
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Modules/GridModule/GridConfig", order = 0)]
    public class GridConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector2Int GridSize { get; private set; }

        [field: SerializeField]
        public Vector2 GridPosition { get; private set; }

        [field: SerializeField]
        public float CellLength { get; private set; }
    }
}