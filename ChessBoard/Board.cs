using ChessBoardModel;
using System;
using System.Data;
using System.Net.Security;

namespace ChessBoard
{
    public partial class Board
    {
        // the SIZE of the board will usually be 8x8
        public const int SIZE = 8; 
        public Team turn { get; set; }
        public Cell[,] grid { get; set; }
        public Piece selectedPiece { get; set; }
        public Cell selectedDestination { get; set; }

        //Constructor
        public Board ()
        {
            // initialize SIZE of the board is defined by s.
            turn = Team.White;

            // create a new 2D array of type Cell 
            grid = new Cell[SIZE, SIZE];

            // fill the 2D array with new Cells, each with unique x and y coordinates
            for (int i = 0; i < SIZE; i++)
            {
                for (int j= 0; j < SIZE; j++)
                {
                    grid[i, j] = new Cell(i, j);
                }
            }

            for (int i = 0; i < SIZE; i++)
            {
                switch (i){
                    case 0:
                    case 7:
                        grid[i, 0].occupiedBy = new Piece(i, 0, 'r', Team.White);
                        grid[i, 7].occupiedBy = new Piece(i, 7, 'r', Team.Black);
                        break;
                    case 1:
                    case 6:
                        grid[i, 0].occupiedBy = new Piece(i, 0, 'k', Team.White);
                        grid[i, 7].occupiedBy = new Piece(i, 7, 'k', Team.Black);
                        break;
                    case 2:
                    case 5:
                        grid[i, 0].occupiedBy = new Piece(i, 0, 'b', Team.White);
                        grid[i, 7].occupiedBy = new Piece(i, 7, 'b', Team.Black);
                        break;
                    case 3:
                        grid[i, 0].occupiedBy = new Piece(i, 0, 'q', Team.White);
                        grid[i, 7].occupiedBy = new Piece(i, 7, 'q', Team.Black);
                        break;
                    case 4:
                        grid[i, 0].occupiedBy = new Piece(i, 0, 'x', Team.White);
                        grid[i, 7].occupiedBy = new Piece(i, 7, 'x', Team.Black);
                        break;
                }
                grid[i, 1].occupiedBy = new Piece(i, 1, 'p', Team.White);
                grid[i, 6].occupiedBy = new Piece(i, 6, 'p', Team.Black);
            }
        }

        public void display()
        {
            int rowNum = 0;
            // print the chess  board to the console. Use X for the piece location. + for a legal move. . for empty square
            Console.WriteLine(" ---------------------------------");
            for (int i = 0; i < SIZE; i++)
            {
                Console.Write(rowNum++);
                Console.Write("|");
                for (int j = 0; j < SIZE; j++)
                {
                    Cell c = this.grid[i, j];

                    if (c.currentlyOccupied == true)
                    {
                        Console.Write($" {c.occupiedBy.symbol} |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine(" ---------------------------------");
            }

            Console.WriteLine("   0   1   2   3   4   5   6   7  ");
            Console.WriteLine("===================================");
        }

        public void selectPiece()
        {
            bool selectionIsValid;
            int row = 0, column = 0;
            do
            {
                selectionIsValid = true;
                Console.WriteLine("What piece would you like to move? Ex: 2,3");
                string input = Console.ReadLine();
                if (input.Split(',').Length != 2)
                {
                    Console.WriteLine("Unrecognizeable input. Please check your format, and try again.");
                    selectionIsValid = false;
                }
                else
                {
                    row = int.Parse(input.Split(',')[0]);
                    column = int.Parse(input.Split(',')[1]);
                    if (row >= SIZE || column >= SIZE || row < 0 || column < 0)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("That cell does not exist on the gameboard. Try again.");
                    }
                    else if (grid[row, column].occupiedBy == null)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("There is no piece on that cell. Try again.");
                    }
                    else if (grid[row, column].occupiedBy.team != turn)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("That is not your piece. Try again.");
                    }

                }
            } while (!selectionIsValid);
            selectedPiece = grid[row, column].occupiedBy;
        }

