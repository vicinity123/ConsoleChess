using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleChess.Utilities;

class MoveValidator
{
    static Regex plainRgx = new Regex("^([KQRBN])?([a-h]|[1-8])?([a-h][1-8])$");
    static Regex captureRgx = new Regex("^([KQRBN]|[a-h])?([a-h]|[1-8])?x([a-h][1-8])$");
    static Regex promoteRgx = new Regex("^([a-h])?x?([a-h][18])([QRBN])$");
    static Regex castlingRgx = new Regex("^(O-O|O-O-O)$");

    public static void Test()
    {
        string guess = "";
        Regex rgx = new Regex("");
        while (true)
        {
            Console.Write("Input a string: ");
            guess = Console.ReadLine();

            if (guess == "!e")
                break;

            if (plainRgx.IsMatch(guess))
            {
                Console.WriteLine("Plain move");
                rgx = plainRgx;
                break;
            }
            else if (captureRgx.IsMatch(guess))
            {
                Console.WriteLine("Capture move");
                rgx = captureRgx;
                break;
            }
            else if (promoteRgx.IsMatch(guess))
            {
                Console.WriteLine("Promotion move");
                rgx = promoteRgx;
                break;
            }
            else if (castlingRgx.IsMatch(guess))
            {
                Console.WriteLine("Castling move");
                rgx = castlingRgx;
                break;
            }

            Console.WriteLine();
        }
        Interpret(rgx, rgx.Match(guess));
    }
    private static void Interpret(Regex moveRgx, Match moveMatch)
    {
        // Move to interpret: Qxa3, dxc7, g3, Nf6, exf1Q
        string toOutput = "";
        
        string[] groupKeys = moveRgx.GetGroupNames();
        for (int i = 0; i < groupKeys.Length; i++)
        {
            Group grp = moveMatch.Groups[groupKeys[i]];
            if (grp.Value != "" && i != 0)
            {
                toOutput += $"{grp.Value}, ";
            }
        }
        Console.WriteLine(toOutput);
    }
}
