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
    private bool noChao = false;

    [Header("Agachar")]
    public float escalaAgachado = 0.5f;     // escala Y quando agachado
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
        escalaOriginal = transform.localScale;  // guarda a escala original
    }

    void Update()
{
    // Verifica se está no chão pela posição Y
    noChao = transform.position.y <= 1.2f;

    // Salto
    if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && noChao)
    {
        rb.AddForce(Vector3.up * forcaSalto, ForceMode.Impulse);
        noChao = false;
    }

    // Agachar
    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
    {
        agachado = true;
        transform.localScale = new Vector3(escalaOriginal.x, escalaOriginal.y * escalaAgachado, escalaOriginal.z);
        // Desce o jogador para não flutuar
        transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
    }

    // Levantar
    if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftShift))
    {
        agachado = false;
        transform.localScale = escalaOriginal;
        // Volta à altura normal
        transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
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

    // Deteta quando o jogador toca no chão
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.name.Contains("Ground"))
        {
            noChao = true;
        }
    }
}