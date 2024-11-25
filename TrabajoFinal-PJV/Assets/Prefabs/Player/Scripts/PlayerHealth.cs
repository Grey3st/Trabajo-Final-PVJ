using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private Renderer playerRenderer;
    private Color originalColor;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.GetColor("_Color");
        playerController = gameObject.GetComponent<PlayerController>();  // Obtener referencia al PlayerController
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemigo") || other.CompareTag("BalaEnemigoElite"))
        {
            StartCoroutine(CambiarColor());
            Destroy(other.gameObject);
            // Notificar al PlayerController que el jugador fue golpeado
            playerController.GolpeadoPorBala();
        }
    }
    private IEnumerator CambiarColor()
    {
        // Cambia el color a rojo
        playerRenderer.material.SetColor("_Color", Color.red);

        // Espera un segundo
        yield return new WaitForSeconds(1.0f);

        // Restaura el color original
        playerRenderer.material.SetColor("_Color", originalColor);
    }
}
