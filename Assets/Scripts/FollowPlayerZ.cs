using UnityEngine;

public class FollowPlayerZ : MonoBehaviour
{
    public Transform jogador;

    void Update()
    {
        if (jogador == null) return;

        Vector3 pos = transform.position;
        pos.z = jogador.position.z;
        transform.position = pos;
    }
}