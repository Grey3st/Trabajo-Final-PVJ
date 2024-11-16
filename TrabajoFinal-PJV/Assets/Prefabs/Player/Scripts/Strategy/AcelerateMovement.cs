using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelerateMovement : IMovementStrategy
{
    public void Move(Transform transform, Player player, float direction)
    {
        float movement = direction * (player.Velocity +  player.Acceleration) * Time.deltaTime;
        transform.Translate(movement * Time.deltaTime, 0, 0);
    }
}
