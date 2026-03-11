using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Solution
{
    static void Main(string[] args)
    {
        GhostLegs ghostLegMap;

        string[] inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);

        ghostLegMap = new GhostLegs(H - 2, ((W - 1) / 3) + 1);

        ghostLegMap.FillHeadValues(Console.ReadLine());
        ghostLegMap.BuildGhostLegs(Enumerable.Range(0, H-2).Select(_ => Console.ReadLine()));
        ghostLegMap.FillTailValues(Console.ReadLine());

        ghostLegMap.TraverseMap();
    }
}

public class GhostLegs
{
    int[][] _ghostLegs;
    char[] _head;
    char[] _tail;

    public GhostLegs(int rows, int columns)
    {
        _ghostLegs = new int[rows][];
        for (int rowI = 0; rowI < _ghostLegs.Length; rowI++)
        {
            _ghostLegs[rowI] = new int[columns];

            for (int colI = 0; colI < _ghostLegs[rowI].Length; colI++)
            {
                _ghostLegs[rowI][colI] = 0;
            }
        }

        _head = new char[columns];
        _tail = new char[columns];
    }

    public void BuildGhostLegs(IEnumerable<string> ghostLegMap)
    {
        int rowI = 0;

        foreach(string row in ghostLegMap)
        {
            for (int colI = 0; colI < _ghostLegs[rowI].Length - 1; colI++)
            {
                if (row[(colI * 3) + 1] == '-')
                {
                    _ghostLegs[rowI][colI] = 1;
                    _ghostLegs[rowI][colI + 1] = -1;
                }
            }
            rowI++;
        }
    }
    
    public void FillHeadValues(string input)
    {
        int colI = 0;
        foreach(char c in input)
        {
            if (c != ' ')
            {
                _head[colI] = c;
                colI++;
            }
        }
    }

    public void FillTailValues(string input)
    {
        int colI = 0;
        foreach(char c in input)
        {
            if (c != ' ')
            {
                _tail[colI] = c;
                colI++;
            }
        }
    }

    public void TraverseMap()
    {
        for (int headI = 0; headI < _head.Length; headI++)
        {
            int currentI = headI;

            for (int rowI = 0; rowI < _ghostLegs.Length; rowI++)
            {
                currentI += _ghostLegs[rowI][currentI];
            }

            Console.WriteLine("{0}{1}", _head[headI], _tail[currentI]);
        }
    }

    
    //Debug method
    public void PrintMap()
    {
        foreach(char c in _head)
        {
            Console.Error.Write("{0}  ", c);
        }
        Console.Error.Write(Environment.NewLine);

        foreach(int[] row in _ghostLegs)
        {
            foreach (int i in row)
            {
                Console.Error.Write("{0}  ", i);
            }
            Console.Error.Write(Environment.NewLine);
        }

        foreach (char c in _tail)
        {
            Console.Error.Write("{0}  ", c);
        }
        Console.Error.Write(Environment.NewLine);
    }
}