        public void selectDestination()
        {
            bool selectionIsValid;
            int row = 0, column = 0;
            do
            {
                selectionIsValid = true;
                Console.WriteLine("Where would you like to move your piece? Ex: 2,3");
                string input = Console.ReadLine();
                if (input.Split(',').Length != 2)
                {
                    Console.WriteLine("Unrecognizeable input. Please check your format, and try again.");
                    selectionIsValid = false;
                }
                else
                {
                    row = int.Parse(input.Split(',')[0]);
                    column = int.Parse(input.Split(',')[1]);

                    if (row >= SIZE || column >= SIZE || row < 0 || column < 0)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("That cell does not exist on the gameboard. Try again.");
                    }
                    else if (!selectedPiece.canMoveTo(row, column))
                    {
                            selectionIsValid = false;
                            Console.WriteLine("That is not a valid move for that piece. Try again.");
                    }
                    else if (grid[row, column].occupiedBy?.team == turn)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("You already have a peice that exists in that cell. Try again.");
                    }
                }
            } while (!selectionIsValid);
            selectedDestination = grid[row, column];
        }

        public void movePiece()
        {
            if (selectedDestination.currentlyOccupied)
            {
                Console.WriteLine("You destroyed your enemy. Good Job.");
            } else
            {
                Console.WriteLine($"You move your {selectedPiece.name} to {selectedDestination.ToString()}");
            }
            grid[selectedDestination.row, selectedDestination.column].occupiedBy = selectedPiece;
            grid[selectedPiece.row, selectedPiece.column].occupiedBy = null;
            selectedPiece.column = selectedDestination.column;
            selectedPiece.row = selectedDestination.row;
        }

        public void nextTurn()
        {
            if (turn == Team.Black)
            {
                turn = Team.White;
            } else
            {
                turn = Team.Black;
            }
        }

        public bool gameOver()
        {
            //THIS NEEDS TO BE IMPLIMENTED
            return false;
        }

        public void printWinner()
        {
            //print who the winner of the game is.
        }

        public void MarkNextLegalMoves( Cell currentCell, string chessPiece)
        {
            // step 1 - clear all previous legal moves
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    grid[i, j].legalNextMove = false;
                    grid[i, j].currentlyOccupied = false;
                }
            }

            //step 2 - find all legal moves and mark the cells as "legal"
            switch (chessPiece)
            {
                case "Knight":
                    grid[currentCell.row + 2, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column + 2].legalNextMove = true;
                    break;
                case "King":
                    grid[currentCell.row + 1, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column + 1].legalNextMove = true;
                    break;
                case "Queen":
                    grid[currentCell.row + 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 7].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column - 7].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column - 7].legalNextMove = true;
                    break;
                case "Rook":
                    grid[currentCell.row + 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row, currentCell.column - 7].legalNextMove = true;
                    break;
                case "Bishop":
                    grid[currentCell.row + 1, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column - 7].legalNextMove = true;
                    grid[currentCell.row - 1, currentCell.column + 1].legalNextMove = true;
                    grid[currentCell.row - 2, currentCell.column + 2].legalNextMove = true;
                    grid[currentCell.row - 3, currentCell.column + 3].legalNextMove = true;
                    grid[currentCell.row - 4, currentCell.column + 4].legalNextMove = true;
                    grid[currentCell.row - 5, currentCell.column + 5].legalNextMove = true;
                    grid[currentCell.row - 6, currentCell.column + 6].legalNextMove = true;
                    grid[currentCell.row - 7, currentCell.column + 7].legalNextMove = true;
                    grid[currentCell.row + 1, currentCell.column - 1].legalNextMove = true;
                    grid[currentCell.row + 2, currentCell.column - 2].legalNextMove = true;
                    grid[currentCell.row + 3, currentCell.column - 3].legalNextMove = true;
                    grid[currentCell.row + 4, currentCell.column - 4].legalNextMove = true;
                    grid[currentCell.row + 5, currentCell.column - 5].legalNextMove = true;
                    grid[currentCell.row + 6, currentCell.column - 6].legalNextMove = true;
                    grid[currentCell.row + 7, currentCell.column - 7].legalNextMove = true;
                    break;
                default:
                    break;
            }
            grid[currentCell.row, currentCell.column].currentlyOccupied = true;
        }
    }
}
