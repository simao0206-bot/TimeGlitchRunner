using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstáculos por época")]
    public GameObject[] obstaculosPreHistoria;
    public GameObject[] obstaculosMedieval;
    public GameObject[] obstaculosModerno;
    public GameObject[] obstaculosFuturo;

    [Header("Estado atual")]
    public int epocaAtual = 0; // 0 = Pré-história, 1 = Medieval, 2 = Moderno, 3 = Futuro

    [Header("Referência")]
    public Transform jogador;

    [Header("Spawning Pré-história")]
    public float distanciaAFrentePreHistoria = 25f;
    public float offsetYPreHistoria = 0.5f;

    [Header("Spawning Medieval")]
    public float distanciaAFrenteMedievalNormal = 55f;
    public float distanciaAFrenteMedievalLargo = 50f;
    public float offsetYMedieval = 0f;

    [Header("Spawning Moderno")]
    public float distanciaAFrenteModernoNormal = 45f;
    public float distanciaAFrenteModernoLargo = 40f;
    public float offsetYModerno = 0f;

    [Header("Spawning Futuro")]
    public float distanciaAFrenteFuturoNormal = 55f;
    public float distanciaAFrenteFuturoLargo = 50f;
    public float offsetYFuturo = 0f;

    [Header("Intervalo")]
    public float intervalo = 2f;
    public float variacaoIntervalo = 0.5f;

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
            intervaloAtual = intervalo + Random.Range(-variacaoIntervalo, variacaoIntervalo);
            intervaloAtual = Mathf.Max(0.8f, intervaloAtual);
            EscolherTipoObstaculo();
        }
    }

    GameObject EscolherPrefab()
    {
        GameObject[] listaAtual = null;

        switch (epocaAtual)
        {
            case 0:
                listaAtual = obstaculosPreHistoria;
                break;
            case 1:
                listaAtual = obstaculosMedieval;
                break;
            case 2:
                listaAtual = obstaculosModerno;
                break;
            case 3:
                listaAtual = obstaculosFuturo;
                break;
            default:
                listaAtual = obstaculosPreHistoria;
                break;
        }

        if (listaAtual == null || listaAtual.Length == 0)
            return null;

        return listaAtual[Random.Range(0, listaAtual.Length)];
    }

    void EscolherTipoObstaculo()
    {
        float chance = Random.value;

        if (chance < 0.15f)
            SpawnarObstaculoLargo();
        else if (chance < 0.35f)
            SpawnarDoisObstaculos();
        else
            SpawnarObstaculoNormal();
    }

    void SpawnarObstaculoNormal()
    {
        GameObject prefab = EscolherPrefab();
        if (prefab == null || jogador == null) return;

        float posX = Random.Range(-2f, 2f);
        float distancia = ObterDistanciaNormalAtual();
        float offsetY = ObterOffsetYAtual();

        Vector3 posicao = new Vector3(posX, offsetY, jogador.position.z + distancia);
        Quaternion rotacao = ObterRotacaoNormalAtual();

        GameObject obj = Instantiate(prefab, posicao, rotacao);
        AplicarEscalaPorEpoca(obj);
    }

    void SpawnarObstaculoLargo()
    {
        GameObject prefab = EscolherPrefab();
        if (prefab == null || jogador == null) return;

        float distancia = ObterDistanciaLargaAtual();
        float offsetY = ObterOffsetYAtual();

        Vector3 posicao = new Vector3(0f, offsetY, jogador.position.z + distancia);
        Quaternion rotacao = ObterRotacaoLargaAtual();

        GameObject obj = Instantiate(prefab, posicao, rotacao);

        if (epocaAtual == 2)
        {
            obj.transform.localScale = new Vector3(2.5f, 1.2f, 1.2f);
        }
        else if (epocaAtual == 3)
        {
            obj.transform.localScale = new Vector3(2.2f, 2f, 2.2f);
        }
        else
        {
            obj.transform.localScale = new Vector3(5f, 1.5f, 1.5f);
        }
    }

    void SpawnarDoisObstaculos()
    {
        GameObject prefab = EscolherPrefab();
        if (prefab == null || jogador == null) return;

        float espacamento = Random.Range(1.5f, 2.5f);
        float distancia = ObterDistanciaNormalAtual();
        float offsetY = ObterOffsetYAtual();
        Quaternion rotacao = ObterRotacaoNormalAtual();

        Vector3 pos1 = new Vector3(-espacamento, offsetY, jogador.position.z + distancia);
        Vector3 pos2 = new Vector3(espacamento, offsetY, jogador.position.z + distancia);

        GameObject obj1 = Instantiate(prefab, pos1, rotacao);
        GameObject obj2 = Instantiate(prefab, pos2, rotacao);

        AplicarEscalaPorEpoca(obj1);
        AplicarEscalaPorEpoca(obj2);
    }

    Quaternion ObterRotacaoNormalAtual()
    {
        if (epocaAtual == 3)
            return Quaternion.identity;

        return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    Quaternion ObterRotacaoLargaAtual()
    {
        return Quaternion.identity;
    }

    void AplicarEscalaPorEpoca(GameObject obj)
    {
        if (obj == null) return;

        if (epocaAtual == 1)
        {
            obj.transform.localScale = Vector3.one * Random.Range(1.2f, 1.6f);
        }
        else if (epocaAtual == 2)
        {
            obj.transform.localScale = Vector3.one * Random.Range(1f, 1.2f);
        }
        else if (epocaAtual == 3)
        {
            obj.transform.localScale = Vector3.one * Random.Range(1.1f, 1.4f);
        }
        else
        {
            obj.transform.localScale = Vector3.one * Random.Range(1f, 1.5f);
        }
    }

    float ObterDistanciaNormalAtual()
    {
        switch (epocaAtual)
        {
            case 1: return distanciaAFrenteMedievalNormal;
            case 2: return distanciaAFrenteModernoNormal;
            case 3: return distanciaAFrenteFuturoNormal;
            default: return distanciaAFrentePreHistoria;
        }
    }

    float ObterDistanciaLargaAtual()
    {
        switch (epocaAtual)
        {
            case 1: return distanciaAFrenteMedievalLargo;
            case 2: return distanciaAFrenteModernoLargo;
            case 3: return distanciaAFrenteFuturoLargo;
            default: return distanciaAFrentePreHistoria;
        }
    }

    float ObterOffsetYAtual()
    {
        switch (epocaAtual)
        {
            case 1: return offsetYMedieval;
            case 2: return offsetYModerno;
            case 3: return offsetYFuturo;
            default: return offsetYPreHistoria;
        }
    }

    public void AtivarPreHistoria()
    {
        epocaAtual = 0;
    }

    public void AtivarMedieval()
    {
        epocaAtual = 1;
    }

    public void AtivarModerno()
    {
        epocaAtual = 2;
    }

    public void AtivarFuturo()
    {
        epocaAtual = 3;
    }
}