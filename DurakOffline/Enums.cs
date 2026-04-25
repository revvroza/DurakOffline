using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    /// <summary>
    /// Перечисление мастей игральных карт
    /// </summary>
    public enum Suit
    {
        Hearts,     ///< Черви (красная масть)
        Diamonds,   ///< Буби (красная масть)
        Clubs,      ///< Крести (чёрная масть)
        Spades      ///< Пики (чёрная масть)
    }

    /// <summary>
    /// Перечисление достоинств игральных карт
    /// </summary>
    /// <remarks>
    /// Числовые значения соответствуют старшинству карт:
    /// 6 - самая младшая, 14 (Туз) - самая старшая
    /// </remarks>

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

    /// <summary>
    /// Перечисление состояний игры
    /// </summary>
    public enum GameState
    {
        Waiting,
        Attacking,
        Defending,
        GameOver
    }
}
