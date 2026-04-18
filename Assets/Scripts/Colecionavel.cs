using UnityEngine;

public class Colecionavel : MonoBehaviour
{
    [Header("Tipo")]
    public bool cristalRaro = false;        // false = normal, true = raro

    [Header("Pontos")]
    public int pontos = 10;                 // pontos que dá ao ser apanhado

    [Header("Distorção Temporal")]
    public float duracaoDistorcao = 3f;     // segundos que o tempo abranda
    public float escalaTempoDistorcao = 0.3f; // quão lento fica (0.3 = 30% da velocidade)

    [Header("Rotação")]
    public float velocidadeRotacao = 90f;   // roda para parecer apelativo

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Roda o cristal continuamente
        transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se foi o jogador que apanhou
        if (other.CompareTag("Player"))
        {
            // Adiciona pontos
            gameManager.AdicionarPontos(pontos);

            // Se for raro, abranda o tempo
            if (cristalRaro)
            {
                gameManager.IniciarDistorcaoTemporal(duracaoDistorcao, escalaTempoDistorcao);
            }

            // Destrói o cristal
            Destroy(gameObject);
        }
    }
}