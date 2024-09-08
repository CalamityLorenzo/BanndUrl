
namespace GoodBadUrl
{
    public class SegmentNode
    {
        private List<SegmentNode> _matchingSegments = new();
        public string Key { get; }
        public SegmentNode(string newKey)
        {
            this.Key = newKey;
        }

        public (bool, SegmentNode?) FindSegment(string segment)
        {
            var result = _matchingSegments.FirstOrDefault(a => a.Key == segment);
            if (result == null)
                return (false, null);
            else
            {
                return (true, result);
            }
        }

        internal SegmentNode AddSegment(string segmentValue)
        {
            var segment = new SegmentNode(segmentValue);
            _matchingSegments.Add(segment);
            return segment;
        }

        public override string ToString()
        {
            return $"{Key}: {_matchingSegments.Count}";
        }
        /// <summary>
        /// recursive descent matcher
        /// </summary>
        /// <param name="urlSegments"></param>
        /// <returns></returns>
        internal bool IsMatch(List<string> urlSegments)
        {
            // This is becuase a user url can more segments
            // eg subsite.Websites.com == Websites.com
            if(_matchingSegments.Count == 0 ) return true;
            var firstSegment = urlSegments[0];
            var match = _matchingSegments.FirstOrDefault(a => a.Key == firstSegment);
            if (match !=null && urlSegments.Count > 1)
            {
                // pass in the next segements
                return match.IsMatch(urlSegments[1..]);
            }
            else
            {
                return match == null ? false : true;
            }
        }
    }
}
