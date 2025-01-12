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

        //helper function for getRCS
        //fills the input array with all vakjes from the same row as input vakje (excluding input vakje)
        public void getRow(Vakje vakje, Vakje[] toBeFilled)
        {
            Vakje[] row = this.grid[vakje.coordinates.Item1];
            int k = 0;

            for (int j = 0; j < 9; j++)
            {
                //if its not the same cell
                if (j != vakje.coordinates.Item2)
                {
                    toBeFilled[j - k] = row[j];
                }
                else
                {
                    k++;
                }
            }
        }

        //helper function for getRCS
        //fills the input array with all vakjes from the same column as input vakje (excluding input vakje)

        public void getColumn(Vakje vakje, Vakje[] toBeFilled)
        {
            int columnIndex = vakje.coordinates.Item2;
            int k = 0;
            for(int i = 0; i < 9; i++)
            {
                //if not the same cell
                if (i != vakje.coordinates.Item1)
                {
                    toBeFilled[i + 8 - k] = this.grid[i][columnIndex];
                }
                else
                {
                    k++;
                }
            }
        }

        //helper function for getRCS
        //fills the input array with all vakjes from the same subgrid as input vakje (excluding input vakje)
        public void getSubgrid(Vakje vakje, Vakje[] toBeFilled)
        {
            int k = 0;
            int startingRow = vakje.coordinates.Item1 / 3 * 3;
            int startingColumn = vakje.coordinates.Item2 / 3 * 3;

            for(int i = startingRow; i < startingRow + 3; i++)
            {
                for(int j = startingColumn; j < startingColumn + 3; j++)
                {
                    //no vakjes that have been in getRow and getColumn and also no input vakje itself
                    if(i != vakje.coordinates.Item1 && j != vakje.coordinates.Item2)
                    {
                        toBeFilled[k + 16] = this.grid[i][j];
                        k++;
                    }
                }
            }
        }

        //gets all vakjes that are in the same row, column and subgrid (RCS = ROW COLUMN SUBGRID)
        //does not contain duplicate vakjes and does not contain input vakje
        public Vakje[] getRCS(Vakje vakje)
        {
            Vakje[] result = new Vakje[20];

            getRow(vakje, result);
            getColumn(vakje, result);
            getSubgrid(vakje, result);

            return result;
        }
    }
}
