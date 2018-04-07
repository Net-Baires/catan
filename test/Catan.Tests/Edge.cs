using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.Tests
{
    public class Edge
    {
        public Vertex Up { get; set; }
        public Vertex Down { get; set; }
        public Road Road { get; private set; }


        private static ConcurrentDictionary<string, Edge> _edges = new ConcurrentDictionary<string, Edge>();

        public static Edge Create(Vertex up, Vertex down)
        {
            var key = $"{up}|{down}";
            var edge = new Edge(up, down);
            return _edges.GetOrAdd(key, edge);


        }

        private Edge(Vertex up, Vertex down)
        {
            this.Up = up;
            this.Down = down;
        }

        public void Build(Road road)
        {
            if (Up.Building == null
                  &&
                  Down.Building == null)
                throw new CantCreateRoadException();

            Road = road;

        }
    }
}
