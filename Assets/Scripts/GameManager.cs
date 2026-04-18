using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPontuacao;
    public TextMeshProUGUI textoGameOver;
    public TextMeshProUGUI textoReiniciar;
    public TextMeshProUGUI textoDistorcao;  // mostra "DISTORCAO TEMPORAL!" quando ativo

    private float pontuacao = 0f;
    private int pontosExtras = 0;           // pontos dos cristais
    private bool jogoTerminado = false;

    void Start()
    {
        textoGameOver.gameObject.SetActive(false);
        textoReiniciar.gameObject.SetActive(false);

        if (textoDistorcao != null)
            textoDistorcao.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!jogoTerminado)
        {
            pontuacao += Time.deltaTime;
            int total = Mathf.FloorToInt(pontuacao) + pontosExtras;
            textoPontuacao.text = "Pontos: " + total;
        }

        if (jogoTerminado && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;            // garante que o tempo volta ao normal
            SceneManager.LoadScene("MenuScene");
        }
    }

    // Chamado pelos cristais
    public void AdicionarPontos(int pontos)
    {
        pontosExtras += pontos;
    }

    // Chamado pelo cristal raro
    public void IniciarDistorcaoTemporal(float duracao, float escala)
    {
        StartCoroutine(DistorcaoTemporal(duracao, escala));
    }

    System.Collections.IEnumerator DistorcaoTemporal(float duracao, float escala)
    {
        // Abranda o tempo
        Time.timeScale = escala;

        if (textoDistorcao != null)
        {
            textoDistorcao.gameObject.SetActive(true);
            textoDistorcao.text = "DISTORCAO TEMPORAL!";
        }

        yield return new WaitForSecondsRealtime(duracao);

        // Volta ao normal
        Time.timeScale = 1f;

        if (textoDistorcao != null)
            textoDistorcao.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        jogoTerminado = true;
        textoGameOver.gameObject.SetActive(true);
        textoReiniciar.gameObject.SetActive(true);

        // Para o glitch
        TimeGlitch tg = FindFirstObjectByType<TimeGlitch>();
        if (tg != null) tg.PararGlitch();
    }
}