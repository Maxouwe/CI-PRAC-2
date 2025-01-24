using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class ChronologicalBacktracking
    {
        StateOperatorCB socb;
        SudokuGrid result = new SudokuGrid(); //save the result here
        bool solved;
        public ChronologicalBacktracking(SudokuGrid sg)
        {
            socb = new StateOperatorCB(sg);
        }

        //initiate the recursion for the Chronological Backtracking Algorithm
        public bool runAlgorithm()
        {
            if (socb.sg.grid[0][0].fixed_) socb.goToFirstChild();
            findNextSib();
            return solved;
        }

        //recursive step
        public void recurseAlgorithm()
        {
            //check if the current grid is a solution and save it in the result instance
            if (socb.checkCompleted())
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        result.grid[i][j] = new Vakje(false, socb.sg.grid[i][j].val, (i, j));
                        solved = true;
                    }
                return;
            }
            socb.goToFirstChild(); 
            findNextSib();
            socb.goToParent();
        }

        //for each sibling, check wether the algorithm should terminate or check
        //if the sibling is possible and go to the next recursive step
        private void findNextSib()
        {
            for (int i = 0; i < 9; i++)
            {
                if (result.grid[0][0] != null) return; //if the vakje is still null, no solution has been found yet
                if (socb.checkNextSibling())
                    recurseAlgorithm();
            }
        }

        //return the result in string format
        public string printResult()
        {
            return result.ToString();
        }
    }
}
