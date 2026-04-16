using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstáculo")]
    public GameObject prefabObstaculo;      // o objeto que vai ser criado

    [Header("Spawning")]
    public float distanciaAFrente = 30f;    // quão longe à frente do jogador aparecem
    public float intervalo = 2f;            // segundos entre cada obstáculo
    public float velocidadeMinima = 1f;     // dificuldade mínima
    public float velocidadeMaxima = 3f;     // dificuldade máxima

    [Header("Referência")]
    public Transform jogador;              // referência ao Player

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            timer = 0f;
            SpawnarObstaculo();
        }
    }

    void SpawnarObstaculo()
    {
        // Posição aleatória em X dentro dos limites do caminho
        float posX = Random.Range(-2.5f, 2.5f);

        // Aparece sempre à frente do jogador
        Vector3 posicao = new Vector3(posX, 0.5f, jogador.position.z + distanciaAFrente);

        // Cria o obstáculo na cena
        Instantiate(prefabObstaculo, posicao, Quaternion.identity);
    }
}