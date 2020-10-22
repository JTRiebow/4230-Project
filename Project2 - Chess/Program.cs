using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2___Chess
{
    class Program
    {
        static Board myBoard = new Board(8);

        static void Main(string[] args)
        {
            // show the empty chess board
            printBoard(myBoard);

            // ask the user for an x and y coordinate where we will place a piece
            Cell currentCell = setCurrentCell();
            currentCell.currentlyOccupied = true;

            // calculate all legal moves for that piece
            myBoard.MarkNextLegalMoves(currentCell, "Knight");

            // print the chess board. Use an X for occupied square. Use a + for legal move. Use . for empty cell
            printBoard(myBoard);
            // wait for another enter key press before ending the program
            Console.ReadLine();
        }

        private static Cell setCurrentCell()
        {
            // get x and y coordinates from the user. return a cell location.
            Console.WriteLine("Enter the current row number");
            int currentRow = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the current column number");
            int currentColumn = int.Parse(Console.ReadLine());

            myBoard.theGrid[currentRow, currentColumn].currentlyOccupied = true;
            return myBoard.theGrid[currentRow, currentColumn];
        }

        private static void printBoard(Board myBoard)
        {
            // print the chess  board to the console. Use X for the piece location. + for a legal move. . for empty square
            Console.WriteLine("---------------------------------");
            for (int i = 0; i < myBoard.Size; i++)
            {
                Console.Write("|");
                for (int j = 0; j < myBoard.Size; j++)
                {
                    Cell c = myBoard.theGrid[i, j];

                    if (c.currentlyOccupied == true)
                    {
                        Console.Write(" X |");
                    }
                    else if (c.legalNextMove == true)
                    {
                        Console.Write(" + |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("---------------------------------");
            }

            Console.WriteLine("===================================");
        }

    }
}
