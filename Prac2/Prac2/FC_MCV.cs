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

        public FC_MCV(SudokuGrid sugrid)
        {
            grid = sugrid;
        }

        public void doAlgorithm()
        {
            initAlgorithm();
            mainAlgorithm();
        }

        private void initAlgorithm()
        {
            makeNodeConsistent();
        }

        private void mainAlgorithm()
        {

        }
        private void makeNodeConsistent()
        {

        }
    }
}
