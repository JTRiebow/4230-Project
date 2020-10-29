namespace ChessBoardModel
{
    public class Piece
    {
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
            return true;
            //NEED TO IMPLIMENT
            //based off the piece symbol, current location and input location to move return bool if it is allowed.
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
