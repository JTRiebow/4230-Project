# 4230-Project


### DUMB Chess Game

This is our dumb chess game! What we mean by dumb is that it doesn't adhere to some of the more advanced rules in chess. You Shouldn't expect the program to adhere to rules 
like 'en passant', castling, stalemate, or even 'checkmate'. What you can expect is that the pieces will move as they should normally. They won't make any illegal moves and 
they will overtake other pieces as expected. If you manage to get a pawn to the side of the opposing team, the pawn will be auotmatically promoted to a queen. The program 
will also check, no matter who's turn it is, if someone's king is in 'check'.

### How the Game Works

When the program begins, it will show a new chess board with the white team on the left (lower case letters) and the black team on the right (upper case letters). The Board's
rows and columns are identified by number between zero and seven. The pieces are represented by the following symbols: p is for pawn, r is for rook, k is for knight, b is for 
bishop, q is for queen, and x is for the king. The white team player, because white team always goes first, will be prompted to choose a piece to move. The player chooses a piece 
by inputting the row and cloumn number seperated by a comma (e.g., 2,3). If the selection for the piece is valid, the player will then be prompted to input the row and cloumn
they would like to move the piece to. The first team to kill the opposing team's king wins!
