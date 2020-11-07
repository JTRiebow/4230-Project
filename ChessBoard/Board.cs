using ChessBoardModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Security;

namespace ChessBoard
{
    public partial class Board
    {
        public const int SIZE = 8;
        public Team turn { get; set; }
        public Cell[,] grid { get; set; }
        public Piece selectedPiece { get; set; }
        public Cell selectedDestination { get; set; }
        public List<Piece> PiecesOnBoard = new List<Piece>();

        public bool bKingDied = false;
        public bool wKingDied = false;

        public void displayTurn()
        {
            Console.WriteLine($"{turn.ToString()}'s turn");
        }

        public void testForCheck()
        {
            Piece whiteKing = PiecesOnBoard.Find(p => p.symbol == 'x');
            Piece blackKing = PiecesOnBoard.Find(p => p.symbol == 'X');
            //for each piece left on the board test to see if they can capture the opposing king.
            foreach (Piece piece in PiecesOnBoard)
            {
                if (piece.team == Team.Black)
                {
                    if(piece.canMoveTo(whiteKing.row,whiteKing.column)) Console.WriteLine("White is in check.");
                } 
                else if (piece.team == Team.White)
                {
                    if (piece.canMoveTo(blackKing.row, blackKing.column)) Console.WriteLine("Black is in check.");
                }
            }
        }

