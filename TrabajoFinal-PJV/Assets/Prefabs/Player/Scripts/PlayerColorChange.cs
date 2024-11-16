using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    private Renderer playerRenderer;
    private Color originalColor;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.GetColor("_Color");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es la bala
        if (other.CompareTag("BalaEnemigo") || other.CompareTag("BalaEnemigoElite"))
        {
            Debug.Log("Colisión detectada con la bala");  // Mensaje en consola

            // Inicia la corutina para cambiar el color temporalmente
            StartCoroutine(CambiarColor());
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