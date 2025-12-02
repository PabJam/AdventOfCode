using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class NodeAs : IComparable<NodeAs>
    {
        public NodeAs(Vector2Int pos, int Gcost, int straightCount, NodeAs parent)
        {
            this.pos = pos;
            this.Gcost = Gcost;
            this.parent = parent;
            this.Hcost = parent.Fcost + Gcost;
            this.Fcost = Gcost + Hcost;
            this.straightCount = straightCount;
        }
        public NodeAs(Vector2Int pos, int Gcost, int straightCount)
        {
            this.pos = pos;
            this.Gcost = Gcost;
            this.Hcost = Gcost;
            this.Fcost = Gcost + Hcost;
            this.straightCount = straightCount;
            parent = this;
        }
        public NodeAs parent { get; private set; }
        public Vector2Int pos { get; private set; }
        public int Gcost { get; private set; }
        public int Hcost { get; private set; }
        public int Fcost { get; private set; }
        public int straightCount { get; private set; }

        public int CompareTo(NodeAs? other)
        {
            if (other == null) { throw new Exception("Can't compare to null Node"); }
            if (this.Fcost < other.Fcost) { return -1; }
            if (this.Fcost > other.Fcost) { return 1; }
            if (this.Hcost < other.Hcost) { return -1; }
            if (this.Hcost > other.Hcost) { return 1; }
            if (this.Gcost < other.Gcost) { return -1; }
            if (this.Gcost > other.Gcost) { return 1; }
            return this.pos.CompareTo(other.pos);
        }
    }
}
