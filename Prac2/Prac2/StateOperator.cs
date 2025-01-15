using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    //in these classes i mention Vj's alot which refers to all the Vakjes whos domain
    //are affected by a value change of currentVakje
    //which are all vakjes in the same row,column and subgrid as currentVakje

    //StateOperator can be used for both Chronological backtracking and Forward Checking
    //For chronological backtracking we dont have to change domains
    //FC-MCV needs different Operators because the fill-in order is not from left to right and top to bottom


    //the idea of stateoperator(for chron-back and for-check) is that we only remember whats necessary for the current state
    //for that we only need to remember what the currentVakje is
    //if we go from parent to child we go one cell to the right, currentVakje becomes that child
    //we start trying out values for this child (which is now currentVakje)
    //if that child fails we try out the next value for currentVakje (which is the next sibling)
    //if all siblings fail only then we go back to the parent

    //we try out all cells from left to right and top to bottom and we try out the values 1 to 9 in that order
    //so we know based on the coordinates of currentVakje and the value of currentVakje:
    //-which vakje corresponds to the parentstate (the value of that vakje is stored in the sudokugrid)
    //-which value we should try out for the nextSibling
    //-which value we need to look at if we want to undo the previous operation

    internal class StateOperatorCB
    {
        Vakje currentVakje;

        public StateOperatorCB() { }


        //checks wether the next value for currentVakje does not violate any constraints
        //this is the sibling of the current state
        //i thought checking the next sibling first before changing the sudokugrid
        //because im not sure if its safe to change things 
        //and then finding out halfway through that it doesnt work
        public void checkNextSibling()
        {

        }

        //go to the next sibling of the current state
        //make sure to first call undoOperator()
        //-fill in the next value for Vi
        public void goToNextSibling()
        {

        }

        //remove the value of currentVakje
        public void undoOperator()
        {

        }

        //make sure to first call undoOperator()
        //go one vakje terug (for chron backtrack and Forward Checking) that is the cell to the left
        //or if there is no to the left, go to the cell that is most right and one row up
        public void goToParent()
        {

        }

        //just go one vakje to the right (i.e. reassign currentVakje)
        //ofcourse make sure if its not fixed
        //after this currentVakje.val = 0
        //call checkNextSibling and goToNextSibling this will start at trying out value = 1
        public void goToFirstChild()
        {

        }
    }

    internal class StateOperatorFC
    {
        Vakje currentVakje;

        public StateOperatorFC() { }


        //checks wether the next value for currentVakje does not violate any constraints
        //this is the sibling of the current state
        //i thought checking the next sibling first before changing the sudokugrid
        //because im not sure if its safe to change the domains
        //and then finding out halfway through that it doesnt work
        public void checkNextSibling()
        {

        }

        //go to the next sibling of the current state
        //make sure to first call undoOperator()
        //-fill in the next value for Vi
        //-update the domains of all Vj's
        public void goToNextSibling()
        {

        }

        //remove the value of currentVakje
        //add the value of currentVakje back to the domains of all Vj's
        public void undoOperator()
        {

        }

        //make sure to first call undoOperator()
        //go one vakje terug (for chron backtrack and Forward Checking) that is the cell to the left
        //or if there is no to the left, go to the cell that is most right and one row up
        public void goToParent()
        {

        }

        //just go one vakje to the right (i.e. reassign currentVakje)
        //ofcourse make sure if its not fixed
        //after this currentVakje.val = 0
        //call checkNextSibling and goToNextSibling this will start at trying out value = 1
        public void goToFirstChild()
        {

        }
    }

    //the FC-MCV algorithm needs different operators than forwardchecking and chronological backtracking
    class StateOperatorMCV
    {
        Vakje currentVakje;

        public StateOperatorMCV() { }

        
        public void checkNextSibling(SudokuGrid grid)
        {

        }

        public void goToNextSibling(SudokuGrid grid)
        {

        }

        public void undoOperator(SudokuGrid grid)
        {

        }

        public void goToParent(SudokuGrid grid)
        {

        }

        public bool goToFirstChild(SudokuGrid grid)
        {
            throw new NotImplementedException();
        }

        //the sorting may be optimized more
        //instead of counting every domain size every step
        //we can remember which domains have been changed last and what the size was of them
        //we can just remember that we just need to increase or decrease their size by 1
        //(because every step at most only 1 value is added or removed from every domain
        //finds vakje with smallest domain(most constrained vakje)
        public Vakje? getSmallestDomain(SudokuGrid grid)
        {
            //start with any empty vakje in grid
            Vakje smallestDomain = findEmptyCell(grid);
            int smallestDomainSize = domainSize(smallestDomain);

            if(smallestDomain != null)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int tempDomainSize = domainSize(grid.grid[i][j]);
                        if(tempDomainSize < smallestDomainSize)
                        {
                            smallestDomain = grid.grid[i][j];
                            smallestDomainSize = tempDomainSize;
                        }
                    }
                }
                return smallestDomain;
            }
            else
            {
                return null;
            }
        }

        //returns the first empty cell from top to bottom left to right
        public Vakje? findEmptyCell(SudokuGrid grid)
        {
            for(int i = 0; i < 0; i++)
            {
                for(int j=0; j < 9; j++)
                {
                    if (grid.grid[i][j].val == 0)
                    {
                        return grid.grid[i][j];
                    }
                }
            }
            return null;
        }

        //counts the amount of values in the domain of input vakje
        public int domainSize(Vakje vakje)
        {
            int count = 0;
            for(int i = 0; i < 9; i++)
            {
                if (vakje.domain[i])
                {
                    count++;
                }
            }
            return count;
        }
    }
}
