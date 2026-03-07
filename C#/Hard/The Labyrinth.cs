using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Data;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static int _alarm;
    static Maze _mazeInstance;

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        _mazeInstance = new Maze(inputs[0], inputs[1]);

        _alarm = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.

         // game loop
         Game();
    }

    static void Game()
    {
        
        Coordinates _player = new Coordinates(-1, -1);
        Coordinates _startPoint = new Coordinates(-1, -1);
        Coordinates _controlCentre = new Coordinates(-1, -1);
        ETarget _target = ETarget.control;
        List<Coordinates> _visited;

        PathNode _head;

        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            _player.Y = int.Parse(inputs[0]); // row where Rick is located.
            _player.X = int.Parse(inputs[1]); // column where Rick is located.

            for (int i = 0; i < _mazeInstance.Rows; i++)
            {
                string sequence = Console.ReadLine();
                _mazeInstance.ConvertStringToRow(sequence, i); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).

                CheckForStartOrCCCordinate(sequence, i);
            }

            Console.Error.WriteLine("Player is at {0}, {1}", _player.X, _player.Y);
            Console.Error.WriteLine("Start Point is at {0}, {1}", _startPoint.X, _startPoint.Y);
            Console.Error.WriteLine("Control Centre is at {0}, {1}", _controlCentre.X, _controlCentre.Y);

            if (_head == null)
            {
                _head = new PathNode(_player);
                string coordinateVal = _mazeInstance.GetCoordinateValue(_head.Self.X, _head.Self.Y, EDirection.up);
                if (!((coordinateVal == "-1") || (coordinateVal == "#") || (coordinateVal == "?")))
                {
                    _head.Up = new PathNode(new Coordinates(_player.X, _player.Y - 1));
                }

                coordinateVal = _mazeInstance.GetCoordinateValue(_head.Self.X, _head.Self.Y, EDirection.down);
                if (!((coordinateVal == "-1") || (coordinateVal == "#") || (coordinateVal == "?")))
                {
                    _head.Down = new PathNode(new Coordinates(_player.X, _player.Y + 1));
                }

                coordinateVal = _mazeInstance.GetCoordinateValue(_head.Self.X, _head.Self.Y, EDirection.left);
                if (!((coordinateVal == "-1") || (coordinateVal == "#") || coordinateVal == "?"))
                {
                    _head.Left = new PathNode(new Coordinates(_player.X - 1, _player.Y));
                }

                coordinateVal = _mazeInstance.GetCoordinateValue(_head.Self.X, _head.Self.Y, EDirection.right);
                if (!((coordinateVal == "-1") || (coordinateVal == "#") || coordinateVal == "?"))
                {
                    _head.Right = new PathNode(new Coordinates(_player.X + 1, _player.Y));
                }
            }

            while (true)
            {
                
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine("RIGHT"); // Rick's next move (UP DOWN LEFT or RIGHT).

        }
    }

    static void CheckForStartOrCCCordinate(string seq, int index)
    {
        if (seq.Contains('C'))
        {
            _controlCentre.Y = index;
            _controlCentre.X = seq.IndexOf('C');
            return;
        }

        if (seq.Contains('T'))
        {
            _startPoint.Y = index;
            _startPoint.X = seq.IndexOf('T');
            return;
        }
    }

}

public class Maze
{
    int _rows;
    public int Rows { get {return _rows;}}
    int _columns;
    public int Columns { get {return _columns;}}

    public string[][] Board;

    public Maze(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;

        Board = new string[_rows][];
        for (int y = 0; y < _rows; y++)
        {
            Board[y] = new string[_columns];
        }
    }

    public void ConvertStringToRow(string sequence, int index)
    {
        Board[index] = sequence.Split();
    }

    public string GetCoordinateValue(int x, int y, EDirection dir)
    {
        if ((x < 0) || (x >= _columns)) return "-1";

        if ((y < 0) || (y >= _rows)) return "-1";

        switch (dir)
        {
            case EDirection.up:
                if (y - 1 < 0) return "-1";
                return Board[y - 1][x];

            case EDirection.down:
                if (y + 1 >= _rows) return "-1";
                return Board[y + 1][x];

            case EDirection.left:
                if (x - 1 < 0) return "-1";
                return Board[y][x - 1];
            
            case EDirection.right:
                if (x + 1 >= _columns) return "-1";
                return Board[y][x + 1];
        }
    }
}

public enum ETarget
{
    control,
    startPos
}

public enum EDirection
{
    up,
    down,
    left,
    right
}

public struct Coordinates
{
    public int X;
    public int Y;

    public Coordinates(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }
}

public struct PathNode
{
    public Coordinates Self;
    public PathNode Parent;
    public PathNode Left;
    public PathNode Right;
    public PathNode Up;
    public PathNode Down;

    public PathNode(Coordinates coord)
    {
        Self = coord;
    }
}