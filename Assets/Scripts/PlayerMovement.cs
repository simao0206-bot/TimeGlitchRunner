using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // O Rigidbody permite que o Unity controle a física do objeto
    public Rigidbody rb;

    // Força para mover para a frente e para os lados
    public float forwardForce = 1000f; 
    public float sidewaysForce = 50f;

    // A lógica de física tem de estar no FixedUpdate (exigência do professor)
    void FixedUpdate()
    {
        // Movimento contínuo no eixo Z (para a frente)
        rb.AddForce(0, 0, forwardForce * Time.fixedDeltaTime);

        // Input do jogador (teclas laterais A/D ou setas)
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sidewaysForce * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sidewaysForce * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }
}