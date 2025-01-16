using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class Vakje
    {
        public readonly bool fixed_;
        public int val;

        //if bool[i] = true then i + 1 is in the domain
        public bool[] domain;
        public int domainSize;
        //(row index, column index)
        public readonly (int, int) coordinates;

        public Vakje(bool isFixed, int val, (int, int) coords)
        {
            fixed_ = isFixed;
            this.val = val;
            coordinates = coords;

            domain = new bool[9];
            for(int i = 0; i < 9; i++)
            {
                domain[i] = true;
            }
            domainSize = 9;
        }
    }
}
