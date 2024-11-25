using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    private readonly PlayerJump playerJump;

    public JumpCommand(PlayerJump playerJump)
    {
        this.playerJump = playerJump;
    }

    public void Execute()
    {
        playerJump.Jump();
    }
}
