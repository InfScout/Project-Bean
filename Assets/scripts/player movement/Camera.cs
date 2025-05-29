using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float sense = 350;
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerRotate;
    private float xRotation;
    private float yRotation;
        
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Cam();
    }
    private void Cam()
    {
        float mouseX = Input.GetAxis("Mouse X") * sense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sense * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        target.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        playerRotate.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
