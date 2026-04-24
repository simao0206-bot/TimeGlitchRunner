using UnityEngine;

public class CristalSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabCristalNormal;
    public GameObject prefabCristalRaro;

    [Header("Spawning")]
    public float intervalo = 3f;
    public float chanceRaro = 0.15f;
    public float distanciaAFrente = 35f;
    public float alturaCristal = 1.4f;

    [Header("Linhas")]
    public float[] posicoesX = { -2f, 0f, 2f };

    [Header("Segurança")]
    public float raioVerificacao = 3f;

    [Header("Referência")]
    public Transform jogador;

    private float timer = 0f;

    void Update()
    {
        if (jogador == null) return;

        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            timer = 0f;
            SpawnarCristal();
        }
    }

    void SpawnarCristal()
    {
        if (prefabCristalNormal == null && prefabCristalRaro == null) return;
        if (posicoesX == null || posicoesX.Length == 0) return;

        float posX = posicoesX[Random.Range(0, posicoesX.Length)];
        Vector3 posicao = new Vector3(posX, alturaCristal, jogador.position.z + distanciaAFrente);

        Collider[] colisoes = Physics.OverlapSphere(posicao, raioVerificacao);

        foreach (Collider col in colisoes)
        {
            if (col.CompareTag("Obstaculo"))
                return;
        }

        GameObject prefabEscolhido = prefabCristalNormal;

        if (prefabCristalRaro != null && Random.value < chanceRaro)
            prefabEscolhido = prefabCristalRaro;

        if (prefabEscolhido != null)
            Instantiate(prefabEscolhido, posicao, Quaternion.identity);
    }
}