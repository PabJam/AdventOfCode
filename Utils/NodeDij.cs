using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class NodeDij : IComparable<NodeDij>
    {
        public NodeDij(Vector2Int pos, Vector2Int dir, int straightCount, int cost, NodeDij parent)
        {
            this.pos = pos;
            this.dir = dir;
            this.straightCount = straightCount;
            this.cost = cost;
            this.parent = parent;
        }
        public NodeDij(Vector2Int pos, Vector2Int dir, int straightCount, int cost)
        {
            this.pos = pos;
            this.dir = dir;
            this.straightCount = straightCount;
            this.cost = cost;
            parent = this;
        }
        public Vector2Int pos { get; private set; }
        public Vector2Int dir { get; private set; }
        public int straightCount { get; private set; }
        public int cost { get; set; }
        public NodeDij parent { get; set; }

        public int CompareTo(NodeDij? other)
        {
            if (other == null) { return 1; }
            if (this.cost < other.cost) { return -1; }
            if (this.cost > other.cost) { return 1; }
            return 0;
            throw new NotImplementedException();
        }
    }
}
