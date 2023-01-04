using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTile : Tile
{
    public override void OnStep(Player player)
    {
        player.Health = 0;
    }
}
