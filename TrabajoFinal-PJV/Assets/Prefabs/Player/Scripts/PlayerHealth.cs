using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();  // Obtener referencia al PlayerController
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemigo") || other.CompareTag("BalaEnemigoElite"))
        {
            Destroy(other.gameObject);
            // Notificar al PlayerController que el jugador fue golpeado
            playerController.GolpeadoPorBala();
        }
    }
}
