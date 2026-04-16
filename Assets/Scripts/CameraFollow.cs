using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo a seguir")]
    public Transform alvo;              // o objeto Player

    [Header("Offset da câmara")]
    public Vector3 offset = new Vector3(0f, 4f, -7f); // posição relativa ao jogador

    public float velocidadeSeguir = 10f; // suavidade do movimento

    void LateUpdate()
    {
        // LateUpdate corre depois do FixedUpdate — ideal para câmaras
        // para garantir que o jogador já se moveu antes de a câmara atualizar

        if (alvo == null) return; // segurança: se não tiver alvo, não faz nada

        // Posição desejada = posição do jogador + offset
        Vector3 posicaoDesejada = alvo.position + offset;

        // Move a câmara suavemente para a posição desejada
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, velocidadeSeguir * Time.deltaTime);

        // A câmara olha sempre para o jogador
        transform.LookAt(alvo);
    }
}