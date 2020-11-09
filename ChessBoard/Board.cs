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
                    if (piece.canMoveTo(whiteKing.row, whiteKing.column))
                    {
                        Console.WriteLine("White is in check.");
                    }
                } 
                else if (piece.team == Team.White)
                {
                    if (piece.canMoveTo(blackKing.row, blackKing.column))
                    {
                        Console.WriteLine("Black is in check.");
                    }
                }
            }
        }

        public Board()
        {
            turn = Team.White;

            grid = new Cell[SIZE, SIZE];

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
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'r', Team.White, this);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'r', Team.Black, this);
                        break;
                    case 1:
                    case 6:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'k', Team.White, this);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'k', Team.Black, this);
                        break;
                    case 2:
                    case 5:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'b', Team.White, this);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'b', Team.Black, this);
                        break;
                    case 3:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'q', Team.White, this);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'q', Team.Black, this);
                        break;
                    case 4:
                        addedPiece1 = grid[i, 0].occupiedBy = new Piece(i, 0, 'x', Team.White, this);
                        addedPiece2 = grid[i, 7].occupiedBy = new Piece(i, 7, 'x', Team.Black, this);
                        break;
                }
                addedPiece3 = grid[i, 1].occupiedBy = new Piece(i, 1, 'p', Team.White, this);
                addedPiece4 = grid[i, 6].occupiedBy = new Piece(i, 6, 'p', Team.Black, this);
                PiecesOnBoard.Add(addedPiece1);
                PiecesOnBoard.Add(addedPiece2);
                PiecesOnBoard.Add(addedPiece3);
                PiecesOnBoard.Add(addedPiece4);
            }
        }

        public void display()
        {
            int rowNum = 0;
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

        public bool selectDestination()
        {
            bool selectionIsValid;
            int row = 0, column = 0;
            do
            {
                selectionIsValid = true;
                Console.WriteLine("Where would you like to move your piece? Ex: 2,3. If you would like to change the selected piece enter \"change\"");
                string input = Console.ReadLine();
                if (input == "change") return true;
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
                        Console.WriteLine("You already have a piece that exists in that cell. Try again.");
                    }
                }
            } while (!selectionIsValid);
            selectedDestination = grid[row, column];
            return false;
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
        public void pawnPromotion()
        {
            List<Piece> pawnsToPromote = PiecesOnBoard.Where(
                p => char.ToLower(p.symbol) == 'p' &&
                ((p.team == Team.White && p.column == SIZE -1) || (p.team == Team.Black && p.column == 0)))
                .ToList();

            foreach (Piece piece in pawnsToPromote)
            {
                piece.symbol = 'q';
            }
        }
    }
}