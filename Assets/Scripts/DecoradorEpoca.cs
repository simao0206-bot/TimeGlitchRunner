using UnityEngine;
using System.Collections.Generic;

public class DecoradorEpoca : MonoBehaviour
{
    [Header("Referência")]
    public Transform jogador;

    [Header("Configuração")]
    public float distanciaLateral = 6f;
    public float intervaloDecoracao = 5f;
    public int numeroDecoracoes = 12;

    [Header("Prefabs Pré-História")]
    public GameObject[] prefabsPreHistoria;  // arrasta Tree_01, Rock_01, Bush_01, etc.

    [Header("Prefabs Medieval")]
    public GameObject[] prefabsMedieval;     // assets medievais quando tiveres

    [Header("Prefabs Moderno")]
    public GameObject[] prefabsModerno;      // assets modernos quando tiveres

    [Header("Prefabs Futuro")]
    public GameObject[] prefabsFuturo;       // assets futuristas quando tiveres

    private List<GameObject> decoracoesAtivas = new List<GameObject>();
    private float zUltimaDecoracao = 0f;
    private int epocaAtual = 0;

    void Start()
    {
        for (int i = 0; i < numeroDecoracoes; i++)
        {
            CriarParDeDecoracoes(i * intervaloDecoracao);
        }
        zUltimaDecoracao = numeroDecoracoes * intervaloDecoracao;
    }

    void Update()
    {
        if (jogador.position.z + 60f > zUltimaDecoracao)
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
            if (decoracoesAtivas[i].transform.position.z < jogador.position.z - 30f)
            {
                Destroy(decoracoesAtivas[i]);
                decoracoesAtivas.RemoveAt(i);
            }
        }
    }

    void CriarParDeDecoracoes(float z)
{
    // Camada 1 — perto do caminho (objetos pequenos)
    CriarDecoracao(new Vector3(-distanciaLateral, 0f, z), epocaAtual, 0.8f);
    CriarDecoracao(new Vector3(distanciaLateral, 0f, z), epocaAtual, 0.8f);

    // Camada 2 — meio (objetos médios)
    if (Random.value > 0.3f)
    {
        CriarDecoracao(new Vector3(-distanciaLateral - 2.5f, 0f, z + Random.Range(-1.5f, 1.5f)), epocaAtual, 1.2f);
        CriarDecoracao(new Vector3(distanciaLateral + 2.5f, 0f, z + Random.Range(-1.5f, 1.5f)), epocaAtual, 1.2f);
    }

    // Camada 3 — longe
if (Random.value > 0.5f)
{
    CriarDecoracao(new Vector3(-distanciaLateral - 8f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 2.0f);
    CriarDecoracao(new Vector3(distanciaLateral + 8f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 2.0f);
}

// Camada 4 — muito longe (tapa as bordas)
if (Random.value > 0.4f)
{
    CriarDecoracao(new Vector3(-distanciaLateral - 12f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 3.0f);
    CriarDecoracao(new Vector3(distanciaLateral + 12f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 3.0f);
}

// Camada 5 — extremo
CriarDecoracao(new Vector3(-distanciaLateral - 16f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 4.0f);
CriarDecoracao(new Vector3(distanciaLateral + 16f, 0f, z + Random.Range(-2f, 2f)), epocaAtual, 4.0f);

// Camada 6 — muro de árvores gigantes
CriarDecoracao(new Vector3(-distanciaLateral - 20f, 0f, z + Random.Range(-1f, 1f)), epocaAtual, 6.0f);
CriarDecoracao(new Vector3(distanciaLateral + 20f, 0f, z + Random.Range(-1f, 1f)), epocaAtual, 6.0f);
}

    void CriarDecoracao(Vector3 posicao, int epoca, float escala)
    {
        GameObject[] prefabsEpoca = GetPrefabsEpoca(epoca);

        if (prefabsEpoca == null || prefabsEpoca.Length == 0)
        {
            // Se não tiver prefabs, usa primitivos simples
            CriarPrimitivoSimples(posicao, epoca, escala);
            return;
        }

        // Escolhe um prefab aleatório da época
        GameObject prefab = prefabsEpoca[Random.Range(0, prefabsEpoca.Length)];
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, posicao, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        obj.transform.localScale = Vector3.one * escala * Random.Range(0.8f, 1.3f);

        // Remove colliders para não interferir com o jogo
        foreach (Collider col in obj.GetComponentsInChildren<Collider>())
            col.enabled = false;

        decoracoesAtivas.Add(obj);
    }

    GameObject[] GetPrefabsEpoca(int epoca)
    {
        switch (epoca)
        {
            case 0: return prefabsPreHistoria;
            case 1: return prefabsMedieval;
            case 2: return prefabsModerno;
            case 3: return prefabsFuturo;
            default: return prefabsPreHistoria;
        }
    }

    void CriarPrimitivoSimples(Vector3 pos, int epoca, float escala)
    {
        // Fallback simples caso não haja prefabs
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos + Vector3.up * 0.5f;
        obj.transform.localScale = Vector3.one * escala;
        Destroy(obj.GetComponent<Collider>());
        decoracoesAtivas.Add(obj);
    }

    public void MudarEpoca(int novaEpoca)
    {
        epocaAtual = novaEpoca;

        foreach (GameObject dec in decoracoesAtivas)
            if (dec != null) Destroy(dec);
        decoracoesAtivas.Clear();

        for (int i = -2; i < numeroDecoracoes; i++)
            CriarParDeDecoracoes(jogador.position.z + i * intervaloDecoracao);

        zUltimaDecoracao = jogador.position.z + numeroDecoracoes * intervaloDecoracao;
    }
}