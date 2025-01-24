The algorithm does work on all 5 sudoku puzzels from the txt file on blackboard.
And for most randomly generated sudoku's it also works. But weve noticed
that for some randomly generated  sudoku's the FC_MCV algorithm terminates before it finds a solution.
What happens is that the algorithm branches out and then backtracks all the way back to the starting node.
It then tries to backtrack even further. That means the algorithm has exausted all possible values from the starting node's domain.
It turns out that not every valid sudoku(a sudoku that doesnt break any rules) is solvable. 
So the algorithm might be trying an unsolvable grid and that takes up unecessary time. We made sure that if sudoku cant be solved the algorithm returns false.
So we can distinguish when to add the elapsed time to the running sum of total time and when not to do that. So even if the actual test does take longer it does not contribute to average running time.
Also its worth mentioning that making a random sudoku does take some time because we have to see which values are still available for every square when generating fixed values.
Sometimes the generator starts making a sudoku that takes a long time to finish generating before its reached the specified amount of fixed values.
So we built in a timer that resets everything so the generating starts from scratch. So this also adds to the actual running time of the benchmarking functions. 
All these things mentioned before does make the test running take much longer than the actual solving time of solvable sudokus.

CalculationTime chronological backtracking
We start at the first non empty square. Then for the first value 1to9 we check wether it breaks any constraints or not.
For each non violating value we go down to the next node and we repeat until we either have to backtrack or reach a leaf, which means the sudoku is solved.
Every square can have 9 values so the branching factor of the search tree is 9. Since chronological backtracking is basically a dumb-depthfirst search
in worst case it takes the amount of time a dfs for tree branching factor 9 takes. 

CalculationTime Forward Checking
Our forward checking only makes node consistent at the beginning and does not apply arc consisency algorithm in between.
Because every constraint C(Vi, Vj) with square Vi and Vj can be replaced with the reflexive constraints C(Vi, Vi) = {Value(Vi) in Domain(Vi)} (same for Vj).
So updating the domain of Vi and only choosing elements from the domain of Vi will never violate any non-reflexive constraints.
CB and forward checking have the same ordering of filling in the squares and also even the same order of which values it tries out.
Because if value x is not possible for Vi in CB <-> x is not in domain for Vi.
The only difference would be that CB has to check for each value every time wether his neghbouring squares contain that value. 
And in FC we dont have to because we already removed the value from its domain preemptively. The difference in time wont be that different from CB.

CalculationTime FC_MCV
FC_MCV is the same as FC only that the algorithm picks the square with the smallest domain first for the next node in the search tree. The order is now
not simply left to right top to bottom. This algorithm reduces the branching factor of the search tree as we go deeper so it has chances to stop earlier than regular FC.


