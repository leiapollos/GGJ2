using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public int numLives;
    public override void Grab()
    {
        player.lives += numLives;
        player.lives = Mathf.Clamp(player.lives, 0, player.MaxLives);
        player.sounds.PlayOnce("Health");
    }
}
