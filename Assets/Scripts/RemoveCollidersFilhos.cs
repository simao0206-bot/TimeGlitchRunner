using UnityEngine;

public class RemoveCollidersFilhos : MonoBehaviour
{
    void Start()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>(true);

        foreach (Collider c in colliders)
        {
            Destroy(c);
        }
    }
}