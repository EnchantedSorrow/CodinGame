using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


class Solution
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);

        string[] mayanNumbers = new string[20];

        for (int i = 0; i < H; i++)
        {
            string numeral = Console.ReadLine();

            for (int numI = 0; numI < mayanNumbers.Length; numI++)
            {
                mayanNumbers[numI] += numeral.Substring(numI * L, L);
            }
        }
        
        //1st number
        string numberSeq = "";
        int S1 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S1; i++)
        {
            string numLine = Console.ReadLine();
            numberSeq += numLine;
        }

        int number1 = MayaToArabic(mayanNumbers, numberSeq, S1, L, H);
        
        //2nd number
        numberSeq = "";
        int S2 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S2; i++)
        {
            string numLine = Console.ReadLine();
            numberSeq += numLine;
        }

        int number2 = MayaToArabic(mayanNumbers, numberSeq, S2, L, H);

        //Solve operation
        string operation = Console.ReadLine();

        double result = PerformOperataton(operation, (double)number1, (double)number2);
        Console.Error.WriteLine("Result is " + result);

        int maxPower = GetMaxPower(result);
        List<int> mayanResult = SplitNumberIntoPower20(result, maxPower);

        string[] outputSequence = new string[H];

        foreach (int n in mayanResult)
        {
            string mayaSeq = mayanNumbers[n];
            for (int i = 0; i < H; i++)
            {
                Console.WriteLine(mayaSeq.Substring(i * L, L));
            }
        }
    }

    static void PrintNumbers(string[] number, int length, int height)
    {
        foreach (string digit in number)
        {
            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(digit.Substring(i * length, length));
            }
        }
    }

    static int MayaToArabic(string[] numberList, string numSeq, int totalH, int numL, int numH)
    {
        int maxPower = totalH / numH;
        Stack<int> powerSeq = new Stack<int>();

        for (int i = 0; i < maxPower; i++)
        {
            string digitSeq = numSeq.Substring(i * numH * numL, numH * numL);
            
            for (int listIndex = 0; listIndex < numberList.Length; listIndex++)
            {
                if (digitSeq == numberList[listIndex])
                {
                    powerSeq.Push(listIndex);
                }
            }
        }

        int power = 0;
        int total = 0;
        while (powerSeq.Count > 0)
        {
            total += powerSeq.Pop() * PowerOf20(power);
            power++;
        }
        Console.Error.WriteLine("Number is " + total);

        return total;
    }

    static int PowerOf20(int power)
    {
        if (power == 0) return 1;
        
        return 20 * PowerOf20(power - 1);
    }

    static double PerformOperataton(string op, double num1, double num2)
    {
        switch (op)
        {
            case "+":
                return num1 + num2;

            case "-":
                return num1 - num2;

            case "*":
                return num1 * num2;

            case "/":
                return num1 / num2;

            default:
                return 0;
        }
    }

    static int GetMaxPower(double num)
    {
        int threshold = 0;
        int power = 0;
        while (num >= threshold)
        {
            power++;
            threshold = PowerOf20(power);
        }

        return power - 1;
    }

    static List<int> SplitNumberIntoPower20(double num, int maxPower)
    {
        List<int> numList = new List<int>();

        for (int p = maxPower; p >= 0; p--)
        {
            int count = 0;
            while (num >= PowerOf20(p))
            {
                num -= PowerOf20(p);
                count++;
            }

            numList.Add(count);
        }

        return numList;
    }
}