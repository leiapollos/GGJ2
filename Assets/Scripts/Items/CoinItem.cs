using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    public int coinScore;
    public override void Grab()
    {
        Score.Instance.AddScore(coinScore);
    }
}
