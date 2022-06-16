using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.Utilities;
enum CastlingSide
{
    King,
    Queen
}
class CastlingUtil
{
    private static readonly int kingMask = 0b1000;
    private static readonly int queenMask = 0b100;
    public static int ToggleOn(CastlingSide side, PieceColor clr, int bits)
    {
        return (side == CastlingSide.King) ? ToggleOnKCastling(bits, clr) : ToggleOnQCastling(bits, clr);
    }
    public static int ToggleOff(CastlingSide side, PieceColor clr, int bits)
    {
        return (side == CastlingSide.King) ? ToggleOffKCastling(bits, clr) : ToggleOffQCastling(bits, clr);
    }
    private static int ToggleOnKCastling(int bitSet, PieceColor clr)
    {
        Debug.Assert(clr != PieceColor.None, "In ToggleOn(),\n\tUsed 'PieceColor.None' as argument for [PieceColor clr] parameter");
        
        int castling = bitSet;
        if (clr == PieceColor.White)
            castling |= kingMask;
        else
            castling |= (kingMask >> 2);
        return castling;
    }
    private static int ToggleOnQCastling(int bitSet, PieceColor clr)
    {
        Debug.Assert(clr != PieceColor.None, "In ToggleOn(),\n\tUsed 'PieceColor.None' as argument for [PieceColor clr] parameter");

        int castling = bitSet;
        if (clr == PieceColor.White)
            castling |= queenMask;
        else
            castling |= (queenMask >> 2);
        return castling;
    }
    private static int ToggleOffKCastling(int bitSet, PieceColor clr)
    {
        Debug.Assert(clr != PieceColor.None, "In ToggleOff(),\n\tUsed 'PieceColor.None' as argument for [PieceColor clr] parameter");

        int castling = bitSet;
        if (clr == PieceColor.White)
            castling &= ~(kingMask);
        else
            castling &= ~(kingMask >> 2);
        return castling;
    }
    private static int ToggleOffQCastling(int bitSet, PieceColor clr)
    {
        Debug.Assert(clr != PieceColor.None, "In ToggleOff(),\n\tUsed 'PieceColor.None' as argument for [PieceColor clr] parameter");

        int castling = bitSet;
        if (clr == PieceColor.White)
            castling &= ~(queenMask);
        else
            castling &= ~(queenMask >> 2);
        return castling;
    }
    public static bool HasCastlingRight(CastlingSide side, PieceColor clr, int bits)
    {
        Debug.Assert(clr != PieceColor.None, "In HasCastlingRight(), \n\tUsed 'PieceColor.None' as argument for [PieceColor clr] parameter");

        bool castling = false;
        if (side == CastlingSide.King && clr == PieceColor.White)       // White Kingside
            castling = (bits & kingMask) != 0;
        else if (side == CastlingSide.Queen && clr == PieceColor.White) // White Queenside
            castling = (bits & queenMask) != 0;
        else if (side == CastlingSide.King && clr == PieceColor.Black)  // Black Kingside
            castling = (bits & (kingMask >> 2)) != 0;
        else if (side == CastlingSide.Queen && clr == PieceColor.Black) // Black Queenside
            castling = (bits & (queenMask >> 2)) != 0;
           
        return castling;
    }
}
