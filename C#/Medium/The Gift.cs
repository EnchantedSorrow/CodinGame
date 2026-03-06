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
        int participantCount = int.Parse(Console.ReadLine());
        int giftPrice = int.Parse(Console.ReadLine());

        int[] participantBudgets = new int[participantCount];
        List<int> allocation = new List<int>();

        for (int i = 0; i < participantCount; i++)
        {
            //Get each participant's budgets
            participantBudgets[i] = int.Parse(Console.ReadLine());
        }

        //Sort the available budgets
        Array.Sort(participantBudgets);

        if (participantBudgets.Sum() < giftPrice)
        {
            Console.WriteLine("IMPOSSIBLE");
            return;
        }
        
        
        //Start the allocation
        int remainingSpend = giftPrice;
        
        for (int i = 0; i < participantCount; i++)
        {
            //Added these allocation to make it easier to read
            int budget = participantBudgets[i];
            int splitAllocation = SplitBudgetAllocation(remainingSpend, participantCount - i);

            if (budget < splitAllocation)
            {
                //Person does not have enough
                //Give entire budget, add leftover to remainder
           	    allocation.Add(budget);
                remainingSpend -= budget;
            }
            else
            {
                //Person has enough to cover their allocation
                allocation.Add(splitAllocation);
                remainingSpend -= splitAllocation;
            }

        }

        //Print budget allocations
        allocation.ForEach(p=> Console.WriteLine(p)); 
        
    }

    public static int SplitBudgetAllocation(int totalAmount, int splitCount)
    {
        return totalAmount / splitCount;
    }

}