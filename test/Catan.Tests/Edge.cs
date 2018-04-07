using System;

namespace Catan.Tests
{
    public class Edge
    {
        public Vertex Up { get; set; }
        public Vertex Down { get; set; }
        public Road Road { get; private set; }
        public Edge(Field current, Field right, Field up, Field down)
        {
            this.Up = new Vertex { Field1 = current, Field2 = right, Field3 = up };
            this.Down = new Vertex { Field1 = current, Field2 = down, Field3 = right };
        }

        public Edge(Vertex up, Vertex down)
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
