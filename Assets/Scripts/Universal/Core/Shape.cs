using System.Collections.Generic;
using UnityEngine;

namespace Universal.Core
{
    [System.Serializable]
    public class Shape
    {
        #region fields & properties
        public int Width => width;
        [SerializeField] private int width = 1;
        public int Height => height;
        [SerializeField] private int height = 1;
        public IReadOnlyCollection<bool> ShapeGrid => shapeGrid;
        [SerializeField] private bool[] shapeGrid = { true };
        #endregion fields & properties

        #region methods
        public bool GetValueAt(int localX, int localY)
        {
            if (localX < 0 || localX >= width || localY < 0 || localY >= height)
            {
                return false;
            }

            int index = localY * width + localX;

            if (index >= shapeGrid.Length) return false;

            return shapeGrid[index];
        }

        public void Resize(int newWidth, int newHeight)
        {
            width = Mathf.Max(1, newWidth);
            height = Mathf.Max(1, newHeight);
            shapeGrid = new bool[width * height];
        }
        
        public Shape()
        {
            width = 1;
            height = 1;
            shapeGrid = new bool[] { true };
        }
        #endregion methods
    }
}