using UnityEngine;
using System.Collections.Generic;

public class CidadeModernaSpawner : MonoBehaviour
{
    [Header("Referência")]
    public Transform jogador;

    [Header("Prefabs do lado esquerdo (já orientados)")]
    public GameObject[] prediosEsquerda;

    [Header("Prefabs do lado direito (já orientados)")]
    public GameObject[] prediosDireita;

    [Header("Configuração")]
    public float distanciaSpawn = 160f;
    public float distanciaEntre = 14f;

    [Header("Posições laterais")]
    public float xEsquerda = -14f;
    public float xDireita = 14f;

    private float zUltimoSpawn = 0f;
    private List<GameObject> ativos = new List<GameObject>();

    void Start()
    {
        if (jogador == null) return;

        LimparTudo();
        zUltimoSpawn = jogador.position.z;

        for (int i = 0; i < 12; i++)
        {
            SpawnLinha(zUltimoSpawn);
            zUltimoSpawn += distanciaEntre;
        }
    }

    void Update()
    {
        if (jogador == null) return;

        if (jogador.position.z + distanciaSpawn > zUltimoSpawn)
        {
            SpawnLinha(zUltimoSpawn);
            zUltimoSpawn += distanciaEntre;
        }

        LimparAntigos();
    }

    void SpawnLinha(float z)
    {
        SpawnPredioEsquerda(z);
        SpawnPredioDireita(z);
    }

    void SpawnPredioEsquerda(float z)
    {
        if (prediosEsquerda == null || prediosEsquerda.Length == 0) return;

        GameObject prefab = prediosEsquerda[Random.Range(0, prediosEsquerda.Length)];
        if (prefab == null) return;

        Vector3 pos = new Vector3(xEsquerda, 0f, z);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        foreach (Collider c in obj.GetComponentsInChildren<Collider>())
            c.enabled = false;

        ativos.Add(obj);
    }

    void SpawnPredioDireita(float z)
    {
        if (prediosDireita == null || prediosDireita.Length == 0) return;

        GameObject prefab = prediosDireita[Random.Range(0, prediosDireita.Length)];
        if (prefab == null) return;

        Vector3 pos = new Vector3(xDireita, 0f, z);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        foreach (Collider c in obj.GetComponentsInChildren<Collider>())
            c.enabled = false;

        ativos.Add(obj);
    }

    void LimparAntigos()
    {
        for (int i = ativos.Count - 1; i >= 0; i--)
        {
            if (ativos[i] == null)
            {
                ativos.RemoveAt(i);
                continue;
            }

            if (ativos[i].transform.position.z < jogador.position.z - 60f)
            {
                Destroy(ativos[i]);
                ativos.RemoveAt(i);
            }
        }
    }

    public void LimparTudo()
    {
        for (int i = ativos.Count - 1; i >= 0; i--)
        {
            if (ativos[i] != null)
                Destroy(ativos[i]);
        }

        ativos.Clear();
    }

    void OnDisable()
    {
        LimparTudo();
    }
}