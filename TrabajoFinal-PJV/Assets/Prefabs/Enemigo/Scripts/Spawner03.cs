using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner03 : MonoBehaviour
{
    public GameObject Enemigo03; // Prefab del enemigo03
    public float tiempoEntreEnemigos = 10f; // Tiempo entre apariciones de enemigo03
    public Vector3 rangoSpawn = new Vector3(10f, 5f, 10f); // Rango de spawn aleatorio en el fondo
    public Vector3 posicionBase = new Vector3(0f, 0f, -20f); // Posici�n base para spawn en el fondo

    private void Start()
    {
        InvokeRepeating("CrearEnemigoAleatorio", 0f, tiempoEntreEnemigos);
    }

    private void CrearEnemigoAleatorio()
    {
        // Generar una posici�n aleatoria en el rango de spawn
        Vector3 posicionAleatoria = new Vector3(
            posicionBase.x + Random.Range(-rangoSpawn.x, rangoSpawn.x),
            posicionBase.y + Random.Range(-rangoSpawn.y, rangoSpawn.y),
            posicionBase.z + Random.Range(-rangoSpawn.z, rangoSpawn.z)
        );

        // Instanciar el enemigo en la posici�n aleatoria
        Instantiate(Enemigo03, posicionAleatoria, Quaternion.identity);
    }
}
