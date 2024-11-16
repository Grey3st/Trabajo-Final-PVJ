using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spawnBala;
    private PlayerMovement playerMovement;
    private List<ICommand> commands;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
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

        foreach (var command in commands)
        {
            command.Execute();
        }
    }

    private void DispararBala()
    {
        if (Input.GetMouseButtonDown(0))  // 0 es el bot�n izquierdo del rat�n
        {
            // Instanciar la bala en la posici�n y rotaci�n del spawner (es decir, la posici�n del jugador)
            Instantiate(spawnBala, transform.position, transform.rotation);
        }
    }
}
