using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner01 : MonoBehaviour
{
    private GameObject Enemigo01;  // Prefab de la bala
    private float tiempoEntreEnemigos = 2f;
    private Transform puntoDeSpawn;

    private void Start()
    {
        puntoDeSpawn = transform;
        Enemigo01 = Resources.Load<GameObject>("RPG Monster DUO PBR Polyart/Prefabs/PBRDefault/Enemigo01");
        InvokeRepeating("CrearEnemigo", 0f, tiempoEntreEnemigos);
    }

    // Update is called once per frame
    private void CrearEnemigo()
    {
        // Instanciar la bala en el punto de spawn
        Instantiate(Enemigo01, puntoDeSpawn.position, puntoDeSpawn.rotation);
    }
}
