using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private GameObject spawnBala;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private List<ICommand> commands;

    private int enemigosEliminados = 0;
    private int golpesRecibidos = 0;
    private int enemigosAEliminar = 20; // N�mero de enemigos para ganar
    private int maxGolpes = 5;  // N�mero m�ximo de golpes para perder
    private bool juegoActivo = true; // Verifica si el juego sigue activo

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        commands = new List<ICommand>();
        spawnBala = Resources.Load<GameObject>("BalaPlayer");
    }

    void Update()
    {
        if (!juegoActivo) return;
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
        if (Input.GetMouseButtonDown(0))  // 0 es el bot�n izquierdo del rat�n
        {
            // Instanciar la bala en la posici�n y rotaci�n del spawner (es decir, la posici�n del jugador)
            Instantiate(spawnBala, transform.position, transform.rotation);
        }
    }
    // M�todo que el enemigo llama cuando es golpeado por una bala
    public void GolpeadoPorBala()
    {
        if (!juegoActivo) return;

        golpesRecibidos++;
        ComprobarDerrota();
    }
    // M�todo que es llamado cuando un enemigo es eliminado
    public void EnemigoEliminado()
    {
        if (!juegoActivo) return;

        enemigosEliminados++;
        ComprobarVictoria();
    }

    private void ComprobarVictoria()
    {
        if (enemigosEliminados >= enemigosAEliminar)
        {
            juegoActivo = false;  // Detener el juego
            SceneManager.LoadScene("Victoria");
        }
    }
    private void ComprobarDerrota()
    {
        if (golpesRecibidos >= maxGolpes)
        {
            juegoActivo = false;  // Detener el juego
            SceneManager.LoadScene("Derrota");
        }
    }
}
