using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadeInicial = 8f;    // velocidade no início
    public float velocidadeMaxima = 20f;    // velocidade máxima
    public float aumentoVelocidade = 0.5f;  // quanto aumenta por segundo
    public float velocidadeLateral = 5f;

    [Header("Limites laterais")]
    public float limiteX = 3f;

    private Rigidbody rb;
    private float velocidadeAtualFrente;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadeAtualFrente = velocidadeInicial;
    }

    void FixedUpdate()
    {
        // Aumenta a velocidade gradualmente até ao máximo
        velocidadeAtualFrente = Mathf.Min(
            velocidadeAtualFrente + aumentoVelocidade * Time.fixedDeltaTime,
            velocidadeMaxima
        );

        // Movimento automático para a frente
        Vector3 velocidade = rb.linearVelocity;
        velocidade.z = velocidadeAtualFrente;

        // Input lateral
        float inputLateral = Input.GetAxis("Horizontal");
        velocidade.x = inputLateral * velocidadeLateral;

        rb.linearVelocity = velocidade;

        // Limita posição lateral
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }

    // Devolve a velocidade atual (para o GameManager poder mostrar)
    public float GetVelocidade()
    {
        return velocidadeAtualFrente;
    }
}