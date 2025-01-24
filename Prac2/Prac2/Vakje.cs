using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class Vakje
    {
        public bool fixed_;
        public int val;

        //if bool[i] = true then i + 1 is in the domain
        public bool[] domain;
        //so we remember by which vakje which domain element was removed
        public (int, int)[] modifiedBy;

        public int domainSize;

        //(row index, column index)
        public readonly (int, int) coordinates;

        public Vakje(bool isFixed, int val, (int, int) coords)
        {
            fixed_ = isFixed;
            this.val = val;
            coordinates = coords;

            modifiedBy = new (int, int)[9];
            domain = new bool[9];
            for(int i = 0; i < 9; i++)
            {
                domain[i] = true;
                modifiedBy[i] = (-1, -1);
            }
            domainSize = 9;
        }
    }
}
