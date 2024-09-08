using GoodBadUrl;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var userRequests = File.ReadAllLines(args[0]);
            var blockedUrls = File.ReadAllLines(args[1]);
            var results = Solution.BadUrlProcessor(userRequests, blockedUrls);

            Console.WriteLine("Safe Urls");
            Console.WriteLine($"[{String.Join(", ", results)}]");



            var dict = new Dictionary<string, object>();
            var t = dict["will"];
        }
    }
}
