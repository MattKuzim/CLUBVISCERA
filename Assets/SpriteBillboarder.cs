using UnityEngine;

public class SpriteBillboarder : MonoBehaviour
{
    void FixedUpdate()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform.position);
        }
    }
}