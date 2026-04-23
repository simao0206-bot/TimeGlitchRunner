using System.Collections.Generic;
using UnityEngine;

public class BuildingLoopRowIrregular : MonoBehaviour
{
    public Transform jogador;

    public float distanciaParaReciclar = 40f;

    private List<Transform> predios = new List<Transform>();
    private float distanciaMedia = 15f;

    void Start()
    {
        RecolherFilhos();
        OrdenarPorZ();
        CalcularDistanciaMedia();
    }

    void Update()
    {
        if (jogador == null || predios.Count == 0) return;

        // recicla TODOS os que estiverem atrás (não só 1)
        while (predios[0].position.z < jogador.position.z - distanciaParaReciclar)
        {
            Transform primeiro = predios[0];
            Transform ultimo = predios[predios.Count - 1];

            float novoZ = ultimo.position.z + distanciaMedia;

            Vector3 pos = primeiro.position;
            pos.z = novoZ;
            primeiro.position = pos;

            predios.RemoveAt(0);
            predios.Add(primeiro);
        }
    }

    void RecolherFilhos()
    {
        predios.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            predios.Add(transform.GetChild(i));
        }
    }

    void OrdenarPorZ()
    {
        predios.Sort((a, b) => a.position.z.CompareTo(b.position.z));
    }

    void CalcularDistanciaMedia()
    {
        if (predios.Count < 2) return;

        float total = 0f;

        for (int i = 0; i < predios.Count - 1; i++)
        {
            total += predios[i + 1].position.z - predios[i].position.z;
        }

        distanciaMedia = total / (predios.Count - 1);
    }

    public void ReposicionarParaFrente()
    {
        if (jogador == null || predios.Count == 0) return;

        OrdenarPorZ();
        CalcularDistanciaMedia();

        float zBase = jogador.position.z + 30f;

        for (int i = 0; i < predios.Count; i++)
        {
            Vector3 pos = predios[i].position;
            pos.z = zBase + (i * distanciaMedia);
            predios[i].position = pos;
        }

        OrdenarPorZ();
    }
}