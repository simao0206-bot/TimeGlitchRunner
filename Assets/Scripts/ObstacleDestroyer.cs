using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private Transform jogador;

    void Start()
    {
        jogador = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Destrói se ficou para trás do jogador
        if (transform.position.z < jogador.position.z - 20f)
        {
            Destroy(gameObject);
        }

        // Destrói se caiu para fora do chão
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}