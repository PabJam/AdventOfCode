using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public struct Vector3Int : IComparable
    {
        public static readonly Vector3Int Up = new Vector3Int(0, 1, 0);
        public static readonly Vector3Int Down = new Vector3Int(0, -1, 0);
        public static readonly Vector3Int Left = new Vector3Int(-1, 0, 0);
        public static readonly Vector3Int Right = new Vector3Int(1, 0, 0);
        public static readonly Vector3Int Front = new Vector3Int(0, 0, 1);
        public static readonly Vector3Int Back = new Vector3Int(0, 0, -1);
        public static readonly Vector3Int Zero = new Vector3Int(0, 0, 0);
        public static readonly Vector3Int One = new Vector3Int(1, 1, 1);
        public static readonly Vector3Int[] Directions = { Right, Down, Left, Up};
        public long x;
        public long y;
        public long z;
        public Vector3Int(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Int operator +(Vector3Int v1, Vector3Int v2)
        {
            return new Vector3Int(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        public static Vector3Int operator -(Vector3Int v1, Vector3Int v2)
        {
            return new Vector3Int(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
        public static Vector3Int operator *(int i, Vector3Int vec)
        {
            return new Vector3Int(i * vec.x, i * vec.y, i * vec.z);
        }
        public static Vector3Int operator *(Vector3Int vec, int i)
        {
            return new Vector3Int(i * vec.x, i * vec.y, i * vec.z);
        }
        public static Boolean operator ==(Vector3Int v1, Vector3Int v2)
        {
            if (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z) { return true; }
            return false;
        }
        public static Boolean operator !=(Vector3Int v1, Vector3Int v2)
        {
            return !(v1 == v2);
        }

        // Override Equals
        public override bool Equals(object? obj)
        {
            if (obj is Vector3Int other)
            {
                return this == other;
            }
            return false;
        }

        // Override GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public double Length()
        {
            return MathF.Sqrt(x * x + y * y + z * z);
        }

        public static double Length(Vector3Int vec)
        {
            return MathF.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        }

        public static double Distance(Vector3Int v1, Vector3Int v2)
        {
            return Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z));
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Vector3Int || obj == null)
            {
                return -1;
            }
            Vector3Int other = (Vector3Int)obj;
            double l1, l2;
            l1 = this.Length();
            l2 = other.Length();
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
    }
}
