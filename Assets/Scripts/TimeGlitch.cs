using UnityEngine;
using TMPro;

public class TimeGlitch : MonoBehaviour
{
    [Header("Épocas")]
    public Material materialChao;           // material do chão
    public Material materialObstaculo;      // material dos obstáculos
    public Material materialPlayer;         // material do jogador

    [Header("Duração de cada época")]
    public float duracaoEpoca = 15f;        // segundos por época

    [Header("UI")]
    public TextMeshProUGUI textoEpoca;      // texto que mostra a época atual

    // Cores para cada época
    private Color[] coresChao = {
        new Color(0.4f, 0.3f, 0.1f),       // Pré-história (castanho)
        new Color(0.2f, 0.3f, 0.1f),       // Medieval (verde escuro)
        new Color(0.3f, 0.3f, 0.3f),       // Moderno (cinzento)
        new Color(0.0f, 0.2f, 0.4f)        // Futuro (azul escuro)
    };

    private Color[] coresObstaculo = {
        new Color(0.6f, 0.2f, 0.0f),       // Pré-história (laranja escuro)
        new Color(0.5f, 0.4f, 0.1f),       // Medieval (dourado)
        new Color(0.8f, 0.0f, 0.0f),       // Moderno (vermelho)
        new Color(0.0f, 0.8f, 0.8f)        // Futuro (ciano)
    };

    private Color[] coresPlayer = {
        new Color(0.8f, 0.6f, 0.2f),       // Pré-história (bege)
        new Color(0.6f, 0.6f, 0.6f),       // Medieval (prata)
        new Color(0.2f, 0.4f, 0.8f),       // Moderno (azul)
        new Color(0.0f, 1.0f, 0.6f)        // Futuro (verde néon)
    };

    private string[] nomesEpocas = {
        "PRÉ-HISTÓRIA",
        "MEDIEVAL",
        "MODERNO",
        "FUTURO"
    };

    private int epocaAtual = 0;
    private float timerEpoca = 0f;

    void Update()
    {
        timerEpoca += Time.deltaTime;

        // Quando o tempo da época acabar, muda para a próxima
        if (timerEpoca >= duracaoEpoca)
        {
            timerEpoca = 0f;
            MudarEpoca();
        }
    }

    void MudarEpoca()
    {
        // Avança para a próxima época (volta ao início quando chega ao fim)
        epocaAtual = (epocaAtual + 1) % nomesEpocas.Length;

        // Muda as cores dos materiais
        materialChao.color = coresChao[epocaAtual];
        materialObstaculo.color = coresObstaculo[epocaAtual];
        materialPlayer.color = coresPlayer[epocaAtual];

        // Atualiza o texto da época
        if (textoEpoca != null)
            textoEpoca.text = nomesEpocas[epocaAtual];

        Debug.Log("Época mudou para: " + nomesEpocas[epocaAtual]);
    }

    // Define a época inicial no arranque
    void Start()
    {
        materialChao.color = coresChao[0];
        materialObstaculo.color = coresObstaculo[0];
        materialPlayer.color = coresPlayer[0];

        if (textoEpoca != null)
            textoEpoca.text = nomesEpocas[0];
    }
}