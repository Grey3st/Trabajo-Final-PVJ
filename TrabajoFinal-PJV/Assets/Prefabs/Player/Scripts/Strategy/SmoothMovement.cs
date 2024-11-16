using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : IMovementStrategy
{
    public void Move(Transform transform, Player player, float direction)
    {
        float movement = direction * player.Velocity * Time.deltaTime;
        transform.Translate(movement, 0, 0);
    }
}