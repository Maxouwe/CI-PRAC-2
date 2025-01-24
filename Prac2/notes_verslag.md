The algorithm does work on all 5 sudoku puzzels from the txt file on blackboard.
And for most randomly generated sudoku's it also works. But weve noticed
that for some randomly generated  sudoku's the FC_MCV algorithm terminates before it finds a solution.
What happens is that the algorithm branches out and then backtracks all the way back to the starting node.
It then tries to backtrack even further. That means the algorithm has exausted all possible values from the starting node's domain.
Its unclear if the algorithm is not complete, (in the sense of soundness/completeness), if there is something wrong with the sudokugenerator,
or if its a bug. We did not have enough time to figure this out.

