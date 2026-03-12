using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Data;

class Solution
{
    static void Main(string[] args)
    {
        LetterMatrix letterMatrix = new LetterMatrix(int.Parse(Console.ReadLine()));
        letterMatrix.BuildMatrix(Enumerable.Range(0, letterMatrix.Length).Select(_ => Console.ReadLine()));

        List<LetterCoordNode> outputCoordinates = letterMatrix.TraverseMatrix();

        char[][]output = FormatOutputMatrix(outputCoordinates, letterMatrix.Length);

        foreach (char[] row in output)
        {
            foreach (char c in row)
            {
                Console.Write(c);
            }
            Console.Write(Environment.NewLine);
        }

    }

    static char[][] FormatOutputMatrix(List<LetterCoordNode> listCoords, int length)
    {
        char[][] outputMatrix = new char[length][];
        for (int rowI = 0; rowI < length; rowI++)
        {
            outputMatrix[rowI] = new char[length];
            for (int colI = 0; colI < length; colI++)
            {
                outputMatrix[rowI][colI] = '-';
            }
        }

        foreach(LetterCoordNode node in listCoords)
        {
            outputMatrix[node.Row][node.Col] = node.Value;
        }

        return outputMatrix;
    }
}

public struct LetterCoordNode
{
    public char Value;
    public int Row;
    public int Col;

    public LetterCoordNode(char value, int row, int col)
    {
        Value = value;
        Row = row;
        Col = col;
    }
}

class LetterMatrix
{
    char[][] _letterMatrix;

    char[] _letters;

    public int Length { get { return _letterMatrix.Length; }}

    public LetterMatrix(int index)
    {
        _letterMatrix = new char[index][];

        for (int rowI = 0; rowI < index; rowI++)
        {
            _letterMatrix[rowI] = new char[index];
        }

        int letterI = 0;
        _letters = new char[26];
        for (int letterASCII = 97; letterASCII <= 122; letterASCII++)
        {
            _letters[letterI] += Convert.ToChar(letterASCII);
            letterI++;
        }
    }

    public void BuildMatrix(IEnumerable<string> content)
    {
        int rowI = 0;
        foreach(string line in content)
        {
            int colI = 0;

            foreach (char c in line)
            {
                if (colI < this.Length)
                {
                    _letterMatrix[rowI][colI] = c;
                    colI++;
                }
            }

            rowI++;
        }
    }

    public List<LetterCoordNode> TraverseMatrix()
    {
        bool canMove = false;
        int letterI = 0;

        int rowI = 0;
        
        while (rowI < this.Length)
        {
            int colI = 0;

            while (colI < this.Length)
            {
                if (_letterMatrix[rowI][colI] == _letters[0])
                {
                    List<LetterCoordNode> outputList = BuildLetterList(rowI, colI);
                    if (outputList.Count == _letters.Length) return outputList;
                }

                colI++;
            }

            rowI++;
        }

        return new List<LetterCoordNode>();
    }

    public List<LetterCoordNode> BuildLetterList(int row, int col)
    {
        int letterI = 0;
        int rowI = row;
        int colI = col;

        bool canMove = true;

        List<LetterCoordNode> outputList = new List<LetterCoordNode>();
        outputList.Add(new LetterCoordNode(_letters[letterI], rowI, colI));
        letterI++;

        while (canMove)
        {
            canMove = false;

            //Search clockwise from top
            if (ValidSpot(rowI - 1, colI))
            {
                if (_letterMatrix[rowI - 1][colI] == _letters[letterI])
                {
                    rowI--; 
                    outputList.Add(new LetterCoordNode(_letters[letterI], rowI, colI));
                    letterI++;
                    canMove = true;
                }
            }
            if (ValidSpot(rowI, colI + 1) && (canMove == false))
            {
                if (_letterMatrix[rowI][colI + 1] == _letters[letterI])
                {
                    colI++;
                    outputList.Add(new LetterCoordNode(_letters[letterI], rowI, colI));
                    letterI++;
                    canMove = true;
                }
            }
            if (ValidSpot(rowI + 1, colI) && !canMove)
            {
                if (_letterMatrix[rowI + 1][colI] == _letters[letterI])
                {
                    rowI++;
                    outputList.Add(new LetterCoordNode(_letters[letterI], rowI, colI));
                    letterI++;
                    canMove = true;
                }
            }
            if (ValidSpot(rowI, colI - 1) && !canMove)
            {
                if (_letterMatrix[rowI][colI - 1] == _letters[letterI])
                {
                    colI--;
                    outputList.Add(new LetterCoordNode(_letters[letterI], rowI, colI));
                    letterI++;
                    canMove = true;
                }
            }

            if (letterI > (_letters.Length - 1)) return outputList;
        }

        return outputList;
    } 
    
    public bool ValidSpot(int rowI, int colI)
    {
        if (!((rowI >= 0) && (rowI < this.Length))) return false;
        if (!((colI >= 0) && (colI < this.Length))) return false;
        return true;
    }


    //Debug
    public void PrintMatrix()
    {
        foreach (char[] row in _letterMatrix)
        {
            foreach(char c in row)
            {
                Console.Error.Write(c);
            }
            Console.Error.Write(Environment.NewLine);
        }

        Console.Error.WriteLine(_letters);
    }
}