using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    //makes random sudokugrids
    internal class SudokuGenerator
    {
        //fixedValues is the amount of fixed cells
        //if it takes longer than timeOut(in milliseconds) then restart
        private static SudokuGrid? gen(int fixedValues, Stopwatch sw, int timeOut)
        {
            SudokuGrid grid = new SudokuGrid();
            grid.initEmpty();

            Random rnd = new Random();
            int row;
            int column;
            int val;

            while (fixedValues > 0)
            {
                row = rnd.Next(0, 9);
                column = rnd.Next(0, 9);
                
                Vakje vakje = grid.grid[row][column];

                if (!vakje.fixed_)
                {
                    bool[] avVals = availableValues(grid, vakje);
                    
                    LinkedList<int> vals = new LinkedList<int>();


                    for (int i = 0; i < 9; i++)
                    {
                        if (avVals[i])
                        {
                            
                            vals.AddFirst(i + 1);
                        }
                    }

                    if(vals.Count != 0)
                    {

                        val = vals.ElementAt(rnd.Next(0, vals.Count));

                        grid.grid[row][column].fixed_ = true;
                        grid.grid[row][column].val = val;
                        fixedValues--;

                    }
                    

                    
                    
                }
                if (sw.ElapsedMilliseconds > timeOut)
                {
                    return null;
                }
            }
            return grid;
        }

        public static SudokuGrid generate(int fixedValues, int timeOut)
        {
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SudokuGrid grid = gen(fixedValues, sw, timeOut);

            //if grid == null it means generating the sudokugrid took too long
            //and we retry. We need to do this because sometimes the generator generates
            //grids that take a long time to generate
            while (grid == null)
            {
                sw = new Stopwatch();
                sw.Start();
                grid = gen(fixedValues, sw, timeOut);
            }

            return grid;
        }

        //returns an array of possible values for input vakje
        //makes the generation less random but does speeds up the process
        static bool[] availableValues(SudokuGrid grid, Vakje vakje)
        {
            bool[] possibleValues = new bool[9];
            for(int i = 0; i < 9; i++)
            {
                possibleValues[i] = true;
            }

            Vakje[] rcs = grid.getRCS(vakje);

            foreach (Vakje v in rcs)
            {
                if(v.val != 0)
                {
                    possibleValues[v.val - 1] = false;
                }
            }
            return possibleValues;
        }


    }
}
