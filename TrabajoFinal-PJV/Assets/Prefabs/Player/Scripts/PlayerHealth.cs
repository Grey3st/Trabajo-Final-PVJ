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

    // M�todo para manejar la desaparici�n del jugador
    private void Desaparecer()
    {
        Destroy(gameObject);
    }
}
