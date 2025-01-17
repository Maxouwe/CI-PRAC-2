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


            goToChild = false;
            recurseAlgorithm();
            if (!checkVakjes())
            {
                Console.WriteLine("foutje");
            }
        }

        //the recursive part of the algorithm
        private void recurseAlgorithm()
        {
            //Console.WriteLine("Vakje {0} = {1}", op.currentVakje.coordinates, op.currentVakje.val);
            //Console.WriteLine("CurrentNode coordinates {0} {1}", op.currentNode.coordinates.Item1, op.currentNode.coordinates.Item2);
            //Console.WriteLine(grid.ToString());
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
                    else
                    {
                        solved = true;
                    }
                    //if there is no next square that means we are done
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
            //if there are values left in the domain
            if (op.fillInNextValueFromDomain())
            {
                if (op.removeFromDomains())
                {
                    goToChild = true;
                    
                }
                else
                {
                    recoverAndTryNextValue();
                }
            }
            else
            {
                recoverAndGoToParent();
            }
        }

        //the value of the current square is added back to the domains of the Vj's
        //then we empty the square
        //and go to the square we filled in before this one
        //then try out its sibling (by invoking recurseAlgorithm(false))

        private void recoverAndGoToParent()
        {
            op.addBackToDomains();
            op.emptySquare();
            
            op.goToParentState();
            recoverAndTryNextValue();
            checkDomains();
            checkDomains2();

            goToChild = false;
            
        }

        private void recoverAndTryNextValue()
        {
            op.addBackToDomains();
            goToChild = false;
            
        }

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
