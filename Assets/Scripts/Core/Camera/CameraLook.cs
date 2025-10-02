using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Sensibilité")]
    public float mouseSensitivity = 120f;
    public float controllerSensitivity = 90f;

    [Header("Limites verticales")]
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private float pitch = 0f; // rotation X de la caméra

    void Update()
    {
        // Récupérer les entrées souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Récupérer les entrées joystick droit (4th et 5th axis dans Input Manager)
        float joyX = Input.GetAxis("Joystick X") * controllerSensitivity * Time.deltaTime;
        float joyY = Input.GetAxis("Joystick Y") * controllerSensitivity * Time.deltaTime;

        // Combiner
        float yawInput = mouseX + joyX;
        float pitchInput = mouseY + joyY;

        // Appliquer le pitch (caméra haut/bas)
        pitch -= pitchInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Appliquer le yaw (rotation horizontale sur le parent = joueur)
        if (transform.parent != null)
        {
            transform.parent.Rotate(Vector3.up * yawInput, Space.Self);
        }
    }
}