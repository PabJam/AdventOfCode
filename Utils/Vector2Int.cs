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
        public static readonly Vector2Int DownRight = new Vector2Int(1, -1);
        public static readonly Vector2Int DownLeft = new Vector2Int(-1, -1);
        public static readonly Vector2Int UpLeft = new Vector2Int(-1, 1);
        public static readonly Vector2Int UpRight = new Vector2Int(1, 1);
        public static readonly Vector2Int[] Directions = { Right, Down, Left, Up, DownRight, DownLeft, UpLeft, UpRight };
        public long x;
        public long y;
        public Vector2Int(long x, long y)
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
        public static Vector2Int operator *(long i, Vector2Int vec)
        {
            return new Vector2Int(i * vec.x, i * vec.y);
        }
        public static Vector2Int operator *(Vector2Int vec, long i)
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

        // Override Equals
        public override bool Equals(object? obj)
        {
            if (obj is Vector2Int other)
            {
                return this == other;
            }
            return false;
        }

        // Override GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public double Length()
        {
            return MathF.Sqrt(x * x + y * y);
        }

        public long LengthSq()
        {
            return x * x + y * y;
        }

        public static double Length(Vector2Int vec)
        {
            return MathF.Sqrt(vec.x * vec.x + vec.y * vec.y);
        }

        public static long LengthSq(Vector2Int vec)
        {
            return vec.x * vec.x + vec.y * vec.y;
        }

        public static double Distance(Vector2Int v1, Vector2Int v2)
        {
            return Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
        }

        public static long DistanceSq(Vector2Int v1, Vector2Int v2)
        {
            return (v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y);
        }

        public static long DistanceManhatten(Vector2Int v1, Vector2Int v2)
        {
            return Math.Abs(v1.x - v2.x) + Math.Abs(v1.y - v2.y);
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Vector2Int || obj == null)
            {
                return -1;
            }
            Vector2Int other = (Vector2Int)obj;
            long l1, l2;
            l1 = this.LengthSq();
            l2 = other.LengthSq();
            if (l1 < l2)
            {
                return -1;
            }
            else if (l2 > l1)
            {
                return 1;
            }
            return 0;
        }

        public int CompareTo(Vector2Int other)
        {
            return CompareTo((object?)other);
        }
    }
}
