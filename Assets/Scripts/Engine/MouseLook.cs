using UnityEngine;

namespace VoxelValley.Assets.Scripts.Engine
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 350;
        public Transform gameObjectXRotation;
        public Transform gameObjectYRotation;

        float xRotation = 0;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            gameObjectXRotation.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            gameObjectYRotation.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}

