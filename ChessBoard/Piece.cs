using System;
using System.Collections.Generic;

namespace ChessBoardModel
{
    public class Piece
    {
        //Dictionary<char, int[][]> validMoves = new Dictionary<char, int[][]>()
        //{
        //    {'r', new int[,]{ { 1, 2 }, { } } },
        //    {'k', new int[,]{ { 1, 2 }, { } } },
        //    {'b', new int[,]{ { 1, 2 }, { } } },
        //    {'q', new int[,]{ { 1, 2 }, { } } },
        //    {'x', new int[,]{ { 1, 2 }, { } } },
        //    {'p', new int[,]{ { 1, 1 }, { } } },
        //};
        private char Symbol;
        public int row { get; set; }
        public int column { get; set; }
        public char symbol {
            get
            {
                if (team == Team.Black) return char.ToUpper(Symbol);
                return Symbol;
            }
            set => Symbol = value; 
        }
        public string name { get; set; }
        public Team team { get; set; }
        public bool canMoveTo(int row, int column)
        {
            //Console.WriteLine(this.symbol);
            //return true;
            //NEED TO IMPLIMENT
            //based off the piece symbol, current location and input location to move return bool if it is allowed.
            //
            //return true;

            bool isValidMove = false;

            //return true;

            switch (Char.ToLower(this.symbol))
            {
                case 'r':
                    //Rook : Open - Steps on(Vertical, Horizontal) directions
                    if ((row == this.row && column != this.column) || (row != this.row && column == this.column))
                    {
                        isValidMove= true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case 'k':
                    //    //Knight : 3 Steps in LShape directions
                    int[] X = { 2, 1, -1, -2, -2, -1, 1, 2 };
                    int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1 };
                    for (int i = 0; i < 8; i++)
                    {
                        int offsetX = this.column - column;
                        int offsetY = this.row - row;
                        if(offsetY == Y[i] && offsetX == X[i])
                        {
                            isValidMove = true;
                            break;
                        }
                    }
                    break;

                case 'b':
                //    //Bishop : Open - Steps on(Diagonal) directions
                    if(Math.Abs(this.row - row) == Math.Abs(this.column - column))
                    {
                        isValidMove = true;
                        break;
                    }
                    break;

                case 'q':
                //    //Queen : Open - Steps on(Vertical, Horizontal, Diagonal) directions
                    //check straight movement
                    if((row == this.row && column != this.column) || (row != this.row && column == this.column))
                    {
                        isValidMove = true;
                        break;
                    }
                    //check diagonal movement
                    else if (Math.Abs(this.row - row) == Math.Abs(this.column - column))
                    {
                        isValidMove = true;
                        break;
                    }
                    break;

                case 'x':
                //    //King: One Step on(Vertical, Horizontal, Diagonal) directions
                if((this.row + 1 == row && this.column == column) || (this.row + 1 == row && this.column + 1 == column) || (this.row == row && this.column + 1 == column) || (this.row - 1 == row && this.column + 1 == column) || (this.row - 1 == row && this.column == column) || (this.row - 1 == row && this.column - 1 == column) || (this.row == row && this.column - 1 == column) || (this.row + 1 == row && this.column - 1 == column))
                    {
                        isValidMove = true;
                        break;
                    }
                    break;
                    
                case 'p':
                    //Pawn: One - tep(Vertical) and can go One-Step(Diagonal) to eliminate another piece.
                    if (this.team == Team.White)
                    {
                        if (this.column + 1 == column)
                        {
                            isValidMove = true;
                            break;
                        }
                        else
                            break;
                    }
                    else
                    {
                        if (this.column - 1 == column)
                        {
                            isValidMove = true;
                            break;
                        }
                        else
                            break;
                    }
            }
            return isValidMove;
        }
            public Piece(int row, int column, char symbol, Team team)
        {
            this.row = row;
            this.column = column;
            this.symbol = symbol;
            this.team = team;
        }

        public override string ToString()
        {
            return $"Row: {row} Column:{column}";
        }

    }
}
