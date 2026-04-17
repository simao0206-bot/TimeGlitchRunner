using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Chão")]
    public GameObject prefabChao;
    public int numeroPecas = 6;            // número fixo de peças que se reciclam
    public float comprimentoPeca = 20f;    // comprimento de cada peça

    [Header("Referência")]
    public Transform jogador;

    private List<Transform> pecas = new List<Transform>();

    void Start()
    {
        // Cria as peças todas no início
        for (int i = 0; i < numeroPecas; i++)
        {
            GameObject peca = Instantiate(prefabChao, new Vector3(0, 0, i * comprimentoPeca), Quaternion.identity);
            pecas.Add(peca.transform);
        }
    }

    void Update()
    {
        // Para cada peça, verifica se ficou para trás do jogador
        foreach (Transform peca in pecas)
        {
            // Se a peça está mais de uma peça atrás do jogador, move para a frente
            if (peca.position.z < jogador.position.z - comprimentoPeca)
            {
                // Encontra a peça mais à frente
                float zMaisAFrente = GetZMaisAFrente();
                // Move esta peça para depois da mais à frente
                peca.position = new Vector3(0, 0, zMaisAFrente + comprimentoPeca);
            }
        }
    }

    float GetZMaisAFrente()
    {
        float zMax = float.MinValue;
        foreach (Transform peca in pecas)
        {
            if (peca.position.z > zMax)
                zMax = peca.position.z;
        }
        return zMax;
    }
}