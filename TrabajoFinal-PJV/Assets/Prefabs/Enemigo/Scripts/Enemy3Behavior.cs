using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserBehavior : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent; // Componente de navegación
    private bool sigue = false;
    private float sigueDuration = 5f; 
    private float saltoF = 5f; // Fuerza del salto
    private bool enTierra = false; // Verifica si el enemigo está en el suelo
    private Rigidbody rigidb; // Rigidbody para manejar el salto

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        rigidb = GetComponent<Rigidbody>();

        // Encontrar al jugador 
        player = GameObject.FindWithTag("Player").transform;

        // Empezar el comportamiento de seguimiento
        StartCoroutine(ChasePlayer());
    }

    void Update()
    {
        // Comprobar si el enemigo está en el suelo con un Raycast
        CheckGround();

        // Salto si está en el suelo y no está siguiendo al jugador
        if (enTierra && !sigue)
        {
            Jump();
        }
    }

    private void CheckGround()
    {
        // Lanza un raycast hacia abajo para verificar si está en el suelo
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f))
        {
            enTierra = true; // En el suelo si el raycast golpea algo
        }
        else
        {
            enTierra = false; // No está en el suelo
        }
    }

    private void Jump()
    {
        // Agrega una fuerza hacia arriba para saltar
        rigidb.AddForce(Vector3.up * saltoF, ForceMode.Impulse);
        Debug.Log("Enemigo saltó!");
    }

    private IEnumerator ChasePlayer()
    {
        while (true)
        {
            // Inicia el seguimiento del jugador
            sigue  = true;
            agent.SetDestination(player.position);

            // Persigue al jugador durante 5 segundos
            yield return new WaitForSeconds(sigueDuration);

            // Detiene el seguimiento
            sigue = false;
            agent.ResetPath();

            // Espera 3 segundos antes de volver a perseguir
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta colisiones con balas del jugador
        if (other.CompareTag("BalaJugador"))
        {
            Debug.Log("Enemigo golpeado por la bala del jugador.");
            Destroy(gameObject); 
        }
    }
}

