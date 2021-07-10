using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    [SerializeField] private float mouseSensitivity = 0;
    private float xRotate = 0.0f; 
    private float yRotate = 0.0f;

    // References
    private Transform parent;

    // Start is called before the first frame update
    private void Start()
    {
        parent = transform.parent;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCharacterHorizontally();
        //RotateCameraVertically();
    }

    private void RotateCharacterHorizontally()
    {
        xRotate = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, xRotate);
    }

    private void RotateCameraVertically()
    {
        yRotate += Input.GetAxis("Mouse Y") * (mouseSensitivity / 10000);
        transform.Rotate(Vector3.left, yRotate);
    }
}
