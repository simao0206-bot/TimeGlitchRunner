using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstáculo")]
    public GameObject prefabObstaculo;

    [Header("Spawning")]
    public float distanciaAFrente = 25f;
    public float intervalo = 2f;
    public float variacaoIntervalo = 0.5f;  // intervalo varia ±0.5s para ser imprevisível

    [Header("Referência")]
    public Transform jogador;

    private float timer = 0f;
    private float intervaloAtual = 2f;

    void Start()
    {
        intervaloAtual = intervalo;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervaloAtual)
        {
            timer = 0f;
            // Intervalo aleatório para ser imprevisível
            intervaloAtual = intervalo + Random.Range(-variacaoIntervalo, variacaoIntervalo);
            intervaloAtual = Mathf.Max(0.8f, intervaloAtual); // nunca menos de 0.8s

            EscolherTipoObstaculo();
        }
    }

    void EscolherTipoObstaculo()
    {
        float chance = Random.value;

        if (chance < 0.15f)
            SpawnarObstaculoLargo();        // 15% — cobre pista toda (só pode saltar)
        else if (chance < 0.35f)
            SpawnarDoisObstaculos();        // 20% — dois obstáculos juntos
        else
            SpawnarObstaculoNormal();       // 65% — obstáculo normal
    }

    void SpawnarObstaculoNormal()
    {
        float posX = Random.Range(-2f, 2f);
        Vector3 posicao = new Vector3(posX, 0.5f, jogador.position.z + distanciaAFrente);
        GameObject obj = Instantiate(prefabObstaculo, posicao, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        obj.transform.localScale = Vector3.one * Random.Range(1f, 1.5f);
    }

    void SpawnarObstaculoLargo()
    {
        // Cobre quase toda a pista — jogador TEM de saltar
        Vector3 posicao = new Vector3(0f, 0.5f, jogador.position.z + distanciaAFrente);
        GameObject obj = Instantiate(prefabObstaculo, posicao, Quaternion.identity);
        // Escala muito larga
        obj.transform.localScale = new Vector3(5f, 1.5f, 1.5f);
    }

    void SpawnarDoisObstaculos()
    {
        // Dois obstáculos lado a lado com espaço no meio
        float espacamento = Random.Range(1.5f, 2.5f);
        Vector3 pos1 = new Vector3(-espacamento, 0.5f, jogador.position.z + distanciaAFrente);
        Vector3 pos2 = new Vector3(espacamento, 0.5f, jogador.position.z + distanciaAFrente);

        GameObject obj1 = Instantiate(prefabObstaculo, pos1, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        GameObject obj2 = Instantiate(prefabObstaculo, pos2, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));

        obj1.transform.localScale = Vector3.one * Random.Range(1f, 1.3f);
        obj2.transform.localScale = Vector3.one * Random.Range(1f, 1.3f);
    }
}