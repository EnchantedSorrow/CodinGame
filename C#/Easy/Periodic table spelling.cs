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
    static List<string> _prunedElements;
    static List<string> _wordOutputs;

    static void Main(string[] args)
    {
        PeriodicTable refTable;
        refTable = new PeriodicTable();
        _prunedElements = new List<string>();
        _wordOutputs = new List<string>();

        string word = Console.ReadLine();
        //Console.Error.WriteLine(word);  

        //Remove unneeded elements
        _prunedElements = RemoveUnneededListItems(refTable.ElementList, word);

        //Console.Error.WriteLine("List count: {0}", editedList.Count);
        //_prunedElements.ForEach(i => Console.Error.WriteLine(i));

        BuildWord("", word);

        if (_wordOutputs.Count == 0)
        {
            Console.WriteLine("none");
        }
        else
        {
            _wordOutputs.ForEach(word => Console.WriteLine(word));
        }
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

    static public void BuildWord(string word, string remainingLetters)
    {
        if (remainingLetters.Length == 0) _wordOutputs.Add(word);
        else
        {
            string substring = remainingLetters.Substring(0, 1).ToUpper(); //1st letter of element
            if (_prunedElements.Contains(substring))
            {
                BuildWord(word + substring, remainingLetters.Substring(1));
            }
            //Check for 2 letter elements
            if (remainingLetters.Length >= 2)
            {
                substring += remainingLetters[1];
                if (_prunedElements.Contains(substring))
                {
                    BuildWord(word + substring, remainingLetters.Substring(2));
                }
            }
        }
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