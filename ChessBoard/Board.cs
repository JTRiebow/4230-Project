using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    public class Board
    {
        // the size of the board will usually be 8x8
        public int Size { get; set; }

        // array of type Cell
        public Cell[,] theGrid { get; set; }

        //Constructor
        public Board (int s)
        {
            // initialize size of the board is defined by s.
            Size = s;

            // create a new 2D array of type Cell 
            theGrid = new Cell[Size, Size];

            // fill the 2D array with new Cells, each with unique x and y coordinates
            for (int i = 0; i < Size; i++)
            {
                for (int j= 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public void MarkNextLegalMoves( Cell currentCell, string chessPiece)
        {
            // step 1 - clear all previous legal moves
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].legalNextMove = false;
                    theGrid[i, j].currentlyOccupied = false;
                }
            }

            //step 2 - find all legal moves and mark the cells as "legal"
            switch (chessPiece)
            {
                case "Knight":
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber + 2].legalNextMove = true;
                    break;
                case "King":
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber + 1].legalNextMove = true;
                    break;
                case "Queen":
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber - 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber - 7].legalNextMove = true;
                    break;
                case "Rook":
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber, currentCell.columnNumber - 7].legalNextMove = true;
                    break;
                case "Bishop":
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber - 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 1, currentCell.columnNumber + 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 2, currentCell.columnNumber + 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 3, currentCell.columnNumber + 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 4, currentCell.columnNumber + 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 5, currentCell.columnNumber + 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 6, currentCell.columnNumber + 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber - 7, currentCell.columnNumber + 7].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 1, currentCell.columnNumber - 1].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 2, currentCell.columnNumber - 2].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 3, currentCell.columnNumber - 3].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 4, currentCell.columnNumber - 4].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 5, currentCell.columnNumber - 5].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 6, currentCell.columnNumber - 6].legalNextMove = true;
                    theGrid[currentCell.rowNumber + 7, currentCell.columnNumber - 7].legalNextMove = true;
                    break;
                default:
                    break;
            }
            theGrid[currentCell.rowNumber, currentCell.columnNumber].currentlyOccupied = true;
        }
    }
}
