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