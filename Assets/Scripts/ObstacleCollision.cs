using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        // Debug: verifica se encontrou o GameManager
        if (gameManager == null)
            Debug.LogError("GameManager NAO encontrado!");
        else
            Debug.Log("GameManager encontrado com sucesso!");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug: mostra com o que colidiu
        Debug.Log("Colisao com: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);

        if (collision.gameObject.name.Contains("Obstaculo"))
        {
            Debug.Log("Tag Obstacle confirmada - chamando GameOver!");

            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;

            gameManager.GameOver();
        }
    }
}