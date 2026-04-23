using UnityEngine;
using System.Collections.Generic;

public class DecoradorEpoca : MonoBehaviour
{
    [Header("Referência")]
    public Transform jogador;

    [Header("Configuração Geral")]
    public float intervaloDecoracao = 5f;
    public int numeroDecoracoes = 20;

    [Header("Distâncias laterais Pré-História")]
    public float preHistoriaCamada1 = 6f;
    public float preHistoriaCamada2 = 8.5f;
    public float preHistoriaCamada3 = 12f;
    public float preHistoriaCamada4 = 16f;
    public float preHistoriaCamada5 = 20f;
    public float preHistoriaCamada6 = 24f;

    [Header("Distâncias laterais Medieval")]
    public float medievalCamada1 = 4.5f;
    public float medievalCamada2 = 6.5f;
    public float medievalCamada3 = 9f;
    public float medievalCamada4 = 12f;
    public float medievalCamada5 = 16f;
    public float medievalCamada6 = 20f;

    [Header("Offset Y por época")]
    public float offsetYPreHistoria = 0f;
    public float offsetYMedieval = 0f;
    public float offsetYModerno = 0f;
    public float offsetYFuturo = 0f;

    [Header("Prefabs Pré-História")]
    public GameObject[] prefabsPreHistoria;

    [Header("Prefabs Medieval - PERTO")]
    public GameObject[] prefabsMedievalPerto;

    [Header("Prefabs Medieval - MEIO")]
    public GameObject[] prefabsMedievalMeio;

    [Header("Prefabs Medieval - LONGE")]
    public GameObject[] prefabsMedievalLonge;

    [Header("Prefabs Moderno")]
    public GameObject[] prefabsModerno;

    [Header("Prefabs Futuro")]
    public GameObject[] prefabsFuturo;

    private List<GameObject> decoracoesAtivas = new List<GameObject>();
    private float zUltimaDecoracao = 0f;
    private int epocaAtual = 0;

    void Start()
    {
        if (jogador == null) return;

        for (int i = -4; i < numeroDecoracoes; i++)
        {
            CriarParDeDecoracoes(jogador.position.z + i * intervaloDecoracao);
        }

        zUltimaDecoracao = jogador.position.z + numeroDecoracoes * intervaloDecoracao;
    }

    void Update()
    {
        if (jogador != null && jogador.position.z + 180f > zUltimaDecoracao)
        {
            CriarParDeDecoracoes(zUltimaDecoracao);
            zUltimaDecoracao += intervaloDecoracao;
        }

        for (int i = decoracoesAtivas.Count - 1; i >= 0; i--)
        {
            if (decoracoesAtivas[i] == null)
            {
                decoracoesAtivas.RemoveAt(i);
                continue;
            }

            if (decoracoesAtivas[i].transform.position.z < jogador.position.z - 40f)
            {
                Destroy(decoracoesAtivas[i]);
                decoracoesAtivas.RemoveAt(i);
            }
        }
    }

    void CriarParDeDecoracoes(float z)
    {
        if (epocaAtual == 1)
            CriarParDeDecoracoesMedieval(z);
        else
            CriarParDeDecoracoesNormal(z);
    }

