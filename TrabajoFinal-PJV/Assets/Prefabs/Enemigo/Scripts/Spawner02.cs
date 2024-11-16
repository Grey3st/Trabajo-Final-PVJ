using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner02 : MonoBehaviour
{
    public GameObject Enemigo02;
    private Vector3 posicionInicial;
    private float tiempoEntreEnemigos = 2f;
    private float velocidad = 6f;
    private float limiteInferior = -7f;
    private float limiteSuperior = 7f;
    private int direccion = 1;

    private int contadorEnemigos = 0; // Contador de cuántas veces se ha instanciado el objeto
    private int maximoEnemigos = 5; // Ya que el enunciado no especifica que deben destruirse,
                                    // Se generarán solo cinco Enemigo02 para no tener tantas instancias y generar sobrecarga

    private void Start()
    {
        posicionInicial = transform.position;
        InvokeRepeating("CrearEnemigo", 0f, tiempoEntreEnemigos);
    }
    private void Update()
    {
        SpawnerMove();
    }

    private void SpawnerMove()
    {
        transform.Translate(Vector3.back * direccion * velocidad * Time.deltaTime);

        // Cambia de dirección al llegar a los límites
        if (transform.position.z >= limiteSuperior)
        {
            direccion = -1; 
        }
        else if (transform.position.z <= limiteInferior)
        {
            direccion = 1; 
        }
    }
    private void CrearEnemigo()
    {
        if (contadorEnemigos < maximoEnemigos)
        {
            Instantiate(Enemigo02, transform.position, transform.rotation);
            contadorEnemigos++;
        }
        else
        {
            CancelInvoke("InstanciarObjeto");
        }
    }

}
