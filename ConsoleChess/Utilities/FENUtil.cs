using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.Utilities;

class FENUtil
{
    static readonly Dictionary<char, PieceType> pieceRef = new Dictionary<char, PieceType>()
    {
        ['k'] = PieceType.King,
        ['q'] = PieceType.Queen,
        ['r'] = PieceType.Rook,
        ['b'] = PieceType.Bishop,
        ['n'] = PieceType.Knight,
        ['p'] = PieceType.Pawn,
    };
    public Piece[,] BoardSquares { get; private set; }
    public bool IsWhiteToMove { get; private set; }
    public int Castling { get; private set; }
    // public Move EnPassant { get; private set; }
    public int HalfMoves { get; private set; }
    public int FullMoves { get; private set; }
    private string fenStr;
    string[] fenParts;
    public FENUtil(string fenString)
    {
        fenStr = fenString;
        fenParts = fenStr.Split(" ");
        BoardSquares = new Piece[Board.BOARD_LEN, Board.BOARD_LEN];
        initBoard();
    }
    private void initBoard()
    {
        for (int row = 0; row < Board.BOARD_LEN; row++)
        {
            for (int col = 0; col < Board.BOARD_LEN; col++)
            {
                BoardSquares[row, col] = new Piece(PieceType.None, PieceColor.None);
            }
        }
    }
    public void Interpret()
    {
        // Piece placement
        piecePlacement();
        // Color to move
        colorToMove();
        // Castling rights for both colors
        castlingRights();
        // TODO: En passant square
        enPassantSquares();
        // Half move counter
        halfMoveCounter();
        // Full move counter
        fullMoveCounter();
    }
    private void piecePlacement()
    {
        string piecesFEN = fenParts[0];
        int boardIndex = 0;
        int row, col;
        PieceColor color = PieceColor.None;
        foreach (char letter in piecesFEN)
        {
            color = (Char.IsUpper(letter)) ? PieceColor.White : PieceColor.Black;
            row = boardIndex / 8;
            col = boardIndex % 8;
            if (letter == '/')
                continue;
            else if (Char.IsDigit(letter))
            {
                boardIndex += letter % '0';
                continue;
            }
            else if (pieceRef.ContainsKey(Char.ToLower(letter)))
            {
                BoardSquares[row, col] = new Piece(pieceRef[Char.ToLower(letter)], color);
                boardIndex++;
            }
        }
    }
    private void colorToMove()
    {
        char sideToMoveFEN = fenParts[1][0];
        if (sideToMoveFEN == 'w')
            IsWhiteToMove = true;
        else 
            IsWhiteToMove = false;
    }
    private void castlingRights()
    {
        string castlingFEN = fenParts[2];
        int rights = 0b0000;
        foreach (char right in castlingFEN)
        {
            switch (right)
            {
                case 'K':
                    CastlingUtil.ToggleOn(CastlingSide.King, PieceColor.White, rights);
                    break;
                case 'Q':
                    CastlingUtil.ToggleOn(CastlingSide.Queen, PieceColor.White, rights);
                    break;
                case 'k':
                    CastlingUtil.ToggleOn(CastlingSide.King, PieceColor.Black, rights);
                    break;
                case 'q':
                    CastlingUtil.ToggleOn(CastlingSide.Queen, PieceColor.Black, rights);
                    break;
            }
        }
        Castling = rights;
    }
    private void enPassantSquares()
    {
    }
    private void halfMoveCounter() => HalfMoves = fenParts[4][0] % '0';
    private void fullMoveCounter() => FullMoves = fenParts[5][0] % '0';
}
