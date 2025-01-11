namespace Prac2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SudokuGrid sud = new SudokuGrid();
            sud.ParseFromString(Console.ReadLine());
            Console.WriteLine(sud.ToString());
        }


        //maybe useful for testing or you can modify the function to test something else
        //might save a bit of time
        //asks for coordinates of a vakje
        //prints all the vakjes of the same row, column and subgrid
        //asks the new value of het vakje
        //prints the same row, column and subgrid again
        public void testFunc1()
        {
            SudokuGrid sud = new SudokuGrid();
            sud.ParseFromString(Console.ReadLine());
            Console.WriteLine(sud.ToString());

            (int, int) coords = (Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));

            Vakje vakje = sud.grid[coords.Item1][coords.Item2];
            Vakje[] row = sud.getRow(vakje);
            Vakje[] column = sud.getColumn(vakje);
            Vakje[] subgrid = sud.getSubgrid(vakje);

            for (int i = 0; i < 9; i++)
            {
                Console.Write(row[i].val + " ");
            }
            Console.WriteLine('\n');

            for (int i = 0; i < 9; i++)
            {
                Console.Write(column[i].val + " ");
            }
            Console.WriteLine('\n');

            for (int i = 0; i < 9; i++)
            {
                Console.Write(subgrid[i].val + " ");
            }

            int newVal = Convert.ToInt32(Console.ReadLine());
            vakje.val = newVal;

            for (int i = 0; i < 9; i++)
            {
                Console.Write(row[i].val + " ");
            }
            Console.WriteLine('\n');

            for (int i = 0; i < 9; i++)
            {
                Console.Write(column[i].val + " ");
            }
            Console.WriteLine('\n');

            for (int i = 0; i < 9; i++)
            {
                Console.Write(subgrid[i].val + " ");
            }
        }
    }
}