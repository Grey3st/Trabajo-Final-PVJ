using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    private Vector3 posicionInicial;
    public GameObject balaEnemigo;
    private void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(DispararBalaCoroutine());
    }

    // Update is called once per frame
    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        transform.Translate(3f * Time.deltaTime, 0, 3f * Time.deltaTime);

        float distanciaActual = Vector3.Distance(posicionInicial, transform.position);

        // Verifica si el enemigo ha recorrido la distancia especificada
        if (distanciaActual >= 30f)
        {
            // Destruir enemigo
            Destroy(gameObject);
        }
    }

    private IEnumerator DispararBalaCoroutine()
    {
        while (true)  // Bucle infinito
        {
            Instantiate(balaEnemigo, transform.position, transform.rotation);

            // Esperar un segundo antes de crear la siguiente bala
            yield return new WaitForSeconds(1);
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
