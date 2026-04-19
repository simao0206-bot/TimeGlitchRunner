using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadeInicial = 8f;
    public float velocidadeMaxima = 20f;
    public float aumentoVelocidade = 0.5f;
    public float velocidadeLateral = 5f;

    [Header("Salto")]
    public float forcaSalto = 7f;
    public float forcaSaltoMaxima = 10f;    // limita o salto mesmo com velocidade alta
    private bool noChao = false;

    [Header("Agachar")]
    public float escalaAgachado = 0.5f;
    private bool agachado = false;
    private Vector3 escalaOriginal;

    [Header("Limites laterais")]
    public float limiteX = 3f;

    private Rigidbody rb;
    private float velocidadeAtualFrente;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadeAtualFrente = velocidadeInicial;
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        // Verifica se está no chão
        noChao = transform.position.y <= 1.5f;

        // Salto — força limitada independente da velocidade do jogo
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && noChao)
        {
            float forca = Mathf.Min(forcaSalto, forcaSaltoMaxima);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * forca, ForceMode.Impulse);
            noChao = false;
        }

        // Agachar — só com S ou Shift, NUNCA com W
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && !agachado)
    {   
         agachado = true;
         transform.localScale = new Vector3(escalaOriginal.x, escalaOriginal.y * escalaAgachado, escalaOriginal.z);
         if (!noChao)
             rb.linearVelocity = new Vector3(rb.linearVelocity.x, -15f, rb.linearVelocity.z);
         else
             transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
    }

        // Levantar
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (agachado)
            {
                agachado = false;
                transform.localScale = escalaOriginal;
                if (noChao)
                    transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
            }
        }
    }

    void FixedUpdate()
    {
        // Aumenta velocidade gradualmente
        velocidadeAtualFrente = Mathf.Min(
            velocidadeAtualFrente + aumentoVelocidade * Time.fixedDeltaTime,
            velocidadeMaxima
        );

        Vector3 velocidade = rb.linearVelocity;
        velocidade.z = velocidadeAtualFrente;

        float inputLateral = Input.GetAxis("Horizontal");
        velocidade.x = inputLateral * velocidadeLateral;

        rb.linearVelocity = velocidade;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }

    public float GetVelocidade()
    {
        return velocidadeAtualFrente;
    }
}