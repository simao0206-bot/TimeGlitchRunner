using UnityEngine;

public class FollowPlayerZComOffset : MonoBehaviour
{
    public Transform jogador;
    public float offsetZ = -120f;

    void Update()
    {
        if (jogador == null) return;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            jogador.position.z + offsetZ
        );
    }
}