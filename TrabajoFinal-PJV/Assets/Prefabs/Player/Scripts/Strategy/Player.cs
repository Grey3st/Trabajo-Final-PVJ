using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float velocity;
    private float acceleration;
    public Player(float velocity, float acceleration)
    {
        this.velocity = velocity;
        this.acceleration = acceleration;  
    }
    public float Velocity { get => velocity; set => velocity = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
}
