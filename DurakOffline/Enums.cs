using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    public enum Suit
    {
        Hearts,  //Черви
        Diamonds,   //Буби
        Clubs,  //Крести
        Spades  //Пики
    }

    public enum Rank
    {
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    public enum GameState
    {
        Waiting,
        Attacking,
        Defending,
        GameOver
    }
}
