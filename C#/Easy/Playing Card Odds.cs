using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication.ExtendedProtection;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        CardDeck deck = new CardDeck();
        

        string[] inputs = Console.ReadLine().Split(' ');
        int R = int.Parse(inputs[0]);
        int S = int.Parse(inputs[1]);
        for (int i = 0; i < R; i++)
        {
            ProcessCardRemoval(deck, Console.ReadLine());
        }

        int chanceOfDraw = 0;
        for (int i = 0; i < S; i++)
        {
            chanceOfDraw += ProcessSoughtCards(deck, Console.ReadLine());
        }

        Console.WriteLine("{0}%", GetPercentageChange(chanceOfDraw, deck.DeckSize));
    }

    static int GetPercentageChange(int possibleCards, int deckSize)
    {
        double chance = (double)possibleCards / (double)deckSize;

        return (int)(chance * 100.0);
    }

    static void ProcessCardRemoval(CardDeck cardDeck, string cardCode)
    {
        List<string>  suitsDeck = new List<string>();   //Hold suits to remove
        List<string> numbersDeck = new List<string>();  //Hold number value to remove

        //Split cards by suit and numbers
        SortCards(ref suitsDeck, ref numbersDeck, cardCode.ToCharArray());

        //Merge into individual cards and remove cards from cardDeck
        foreach (string numberCard in numbersDeck)
        {
            foreach (string suit in suitsDeck)
            {
                cardDeck.RemoveCard(numberCard + suit);
            }
        }
    }

    static int ProcessSoughtCards(CardDeck cardDeck, string cardCode)
    {
        List<string>  suitsDeck = new List<string>();   //Hold suits to remove
        List<string> numbersDeck = new List<string>();  //Hold number value to remove

        //Split cards by suit and numbers
        SortCards(ref suitsDeck, ref numbersDeck, cardCode.ToCharArray());

        int cardChances = 0;
        //Get chance of draw for this card code
        foreach (string numberCard in numbersDeck)
        {
            foreach (string suit in suitsDeck)
            {
                if (cardDeck.CheckForCard(numberCard + suit))
                {
                    cardChances++;
                }
            }
        }

        return cardChances;
    }

    static void SortCards(ref List<string> suitsDeck, ref List<string> numbersDeck, char[] charCodes)
    {
        //Split cards by suit and numbers
        foreach (char code in charCodes)
        {
            if (CheckForSuit(code))
            {
                suitsDeck.Add(code.ToString());
            }
            else
            { 
                numbersDeck.Add(code.ToString());
            }
        }

        if (suitsDeck.Count == 0)
        {
            suitsDeck.Add("H");
            suitsDeck.Add("D");
            suitsDeck.Add("C");
            suitsDeck.Add("S");
        }

        if (numbersDeck.Count == 0)
        {
            for (int i = 2; i < 10; i++)
            {
                numbersDeck.Add(i.ToString());
            }

            numbersDeck.Add("T");
            numbersDeck.Add("J");
            numbersDeck.Add("Q");
            numbersDeck.Add("K");
            numbersDeck.Add("A");
        }
    }

    static bool CheckForSuit(char suitCode)
    {
        switch (suitCode)
        {
            case 'H':
                return true;
            case 'D':
                return true;
            case 'C':
                return true;
            case 'S':
                return true;
            default:
                return false;
        }
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
    List<Card>[] _deck;
    public int DeckSize { get { return GetDeckSize(); }}

    public CardDeck()
    {
        _deck = new List<Card>[4];

        for (int i = 0; i < _deck.Length; i++)
        {
            string suit = GetSuitType(i);
            _deck[i] = new List<Card>();

            for (int num = 2; num <= 9; num++)
            {
                _deck[i].Add(new Card(num.ToString() + suit));
            }

            _deck[i].Add(new Card("T" + suit));
            _deck[i].Add(new Card("J" + suit));
            _deck[i].Add(new Card("Q" + suit));
            _deck[i].Add(new Card("K" + suit));
            _deck[i].Add(new Card("A" + suit));
        }
    }

    string GetSuitType(int index)
    {
        switch (index)
        {
            case 0:
                return "D";

            case 1:
                return "H";

            case 2:
                return "C";

            case 3:
                return "S";

            default:
                return "";
        }
    }

    int GetDeckSize()
    {
        int total = 0;

        foreach(List<Card> suit in _deck)
        {
            total += suit.Count;
        }

        return total;
    }

    public bool CheckForCard(string cardValue)
    {
        for (int suitI = 0; suitI < _deck.Length; suitI++)
        {
            for (int cardI = 0; cardI < _deck[suitI].Count; cardI++)
            {
                Card card = _deck[suitI][cardI];
                if ((card.Value == cardValue) && (card.Counted == false))
                {
                    card.Counted = true;
                    _deck[suitI][cardI] = card;
                    return true;
                }             
            }
        }

        return false;
    }

    public void RemoveCard(string cardValue)
    {
        for (int suitI = 0; suitI < _deck.Length; suitI++)
        {
            for (int cardI = 0; cardI < _deck[suitI].Count; cardI++)
            {
                Card card = _deck[suitI][cardI];
                if (card.Value == cardValue)
                {
                    _deck[suitI].Remove(card);
                }
            }
        }
    }

    //Debug method
    public void PrintCurrentDeck()
    {
        foreach (List<Card> suit in _deck)
        {
            suit.ForEach(card=> Console.Error.WriteLine(card.Value));
        }
    }
}