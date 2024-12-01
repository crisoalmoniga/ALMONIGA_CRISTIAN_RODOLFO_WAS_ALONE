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

    private Transform currentPlatform;  // Plataforma actual que toca el personaje

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto " + gameObject.name);
        }

        // Si no tienes un objeto de verificación de suelo (GroundCheck) en el editor,
        // el siguiente código crea uno automáticamente justo debajo del personaje
        if (groundCheck == null)
        {
            groundCheck = new GameObject("GroundCheck").transform;
            groundCheck.SetParent(transform);
            groundCheck.localPosition = new Vector3(0, -0.5f, 0); // Ajusta según el tamaño del personaje
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

        // Sincronización con plataformas en movimiento
        if (isGrounded && currentPlatform != null)
        {
            // Sincronizar la posición del personaje con la plataforma
            Vector3 platformMovement = currentPlatform.position - currentPlatform.GetComponent<Rigidbody>().position;
            transform.position += platformMovement;

            // Mantener la velocidad X del personaje sin modificar la Y
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Asociar la plataforma con el personaje al entrar en contacto
            currentPlatform = collision.transform;

            // Hacer que la plataforma se mueva sin que el personaje quede pegado
            rb.isKinematic = true; // Desactivar la física del personaje temporalmente
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Restablecer la referencia de la plataforma cuando se deje de tocar
            currentPlatform = null;

            // Habilitar de nuevo la física del personaje
            rb.isKinematic = false; // Reactivar la física
        }
    }
}
