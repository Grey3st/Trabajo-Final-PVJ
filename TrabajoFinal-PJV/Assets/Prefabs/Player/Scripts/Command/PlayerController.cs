using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spawnBala;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private List<ICommand> commands;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        commands = new List<ICommand>();
    }

    void Update()
    {
        DispararBala();
        commands.Clear();
        float horizontalInput = Input.GetAxis("Horizontal");
        commands.Add(new MoveCommand(playerMovement, horizontalInput));
        

        if (Input.GetKey(KeyCode.Space))
        {
            commands.Add(new AcelerateMoveCommand(playerMovement, horizontalInput));
        }
        if (Input.GetMouseButtonDown(1))
        {
            commands.Add(new JumpCommand(playerJump));
        }
        foreach (var command in commands)
        {
            command.Execute();
        }
    }

    private void DispararBala()
    {
        if (Input.GetMouseButtonDown(0))  // 0 es el botón izquierdo del ratón
        {
            // Instanciar la bala en la posición y rotación del spawner (es decir, la posición del jugador)
            Instantiate(spawnBala, transform.position, transform.rotation);
        }
    }
}
