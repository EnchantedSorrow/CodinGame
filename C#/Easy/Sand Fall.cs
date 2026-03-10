using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;

class Solution
{
    static void Main(string[] args)
    {
        SandBox sandbox;

        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        sandbox = new SandBox(int.Parse(inputs[1]), int.Parse(inputs[0]));


        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            sandbox.AddItemToSandbox(char.Parse(inputs[0]), int.Parse(inputs[1]));
        }

        sandbox.PrintSandbox();
    }
}

public class SandBox
{
    char[][] _sandbox;
    char _placeholderChar = '0';

    public SandBox(int rows, int columns)
    {
        _sandbox = new char[rows][];
        for(int rowI = 0; rowI < _sandbox.Length; rowI++)
        {
            _sandbox[rowI] = new char[columns];
        }

        InitialiseSandbox();
    }

    void InitialiseSandbox()
    {
        for (int rowI = 0; rowI < _sandbox.Length; rowI++)
        {
            for (int colI = 0; colI < _sandbox[rowI].Length; colI++)
            {
                _sandbox[rowI][colI] = _placeholderChar;
            }
        }
    }

    public void PrintSandbox()
    {
        foreach (char[] row in _sandbox)
        {
            Console.Write("|");
            foreach (char slot in row)
            {
                Console.Write((slot == _placeholderChar) ? " " : slot);
            }

            Console.Write("|" + Environment.NewLine);
        }

        Console.Write("+");
        for (int i = 0; i < _sandbox[0].Length; i++)
        {
            Console.Write("-");
        }
        Console.Write("+" + Environment.NewLine);
    }

    bool SlotFree(int rowI, int colI)
    {
        if (!CheckValidRowIndex(rowI)) return false;
        if (!CheckValidColumnIndex(colI)) return false;
        return _sandbox[rowI][colI] == _placeholderChar;
    }

    bool CheckValidRowIndex(int index)
    {
        return ((index >= 0) && (index < _sandbox.Length));
    }

    bool CheckValidColumnIndex(int index)
    {
        return ((index >= 0) && (index < _sandbox[0].Length));
    }

    bool IsCapitalLetter(char value)
    {
        return ((value >= 'A') && (value <= 'Z'));
    }

    public void AddItemToSandbox(char item, int index)
    {
        bool canMove = true;
        int rowI = -1;  //To check row 0 as well
        int colI = index;

        while (canMove)
        {
            if(SlotFree(rowI + 1, colI))
            {
                rowI++;
            }
            else if (IsCapitalLetter(item))
            {
                //Check left then right

                if (SlotFree(rowI + 1, colI - 1))
                {
                    rowI++;
                    colI--;
                }
                else if (SlotFree(rowI + 1, colI + 1))
                {
                    rowI++;
                    colI++;
                }
                else
                {
                    canMove = false;
                }
            }
            else
            {
                //Check right then left

                if (SlotFree(rowI + 1, colI + 1))
                {
                    rowI++;
                    colI++;
                }
                else if (SlotFree(rowI + 1, colI - 1))
                {
                    rowI++;
                    colI--;
                }
                else
                {
                    canMove = false;
                }
            }
        }

        _sandbox[rowI][colI] = item;
    }
}