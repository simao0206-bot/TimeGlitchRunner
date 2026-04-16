using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadeFrente = 8f;   // velocidade para a frente (automática)
    public float velocidadeLateral = 5f;  // velocidade ao pressionar A ou D

    [Header("Limites laterais")]
    public float limiteX = 3f;            // até onde o jogador se pode desviar

    private Rigidbody rb;                 // referência ao Rigidbody do jogador

    void Start()
    {
        // Vai buscar o componente Rigidbody que está no mesmo objeto
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // FixedUpdate é usado para física — o professor avalia isto!

        // 1. Movimento automático para a frente (eixo Z)
        Vector3 velocidadeAtual = rb.linearVelocity;
        velocidadeAtual.z = velocidadeFrente;

        // 2. Input lateral do jogador (teclas A/D ou setas)
        float inputLateral = Input.GetAxis("Horizontal"); // -1 (esquerda), 0, ou 1 (direita)
        velocidadeAtual.x = inputLateral * velocidadeLateral;

        // 3. Aplica a velocidade ao Rigidbody
        rb.linearVelocity = velocidadeAtual;

        // 4. Limita a posição lateral para o jogador não sair do caminho
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }
}