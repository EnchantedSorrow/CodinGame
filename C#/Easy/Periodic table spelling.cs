using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    //static PeriodicTable _refTable;

    static void Main(string[] args)
    {
        PeriodicTable _refTable;
        _refTable = new PeriodicTable();

        string word = Console.ReadLine();
        Console.Error.WriteLine(word);  

        //Remove unneeded elements
        List<string> editedList = RemoveUnneededListItems(_refTable.ElementList, word);

        /*Console.Error.WriteLine("List count: {0}", editedList.Count);
        editedList.ForEach(i => Console.Error.WriteLine(i));*/

        BuildWord(editedList, word);
    }

    static List<string> RemoveUnneededListItems(List<string> list, string word)
    {
        List<string> editedList = new List<string>();

        foreach (string item in list)
        {
            if (word.ToLower().Contains(item.ToLower())) editedList.Add(item);
        }

        return editedList;
    }

    static public void BuildWord(List<string> list, string word)
    {
        Stack<LetterNode> nodeStack = new Stack<LetterNode>();
        LetterNode head = new LetterNode("", -1);
        nodeStack.Push(head);

        while (nodeStack.Count > 0)
        {
            LetterNode currentNode = nodeStack.Pop();

            int currentIndex = (currentNode.Index >= 0) ? (currentNode.Index + currentNode.Value.Length) : 0;

            foreach (string letter in list)
            {
                if (currentIndex + letter.Length < word.Length)
                {
                    if (letter.ToLower() == word.Substring(currentIndex, letter.Length).ToLower())
                    {
                        LetterNode newNode = new LetterNode(letter, currentIndex);
                        currentNode.Children.Add(newNode);
                    }
                }
            }
            
            foreach (LetterNode node in currentNode.Children)
            {
                Console.Error.WriteLine("{0} {1}", node.Value, node.Index);
                nodeStack.Push(node);
            }
        }
    }
}

public struct LetterNode
{
    public string Value;
    public int Index;
    public List<LetterNode> Children;

    public LetterNode(string value, int index)
    {
        Value = value;
        Index = index;
        Children = new List<LetterNode>();
    }
}

class PeriodicTable
{
    List<string> _elements;
    public List<string> ElementList { get { return _elements; } }

    public PeriodicTable()
    {
        string elementString = "H He Li Be B C N O F Ne Na Mg Al Si P S Cl Ar K Ca Sc Ti V Cr Mn Fe Co Ni Cu Zn Ga Ge As Se Br Kr Rb Sr Y Zr Nb Mo Tc Ru Rh Pd Ag Cd In Sn Sb Te I Xe Cs Ba La Ce Pr Nd Pm Sm Eu Gd Tb Dy Ho Er Tm Yb Lu Hf Ta W Re Os Ir Pt Au Hg Tl Pb Bi Po At Rn Fr Ra Ac Th Pa U Np Pu Am Cm Bk Cf Es Fm Md No Lr Rf Db Sg Bh Hs Mt Ds Rg Cn Nh Fl Mc Lv Ts Og";
        string[] splitElements = elementString.Split(' ');
        _elements = new List<string>();
        
        foreach (string element in splitElements)
        {
            _elements.Add(element);
        }
    }
}