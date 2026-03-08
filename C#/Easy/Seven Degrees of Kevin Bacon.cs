using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

class Solution
{
    const string targetName = "Kevin Bacon";

    static void Main(string[] args)
    {
        int degreeCount = 0;

        string actorName = Console.ReadLine();

        //Check if Kevin
        if (actorName == targetName)
        {
            Console.WriteLine(degreeCount);
            return;    
        }

        //Setup dictionary
        Dictionary<string, List<string>> movies = BuildActorDictionary();
        List<string> addedCategories = new List<string>();
        Stack<string> categoryStack = new Stack<string>();

        //Create Head Node
        foreach (string key in movies.Keys)
        {
            if (movies[key].Contains(actorName))
            {
                categoryStack.Push(key);
                addedCategories.Add(key);
            }
        }

        //Start Traversal
        bool targetFound = false;
        while ((categoryStack.Count > 0) && !targetFound)
        {
            degreeCount++;
            List<string> categoriesToAddToStack = new List<string>();

            while (categoryStack.Count > 0)
            {
                string currentCategory = categoryStack.Pop();
                if (movies[currentCategory].Contains(targetName))
                {
                    Console.WriteLine(degreeCount);
                    targetFound = true;
                    break;
                }

                //Get new categories and filter out known ones
                List<string> newCategories = GetAssociatedCategories(currentCategory, movies);
                foreach (string category in newCategories)
                {
                    if (!addedCategories.Contains(category))
                    {
                        categoriesToAddToStack.Add(category);
                        addedCategories.Add(category);
                    }
                }        
                
            }
            
            //Add new categories to Stack
            foreach(string category in categoriesToAddToStack)
            {
                categoryStack.Push(category);
            }
        }
    }

    static List<string> GetAssociatedCategories(string category, Dictionary<string, List<string>> movieDict)
    {
        List<string> listToReturn = new List<string>();

        foreach (string actor in movieDict[category])
        {
            foreach (string key in movieDict.Keys)
            {
                if ((movieDict[key].Contains(actor)) && (!listToReturn.Contains(key)))
                {
                    listToReturn.Add(key);
                }
            }
        }

        return listToReturn;
    }


    static Dictionary<string, List<string>> BuildActorDictionary()
    {
        Dictionary<string, List<string>> movies = new Dictionary<string, List<string>>();

        int categoriesCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < categoriesCount; i++)
        {
            string[] movieCast = Console.ReadLine().Split(':');
            string category = movieCast[0];
            string[] actorList = movieCast[1].Split(',');
            movies[category] = new List<string>();
            foreach (string actor in actorList)
            {
                movies[category].Add(actor.Trim());
            }
        }

        return movies;
    }

    //Debug methods
    static void DebugDictionary(ref Dictionary<string, List<string>> dict)
    {
        foreach (string key in dict.Keys)
        {
            Console.Error.WriteLine(key);
            foreach (string actor in dict[key])
            {
                Console.Error.WriteLine(actor);
            }
        }
    }
}