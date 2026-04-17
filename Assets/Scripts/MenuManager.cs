using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button botaoCronologico;
    public Button botaoAnomalia;

    void Start()
    {
        // Liga os botões via código — sem precisar do Inspector
        botaoCronologico.onClick.AddListener(IniciarCronologico);
        botaoAnomalia.onClick.AddListener(IniciarAnomalia);
    }

    void IniciarCronologico()
    {
        PlayerPrefs.SetInt("ModoJogo", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    void IniciarAnomalia()
    {
        PlayerPrefs.SetInt("ModoJogo", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}