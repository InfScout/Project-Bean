using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform CameraPos;

    private void Update()
    {
        transform.position = CameraPos.position;
    }
}
