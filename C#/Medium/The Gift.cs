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
    static void Main(string[] args)
    {
        int participantCount = int.Parse(Console.ReadLine());
        int giftPrice = int.Parse(Console.ReadLine());

        int[] participantBudgets = new int[participantCount];
        List<int> outputBudgets = new List<int>();

        

        Console.Error.WriteLine("Participant Count: " + participantCount);
        Console.Error.WriteLine("Gift price: " + giftPrice);

        for (int i = 0; i < participantCount; i++)
        {
            //Get each participant's budgets
            participantBudgets.Add(int.Parse(Console.ReadLine()));
        }

        foreach (int budget in participantBudgets)
        {
            Console.Error.WriteLine(budget);
        }

        int[] splitAmount = SplitBudgetAllocation(giftPrice, participantCount);

        int remainder = 0;

        foreach (int participant in participantBudgets)
        {
            
        }
/*
        if (remainder > 0)
        {
            Console.WriteLine("IMPOSSIBLE");
        }
        else
        {
            foreach(int amount in outputBudgets)
            {
                Console.WriteLine(amount);
            }
        }
*/
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        
    }
}


public static int[] SplitBudgetAllocation(int totalAmount, int splitCount)
{
    int[] splitAmount = new int[splitCount];

    int remainder = totalAmount;

    for (int i = 0; i < splitCount; i++)
    {
        int split = remainder / (splitCount - i);
        Console.Error.WriteLine(split);

        splitAmount[i] = split;
        remainder -= split;
    }

    return splitAmount;
}