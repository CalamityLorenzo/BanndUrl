# Domain Test

Hi, please find enclosed my entry for the code task.
This ended up being an interesting challenge, and I did actually learn something new in the writing this and that is always a good thing.

I've being working with on a compilers and interpreters recently, and I think that probably explains why I chose a recursive descent solution,
The structure is driven that the first entry is found by dictionary matching (So the hash function will be incredibly quick), but I used Linqs .FirstOrDefault for the nested structure. It would also been possible to use *Dictionary<string, SegmentNode>* matching all the way done had I chosen to (Which makes the match O(1) as dictionary matching is constant time).

The console app expects two files to be provided eg
`
ConsoleApp.exe userdata.txt blocked.txt
`

And two files are provided as examples.

The Tests project is another way to test the output, and demonstrates the output of the two files I provided.


Thanks very much for the opportunity.