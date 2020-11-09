using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ChessBoardModel
{
    public class Piece
    {
        private Board gameBoard;
        private char Symbol;
        public int row { get; set; }
        public int column { get; set; }
        public char symbol {
            get
            {
                if (team == Team.Black) return char.ToUpper(Symbol);
                return Symbol;
            }
            set => Symbol = char.ToLower(value); 
        }
        public string name {
            get
            {
                switch (Symbol) {
                    case 'p': return "Pawn";
                    case 'r': return "Rook";
                    case 'k': return "Knight";
                    case 'b': return "Bishop";
                    case 'q': return "Queen";
                    case 'x': return "King";
                    default: return "Unknown";
                }
            }
        }
        public Team team { get; set; }
        public Piece(int row, int column, char symbol, Team team, Board board)
        {
            this.row = row;
            this.column = column;
            this.symbol = symbol;
            this.team = team;
            this.gameBoard = board;
        }

        public bool canMoveTo(int row, int column)
        {
            int rowOffset = Math.Abs(row - this.row);
            int colOffset = Math.Abs(column - this.column);
            int colDifference = column - this.column;

            if ((rowOffset == 0) && (colOffset == 0)) return false;

            switch (Char.ToLower(symbol))
            {
                case 'r':
                    if (row == this.row ^ column == this.column)
                        return !jumpRequired(row, column);
                    break;
                case 'k':
                    if ((rowOffset == 2 && colOffset == 1) || (rowOffset == 1 && colOffset == 2))
                        return true;
                    break;
                case 'b':
                    if (rowOffset == colOffset)
                        return !jumpRequired(row, column);
                    break;
                case 'q':
                    if (rowOffset == colOffset)
                        return !jumpRequired(row, column);
                    if (row == this.row ^ column == this.column)
                        return !jumpRequired(row, column);
                    break;
                case 'x':
                    if (rowOffset <= 1 && colOffset <= 1)
                        return true;
                    break;
                case 'p':
                    if ((colDifference > 0 && this.team == Team.Black) || (colDifference < 0 && this.team == Team.White))
                        return false;
                    if (rowOffset == 1)
                    {
                        if (colOffset == 1 && gameBoard.grid[row, column].currentlyOccupied && gameBoard.grid[row, column].occupiedBy.team != this.team)
                            return true;
                    }
                    else if (rowOffset == 0)
                    {
                        if (colOffset == 2)
                        {
                            if (this.column == 1 && this.team == Team.White)
                                return !jumpRequired(row, column);
                            if (this.column == 6 && this.team == Team.Black)
                                return !jumpRequired(row, column);
                        }
                        else if (colOffset == 1)
                            return true;
                    }
                    break;
            }
            return false;
        }

        private bool jumpRequired(int row, int column)
        {
            int rowDirection;
            int colDirection;
            int rowCursor;
            int colCursor;

            if (row - this.row == 0)
                rowDirection = 0;
            else
                rowDirection = (row - this.row) / Math.Abs(row - this.row);

            if (column - this.column == 0)
                colDirection = 0;
            else
                colDirection = (column - this.column) / Math.Abs(column - this.column);

            rowCursor = this.row;
            colCursor = this.column;

            do
            {
                rowCursor += rowDirection;
                colCursor += colDirection;
                if ((rowCursor == row) && (colCursor == column)) break;
                if (gameBoard.grid[rowCursor, colCursor].currentlyOccupied) return true;
            } while ((rowCursor != row) || (colCursor != column));

            return false;
        }

        public override string ToString()
        {
            return $"Row: {row} Column:{column}";
        }

    }
}
