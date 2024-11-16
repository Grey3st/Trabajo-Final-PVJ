using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    private Vector3 posicionInicial;
    public GameObject balaEnemigo;
    private float velocidad = 5f;


    private float limiteIzquierdo = -10f;
    private float limiteDerecho = 10f;
    private int direccion = 1;
    private void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(DispararBalaCoroutine());
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        transform.Translate(Vector3.left * direccion * velocidad * Time.deltaTime);

        // Cambia de dirección al llegar a los límites
        if (transform.position.x >= limiteDerecho)
        {
            direccion = -1;
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            direccion = 1;
        }
    }

    private IEnumerator DispararBalaCoroutine()
    {
        while (true)  // Bucle infinito
        {
            float tiempoDeEspera = Random.Range(1, 3);
            Instantiate(balaEnemigo, transform.position, transform.rotation);

            // Esperar el tiempo aleatorio antes de crear la siguiente bala
            yield return new WaitForSeconds(tiempoDeEspera);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaJugador"))
        {
            Debug.Log("Colisión con la bala del jugador detectada");
            Destroy(gameObject); // Destruir el enemigo
        }
    }
}
