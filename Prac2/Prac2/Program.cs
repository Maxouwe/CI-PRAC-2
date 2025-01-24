namespace Prac2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //inputMode();

            runExperiments();

            
            
            

        }

        //Experiments.testAlg(m, n)
        //returns average amount of time the algorithm takes to solve a grid over n amount of runs
        //for randomly generated sudokugrids with m amount of fixed cells
        //also the generator can generate valid but unsolvable grids, which waste alot of time
        //we did not enough time to figure out to ensure a generated grid is solvable
        static void runExperiments()
        {
            int n = 50;
            for (int j = 0; j < 10; j++)
            {
                int m = 25 + j;
                (float, int) CBTEST = Experiments.testCB(m, n);
                (float, int) FCTEST = Experiments.testFC(m, n);
                (float, int) FC_MCVTEST = Experiments.testFC_MCV(m, n);
                Console.WriteLine("CB, {1} fixed cells, {3} runs, took {0} ms on average, {2} fails", CBTEST.Item1, m, CBTEST.Item2, n);
                Console.WriteLine("FC, {1} fixed cells, {3} runs, took {0} ms on average, {2} fails", FCTEST.Item1, m, FCTEST.Item2, n);
                Console.WriteLine("FC_MCV, {1} fixed cells, {3} runs, took {0} ms on average, {2} fails", FC_MCVTEST.Item1, m, FC_MCVTEST.Item2, n);
            }
        }

        static void inputMode()
        {
            SudokuGrid grid = new SudokuGrid();
            grid.ParseFromString(Console.ReadLine());
            FC_MCV alg = new FC_MCV(grid);
            alg.executeAlgorithm();
            Console.WriteLine(grid.ToString());
        }

    }
}