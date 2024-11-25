using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject spawnBala;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private List<ICommand> commands;

    private int enemigosEliminados = 0;
    private int golpesRecibidos = 0;
    private int enemigosAEliminar = 20; // Número de enemigos para ganar
    private int maxGolpes = 5;  // Número máximo de golpes para perder
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
        if (Input.GetMouseButtonDown(0))  // 0 es el botón izquierdo del ratón
        {
            // Instanciar la bala en la posición y rotación del spawner (es decir, la posición del jugador)
            Instantiate(spawnBala, transform.position, transform.rotation);
        }
    }
    // Método que el enemigo llama cuando es golpeado por una bala
    public void GolpeadoPorBala()
    {
        if (!juegoActivo) return;

        golpesRecibidos++;
        ComprobarDerrota();
    }
    // Método que es llamado cuando un enemigo es eliminado
    public void EnemigoEliminado()
    {
        if (!juegoActivo) return;

        enemigosEliminados++;
        ComprobarVictoria();
    }

    // Verificar si el jugador ha ganado
    private void ComprobarVictoria()
    {
        if (enemigosEliminados >= enemigosAEliminar)
        {
            juegoActivo = false;  // Detener el juego
            Debug.Log("¡Victoria! Has eliminado suficientes enemigos.");
            // Aquí puedes añadir más lógica como mostrar una pantalla de victoria, detener el juego, etc.
            Time.timeScale = 0;  // Detener el juego (opcional)
        }
    }

    // Verificar si el jugador ha perdido
    private void ComprobarDerrota()
    {
        if (golpesRecibidos >= maxGolpes)
        {
            juegoActivo = false;  // Detener el juego
            Debug.Log("¡Derrota! Has recibido demasiados golpes.");
            // Aquí puedes añadir más lógica como mostrar una pantalla de derrota, reiniciar el juego, etc.
            Time.timeScale = 0;  // Detener el juego (opcional)
        }
    }
}
