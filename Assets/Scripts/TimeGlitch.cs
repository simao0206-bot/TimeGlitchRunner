using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TimeGlitch : MonoBehaviour
{
    [Header("Materiais")]
    public Material materialChao;
    public Material materialObstaculo;
    public Material materialPlayer;

    [Header("Épocas")]
    public float duracaoMinima = 18f;
    public float duracaoMaxima = 25f;

    [Header("UI")]
    public TextMeshProUGUI textoEpoca;
    public TextMeshProUGUI textoAviso;
    public Image painelFlash;

    [Header("Som")]
    public AudioClip somGlitch;

    [Header("Referências")]
    public PlayerMovement playerMovement;   // para ajustar velocidade por época
    public ObstacleSpawner obstacleSpawner; // para ajustar intervalo por época

    // =================== CORES POR ÉPOCA ===================
    private Color[] coresChao = {
        new Color(0.4f, 0.3f, 0.1f),       // Pré-história (terra)
        new Color(0.35f, 0.35f, 0.35f),     // Medieval (pedra)
        new Color(0.2f, 0.2f, 0.2f),        // Moderno (alcatrão)
        new Color(0.05f, 0.05f, 0.15f)      // Futuro (metal escuro)
    };

    private Color[] coresObstaculo = {
        new Color(0.5f, 0.4f, 0.3f),        // Pré-história (rocha)
        new Color(0.4f, 0.25f, 0.1f),       // Medieval (madeira)
        new Color(0.9f, 0.4f, 0.0f),        // Moderno (cone laranja)
        new Color(0.8f, 0.0f, 0.2f)         // Futuro (laser vermelho)
    };

    private Color[] coresPlayer = {
        new Color(0.7f, 0.5f, 0.2f),        // Pré-história (bege)
        new Color(0.7f, 0.7f, 0.7f),        // Medieval (prata)
        new Color(0.1f, 0.4f, 0.9f),        // Moderno (azul)
        new Color(0.0f, 0.9f, 1.0f)         // Futuro (ciano néon)
    };

    // =================== NÉVOA POR ÉPOCA ===================
    private Color[] coresNevoa = {
        new Color(0.7f, 0.5f, 0.3f),        // Pré-história (laranja)
        new Color(0.4f, 0.4f, 0.4f),        // Medieval (cinzento denso)
        new Color(0.5f, 0.5f, 0.5f),        // Moderno (poluição)
        new Color(0.1f, 0.0f, 0.2f)         // Futuro (roxo escuro)
    };

    private float[] densidadeNevoa = {
        0.02f,                               // Pré-história (fraca)
        0.06f,                               // Medieval (muito densa!)
        0.03f,                               // Moderno (média)
        0.04f                                // Futuro (média/escura)
    };

    // =================== CÉU POR ÉPOCA ===================
    private Color[] coresCeu = {
        new Color(0.8f, 0.5f, 0.2f),        // Pré-história (laranja)
        new Color(0.3f, 0.3f, 0.4f),        // Medieval (nublado)
        new Color(0.4f, 0.4f, 0.5f),        // Moderno (cinzento urbano)
        new Color(0.02f, 0.0f, 0.08f)       // Futuro (preto/roxo)
    };

    // =================== VELOCIDADE POR ÉPOCA ===================
    private float[] velocidadesMaximas = {
        12f,                                 // Pré-história (lento)
        15f,                                 // Medieval (médio)
        18f,                                 // Moderno (rápido)
        22f                                  // Futuro (muito rápido)
    };

    private float[] intervalosObstaculos = {
        2.5f,                                // Pré-história (espaçado)
        2.0f,                                // Medieval
        1.5f,                                // Moderno
        1.0f                                 // Futuro (muito frequente)
    };

    // =================== TEXTOS ===================
    private string[] nomesEpocas = {
        "PRE-HISTORIA",
        "MEDIEVAL",
        "MODERNO",
        "FUTURO"
    };

    private string[] avisosPoéticos = {
        "O tempo desperta da poeira...",
        "A nevoa esconde o fio da espada.",
        "O betao asfixia a pressa dos passos.",
        "Apenas os circuitos sobrevivem."
    };

    // =================== CÂMERA ===================
    private Vector3[] offsetsCamara = {
        new Vector3(0f, 4f, -7f),            // Pré-história (normal)
        new Vector3(0f, 4f, -7f),            // Medieval (normal)
        new Vector3(0f, 4f, -7f),            // Moderno (normal)
        new Vector3(0f, 12f, -2f)            // Futuro (quase Top-Down)
    };

    private int epocaAtual = 0;
    private AudioSource audioSource;
    private Camera camaraMain;
    private CameraFollow cameraFollow;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        camaraMain = Camera.main;
        cameraFollow = FindFirstObjectByType<CameraFollow>();

        // Aplica época inicial
        AplicarEpoca(0);

        if (textoAviso != null)
            textoAviso.gameObject.SetActive(false);

        if (painelFlash != null)
            painelFlash.gameObject.SetActive(false);

        StartCoroutine(CicloGlitch());
    }

    IEnumerator CicloGlitch()
    {
        while (true)
        {
            float espera = Random.Range(duracaoMinima, duracaoMaxima);
            yield return new WaitForSeconds(espera - 3f);
            yield return StartCoroutine(AvisoPreGlitch());
            yield return StartCoroutine(ExecutarGlitch());
        }
    }

    IEnumerator AvisoPreGlitch()
    {
        int proximaEpoca = (epocaAtual + 1) % nomesEpocas.Length;

        if (textoAviso != null)
        {
            textoAviso.gameObject.SetActive(true);
            textoAviso.text = avisosPoéticos[proximaEpoca];
            textoAviso.color = Color.red;
            textoAviso.enabled = true;
        }

        for (int i = 0; i < 3; i++)
        {
            if (textoAviso != null) textoAviso.enabled = false;
            yield return new WaitForSeconds(0.4f);
            if (textoAviso != null) textoAviso.enabled = true;
            yield return new WaitForSeconds(0.6f);
        }

        if (textoAviso != null)
            textoAviso.gameObject.SetActive(false);
    }

    IEnumerator ExecutarGlitch()
    {
        if (somGlitch != null && audioSource != null)
        {
            audioSource.PlayOneShot(somGlitch);
            Invoke("PararSomGlitch", 1f);
        }

        if (painelFlash != null)
        {
            painelFlash.gameObject.SetActive(true);

            // Glitch digital
            for (int i = 0; i < 6; i++)
            {
                float r = Random.Range(0f, 0.3f);
                float g = Random.Range(0f, 0.3f);
                float b = Random.Range(0.3f, 1f);
                painelFlash.color = new Color(r, g, b, Random.Range(0.3f, 0.8f));
                yield return new WaitForSeconds(0.05f);
                painelFlash.color = new Color(0f, 0f, 0f, Random.Range(0.4f, 0.9f));
                yield return new WaitForSeconds(0.05f);
            }

            // Preto total — muda época aqui
            painelFlash.color = new Color(0f, 0f, 0f, 1f);
            epocaAtual = (epocaAtual + 1) % nomesEpocas.Length;
            AplicarEpoca(epocaAtual);
            yield return new WaitForSeconds(0.1f);

            // Mais glitch depois
            for (int i = 0; i < 4; i++)
            {
                float r = Random.Range(0f, 0.3f);
                float g = Random.Range(0f, 0.3f);
                float b = Random.Range(0.3f, 1f);
                painelFlash.color = new Color(r, g, b, Random.Range(0.2f, 0.6f));
                yield return new WaitForSeconds(0.05f);
                painelFlash.color = new Color(0f, 0f, 0f, Random.Range(0.1f, 0.4f));
                yield return new WaitForSeconds(0.05f);
            }

            // Fade out
            float t = 0.8f;
            while (t > 0f)
            {
                t -= Time.deltaTime * 4f;
                painelFlash.color = new Color(0f, 0f, 0f, Mathf.Clamp01(t));
                yield return null;
            }

            painelFlash.gameObject.SetActive(false);
        }
        else
        {
            epocaAtual = (epocaAtual + 1) % nomesEpocas.Length;
            AplicarEpoca(epocaAtual);
        }
    }

    void AplicarEpoca(int indice)
    {
        // 1. Materiais
        materialChao.color = coresChao[indice];
        materialObstaculo.color = coresObstaculo[indice];
        materialPlayer.color = coresPlayer[indice];

        // 2. Névoa
        RenderSettings.fog = true;
        RenderSettings.fogColor = coresNevoa[indice];
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = densidadeNevoa[indice];

        // 3. Cor do céu
        if (camaraMain != null)
        {
            camaraMain.clearFlags = CameraClearFlags.SolidColor;
            camaraMain.backgroundColor = coresCeu[indice];
        }

        // 4. Velocidade do jogador
        if (playerMovement != null)
            playerMovement.velocidadeMaxima = velocidadesMaximas[indice];

        // 5. Frequência dos obstáculos
        if (obstacleSpawner != null)
            obstacleSpawner.intervalo = intervalosObstaculos[indice];

        // 6. Câmera — muda offset por época
        if (cameraFollow != null)
            cameraFollow.offset = offsetsCamara[indice];

        // 7. Atualiza texto da época
        if (textoEpoca != null)
            textoEpoca.text = nomesEpocas[indice];
    }

    public void PararGlitch()
    {
        StopAllCoroutines();
        if (textoAviso != null)
            textoAviso.gameObject.SetActive(false);
    }

    void PararSomGlitch()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}