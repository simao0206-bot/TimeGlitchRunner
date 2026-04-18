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
    public float duracaoMinima = 15f;       // tempo mínimo entre glitches
    public float duracaoMaxima = 30f;       // tempo máximo entre glitches

    [Header("UI")]
    public TextMeshProUGUI textoEpoca;      // texto da época atual
    public TextMeshProUGUI textoAviso;      // texto do aviso poético
    public Image painelFlash;              // painel branco para o flash

    [Header("Som")]
    public AudioClip somGlitch;            // SFX de estática

    // Cores por época
    private Color[] coresChao = {
        new Color(0.4f, 0.3f, 0.1f),       // Pré-história
        new Color(0.2f, 0.3f, 0.1f),       // Medieval
        new Color(0.3f, 0.3f, 0.3f),       // Moderno
        new Color(0.0f, 0.2f, 0.4f)        // Futuro
    };

    private Color[] coresObstaculo = {
        new Color(0.6f, 0.2f, 0.0f),
        new Color(0.5f, 0.4f, 0.1f),
        new Color(0.8f, 0.0f, 0.0f),
        new Color(0.0f, 0.8f, 0.8f)
    };

    private Color[] coresPlayer = {
        new Color(0.8f, 0.6f, 0.2f),
        new Color(0.6f, 0.6f, 0.6f),
        new Color(0.2f, 0.4f, 0.8f),
        new Color(0.0f, 1.0f, 0.6f)
    };

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

    private int epocaAtual = 0;
    private AudioSource audioSource;

    public void PararGlitch()
    {
    StopAllCoroutines();
    if (textoAviso != null)
        textoAviso.gameObject.SetActive(false);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Aplica época inicial
        AplicarEpoca(0);

        if (textoAviso != null)
            textoAviso.gameObject.SetActive(false);

        if (painelFlash != null)
            painelFlash.gameObject.SetActive(false);

        // Inicia o ciclo de glitches
        StartCoroutine(CicloGlitch());
    }

    IEnumerator CicloGlitch()
    {
        while (true)
        {
            // Espera um tempo aleatório
            float espera = Random.Range(duracaoMinima, duracaoMaxima);
            yield return new WaitForSeconds(espera - 3f);  // 3s antes do glitch

            // Aviso 3 segundos antes
            yield return StartCoroutine(AvisoPreGlitch());

            // Glitch!
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

    // Pisca 3 vezes durante 3 segundos
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
    // Som de estática
    if (somGlitch != null && audioSource != null)
    {
        audioSource.PlayOneShot(somGlitch);
        Invoke("PararSomGlitch", 1f);
    }

    if (painelFlash != null)
    {
        painelFlash.gameObject.SetActive(true);

        // Fase 1 — Glitch rápido (0.6s total)
        for (int i = 0; i < 6; i++)
        {
            // Cor aleatória tipo glitch digital
            float r = Random.Range(0f, 0.3f);
            float g = Random.Range(0f, 0.3f);
            float b = Random.Range(0.3f, 1f);
            float alpha = Random.Range(0.3f, 0.8f);
            painelFlash.color = new Color(r, g, b, alpha);
            yield return new WaitForSeconds(0.05f);

            painelFlash.color = new Color(0f, 0f, 0f, Random.Range(0.4f, 0.9f));
            yield return new WaitForSeconds(0.05f);
        }

        // Fase 2 — Preto total por 0.1s (muda a época aqui)
        painelFlash.color = new Color(0f, 0f, 0f, 1f);
        epocaAtual = (epocaAtual + 1) % nomesEpocas.Length;
        AplicarEpoca(epocaAtual);
        yield return new WaitForSeconds(0.1f);

        // Fase 3 — Mais glitch depois da mudança
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

        // Fade out final
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

void PararSomGlitch()
{
    if (audioSource != null)
        audioSource.Stop();
}

    void AplicarEpoca(int indice)
    {
        materialChao.color = coresChao[indice];
        materialObstaculo.color = coresObstaculo[indice];
        materialPlayer.color = coresPlayer[indice];

        if (textoEpoca != null)
            textoEpoca.text = nomesEpocas[indice];
    }
}