using System;

namespace Catan.Tests
{
    public class Tablero
    {
        public void Build(Field center, Field[] fields, Func<Token> generateToken)
        {
            var field1 = fields[0];
            var field2 = fields[1];
            var field3 = fields[2];
            var field4 = fields[3];
            var field5 = fields[4];
            var field6 = fields[5];

            center.Build(field1, field2, field3, field4, field5, field6, generateToken);
        }

        public void Build(Vertex vertex, IBuilding building)
        {
            vertex.Build(building);
        }

        public void Build(Edge edge, Road road)
        {
            edge.Build(road);
        }
    }
    public class CantCreateRoadException : Exception {
    }
}
