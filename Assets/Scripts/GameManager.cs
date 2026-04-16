using UnityEngine;
using UnityEngine.SceneManagement;  // necessário para reiniciar a cena
using TMPro;                         // necessário para o texto no ecrã

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPontuacao;   // texto que mostra a pontuação
    public TextMeshProUGUI textoGameOver;    // texto de Game Over
    public TextMeshProUGUI textoReiniciar;   // texto "Prima R para reiniciar"

    private float pontuacao = 0f;            // pontuação atual (tempo sobrevivido)
    private bool jogoTerminado = false;      // controla se o jogo acabou

    void Start()
    {
        // Esconde os textos de Game Over no início
        textoGameOver.gameObject.SetActive(false);
        textoReiniciar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!jogoTerminado)
        {
            // Pontuação = tempo sobrevivido em segundos
            pontuacao += Time.deltaTime;

            // Atualiza o texto no ecrã
            textoPontuacao.text = "Tempo: " + Mathf.FloorToInt(pontuacao) + "s";
        }

        // Se o jogo terminou, espera o jogador carregar R para reiniciar
        if (jogoTerminado && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Esta função é chamada por outros scripts quando o jogador morre
    public void GameOver()
    {
        jogoTerminado = true;
        textoGameOver.gameObject.SetActive(true);
        textoReiniciar.gameObject.SetActive(true);
    }
}