using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    public LayerMask suelo; // Layer para detectar el suelo
    public float raycastDistancia = 1.1f; // Distancia del Raycast

    private Rigidbody rigidb; // Referencia al Rigidbody

    private void Start()
    {
        rigidb = GetComponent<Rigidbody>(); 
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplica la fuerza de salto
        }
    }

    private bool IsGrounded()
    {
        // Raycast hacia abajo desde la posición del jugador
        return Physics.Raycast(transform.position, Vector3.down, raycastDistancia, suelo);
    }
}

