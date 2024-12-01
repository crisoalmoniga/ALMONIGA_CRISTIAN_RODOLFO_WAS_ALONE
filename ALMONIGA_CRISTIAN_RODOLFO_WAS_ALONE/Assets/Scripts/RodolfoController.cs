using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodolfoController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;  // Capa para detectar el suelo
    public Transform groundCheck;  // Verificaci�n de si el personaje est� tocando el suelo
    public float groundCheckRadius = 0.1f;  // Radio de la verificaci�n
    private Rigidbody rb;

    private Transform currentPlatform;  // Plataforma actual que toca el personaje

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontr� un Rigidbody en el objeto " + gameObject.name);
        }

        // Si no tienes un objeto de verificaci�n de suelo (GroundCheck) en el editor,
        // el siguiente c�digo crea uno autom�ticamente justo debajo del personaje
        if (groundCheck == null)
        {
            groundCheck = new GameObject("GroundCheck").transform;
            groundCheck.SetParent(transform);
            groundCheck.localPosition = new Vector3(0, -0.5f, 0); // Ajusta seg�n el tama�o del personaje
        }
    }

    private void Update()
    {
        if (rb == null) return; // Evitar errores si falta el componente

        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Verificaci�n de si el personaje est� tocando el suelo
        bool isGrounded = Mathf.Abs(rb.velocity.y) < 0.01f;

        // Salto (solo si est� tocando el suelo)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }

        // Sincronizaci�n con plataformas en movimiento
        if (isGrounded && currentPlatform != null)
        {
            // Sincronizar la posici�n del personaje con la plataforma
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
            rb.isKinematic = true; // Desactivar la f�sica del personaje temporalmente
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Restablecer la referencia de la plataforma cuando se deje de tocar
            currentPlatform = null;

            // Habilitar de nuevo la f�sica del personaje
            rb.isKinematic = false; // Reactivar la f�sica
        }
    }
}
