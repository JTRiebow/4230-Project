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
        static Board myBoard = new Board();

        static void Main(string[] args)
        {
            do
            {
                //display the current state of the gameboard
                myBoard.display();
                //have the user select the piece they would like to move. this will check if they select a cell not on the board,
                // a cell without a piece, and a piece that is not theirs.
                myBoard.selectPiece();
                //have the user select where they would like to move their piece. this will check if they select a cell that is
                //not on the board, a cell that is occupied by their own piece, and if that particurlar piece can move to that cell.
                //THIS NEEDS THE THIRD CHECK IMPLIMENTED
                myBoard.selectDestination();
                //moves the piece on the gameboard. This will destroy a peice that was there. output is displayed for no destruction 
                //as well.
                myBoard.movePiece();
                //toggles whos turn it is on the gameboard.
                myBoard.nextTurn();

                //THIS NEEDS TO BE IMPLIMENTED
                //this will check if the game is over.
            } while (!myBoard.gameOver());

            //THIS NEEDS TO BE IMPLIMENTED
            myBoard.printWinner(); 
        }
    }
}
