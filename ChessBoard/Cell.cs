namespace ChessBoardModel
{
    public class Cell
    {
        private Piece OccupiedBy;
        public int row { get; set; }
        public int column { get; set; }
        public bool currentlyOccupied { get; set; }
        //public bool legalNextMove { get; set; }
        public Piece occupiedBy {
            get => OccupiedBy;
            set
            {
                OccupiedBy = value;
                if (value == null)
                {
                    currentlyOccupied = false;
                }
                else
                {
                    currentlyOccupied = true;
                }
            } 
        }


        public Cell(int x, int y)
        {
            row = x;
            column = y;
        }
    }
}
