using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


class Solution
{
    static void Main(string[] args)
    {
        CardDeck deck = new CardDeck();
        

        string[] inputs = Console.ReadLine().Split(' ');
        int R = int.Parse(inputs[0]);
        int S = int.Parse(inputs[1]);
        deck.RemoveCard(Enumerable.Range(0, R).Select(_ => Console.ReadLine()));

        CardDeck soughtCards = new CardDeck(Enumerable.Range(0, S).Select(_ => Console.ReadLine()));


        Console.WriteLine("{0}%", GetPercentageChance(deck.CompareDecks(soughtCards), deck.DeckSize));
    }

    static int GetPercentageChance(int possibleCards, int deckSize)
    {
        double chance = (double)possibleCards / (double)deckSize;

        return (int)(chance * 100.0);
    }
}

struct Card
{
    public string Value;
    public bool Counted;

    public Card(string cardValue)
    {
        Value = cardValue;
        Counted = false;
    }
}

class CardDeck
{
    string _ranks = "23456789TJQKA";
    string _suits = "HDCS";
    
    HashSet<(char, char)> _deck;
    
    public HashSet<(char, char)> Deck { get { return _deck;} }
    public int DeckSize { get { return _deck.Count; }}

    public CardDeck(string cards = "")
    {
        _deck = new HashSet<(char, char)>();
        BuildDeck(cards);
    }

    public CardDeck(IEnumerable<string> cardValues)
    {
        _deck = new HashSet<(char, char)>();
        foreach(string card in cardValues)
        {
            BuildDeck(card);
        }
    }

    void BuildDeck(string cards = "")
    {
        foreach(char rank in cards.Any(c => _ranks.Contains(c)) ? cards.Intersect(_ranks) : _ranks)
        {
            foreach (char suit in cards.Any(c => _suits.Contains(c)) ? cards.Intersect(_suits) : _suits)
            {
                _deck.Add((rank, suit));
            }
        }
    }


    public int CompareDecks(CardDeck compareDeck)
    {
        int matchedCards = 0;
        foreach ((char, char) card in compareDeck.Deck)
        {
            if (CheckCardMatches(card)) matchedCards++;
        }

        return matchedCards;
    }

    public bool CheckCardMatches((char, char) cardValue)
    {
        return _deck.Contains(cardValue);
    }

    public void RemoveCard(string cardValue)
    {
        foreach(char rank in cardValue.Any(c => _ranks.Contains(c)) ? cardValue.Intersect(_ranks) : _ranks)
        {
            foreach (char suit in cardValue.Any(c => _suits.Contains(c)) ? cardValue.Intersect(_suits) : _suits)
            {
                _deck.Remove((rank, suit));
            }
        }
    }

    public void RemoveCard(IEnumerable<string> cardValues)
    {
        foreach (string card in cardValues)
        {
            RemoveCard(card);
        }
    }

    //Debug method
    public void PrintCurrentDeck()
    {
        foreach ((char, char) card in _deck)
        {
            Console.Error.WriteLine("{0}{1}", card.Item1, card.Item2);
        }
    }
}