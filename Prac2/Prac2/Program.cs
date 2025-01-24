namespace Prac2
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //inputmodes expect an input formatted like the example sudokus on blackboard
            //runExperiments dont expect any input


            //uncomment one to pick your mode

            //ForwardChecking
            //inputModeFC();

            //FC-MCV
            //inputModeFC_MCV();

            //Chronological backtracking
            //inputModeCB();


            //runExperiments();





        }

        //Experiments.testAlg(m, n)
        //returns average amount of time the algorithm takes to solve a grid over n amount of runs
        //for randomly generated sudokugrids with m amount of fixed cells
        //also the generator can generate valid but unsolvable grids, which waste alot of time
        static void runExperiments()
        {
            int n = 100;
            for (int j = 0; j < 5; j++)
            {
                int m = 28 + j;
                (float, int) CBTEST = Experiments.testCB(m, n);
                (float, int) FCTEST = Experiments.testFC(m, n);
                (float, int) FC_MCVTEST = Experiments.testFC_MCV(m, n);
                Console.WriteLine("CB, {1} fixed cells, {2} runs, took {0} ms on average", CBTEST.Item1, m, n);
                Console.WriteLine("FC, {1} fixed cells, {2} runs, took {0} ms on average", FCTEST.Item1, m, n);
                Console.WriteLine("FC_MCV, {1} fixed cells, {2} runs, took {0} ms on average", FC_MCVTEST.Item1, m, n);
            }
        }

        static void inputModeFC_MCV()
        {
            SudokuGrid grid = new SudokuGrid();
            grid.ParseFromString(Console.ReadLine());
            FC_MCV alg = new FC_MCV(grid);
            if (alg.executeAlgorithm())
            {
                Console.WriteLine(grid.ToString());
            }
            else
            {
                Console.WriteLine("input sudoku not solvable");
            }
        }

        static void inputModeFC()
        {
            SudokuGrid grid = new SudokuGrid();
            grid.ParseFromString(Console.ReadLine());
            ForwardChecking alg = new ForwardChecking(grid);

            if (alg.executeAlgorithm())
            {
                Console.WriteLine(grid.ToString());
            }
            else
            {
                Console.WriteLine("input sudoku not solvable");
            }
        }

        static void inputModeCB()
        {
            SudokuGrid grid = new SudokuGrid();
            grid.ParseFromString(Console.ReadLine());
            ChronologicalBacktracking alg = new ChronologicalBacktracking(grid);
            
            if (alg.runAlgorithm())
            {
                Console.WriteLine(alg.printResult());
            }
            else
            {
                Console.WriteLine("input sudoku not solvable");
            }
        }
    }
}