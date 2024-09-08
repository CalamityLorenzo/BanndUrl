using GoodBadUrl;

namespace goodBadUrl.tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestUrlProcessor()
        {
            var banned = File.ReadAllLines("banned.txt");
            var user = File.ReadAllLines("userrequests.txt");
            var results = Solution.BadUrlProcessor(user, banned);

            Assert.True(results[0] == 6);
            Assert.True(results[1] == 9);
        }
    }
}