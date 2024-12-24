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
# Day 8
#### Part 1
- Really simple day, find distance between nodes of same antena, then find the next, revious node
#### Part 2
- Instead of find first next, previous, find all.
- So, we just need a while loop
___
# Day 9
#### Part 1
- Easy part, follow the instruction to parse the input into the file id with dot string
- Then use double pointer to check from the begining and the end to swap those file id
#### Part 2
- Kinda different to part1, where we have to ensure there is space for the whole file
- So we need to get the file size, then for each size, we go from the begining to find a big enough space 
___
# Day 10
#### Part 1
- Loop through all 0s, for each one, we try al possible
- Then, just count those distinct
#### Part 2
- We just don't need to distinct, count all
___
# Day 11
#### Part 1
- 25 loops for doing the same thing, foreach element, generate follow the rule and add to list
- Since there are many number which had already been generated, we know the result, so we cache to fasten the generate step
#### Part 2
- 75 loops are massive, so I do some counting for the result after 25 loops. Interestingly, there are a lot of duplicated number
- Total of distinct number are very low, so we can just have some memorize counting for those duplicated 
so we don't need to generate a number again and again
- We can even increase to 100 for this way (use bigger number type like ulong for 100+)
___
# Day 12
#### Part 1
- At first, this seems a hard problem since the region can happen inner another
- But, check carefully, it's just a count if any value have either one of their 4 side open - not next to another similar one.
- So, first parse into list of block, and store the state of each element - which side is already exist a similar one
- Then just count 
#### Part 2
- After part1, part2 means that for all elements in one block has one side not covered, count number of non consecutive position they are on.
___
# Day 13
#### Part 1
- Just a simple system of linear equations
- using int and float to solve
#### Part 2
- Bigger number => long and double
___
# Day 14
#### Part 1
- Just a simple calculation of new position after x second, then module to the grid size to get position in grid
#### Part 2
- Wild guess, I think there should be a strange line which bigger than 50% of the grid tall
- So, have a method to get all the strange continuous line, compare the length to the minimum value
- Surprisingly, the line is about 30% of the grid, and it's the border of the tree, not the middle tree line at all 
___
# Day 15
#### Part 1
- Follow the instruction to push the good around
- If there is a line of goods, we just need to swap the first goods with the empty pos,
then move the robot
#### Part 2
- We cannot use approach like part1 since 1 object contains 2 part left and right side
- After hard time, I realize we just need to have the position of box after all loop, and moving 
is just update the box poisition.
- So, we have a box contains left and right, when move, we update them. The hard part is just
finding the chain of boxes to be moved when the box is moved, then move all of them at once.
___