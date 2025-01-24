namespace Prac2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SudokuGrid sud = new SudokuGrid();
            sud.ParseFromString(Console.ReadLine());
            Console.WriteLine(sud.ToString());
            ChronologicalBacktracking cb = new ChronologicalBacktracking(sud);
            Console.WriteLine(cb.runAlgorithm());
            Console.WriteLine(cb.printResult());
        }


        //maybe useful for testing or you can modify the function to test something else
        //might save a bit of time
        //asks for coordinates of a vakje
        //prints all the vakjes of the same row, column and subgrid
        //in the order row then column then subgrid, no duplications or input vakje itself
        public static void testFunc1()
        {
            SudokuGrid sud = new SudokuGrid();
            sud.ParseFromString(Console.ReadLine());
            Console.WriteLine(sud.ToString());

            (int, int) coords = (Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));

            Vakje vakje = sud.grid[coords.Item1][coords.Item2];
            Vakje[] vakjes = sud.getRCS(vakje);

            for(int i = 0; i < 20; i++)
            {
                Console.Write(vakjes[i].val.ToString());
            }
        }
    }
}