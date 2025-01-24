using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class Experiments
    {
        public Experiments() { }

        //fixedCells = amount of fixed cells in the generated sudokugrid
        //returns the amount of milliseconds it took to solve one random grid
        static private (long, bool) runFC_MCVOnce(int fixedCells)
        {
            SudokuGrid grid;

            //if the generator gets stuck on generating a certain grid for longer
            //than restartTime then restart and try generating a new grid
            int restartTime = 10;
            grid = SudokuGenerator.generate(fixedCells, restartTime);

            Stopwatch sw = new Stopwatch();

            FC_MCV algoObject = new FC_MCV(grid);

            sw.Start();

            if (!algoObject.executeAlgorithm())
            {
                return (0, false);
            }

            sw.Stop();
            return (sw.ElapsedMilliseconds, true);
        }

        //returns average run time over amountOfRuns runs
        static public (float,int) testFC_MCV(int fixedCells, int amountOfRuns) 
        {
            long timeSum = 0;
            int n = amountOfRuns;
            int amountOfFails = 0;
            (long, bool) timeOnce = runFC_MCVOnce(fixedCells);
            while (n > 0)
            {
                if (timeOnce.Item2)
                {
                    timeSum += timeOnce.Item1;
                    n--;
                }
                else
                {
                    amountOfFails++;
                }
                timeOnce = runFC_MCVOnce(fixedCells);
            }
            return (timeSum/(float)amountOfRuns, amountOfFails);
        }

        static private (long, bool) runFCOnce(int fixedCells)
        {
            SudokuGrid grid;

            //if the generator gets stuck on generating a certain grid for longer
            //than restartTime then restart and try generating a new grid
            int restartTime = 10;
            grid = SudokuGenerator.generate(fixedCells, restartTime);

            Stopwatch sw = new Stopwatch();

            ForwardChecking algoObject = new ForwardChecking(grid);

            sw.Start();

            if (!algoObject.executeAlgorithm())
            {
                return (0, false);
            }

            sw.Stop();
            return (sw.ElapsedMilliseconds, true);
        }

        //returns average run time over amountOfRuns runs
        static public (float,int) testFC(int fixedCells, int amountOfRuns)
        {
            long timeSum = 0;
            int n = amountOfRuns;
            int amountOfFails = 0;
            (long, bool) timeOnce = runFCOnce(fixedCells);
            while (n > 0)
            {
                if (timeOnce.Item2)
                {
                    timeSum += timeOnce.Item1;
                    n--;
                }
                else
                {
                    amountOfFails++;
                }
                timeOnce = runFCOnce(fixedCells);
            }
            return (timeSum / (float)amountOfRuns, amountOfFails);
        }

        static public bool checkVakjes(SudokuGrid grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checkVakje(grid, grid.grid[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static public bool checkVakje(SudokuGrid grid, Vakje vakje)
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
