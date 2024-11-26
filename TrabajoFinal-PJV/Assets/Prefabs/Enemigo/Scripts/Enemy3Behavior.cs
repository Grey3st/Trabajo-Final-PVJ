using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserBehavior : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    private NavMeshAgent agent; // Componente de navegaci�n
    private bool sigue = false;
    private float sigueDuration = 5f;
    private float saltoF; // Fuerza del salto
    private bool enTierra = false; // Verifica si el enemigo est� en el suelo
    private Rigidbody rigidb; // Rigidbody para manejar el salto
    private LayerMask mask;
    private int colisionesCount = 0;
    private float zigzagAmplitud = 2f; // Amplitud del zigzag
    private float zigzagFrequencia = 1f; // Frecuencia del zigzag

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidb = GetComponent<Rigidbody>();

        // Encontrar al jugador
        player = GameObject.FindWithTag("Player").transform;

        // Empezar el comportamiento de seguimiento
        StartCoroutine(PerseguirEnZigzag());
    }

    void Update()
    {
        // Comprobar si el enemigo est� en el suelo con un Raycast
        CheckGround();

        // Salto si est� en el suelo y no est� siguiendo al jugador
        if (enTierra && !sigue)
        {
            Jump();
        }
    }

    private void CheckGround()
    {
        // Lanza un raycast hacia abajo para verificar si est� en el suelo
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f, mask))
        {
            enTierra = true; // En el suelo si el raycast golpea algo
        }
        else
        {
            enTierra = false; // No est� en el suelo
        }
    }

    private void Jump()
    {
        // Desactivar el NavMeshAgent antes del salto
        agent.enabled = false;

        // Agrega una fuerza hacia arriba para saltar
        rigidb.AddForce(Vector3.up * saltoF, ForceMode.Impulse);
        Debug.Log("Enemigo salt�!");

        // Reactivar el NavMeshAgent despu�s de un breve retraso
        StartCoroutine(ReenableNavMeshAgentAfterJump());
    }

    private IEnumerator ReenableNavMeshAgentAfterJump()
    {
        // Esperar a que termine el salto (ajusta este valor seg�n la altura del salto)
        yield return new WaitForSeconds(0.5f);

        // Reactivar el NavMeshAgent
        agent.enabled = true;
    }

    private IEnumerator PerseguirEnZigzag()
    {
        while (true)
        {
            sigue = true;

            while (sigue)
            {
                // Direcci�n inicial hacia el jugador
                Vector3 direccion = (player.position - transform.position).normalized;

                // Generar un desplazamiento lateral para el zigzag
                Vector3 zigzaglateral = transform.right * Mathf.Sin(Time.time * zigzagFrequencia) * zigzagAmplitud;

                // Calcular el destino con el zigzag
                Vector3 zigzagDestino = transform.position + direccion * agent.speed + zigzaglateral;

                // Establecer el destino del NavMeshAgent
                agent.SetDestination(zigzagDestino);

                yield return null; // Esperar al pr�ximo frame
            }

            // Detiene el seguimiento despu�s de un per�odo
            sigue = false;
            agent.ResetPath();

            // Espera antes de reiniciar el seguimiento
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta colisiones con balas del jugador
        if (other.CompareTag("BalaJugador"))
        {
            colisionesCount++;
            if (colisionesCount >= 10)
            {
                Destroy(gameObject);
                Debug.Log("Enemigo destruido despu�s de 10 colisiones.");
            }
        }
    }
}


