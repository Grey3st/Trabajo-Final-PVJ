using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy
{
    // Start is called before the first frame update
    public void Move(Transform transform, Player player, float direction);

}
