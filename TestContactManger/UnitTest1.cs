namespace TestContactManger
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Math math = new Math();

            int in1 = 5, in2 = 5;
            int expected = 10;
            int actual = math.Add(in1, in2);
            Assert.Equal(expected, actual);
        }
    }
}