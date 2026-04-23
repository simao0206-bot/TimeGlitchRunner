using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Som")]
    public AudioClip somColisao;
    public float volumeColisao = 1.0f;      // volume do som de colisão

    private GameManager gameManager;
    private bool jaMorreu = false;
    private AudioSource musicaFundo;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        // Encontra qualquer AudioSource na cena que esteja em loop (a música)
            AudioSource[] todosAudios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource audio in todosAudios)
{
    if (audio.loop)
    {
        musicaFundo = audio;
        break;
    }
}
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") && !jaMorreu)
        {
            jaMorreu = true;

            // Para a música de fundo completamente
            if (musicaFundo != null)
                musicaFundo.Stop();

            // Toca o som de colisão bem alto
            AudioSource.PlayClipAtPoint(somColisao, Camera.main.transform.position, volumeColisao);

            // Para o jogador
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;

            // Game Over
           gameManager.GameOver();

        }
    }
}