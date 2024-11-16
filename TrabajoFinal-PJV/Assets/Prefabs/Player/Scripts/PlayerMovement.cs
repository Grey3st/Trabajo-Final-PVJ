using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour //Hereda de
{
    private IMovementStrategy movementStrategy;
    private Player player;

    private void Start()
    {
        player = new Player(5f, 5f);

        SetMovementStrategy(new SmoothMovement());
        //SetMovementStrategy(new AcelerateMovement());
    }

    public void SetMovementStrategy(IMovementStrategy movementStrategy)
    {
        this.movementStrategy = movementStrategy;
    }

    public void MovePlayer(float input)
    {
        movementStrategy.Move(transform, player, input);
    }
}
