using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo a seguir")]
    public Transform alvo;

    [Header("Offset da câmara")]
    public Vector3 offset = new Vector3(0f, 4f, -7f);

    public float velocidadeSeguir = 8f;

    private Vector3 velocidadeAtual = Vector3.zero;

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 posicaoDesejada = alvo.position + offset;

        // SmoothDamp é muito mais suave que Lerp — elimina vibrações
        transform.position = Vector3.SmoothDamp(
            transform.position,
            posicaoDesejada,
            ref velocidadeAtual,
            0.1f
        );

        transform.LookAt(alvo);
    }
}