    void CriarParDeDecoracoesNormal(float z)
    {
        float c1 = preHistoriaCamada1;
        float c2 = preHistoriaCamada2;
        float c3 = preHistoriaCamada3;
        float c4 = preHistoriaCamada4;
        float c5 = preHistoriaCamada5;
        float c6 = preHistoriaCamada6;

        CriarDecoracao(new Vector3(-c1, 0f, z), epocaAtual, 0, 0.8f);
        CriarDecoracao(new Vector3(c1, 0f, z), epocaAtual, 0, 0.8f);

        if (Random.value > 0.3f)
        {
            CriarDecoracao(new Vector3(-c2, 0f, z + Random.Range(-1.5f, 1.5f)), epocaAtual, 1, 1.0f);
            CriarDecoracao(new Vector3(c2, 0f, z + Random.Range(-1.5f, 1.5f)), epocaAtual, 1, 1.0f);
        }

        if (Random.value > 0.5f)
        {
            CriarDecoracao(new Vector3(-c3, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 2, 1.1f);
            CriarDecoracao(new Vector3(c3, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 2, 1.1f);
        }

        if (Random.value > 0.4f)
        {
            CriarDecoracao(new Vector3(-c4, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 3, 1.2f);
            CriarDecoracao(new Vector3(c4, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 3, 1.2f);
        }

        CriarDecoracao(new Vector3(-c5, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 4, 1.3f);
        CriarDecoracao(new Vector3(c5, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 4, 1.3f);

        CriarDecoracao(new Vector3(-c6, 0f, z + Random.Range(-1f, 1f)), epocaAtual, 5, 1.4f);
        CriarDecoracao(new Vector3(c6, 0f, z + Random.Range(-1f, 1f)), epocaAtual, 5, 1.4f);
    }

    void CriarParDeDecoracoesMedieval(float z)
    {
        float c1 = medievalCamada1;
        float c2 = medievalCamada2;
        float c3 = medievalCamada3;
        float c4 = medievalCamada4;
        float c5 = medievalCamada5;
        float c6 = medievalCamada6;

        CriarDecoracao(new Vector3(-c1, 0f, z), 1, 0, 0.9f);
        CriarDecoracao(new Vector3(c1, 0f, z), 1, 0, 0.9f);

        if (Random.value > 0.25f)
        {
            CriarDecoracao(new Vector3(-c2, 0f, z + Random.Range(-1.5f, 1.5f)), 1, 1, 1.0f);
            CriarDecoracao(new Vector3(c2, 0f, z + Random.Range(-1.5f, 1.5f)), 1, 1, 1.0f);
        }

        if (Random.value > 0.35f)
        {
            CriarDecoracao(new Vector3(-c3, 0f, z + Random.Range(-2f, 2f)), 1, 2, 1.0f);
            CriarDecoracao(new Vector3(c3, 0f, z + Random.Range(-2f, 2f)), 1, 2, 1.0f);
        }

        if (Random.value > 0.25f)
        {
            CriarDecoracao(new Vector3(-c4, 0f, z + Random.Range(-2f, 2f)), 1, 3, 1.1f);
            CriarDecoracao(new Vector3(c4, 0f, z + Random.Range(-2f, 2f)), 1, 3, 1.1f);
        }

        CriarDecoracao(new Vector3(-c5, 0f, z + Random.Range(-2f, 2f)), 1, 4, 1.0f);
        CriarDecoracao(new Vector3(c5, 0f, z + Random.Range(-2f, 2f)), 1, 4, 1.0f);

        CriarDecoracao(new Vector3(-c6, 0f, z + Random.Range(-1f, 1f)), 1, 5, 1.1f);
        CriarDecoracao(new Vector3(c6, 0f, z + Random.Range(-1f, 1f)), 1, 5, 1.1f);
    }

    void CriarDecoracao(Vector3 posicaoBase, int epoca, int camada, float multiplicadorEscala)
    {
        GameObject[] prefabsEpoca = GetPrefabsEpocaPorCamada(epoca, camada);

        if (prefabsEpoca == null || prefabsEpoca.Length == 0)
        {
            CriarPrimitivoSimples(posicaoBase, epoca, multiplicadorEscala);
            return;
        }

        GameObject prefab = prefabsEpoca[Random.Range(0, prefabsEpoca.Length)];
        if (prefab == null) return;

        float offsetY = GetOffsetYEpoca(epoca);
        Vector3 posicaoFinal = new Vector3(posicaoBase.x, posicaoBase.y + offsetY, posicaoBase.z);

        GameObject obj = Instantiate(prefab, posicaoFinal, Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f));

        Vector3 escalaOriginal = prefab.transform.localScale;
        float variacao = Random.Range(0.9f, 1.1f);
        obj.transform.localScale = escalaOriginal * (multiplicadorEscala * variacao);

        foreach (Collider col in obj.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        decoracoesAtivas.Add(obj);
    }

    GameObject[] GetPrefabsEpocaPorCamada(int epoca, int camada)
    {
        switch (epoca)
        {
            case 0:
                return prefabsPreHistoria;
            case 1:
                if (camada <= 1)
                    return prefabsMedievalPerto;
                else if (camada <= 3)
                    return prefabsMedievalMeio;
                else
                    return prefabsMedievalLonge;
            case 2:
                return prefabsModerno;
            case 3:
                return prefabsFuturo;
            default:
                return prefabsPreHistoria;
        }
    }

    float GetOffsetYEpoca(int epoca)
    {
        switch (epoca)
        {
            case 0: return offsetYPreHistoria;
            case 1: return offsetYMedieval;
            case 2: return offsetYModerno;
            case 3: return offsetYFuturo;
            default: return 0f;
        }
    }

    void CriarPrimitivoSimples(Vector3 pos, int epoca, float escala)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos + Vector3.up * 0.5f;
        obj.transform.localScale = Vector3.one * escala;

        Collider col = obj.GetComponent<Collider>();
        if (col != null)
            Destroy(col);

        decoracoesAtivas.Add(obj);
    }

    public void MudarEpoca(int novaEpoca)
    {
        epocaAtual = novaEpoca;

        LimparTodasDecoracoes();

        if (jogador == null) return;

        for (int i = -4; i < numeroDecoracoes; i++)
        {
            CriarParDeDecoracoes(jogador.position.z + i * intervaloDecoracao);
        }

        zUltimaDecoracao = jogador.position.z + numeroDecoracoes * intervaloDecoracao;
    }

    public void LimparTodasDecoracoes()
    {
        foreach (GameObject dec in decoracoesAtivas)
        {
            if (dec != null)
                Destroy(dec);
        }

        decoracoesAtivas.Clear();
    }

    void OnDisable()
    {
        LimparTodasDecoracoes();
    }
}