using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMovement : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 direccion;
    private float velocidad;

    // Agregamos un booleano para determinar si es una bala del jugador o del enemigo
    private bool esBalaDelEnemigo; // true si es bala del enemigo, false si es del jugador

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        // Llama al método adecuado dependiendo de si es una bala del enemigo o del jugador
        if (esBalaDelEnemigo)
        {
            BalaEnemy();
        }
        else
        {
            BalaPlayer();
        }
    }

    private void BalaPlayer()
    {
        transform.Translate(0, 0, 6f * Time.deltaTime);

        float distanciaActual = Vector3.Distance(posicionInicial, transform.position);

        // Verifica si la bala ha recorrido la distancia especificada
        if (distanciaActual >= 17f)
        {
            // Destruir la bala
            Destroy(gameObject);
        }
    }

    private void BalaEnemy()
    {
        // Mueve la bala en la dirección especificada
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Verifica si la bala ha recorrido la distancia especificada
        if (Vector3.Distance(Vector3.zero, transform.position) >= 17f) // O ajusta la referencia según sea necesario
        {
            // Destruir la bala
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 direccion, float velocidad, bool esBalaDelEnemigo)
    {
        this.direccion = direccion.normalized; // Asegúrate de normalizar la dirección
        this.velocidad = velocidad;
        this.esBalaDelEnemigo = esBalaDelEnemigo; // Establece si es bala del enemigo

        // Destruir la bala después de 6 segundos si no ha colisionado
        Destroy(gameObject, 6f);
    }
}