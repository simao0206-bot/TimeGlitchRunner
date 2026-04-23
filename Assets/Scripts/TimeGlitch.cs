using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TimeGlitch : MonoBehaviour
{
    [Header("Decorações")]
    public DecoradorEpoca decorador;

    [Header("Materiais dos Objetos")]
    public Material materialObstaculo;
    public Material materialPlayer;

    [Header("Materiais do Chão por Época")]
    public Material materialChaoPreHistoria;
    public Material materialChaoMedieval;
    public Material materialChaoModerno;
    public Material materialChaoFuturo;

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
    public PlayerMovement playerMovement;
    public ObstacleSpawner obstacleSpawner;

    [Header("Modelos por época")]
    public GameObject deerModel;
    public GameObject horseModel;
    public GameObject dogModel;

    [Header("Passeios Modernos")]
    public GameObject passeioEsquerdo;
    public GameObject passeioDireito;

    [Header("Cidade Moderna")]
    public GameObject cidadeModerna;

    private Color[] coresObstaculo = {
        new Color(0.5f, 0.4f, 0.3f),
        new Color(0.4f, 0.25f, 0.1f),
        new Color(0.35f, 0.35f, 0.35f),
        new Color(0.8f, 0.0f, 0.2f)
    };

    private Color[] coresPlayer = {
        new Color(0.7f, 0.5f, 0.2f),
        new Color(0.7f, 0.7f, 0.7f),
        new Color(0.45f, 0.45f, 0.45f),
        new Color(0.0f, 0.9f, 1.0f)
    };

    private Color[] coresNevoa = {
        new Color(0.8f, 0.5f, 0.2f),
        new Color(0.4f, 0.4f, 0.4f),
        new Color(0.45f, 0.45f, 0.48f),
        new Color(0.1f, 0.0f, 0.2f)
    };

    private float[] densidadeNevoa = {
        0.02f,
        0.03f,
        0.06f,
        0.04f
    };

    private float[] velocidadesMaximas = {
        12f,
        15f,
        18f,
        22f
    };

    private float[] intervalosObstaculos = {
        2.5f,
        1.6f,
        1.4f,
        1.0f
    };

    private string[] nomesEpocas = {
        "PRE-HISTORIA",
        "MEDIEVAL",
        "MODERNO",
        "FUTURO"
    };

    private string[] avisosPoeticos = {
        "O tempo desperta da poeira...",
        "A nevoa esconde o fio da espada.",
        "O betao asfixia a pressa dos passos.",
        "Apenas os circuitos sobrevivem."
    };

    private Vector3[] offsetsCamara = {
        new Vector3(0f, 4f, -7f),
        new Vector3(0f, 4f, -7f),
        new Vector3(0f, 4f, -7f),
        new Vector3(0f, 12f, -2f)
    };

    private int epocaAtual = 0;
    private AudioSource audioSource;
    private CameraFollow cameraFollow;
    private CidadeModernaSpawner cidadeModernaSpawner;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        cameraFollow = FindFirstObjectByType<CameraFollow>();

        if (cidadeModerna != null)
            cidadeModernaSpawner = cidadeModerna.GetComponent<CidadeModernaSpawner>();

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
            textoAviso.text = avisosPoeticos[proximaEpoca];
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

            painelFlash.color = new Color(0f, 0f, 0f, 1f);
            epocaAtual = (epocaAtual + 1) % nomesEpocas.Length;
            AplicarEpoca(epocaAtual);
            yield return new WaitForSeconds(0.1f);

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
        AplicarMaterialChao(indice);

        if (materialObstaculo != null)
            materialObstaculo.color = coresObstaculo[indice];

        if (materialPlayer != null)
            materialPlayer.color = coresPlayer[indice];

        RenderSettings.fog = true;
        RenderSettings.fogColor = coresNevoa[indice];
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = densidadeNevoa[indice];

        if (playerMovement != null)
            playerMovement.velocidadeMaxima = velocidadesMaximas[indice];

        if (obstacleSpawner != null)
        {
            obstacleSpawner.intervalo = intervalosObstaculos[indice];

            if (indice == 0)
                obstacleSpawner.AtivarPreHistoria();
            else if (indice == 1)
                obstacleSpawner.AtivarMedieval();
            else if (indice == 2)
                obstacleSpawner.AtivarModerno();
            else
                obstacleSpawner.AtivarFuturo();
        }

        if (cameraFollow != null)
            cameraFollow.offset = offsetsCamara[indice];

        if (textoEpoca != null)
            textoEpoca.text = nomesEpocas[indice];

        if (decorador != null)
        {
            if (indice == 0 || indice == 1)
            {
                decorador.gameObject.SetActive(true);
                decorador.MudarEpoca(indice);
            }
            else
            {
                decorador.LimparTodasDecoracoes();
                decorador.gameObject.SetActive(false);
            }
        }

        AtualizarModeloJogador(indice);

        if (passeioEsquerdo != null)
            passeioEsquerdo.SetActive(indice == 2);

        if (passeioDireito != null)
            passeioDireito.SetActive(indice == 2);

        if (cidadeModerna != null)
        {
            if (indice == 2)
            {
                cidadeModerna.SetActive(true);
                 // força reposicionamento imediato
                BuildingLoopRowIrregular[] loops = cidadeModerna.GetComponentsInChildren<BuildingLoopRowIrregular>();

                 foreach (var loop in loops)
             {
              loop.ReposicionarParaFrente();
              }
            }
            else
            {
                if (cidadeModernaSpawner != null)
                    cidadeModernaSpawner.LimparTudo();

                cidadeModerna.SetActive(false);
            }
        }

        GameObject[] obstaculos = GameObject.FindGameObjectsWithTag("Obstaculo");
        foreach (GameObject obj in obstaculos)
        {
            Destroy(obj);
        }
    }

    void AplicarMaterialChao(int indice)
    {
        Material materialEscolhido = null;

        switch (indice)
        {
            case 0:
                materialEscolhido = materialChaoPreHistoria;
                break;
            case 1:
                materialEscolhido = materialChaoMedieval;
                break;
            case 2:
                materialEscolhido = materialChaoModerno;
                break;
            case 3:
                materialEscolhido = materialChaoFuturo;
                break;
        }

        if (materialEscolhido == null)
            return;

        Renderer[] todosOsRenderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);

        foreach (Renderer rend in todosOsRenderers)
        {
            if (rend == null) continue;

            string nomeObj = rend.gameObject.name.ToLower();
            string nomePai = rend.transform.root.name.ToLower();

            if (nomeObj.Contains("ground") || nomePai.Contains("ground"))
                rend.material = materialEscolhido;
        }
    }

    void AtualizarModeloJogador(int indice)
    {
        if (deerModel != null)
            deerModel.SetActive(indice == 0);

        if (horseModel != null)
            horseModel.SetActive(indice == 1);

        if (dogModel != null)
            dogModel.SetActive(indice == 2 || indice == 3);
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