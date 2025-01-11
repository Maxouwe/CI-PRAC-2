using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    internal class SudokuGrid
    {
        //row based grid Vakjes have coordinates (rowindex, columnindex)
        public Vakje[][] grid;

        public SudokuGrid()
        {
            grid = new Vakje[9][];
            for(int i = 0; i < 9; i++)
            {
                //every row
                grid[i] = new Vakje[9];
            }
        }

        //parses a string and fills the sudokugrid 
        public void ParseFromString(string str)
        {
            string[] strArr = str.Split(' ');
            int k = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int val = Convert.ToInt32(strArr[k]);

                    //if fixed
                    if (val != 0)
                    {
                        grid[i][j] = new Vakje(true, val, (i, j));
                    }
                    else
                    {
                        grid[i][j] = new Vakje(false, val, (i, j));
                    }

                    k++;
                }
            }
        }

        public string ToString()
        {
            List<string> vertLine = new List<string> { "", "-", "-", "-", "+", "-", "-", "-", "+", "-", "-", "-", "+", "\n" };
            List<string> res = new List<string>();
            res.AddRange(vertLine);
            for (int i = 0; i < 9; i++)
            {
                res.Add("|");
                for (int j = 0; j < 9; j++)
                {
                    res.Add(grid[i][j].val.ToString());
                    if (j % 3 == 2)
                    {
                        res.Add("|");
                    }
                }
                res.Add("\n");
                if (i % 3 == 2)
                {
                    res.AddRange(vertLine);
                }
            }
            return string.Join(" ", res.ToArray());
        }

        //returns the row of Vakje (i.e. all vakjes that are in the same row, including the input vakje)
        public Vakje[] getRow(Vakje vakje)
        {
            int rowIndex = vakje.coordinates.Item1;
            return this.grid[rowIndex];
        }

        //returns the column of input Vakje (i.e. all vakjes that are in the same row, including the input vakje)
        public Vakje[] getColumn(Vakje vakje)
        {
            Vakje[] result = new Vakje[9];
            int columnIndex = vakje.coordinates.Item2;

            for(int i = 0; i < 9; i++)
            {
                result[i] = this.grid[i][columnIndex];
            }

            return result;
        }

        //returns subgrid of input Vakje (i.e. all vakjes that are in the same row, including the input vakje)
        public Vakje[] getSubgrid(Vakje vakje)
        {
            int k = 0;
            Vakje[] result = new Vakje[9];
            int startingRow = vakje.coordinates.Item1 / 3 * 3;
            int startingColumn = vakje.coordinates.Item2 / 3 * 3;

            for(int i = startingRow; i < startingRow + 3; i++)
            {
                for(int j = startingColumn; j < startingColumn + 3; j++)
                {
                    result[k] = this.grid[i][j];
                    k++;
                }
            }

            return result;
        }
    }
}
