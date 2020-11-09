﻿using ChessBoard;
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
                //display the current state of the gameboard.
                myBoard.display();

                //display whos turn it is.
                myBoard.displayTurn();

                //display if there is a king in check.
                myBoard.testForCheck();
                do
                {
                    //select a piece to move.
                    myBoard.selectPiece();

                    //select the destination to move to.
                } while (myBoard.selectDestination());


                //move the piece and update the gameboard.
                myBoard.movePiece();

                //toggles whos turn it is on the gameboard.
                myBoard.nextTurn();

                myBoard.pawnPromotion();


                //check if the game is over and if it is not loop.
            } while (!myBoard.gameOver());

            //print out who the winner is
            myBoard.printWinner();
            Console.ReadKey();
        }
    }
}
