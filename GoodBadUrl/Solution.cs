


namespace GoodBadUrl
{
    public class Solution
    {
        public static int[] BadUrlProcessor(string[] UsersRequets, string[] Blocked)
        {
            var blockedUrlSegments = BuildBlockedLinkedList(Blocked);
            var reversedUserUrlSegments = ReverseUrlSegments(UsersRequets);
            List<int> resultList = new();
            for(var x = 0; x < reversedUserUrlSegments.Count; ++x)
            {
                if (!MatchUrl(reversedUserUrlSegments[x], blockedUrlSegments))
                    resultList.Add(x);
            }
            return resultList.ToArray();
        }
        /// <summary>
        /// A match is if an entire banned url is subsumed in the userRequest url 
        /// a.be.com  == be.com
        /// I've used Recursive Descent for each segement.
        /// </summary>
        /// <param name="urlSegments"></param>
        /// <param name="badUrlNodes"></param>
        /// <returns></returns>
        private static bool MatchUrl(List<string> urlSegments, Dictionary<string, SegmentNode> badUrlNodes)
        {
            // our urlSegment[0] == TLD which is the badUrlNode top-level.
            // If it's not there, then this url is free to go.
            if (badUrlNodes.ContainsKey(urlSegments[0]))
            {
                var bannedUrl  = badUrlNodes[urlSegments[0]];
                return bannedUrl.IsMatch(urlSegments[1..]);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// We can't iterate over blocked for every request.
        /// So we build a tree we can walk down instead.
        /// Take the url, split, and then reverse it : www.bbc.co.uk == [uk, co, bbc, www]
        /// And have each Node point to the next.
        /// </summary>
        /// <param name="blocked"></param>
        /// <returns></returns>
        private static Dictionary<string, SegmentNode> BuildBlockedLinkedList(string[] blocked)
        {
            // Split the url into components, and reverse it.
            // We then turn this into our queryable nodes.
            var ReversedBlocked = ReverseUrlSegments(blocked);

            List<SegmentNode> TldSegments = new();

            // Down through the list
            foreach (var blockedUrl in ReversedBlocked)
            {
                SegmentNode currentSegment = null;
                // Across the (reveresed)url segments.
                for (var x = 0; x < blockedUrl.Count; x++)
                {
                    // Find a matching TLD Segment
                    if (x == 0)
                    {
                        var tld = TldSegments.FirstOrDefault(a => a.Key == blockedUrl[x]);
                        if (tld == null)
                        {
                            tld = new SegmentNode(blockedUrl[x]);
                            TldSegments.Add(tld);
                        }
                        currentSegment = tld;
                    }
                    else // build the other segments.
                    {
                        var (hasResult, segment) = currentSegment!.FindSegment(blockedUrl[x]);
                        if (!hasResult)
                        {
                            segment = currentSegment.AddSegment(blockedUrl[x]);
                        }
                        currentSegment = segment!;
                    }
                }
            }

            return TldSegments.ToDictionary(a=>a.Key, b=>b);

        }
        
        /// <summary>
        /// Helper as it's actually used twice.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static List<List<string>> ReverseUrlSegments(string[] array)
        {
            return array.Select(url => url.Split('.').Reverse().ToList()).ToList();
        }
    }

}
