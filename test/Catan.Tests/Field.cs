using System;

namespace Catan.Tests
{
    public class Field
    {
        public Edge Edge1 { get; private set; }
        public Edge Edge2 { get; private set; }
        public Edge Edge3 { get; private set; }
        public Edge Edge4 { get; private set; }
        public Edge Edge5 { get; private set; }
        public Edge Edge6 { get; private set; }
        public Token Token { get; private set; }
        public Field()
        {

        }

        public void Build(
            Field field1,
            Field field2,
            Field field3,
            Field field4,
            Field field5,
            Field field6,
            Func<Token> generateToken)
        {
            this.Edge1 = new Edge(this, field1, field2, field6);
            field1.Edge4 = this.Edge1;

            this.Edge2 = new Edge(new Vertex
            {
                Field1 = this,
                Field2 = field2,
                Field3 = field3
            }, this.Edge1.Up);

            field2.Edge5 = this.Edge2;

            this.Edge3 = new Edge(new Vertex
            {
                Field1 = this,
                Field2 = field3,
                Field3 = field4
            }, this.Edge2.Up);

            field3.Edge6 = this.Edge3;

            Token = generateToken();
        }
    }
    public class Token
    {
        private readonly int number;

        public Token(int number)
        {
            this.number = number;
        }
    }
}
