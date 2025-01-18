using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

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
        public Vakje currentVakje;
        public int previousVal;
        public MCV_Node currentNode;
        SudokuGrid grid;

        public StateOperatorMCV(SudokuGrid grd) 
        {
            grid = grd;
        }

        //get a square with the smallest domain
        //this cant return a square with an empty domain because the algorithm prevents empty domains in the first place
        public bool setInitialState()
        {
            currentVakje = getSmallestDomain();
            if(currentVakje != null)
            {
                currentNode = new MCV_Node(currentVakje);

                return true;
            }

            //if there are no empty squares
            return false;
        }
        

        //currentVakje.val = nextValueInDomain
        //if there is no next value then return false
        public bool fillInNextValueFromDomain()
        {
            for (int i = currentVakje.val; i < 9; i++)
            {
                if (currentVakje.domain[i])
                {
                    currentVakje.val = (i + 1);
                    return true;
                }
            }
            return false;
        }
        
        public void emptySquare()
        {
            currentVakje.val = 0;
        }

        //goes on node up, but does not fill in any value
        public void goToParentState()
        {
            currentNode = currentNode.getParent();

            currentNode.deleteChild();

            currentVakje = grid.grid[currentNode.coordinates.Item1][currentNode.coordinates.Item2];
        }

        //goes to the square with the smallest domain but does not fill in any value yet
        public bool goToChildState()
        {
            Vakje? smallestDomain = getSmallestDomain();
            if(smallestDomain != null)
            {
                currentVakje = smallestDomain;
                MCV_Node newChild = new MCV_Node(currentVakje);

                currentNode.setChild(newChild);
                currentNode = newChild;
                return true;
            }
            return false;
        }

        //reverts all domains to its previous state
        public void rollbackDomains()
        {
            //make sure we dont try to add back value = 0
            if(currentVakje.val != 0)
            {
                //get all squares in the same row,column and subgrid as currentVakje
                Vakje[] RCS = grid.getRCS(currentVakje);

                for (int i = 0; i < 20; i++)
                {
                    Vakje tempVakje = RCS[i];

                    //if (the domain doesnt contain currentValue &&
                    //that value has been removed because of currentVakje &&
                    //tempVakje is an empty square)
                    if (!tempVakje.domain[currentVakje.val - 1] && tempVakje.modifiedBy[currentVakje.val - 1] == currentVakje.coordinates && tempVakje.val == 0)
                    {   
                        //forget which vakje had removed the currentvalue from its domain
                        tempVakje.modifiedBy[currentVakje.val - 1] = (-1, -1);
                        //put it back in its domain
                        tempVakje.domain[currentVakje.val - 1] = true;
                        tempVakje.domainSize++;
                    }
                }
            }
        }

        //updates all domains of empty Vj's after filling in Vi
        //returns false if this leads to an empty domain
        public bool removeFromDomains()
        {
            if(currentVakje.val != 0)
            {
                Vakje[] RCS = grid.getRCS(currentVakje);

                for (int i = 0; i < 20; i++)
                {
                    Vakje tempVakje = RCS[i];
                    
                    //remove value of current vakje from the domain of the other vakje if its empty 
                    if (tempVakje.domain[currentVakje.val - 1] && tempVakje.val == 0)
                    {
                        //remember that currentVakje has removed this value from its domain
                        tempVakje.modifiedBy[currentVakje.val - 1] = currentVakje.coordinates;
                        //removed it from its domain
                        tempVakje.domain[currentVakje.val - 1] = false;
                        tempVakje.domainSize--;

                        //if this leads to an empty domain tell this to the invoker of this function
                        //so we can backtrack
                        if (tempVakje.domainSize == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //finds empty vakje with smallest domain(most constrained vakje)
        //returns null if it cant find an empty cell
        private Vakje? getSmallestDomain()
        {
            //start with any empty vakje in grid
            Vakje vakjeWithSmallestDomain = null;
            int smallestDomain = 9;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(vakjeWithSmallestDomain == null && grid.grid[i][j].val == 0)
                    {
                        vakjeWithSmallestDomain = grid.grid[i][j];
                    }
                    //if we find a vakje with a smaller domain and empty
                    else if (vakjeWithSmallestDomain != null && vakjeWithSmallestDomain.domainSize >= grid.grid[i][j].domainSize && grid.grid[i][j].val == 0)
                    {
                        vakjeWithSmallestDomain = grid.grid[i][j];
                    }
                }
            }
            return vakjeWithSmallestDomain;
        }

        //for each fixed cell Vi remove Value(Vi) from Domain(Vj) With Vj = any square thats affected by Vi's value
        public void makeNodeConsistent()
        {
            //for every square
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    //if square Vi is fixed
                    if (grid.grid[i][j].val != 0)
                    {
                        //update the domains of all Vj's
                        Vakje[] Vjs = grid.getRCS(grid.grid[i][j]);
                        for(int k = 0; k < 20; k++)
                        {
                            if (Vjs[k].domain[grid.grid[i][j].val - 1])
                            {
                                Vjs[k].modifiedBy[grid.grid[i][j].val - 1] = (i, j);
                                Vjs[k].domain[grid.grid[i][j].val - 1] = false;
                                Vjs[k].domainSize--;
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
