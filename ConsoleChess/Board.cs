using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChess.Utilities;

namespace ConsoleChess;

class Board
{
    public static readonly int BOARD_LEN = 8;
    
    public Piece[,] Squares { get; private set; }
    public bool IsWhiteToMove { get; private set; }
    public int Castling { get; private set; }
    private FENUtil fen;

    public Board(string fenString)
    {
        fen = new FENUtil(fenString);
        fen.Interpret();
        Squares = fen.BoardSquares;
        IsWhiteToMove = fen.IsWhiteToMove;
    }

    public void PrintBoard()
    {
        if (IsWhiteToMove)
            PrintWhiteBoard();
        else
            PrintBlackBoard();
    }
    private void PrintWhiteBoard()
    {
        for (int row = 0; row < BOARD_LEN; row++)
        {
            for (int col = 0; col < BOARD_LEN; col++)
            {
                Console.Write($"{Squares[row, col].Symbol} ");
            }
            Console.WriteLine();
        }
    }
    private void PrintBlackBoard()
    {
        for (int row = BOARD_LEN - 1; row >= 0; row--)
        {
            for (int col = BOARD_LEN - 1; col >= 0; col--)
            {
                Console.Write($"{Squares[row, col].Symbol} ");
            }
            Console.WriteLine();
        }
    }
}
