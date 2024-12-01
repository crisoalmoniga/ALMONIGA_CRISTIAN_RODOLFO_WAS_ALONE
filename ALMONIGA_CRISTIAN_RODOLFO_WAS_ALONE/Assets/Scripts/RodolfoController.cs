using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodolfoController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;  // Capa para detectar el suelo
    public Transform groundCheck;  // Verificación de si el personaje está tocando el suelo
    public float groundCheckRadius = 0.1f;  // Radio de la verificación
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto " + gameObject.name);
        }
    }

    private void Update()
    {
        if (rb == null) return; // Evitar errores si falta el componente

        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Verificación de si el personaje está tocando el suelo
        bool isGrounded = Mathf.Abs(rb.velocity.y) < 0.01f;

        // Salto (solo si está tocando el suelo)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Detectar la colisión con el objeto Meta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {
            // Si colisiona con el objeto Meta, muestra "GANASTE" en la consola
            Debug.Log("GANASTE");
        }
    }
}
