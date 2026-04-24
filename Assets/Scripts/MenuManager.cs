using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject painelControlos;

    void Start()
    {
        PlayerPrefs.DeleteKey("ModoJogo");
    }

    public void IniciarModoCronologico()
    {
        PlayerPrefs.SetString("ModoJogo", "Cronologico");
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene");
    }

    public void IniciarModoAnomalia()
    {
        PlayerPrefs.SetString("ModoJogo", "Anomalia");
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene");
    }

    public void MostrarControlos()
    {
        if (painelControlos != null)
            painelControlos.SetActive(true);
    }

    public void FecharControlos()
    {
        if (painelControlos != null)
            painelControlos.SetActive(false);
    }

    public void SairDoJogo()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}