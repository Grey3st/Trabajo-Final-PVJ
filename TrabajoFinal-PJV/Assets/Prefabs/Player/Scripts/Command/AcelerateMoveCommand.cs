using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelerateMoveCommand : ICommand
{
    private readonly PlayerMovement playerMovement;
    private readonly float input;

    public AcelerateMoveCommand(PlayerMovement playerMovement, float input)
    {
        this.playerMovement = playerMovement;
        this.input = input;
    }

    public void Execute()
    {
        playerMovement.SetMovementStrategy(new AcelerateMovement());
        playerMovement.MovePlayer(input);
    }
}
