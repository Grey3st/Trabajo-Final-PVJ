using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int golpesRecibidos = 0; 
    private int maxGolpes = 5; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemigo")||other.CompareTag("BalaEnemigoElite")) 
        {
            golpesRecibidos++; // Incrementa el contador de golpes

            Destroy(other.gameObject);

            if (golpesRecibidos >= maxGolpes)
            {
                Desaparecer(); 
            }
        }
    }

    // Método para manejar la desaparición del jugador
    private void Desaparecer()
    {
        Destroy(gameObject);
    }
}
