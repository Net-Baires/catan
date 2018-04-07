using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Catan.Tests
{

    public class BoardBaseTest
    {
        protected readonly Token _token;
        protected readonly Field _center;
        private readonly Field[] _fields;
        protected readonly WaterThreeByOne _waterThreeByOne;
        protected WaterEmpty _waterEmpty;

        public BoardBaseTest()
        {
            _token = new Token(0);
            _center = new Field(0);
            _fields = Enumerable.Range(0, 18).Select(x => new Field(x + 1)).ToArray();

            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];

            _waterThreeByOne = new WaterThreeByOne();
            _waterEmpty = new WaterEmpty();
            var road = new Road();

            _center.Build(field1, field2, field3, field4, field5, field6, () => _token);
            field1.Build(field7, field8, field2, _center, field6, field18, () => _token);
            field2.Build(field8, this.GetField(9), this.GetField(10), field3, _center, field1, () => _token);
            field3.Build(field2, this.GetField(10), this.GetField(11), this.GetField(12), field4, _center, () => _token);

            field6.Build(field18, field1, _center, field5, this.GetField(16), this.GetField(17), () => _token);
            field7.Build(_waterThreeByOne, _waterEmpty, field8, field1, field18, _waterEmpty, () => _token);

        }
        protected Field GetField(int number)
        {
            return _fields[number - 1];
        }

    }

    public class BoardShould : BoardBaseTest
    {
        private readonly Tablero _sut;

        public BoardShould()
        {
            _sut = new Tablero();

        }

        [Fact]
        public void NotBuildRoad_GivenNoCity()
        {
            var road = new Road();
            var field1 = this.GetField(1);
            Assert.Throws<CantCreateRoadException>(() => _sut.Build(field1.Edge1, road));

        }
        [Fact]
        public void BuildRoad_GivenCloseCity()
        {
            var field1 = this.GetField(1);
            var city = new City();
            var road = new Road();
            _sut.Build(field1.Edge1.Up, city);

            field1.Edge1.Up.Building.ShouldBe(city);

            _sut.Build(field1.Edge1, road);

            field1.Edge1.Road.ShouldBe(road);
        }

        [Fact]
        public void BuildCity_GivenEmptyVertex()
        {
            var city = new City();
            var field1 = this.GetField(1);
            _sut.Build(field1.Edge1.Up, city);
            field1.Edge1.Up.Building.ShouldBe(city);
        }

        [Fact]
        public void BuildCity_GivenBusyVertex()
        {
            var city = new City();
            var field1 = this.GetField(1);
            _sut.Build(field1.Edge1.Up, city);
            Assert.Throws<CantCreateBuildingException>(() => _sut.Build(field1.Edge1.Up, city));
        }


        public void BuildCity_GivenAdjacentFieldEmptyVertex()
        {
            var city = new City();
            var field1 = this.GetField(1);
            _sut.Build(field1.Edge1.Up, city);

            field1.Edge1.Up.Field2.Edge3.Up.ShouldBe(field1.Edge1.Up);

            //field1.Edge1.Up.Field2.Edge3.Up.Building.ShouldBe(city);
        }

    }

    public class CantCreateBuildingException : Exception { }

    public class CenterFieldShould : BoardBaseTest
    {
        public CenterFieldShould()
        {
        }

        private void IsRightClose(Field source, Func<Field, Edge> edge, Field next)
        {
            edge(source).Up.Field1.ShouldBe(next, "some cool message");
        }

        private void EdgeVertexAllFields(Vertex vertex, Field field1, Field field2, Field field3)
        {
            vertex.Field1.ShouldBe(field1);
            vertex.Field2.ShouldBe(field2);
            vertex.Field3.ShouldBe(field3);
        }

        [Fact]
        public void BuildEdge1Field_GivenCenter()
        {
            _center.Edge1.ShouldNotBeNull();

            _center.Edge1.ShouldBe(GetField(1).Edge4);
            GetField(1).Edge1.ShouldBe(GetField(7).Edge4);


            //_center.Edge1.Up.ShouldBe(GetField(1).Edge3.Down);

            ////IsRightClose(_center, x => x.Edge1, _center);
            ////IsRightClose(_center, x => x.Edge1, _center);

            //EdgeVertexAllFields(_center.Edge1.Up, _center, this.GetField(1), this.GetField(2));
            //EdgeVertexAllFields(_center.Edge1.Down, _center, this.GetField(6), this.GetField(1));

            ////vertice the edge1 up tiene que ser igual a vertice edge2 down;
            //_center.Edge1.Up.ShouldBe(_center.Edge2.Down);


            ////field1 edge4 sea igual a desierto edg1
            //this.GetField(1).Edge4.ShouldBe(_center.Edge1);

        }

        [Fact]
        public void BuildEdge2Field_GivenCenter()
        {
            _center.Edge2.ShouldNotBeNull();

            //_center.Edge2.Down.ShouldBe(_center.Edge1.Up);

            //IsRightClose(_center, x => x.Edge2, _center);
            //EdgeVertexAllFields(_center.Edge2.Up, _center, this.GetField(2), this.GetField(3));

            this.GetField(2).Edge5.ShouldBe(_center.Edge2);

        }

        [Fact]
        public void BuildEdge3Field_GivenCenter()
        {
            _center.Edge3.ShouldNotBeNull();
            _center.Edge3.Up.ShouldBe(_center.Edge2.Up);

            //IsRightClose(_center, x => x.Edge3, _center);
            //EdgeVertexAllFields(_center.Edge3.Up, _center, this.GetField(3), this.GetField(4));

            this.GetField(3).Edge6.ShouldBe(_center.Edge3);

        }

        [Fact]
        public void BuildEdg1Field_GivenField1AndSut()
        {
            _center.Edge1.Up.Field2.Edge1.Up.Field2.ShouldBe(this.GetField(7));
            _center.Edge1.Down.Field3.Edge1.Down.Field2.ShouldBe(this.GetField(18));
        }

        public void BuildEdg1Field_GivenField7AndSut()
        {

            _center.Edge1.Up.Field2.Edge1.Up.Field2.Edge1.Up.Field2.ShouldBe(_waterThreeByOne);
            _center.Edge1.Up.Field2.Edge1.Up.Field2.Edge1.Up.Field3.ShouldBe(_waterEmpty);
        }

        //Chhecking field1.edge1.up == field7.edge3.up
        //[Fact]
        public void BuildEdg1Field_GivenField7AndSut2()
        {

            this.GetField(1).Edge1.Up.ShouldBe(this.GetField(7).Edge3.Up);

        }



        [Fact]
        public void BuildAToken()
        {
            _center.Token.ShouldBe(_token);

        }




    }

    public class VertexShould : BoardBaseTest
    {
        public VertexShould()
        {

        }

        [Fact]
        public void algo()
        {
            _center.Edge1.Up.ShouldBe(this.GetField(1).Edge4.Up);

        }

        [Fact]
        public void algo1()
        {
            _center.Edge1.Up.ShouldBe(this.GetField(2).Edge5.Down);

        }

        [Fact]
        public void algo2()
        {
            _center.Edge1.Down.ShouldBe(this.GetField(1).Edge4.Down);
        }

        [Fact]
        public void algo3()
        {
            _center.Edge1.Down.ShouldBe(this.GetField(6).Edge3.Up);
        }
    }
}