        //Constructor
        public Board()
        {
            // initialize SIZE of the board is defined by s.
            turn = Team.White;

            // create a new 2D array of type Cell 
            grid = new Cell[SIZE, SIZE];

            // fill the 2D array with new Cells, each with unique x and y coordinates
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    grid[i, j] = new Cell(i, j);
                }
            }
            Piece addedPiece1 = null, addedPiece2 = null, addedPiece3 = null, addedPiece4 = null;
            for (int i = 0; i < SIZE; i++)
            {
                switch (i)
                {
                    case 0:
                    case 7:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'r', Team.White);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'r', Team.Black);
                        break;
                    case 1:
                    case 6:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'k', Team.White);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'k', Team.Black);
                        break;
                    case 2:
                    case 5:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'b', Team.White);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'b', Team.Black);
                        break;
                    case 3:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'q', Team.White);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'q', Team.Black);
                        break;
                    case 4:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'x', Team.White);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'x', Team.Black);
                        break;
                }
                addedPiece3 = grid[i, 1].occupiedBy = new Piece(i, 1, 'p', Team.White);
                addedPiece4 = grid[i, 6].occupiedBy = new Piece(i, 6, 'p', Team.Black);
                PiecesOnBoard.Add(addedPiece1);
                PiecesOnBoard.Add(addedPiece2);
                PiecesOnBoard.Add(addedPiece3);
                PiecesOnBoard.Add(addedPiece4);
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
            Console.WriteLine($"It is the {turn.ToString()} team's turn.");
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
                    char cellTaken = 't';
                    if (grid[row, column].occupiedBy == null)
                    {
                        cellTaken = 'f';
                    }

                    char pieceInPath = 'f';
                    switch (Char.ToLower(selectedPiece.symbol))
                    {
                        case 'r':

                            if(selectedPiece.row != row)
                            {
                                if(selectedPiece.row < row)
                                {
                                    for (int i = selectedPiece.row + 1; i < row; i++)
                                    {
                                        if (grid[i, column].occupiedBy != null)
                                        {
                                            pieceInPath = 't';
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = selectedPiece.row + 1; i < row; i--)
                                    {
                                        if (grid[i, column].occupiedBy != null)
                                        {
                                            pieceInPath = 't';
                                            break;
                                        }
                                    }
                                }

                            }
                            else if (selectedPiece.column != column)
                            {
                                if (selectedPiece.column < column)
                                {
                                    for (int i = selectedPiece.column + 1; i < column; i++)
                                    {
                                        if (grid[row, i].occupiedBy != null)
                                        {
                                            pieceInPath = 't';
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = selectedPiece.column  -1; i > column; i--)
                                    {
                                        if (grid[row, i].occupiedBy != null)
                                        {
                                            pieceInPath = 't';
                                            break;
                                        }
                                    }
                                }
                            }


                            break;

                        case 'b':

                            if (selectedPiece.row != row)
                            {
                                if (selectedPiece.symbol == 'b')
                                {
                                    if (selectedPiece.row < row)
                                    {
                                        int j = selectedPiece.column + 1;
                                        for (int i = selectedPiece.row + 1; i < row; j++, i++)
                                        {
                                            if (grid[i, j].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int j = selectedPiece.column + 1;
                                        for (int i = selectedPiece.row - 1; i > row; j++, i--)
                                        {
                                            if (grid[i, j].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (selectedPiece.row < row)
                                    {
                                        int j = selectedPiece.column - 1;
                                        for (int i = selectedPiece.row + 1; i < row; j--, i++)
                                        {
                                            if (grid[i, j].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int j = selectedPiece.column - 1;
                                        for (int i = selectedPiece.row - 1; i > row; j--, i--)
                                        {
                                            if (grid[i, j].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }

                            break;

                        case 'q':
                            if (selectedPiece.row < row && selectedPiece.column < column)
                            {
                                int j = selectedPiece.column + 1;
                                for (int i = selectedPiece.row + 1; i < row; j++, i++)
                                {
                                    if (grid[i, j].occupiedBy != null)
                                    {
                                        pieceInPath = 't';
                                        break;
                                    }
                                }
                            }
                            else if (selectedPiece.row > row && selectedPiece.column < column)
                            {
                                int j = selectedPiece.column + 1;
                                for (int i = selectedPiece.row - 1; i > row; j++, i--)
                                {
                                    if (grid[i, j].occupiedBy != null)
                                    {
                                        pieceInPath = 't';
                                        break;
                                    }
                                }
                            }
                            else if (selectedPiece.row > row && selectedPiece.column > column)
                            {
                                int j = selectedPiece.column - 1;
                                for (int i = selectedPiece.row - 1; i > row; j--, i--)
                                {
                                    if (grid[i, j].occupiedBy != null)
                                    {
                                        pieceInPath = 't';
                                        break;
                                    }
                                }
                            }
                            else if (selectedPiece.row > row && selectedPiece.column < column)
                            {
                                int j = selectedPiece.column - 1;
                                for (int i = selectedPiece.row + 1; i < row; j--, i++)
                                {
                                    if (grid[i, j].occupiedBy != null)
                                    {
                                        pieceInPath = 't';
                                        break;
                                    }

                                }
                            }
                            //make sure there was a change in movement
                            else if(selectedPiece.row == row && selectedPiece.column == column)
                            {
                                pieceInPath = 't';
                                break;
                            }
                            else
                            {

                                if (selectedPiece.row != row)
                                {
                                    if (selectedPiece.row < row)
                                    {
                                        for (int i = selectedPiece.row + 1; i < row; i++)
                                        {
                                            if (grid[i, column].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = selectedPiece.row + 1; i < row; i--)
                                        {
                                            if (grid[i, column].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }

                                }
                                else if (selectedPiece.column != column)
                                {
                                    if (selectedPiece.column < column)
                                    {
                                        for (int i = selectedPiece.column + 1; i < column; i++)
                                        {
                                            if (grid[row, i].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = selectedPiece.column + 1; i < column; i--)
                                        {
                                            if (grid[row, i].occupiedBy != null)
                                            {
                                                pieceInPath = 't';
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            break;




                    }





                    //have the user select where they would like to move their piece. this will check if they select a cell that is
                    //not on the board, a cell that is occupied by their own piece, and if that particurlar piece can move to that cell.

                    if (row >= SIZE || column >= SIZE || row < 0 || column < 0)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("That cell does not exist on the gameboard. Try again.");
                    }
                    else if (!selectedPiece.canMoveTo(row, column, cellTaken, pieceInPath))
                    {
                        selectionIsValid = false;
                        Console.WriteLine("That is not a valid move for that piece. Try again.");
                    }
                    else if (grid[row, column].occupiedBy?.team == turn)
                    {
                        selectionIsValid = false;
                        Console.WriteLine("You already have a piece that exists in that cell. Try again.");
                    }
                }
            } while (!selectionIsValid);
            selectedDestination = grid[row, column];
        }

        public void movePiece()
        {
            if (selectedDestination.currentlyOccupied)
            {
                Console.WriteLine($"You captured the enemy {selectedDestination.occupiedBy.name}. Good Job.");
                PiecesOnBoard.Remove(PiecesOnBoard.Find(p => p.row == selectedDestination.row && p.column == selectedDestination.column));
            }
            else
            {
                Console.WriteLine($"You move your {selectedPiece.symbol} to {selectedDestination.row},{selectedDestination.column}");

                Console.WriteLine($"You move your {selectedPiece.name} to {selectedDestination.ToString()}");
                //need to override the tostring method.
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
            }
            else
            {
                turn = Team.Black;
            }
        }

        public bool gameOver()
        {
            if (PiecesOnBoard.FindAll(p => p.name == "King").Count() == 2) return false;
            return true;
        }

        public void printWinner()
        {
            Console.WriteLine($"{PiecesOnBoard.Find(p => p.name == "King").team} team wins!");
        }

        // This function checks to see if any pawns have reached the edge of the opposing side's board.
        // If so, it automatically promotes the pawn to a queen.
        public void pawnPromotion()
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (grid[i, 0].occupiedBy != null)
                {
                    if (grid[i, 0].occupiedBy.symbol == 'P')
                    {
                        grid[i, 0].occupiedBy.symbol = 'Q';
                    }
                }
                if (grid[i, 7].occupiedBy != null)
                {
                    if (grid[i, 7].occupiedBy.symbol == 'p')
                    {
                        grid[i, 7].occupiedBy.symbol = 'q';
                    }
                }
            }
        }
    }
}