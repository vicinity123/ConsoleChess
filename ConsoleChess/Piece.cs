using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess;

enum PieceType
{
    None = 0,
    King = 1,
    Queen = 2,
    Rook = 3,
    Bishop = 4,
    Knight = 5,
    Pawn = 6
}
enum PieceColor
{
    White = 0b01000,
    Black = 0b10000,
    None = 0b0
}
class Piece
{
    public static int BlackMask = 0b10000;
    public static int WhiteMask = 0b1000;
    public static int ColorMask = 0b11000;
    public static int TypeMask = 0b111;

    // Instance variables
    public int Type { get; private set; } // Contains type and color
    public char Symbol { get; private set; }
    public int Value { get; private set; }

    public Piece(PieceType type, PieceColor color)
    {
        Type = (int)color | (int)type;
        setSymbolAndValue();
    }
    private void setSymbolAndValue()
    {
        int pieceValue = -1;
        char pieceSymbol = ' ';
        int pieceType = (int)GetTypeFromPiece(this);
        PieceColor pieceColor = GetColorFromPiece(this);

        switch (pieceType)
        {
            case 1: // King
                pieceSymbol = 'K';
                pieceValue = 10;
                break;
            case 2: // Queen
                pieceSymbol = 'Q';
                pieceValue = 9;
                break;
            case 3: // Rook
                pieceSymbol = 'R';
                pieceValue = 5;
                break;
            case 4: // Bishop
                pieceSymbol = 'B';
                pieceValue = 3;
                break;
            case 5: // Knight
                pieceSymbol = 'N';
                pieceValue = 3;
                break;
            case 6: // Pawn
                pieceSymbol = 'P';
                pieceValue = 1;
                break;
            default: // None or empty square
                pieceSymbol = '.';
                pieceValue = 0;
                break;
        }
        Value = pieceValue;
        Symbol = (pieceColor == PieceColor.White) ? pieceSymbol : Char.ToLower(pieceSymbol);
    }
    public static PieceColor GetColorFromPiece(Piece piece)
    {
        int color = piece.Type & ColorMask;
        return (color == (int)PieceColor.White) ? PieceColor.White : PieceColor.Black;
    }
    public static PieceType GetTypeFromPiece(Piece piece)
    {
        int type = piece.Type & TypeMask;

        PieceType pieceType;
        switch (type)
        {
            case 1:
                pieceType = PieceType.King;
                break;
            case 2:
                pieceType = PieceType.Queen;
                break;
            case 3:
                pieceType = PieceType.Rook;
                break;
            case 4:
                pieceType = PieceType.Bishop;
                break;
            case 5:
                pieceType = PieceType.Knight;
                break;
            case 6:
                pieceType = PieceType.Pawn;
                break;
            default:
                pieceType = PieceType.None;
                break;
        }
        return pieceType;
    }
    public static bool IsColor(Piece piece, PieceColor clr)
    {
        PieceColor pColor = GetColorFromPiece(piece);
        if (piece == null || (piece.Type & TypeMask) == (int)PieceType.None)
            return false;
        return pColor == clr;
    }
    public static bool IsType(Piece piece, PieceType type)
    {
        PieceType pType = GetTypeFromPiece(piece);
        if (piece == null || pType == PieceType.None)
            return false;
        return pType == type;
    }
    public override string ToString()
    {
        return $"Piece: {GetTypeFromPiece(this)}\nColor: {GetColorFromPiece(this)}\nValue: {Value}"; 
    }

}
