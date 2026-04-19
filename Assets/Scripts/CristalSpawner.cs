using UnityEngine;

public class CristalSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabCristalNormal;
    public GameObject prefabCristalRaro;

    [Header("Spawning")]
    public float intervalo = 3f;            // segundos entre cristais
    public float chanceRaro = 0.15f;        // 15% de chance de ser raro
    public float distanciaAFrente = 25f;

    [Header("Referência")]
    public Transform jogador;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            timer = 0f;
            SpawnarCristal();
        }
    }

    void SpawnarCristal()
{
    float posX = Random.Range(-2.5f, 2.5f);
    Vector3 posicao = new Vector3(posX, 1f, jogador.position.z + distanciaAFrente);

    // Verifica se há algum obstáculo nessa posição
    Collider[] colisoes = Physics.OverlapSphere(posicao, 1.5f);
    foreach (Collider col in colisoes)
    {
        if (col.CompareTag("Obstaculo"))
        {
            // Há um obstáculo aqui — não spawna o cristal
            return;
        }
    }

    // Posição livre — spawna o cristal
    if (Random.value < chanceRaro)
        Instantiate(prefabCristalRaro, posicao, Quaternion.identity);
    else
        Instantiate(prefabCristalNormal, posicao, Quaternion.identity);
}
}