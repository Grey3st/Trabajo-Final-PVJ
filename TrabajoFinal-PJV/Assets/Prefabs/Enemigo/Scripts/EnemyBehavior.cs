using System.Collections;
using UnityEngine;
public class EnemyBehavior : MonoBehaviour
{
    private Transform player;
    private GameObject bala;
    private PlayerController playerController;
    private float velocidadBala = 5f;
    private int colisionesRestantes = 10; // Contador de colisiones restantes

    //Patrullaje
    private Vector3[] puntosDePatrullaje;
    private int indicePatrullaje = 0; 
    private float velocidadDePatrullaje = 2f; 
    private float umbralDePosicion = 0.5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }

        // Carga el prefab de bala desde la carpeta Resources
        bala = Resources.Load<GameObject>("BalaEnemigoElite");

        puntosDePatrullaje = new Vector3[]
        {
            new Vector3(0, 0.5f, 0),
            new Vector3(5, 0.5f, 0),
            new Vector3(5, 0.5f, 5),
            new Vector3(0, 0.5f, 5),
            new Vector3(-5, 0.5f, 0),
            new Vector3(-5, 0.5f, -5),
            new Vector3(0, 0.5f, -5),
        };

        InvokeRepeating("AttackPlayer", 2.0f, 1.0f);
        StartCoroutine(Patrullaje());
    }
    private IEnumerator Patrullaje()
    {
        while (true)
        {
            // Mueve al enemigo hacia el punto de patrullaje actual
            Vector3 puntoDePatrullaje = puntosDePatrullaje[indicePatrullaje];

            while (Vector3.Distance(transform.position, puntoDePatrullaje) > umbralDePosicion)
            {
                Vector3 direccion = (puntoDePatrullaje - transform.position).normalized;
                transform.position += direccion * velocidadDePatrullaje * Time.deltaTime;
                yield return null;
            }
            // Cambia al siguiente punto de patrullaje, volviendo al inicio si es necesario
            indicePatrullaje = (indicePatrullaje + 1) % puntosDePatrullaje.Length;
            yield return new WaitForSeconds(1f);
        }
    }
    private void AttackPlayer()
    {
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Eleva la posición de instanciación de la bala
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.5f, 0); // Ajusta el valor de Y según sea necesario

            // Instancia la bala en la posición del enemigo ajustada y establece su dirección
            GameObject bullet = Instantiate(bala, spawnPosition, Quaternion.Euler(0, 180, 0)); // Rotación de 180 grados

            // Asigna la dirección de la bala
            BalaMovement bulletMovement = bullet.GetComponent<BalaMovement>();
            bulletMovement.Initialize(-direction, velocidadBala, true); // Indica que es una bala del enemigo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si colisiona con la bala del jugador
        if (other.CompareTag("BalaJugador"))
        {
            colisionesRestantes--;

            // Si el enemigo ha recibido 5 colisiones, se destruye
            if (colisionesRestantes <= 0)
            {
                // Solo llamar a EnemigoEliminado si la referencia está inicializada
                if (playerController != null)
                {
                    playerController.EnemigoEliminado();
                }
                Destroy(gameObject); // Destruye el enemigo
            }

            // (Opcional) Puedes agregar efectos visuales o sonoros aquí al recibir daño
            Debug.Log("Enemigo golpeado. Colisiones restantes: " + colisionesRestantes);
        }
    }
}
