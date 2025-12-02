using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public struct Vector2Int : IComparable<Vector2Int>
    {
        public static readonly Vector2Int Up = new Vector2Int(0, 1);
        public static readonly Vector2Int Down = new Vector2Int(0, -1);
        public static readonly Vector2Int Left = new Vector2Int(-1, 0);
        public static readonly Vector2Int Right = new Vector2Int(1, 0);
        public static readonly Vector2Int Zero = new Vector2Int(0, 0);
        public static readonly Vector2Int One = new Vector2Int(1, 1);

        public int x;
        public int y;
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
        {
            return new Vector2Int(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
        {
            return new Vector2Int(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector2Int operator *(int i, Vector2Int vec)
        {
            return new Vector2Int(i * vec.x, i * vec.y);
        }
        public static Vector2Int operator *(Vector2Int vec, int i)
        {
            return new Vector2Int(i * vec.x, i * vec.y);
        }
        public static Boolean operator ==(Vector2Int v1, Vector2Int v2)
        {
            if (v1.x == v2.x && v1.y == v2.y) { return true; }
            return false;
        }
        public static Boolean operator !=(Vector2Int v1, Vector2Int v2)
        {
            return !(v1 == v2);
        }

        public int CompareTo(Vector2Int other)
        {
            if (this.x < other.x) { return -1; }
            if (this.x > other.x) { return 1; }
            if (this.y < other.y) { return -1; }
            if (this.y > other.y) { return 1; }
            return 0;
        }
    }
}
