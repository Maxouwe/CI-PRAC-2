using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class FC_MCV
    {
        SudokuGrid grid;
        StateOperatorMCV op;
        bool goToChild;

        public FC_MCV(SudokuGrid sugrid)
        {
            grid = sugrid;
            op = new StateOperatorMCV(grid);
        }

        public void executeAlgorithm()
        {
            op.makeNodeConsistent();

            //this means finding an empty square with the smallest domain and make this the first node of the search tree
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
                    //go to the next square with the smallest domain
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


        //all the next functions are just some test functions

        //returns false if there exists a domain that contains a value that it shouldnt contain
        private bool checkDomains()
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (!checkDomain(grid.grid[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //helper for checkDomains
        private bool checkDomain(Vakje vakje)
        {
            if(vakje.val != 0)
            {
                return true;
            }
            int countDomain = 0;
            for (int k = 0; k < 9; k++)
            {
                if (vakje.domain[k])
                {
                    countDomain++;
                }
            }
            if(countDomain != vakje.domainSize)
            {
                return false;
            }
            for(int k = 0; k < 9; k++)
            {
                Vakje[] RCS = grid.getRCS(vakje);
                
                for(int i = 0; i < 20; i++)
                {
                    Vakje tempVakje = RCS[i];
                    if (vakje.domain[k] && RCS[i].val == k + 1 && vakje.val == 0)
                    {
                        //Console.WriteLine(grid.ToString());
                        return false;
                    }
                }
            }
            return true;
        }

        //returns false if there exists a domain that wrongly rules out a value
        private bool checkDomains2()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checkDomain2(grid.grid[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkDomain2(Vakje vakje)
        {
            if(vakje.val != 0)
            {
                return true;
            }
            for (int k = 0; k < 9; k++)
            {
                if (!vakje.domain[k])
                {
                    bool inRCS = false;
                    Vakje[] RCS = grid.getRCS(vakje);
                    for (int i = 0; i < 20; i++)
                    {
                        Vakje tempVakje = RCS[i];
                        if (RCS[i].val == k + 1 && vakje.val == 0)
                        {
                            
                            inRCS = true;
                        }
                    }
                    if (!inRCS)
                    {
                        //Console.WriteLine(grid.ToString());
                        return false;
                    }
                }
                
            }
            return true;
        }

        private bool checkVakjes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checkVakje(grid.grid[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkVakje(Vakje vakje)
        {
            for (int k = 0; k < 9; k++)
            {
                Vakje[] RCS = grid.getRCS(vakje);
                for (int i = 0; i < 20; i++)
                {
                    if (vakje.val == RCS[i].val && vakje.val != 0)
                    {
                        
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
