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

            recurseAlgorithm(false);
        }

        //the recursive part of the algorithm
        private void recurseAlgorithm(bool goToChild)
        {
            Console.WriteLine("Vakje {0} = {1}", op.currentVakje.coordinates, op.currentVakje.val);
            Console.WriteLine("CurrentNode coordinates {0} {1}", op.currentNode.coordinates.Item1, op.currentNode.coordinates.Item2);
            Console.WriteLine(grid.ToString());
            if (goToChild)
            {
                //go to the next square with the smallest domain
                if(op.goToChildState()) 
                {
                    tryOutNextValueFromDomain();
                }
                //if there is no next square that means we are done
            }
            //stay in the same square but try out the next value 
            else
            {
                tryOutNextValueFromDomain();
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
                    recurseAlgorithm(true);
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
        //TODO: consider the case where there is no parent anymore
        private void recoverAndGoToParent()
        {
            op.addBackToDomains();
            op.emptySquare();
            op.goToParentState();
            recurseAlgorithm(false);
        }

        private void recoverAndTryNextValue()
        {
            op.addBackToDomains();
            recurseAlgorithm(false);
        }
    }
}
