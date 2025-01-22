using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class ForwardChecking
    {
        SudokuGrid grid;
        StateOperatorFC op;
        bool goToChild;

        public ForwardChecking(SudokuGrid sugrid)
        {
            grid = sugrid;
            op = new StateOperatorFC(grid);
        }

        public void executeAlgorithm()
        {
            op.makeNodeConsistent();

            //this means finding the first empty square and make this the first node of the search tree
            op.setInitialState();
            
            //this variable is used in loopAlgorithm()
            goToChild = false;

            loopAlgorithm();
        }

        //the recursive part of the algorithm
        private void loopAlgorithm()
        {
            bool solved = false;
            while(!solved)
            {
                if (goToChild)
                {
                    //go to the next empty square
                    if (op.goToChildState())
                    {
                        tryOutNextValueFromDomain();
                    }
                    //if there is no next square that means we are done
                    else
                    {
                        solved = true;
                    }
                    
                }
                //stay in the same square but try out the next value 
                else
                {
                    tryOutNextValueFromDomain();
                }
            }
            
        }

        //for the current square we try out the next value in its domain
        private void tryOutNextValueFromDomain()
        {
            //if there are values left in its domain (if current node has more siblings to try out)
            if (op.fillInNextValueFromDomain())
            {
                //we try to remove the value from the domains, if it succeeds we go to the next empty square
                if (op.removeFromDomains())
                {
                    goToChild = true;
                }
                //if the removing leads to an empty domain we rollback the domains one step back
                //and we try out the next value in the domain
                else
                {
                    op.rollbackDomains();
                    goToChild = false;
                }
            }
            //if there are no values left in its domain (no siblings left for the current node)
            else
            {
                //recover the domains and empty the current square
                op.rollbackDomains();
                op.emptySquare();

                //go to the previous square
                op.goToParentState();

                //we need to rollback the domains here because
                //we know we are going to try out the next value
                op.rollbackDomains();

                goToChild = false;
            }
        }
    }
}
