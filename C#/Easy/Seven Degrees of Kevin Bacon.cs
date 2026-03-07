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
        int degreeCount = 0;
        string targetName = "Kevin Bacon";
        string actorName = Console.ReadLine();
        if (actorName == targetName)
        {
            Console.WriteLine(degreeCount);
            return;    
        }

        int n = int.Parse(Console.ReadLine());

        Dictionary<string, List<string>> movies = new Dictionary<string, List<string>>();

        for (int i = 0; i < n; i++)
        {
            string movieCast = Console.ReadLine();
            string[] category = movieCast.Split(':');
            string[] actorList = category[1].Split(',');
            movies[category[0]] = new List<string>();
            foreach (string actor in actorList)
            {
                movies[category[0]].Add(actor.Trim());
            }

            
        }

        DebugDictionary(ref movies);

        //Create Head Node
        List<string> addedActors = new List<string>();
        ActorNode head = new ActorNode(actorName);
        addedActors.Add(actorName);
        
        //Create head children - find in dictionary
        ActorNode currentNode = head;
        degreeCount++;

        foreach (string key in movies.Keys)
        {
            if (movies[key].Contains(actorName))
            {
                foreach (string actor in movies[key])
                {
                    Console.Error.WriteLine(actor);
                    //if (actor == targetName) break;
                    if (addedActors.Contains(actor) == false)
                    {
                        //Create new node
                        ActorNode temp = new ActorNode(actor);
                        currentNode.Children.Add(temp);
                        addedActors.Add(actor);
                        
                    }
                }
            }
        }

        //addedActors.ForEach(p=> Console.Error.WriteLine(p));

        //

        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine("N degrees to Kevin Bacon");
    }

    static public void DebugDictionary(ref Dictionary<string, List<string>> dict)
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

public struct ActorNode
{
    public string Name;
    public List<ActorNode> Children;

    public ActorNode(string name)
    {
        Name = name;
        Children = new List<ActorNode>();
    }
}