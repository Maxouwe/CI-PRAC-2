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
        public SudokuGrid sg;

        public StateOperatorCB(SudokuGrid sg) 
        {
            this.sg = sg;
            currentVakje = sg.grid[0][0];
        }


        //checks wether the next value for currentVakje does not violate any constraints
        //this is the sibling of the current state
        //i thought checking the next sibling first before changing the sudokugrid
        //because im not sure if its safe to change things 
        //and then finding out halfway through that it doesnt work
        public bool checkNextSibling()
        {
            currentVakje.val++;
            return check(currentVakje);
        }

        //go to the next sibling of the current state
        //make sure to first call undoOperator()
        //-fill in the next value for Vi
        public void goToNextSibling(int n) //not necessary
        {
            undoOperator();
            currentVakje.val += n;
        }

        //remove the value of currentVakje
        public void undoOperator()
        {
            currentVakje.val = 0;
        }

        //make sure to first call undoOperator()
        //go one vakje terug (for chron backtrack and Forward Checking) that is the cell to the left
        //or if there is no to the left, go to the cell that is most right and one row up
        public void goToParent()
        {
            undoOperator();
            (int,int) parentCoords = previousVakje(currentVakje.coordinates);
            currentVakje = sg.grid[parentCoords.Item1][parentCoords.Item2];
        }

        //just go one vakje to the right (i.e. reassign currentVakje)
        //ofcourse make sure if its not fixed
        //after this currentVakje.val = 0
        //call checkNextSibling and goToNextSibling this will start at trying out value = 1
        public void goToFirstChild()
        {
            sg.grid[currentVakje.coordinates.Item1][currentVakje.coordinates.Item2].val = currentVakje.val;
            (int, int) childCoords = nextVakje(currentVakje.coordinates);
            currentVakje = sg.grid[childCoords.Item1][childCoords.Item2];
        }

        //check if the value of the given vakje is correct
        private bool check(Vakje cv)
        {
            Vakje[] vs = sg.getRCS(cv);
            foreach (Vakje v in vs)
            {
                if (cv.val == v.val) return false;
            }
            return true;
        }

        //check if the current grid is a correct solution
        public bool checkCompleted()
        {
            if (currentVakje.coordinates == (8,8))
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        if (!check(sg.grid[i][j]))
                            return false;
                return true;
            }
            return false;
        }

        //get coordinates of the previous vakje
        private (int, int) previousVakje((int,int) xy)
        {
            //if previous x is still bigger than zero, return that with the same y value, otherwise 8 and the previous y
            int x = xy.Item1 != 0 ? xy.Item1 - 1 : 8;
            int y = xy.Item1 != 0 ? xy.Item2 : xy.Item2 - 1;
            //if the previous vakje is fixed, go one further
            if (!sg.grid[x][y].fixed_)
                return (x, y);
            else
                return previousVakje((x, y));
        }

        //get the coordinates of the next vakje
        private (int,int) nextVakje((int, int) xy)
        {
            //if previous x is still smaller than nine, return that with the same y value, otherwise 0 and the next y
            int x = xy.Item1 != 8 ? xy.Item1 + 1 : 0;
            int y = xy.Item1 != 8 ? xy.Item2 : xy.Item2 + 1;
            //if the next vakje is fixed, go one further
            if (!sg.grid[x][y].fixed_)
                return (x, y);
            else
                return nextVakje((x, y));
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
        public StateOperatorMCV() { }

        
        public void checkNextSibling()
        {

        }

        
        public void goToNextSibling()
        {

        }

        
        public void undoOperator()
        {

        }

        public void goToParent()
        {

        }

        public void goToFirstChild()
        {

        }
    }
}
