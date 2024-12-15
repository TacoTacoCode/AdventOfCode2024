___
# Day 1
#### Part 1
- Since two lists are the same size, sort then sum distance
#### Part 2
- While traversal in list 2 in part1, also build the similarity dictionary
- Then easy sum the multiply
___
# Day 2
#### Part 1
- For each line, get the index that caused data to be unsafe
- For increase/descrease trend, we can just simple check the 2 most nearest node to ensure we are still on the trend
#### Part 2
- Since there maybe one ignore, after the failed, we can try to remove and run the check again
- First idea is just remove the fasly index (i) and its previous (i-1)
- But there are cases that need to remove the begining (j < i - 1). So, the best way is just try removing from the falsy index backward to 0 for these cases
___
# Day 3
*Yes we can easy solve it by REGEX* `mul\(\d+,\d+\)|don\'t\(\)|do\(\)`
#### Part 1
- String search the special delimeters and try convert to number to see if it's a correct pattern, which will be a match
- Sum on the loop or store in to a list and sum later 
#### Part 2
- String search again to find all ranges of don't() => do()
- If any of these ranges contain the match, just not sum it.
___
# Day 4
#### Part 1
- Simple way is just create 2d array, find X then find all surrounding 8 directions to find the XMAS pattern
- Another way, in this project, is to consider all vertical, diagon as a normal line, then use Regex or string search to find the XMAS pattern. 
- It's just hard when we build these diagonal list when findind a generic way to iterate. Key is that 1 end of each diagonal is from the square edge, then all other points are +-1 cal
#### Part 2
- Use simple way, find the A then all surrouding.
- Just need to check to ensure the pattern which we cannot have two M or two S opposite through A
___
# Day 5
#### Part 1
- From secion 1, we can see which node should be after which node, means that some node should be child of other node
- So, a simple approach is that we can have some thing to mark all nodes which are children of a node
- Then the problem is just continuously checking if the next number is a child of current or not.
#### Part 2
- Adding a fix means that we should swap wrong node so the order can be preserved.
- Maybe it's just one swap or multiple swaps needed, so recursive swap and check
___
# Day 6
#### Part 1
- Easy one, just do some index addition to move arround the room and counting
- Use Complex to hold cordinate and add offset, then a HashSet for duplicating and counting
#### Part 2
- Took a while, but we know that obstacle can only be put on path in Part 1
- So, we can loop through the path we store, put an obstacle and see if the guard is moving to the same position with same direction
#### Also add a DisplayDay6 project to see how the guard run
___
# Day 6
#### Part 1
- Easy one, just do some index addition to move arround the room and counting
- Use Complex to hold cordinate and add offset, then a HashSet for duplicating and counting
#### Part 2
- Took a while, but we know that obstacle can only be put on path in Part 1
- So, we can loop through the path we store, put an obstacle and see if the guard is moving to the same position with same direction
#### Also add a DisplayDay6 project to see how the guard run
___
# Day 7
#### Part 1
- Trying all possiple solution by using recursion
#### Part 2
- Similar to part1, we just need to figure out how to concat 2 numbers without conversing since it creats too much string
- So, when concat 2 numbers, we find number of zero need to add to num1, then do some multiply and addition to concat.
___
