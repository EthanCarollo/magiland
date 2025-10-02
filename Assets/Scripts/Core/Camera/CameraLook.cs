using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // Sensibilité de la souris
    private float xRotation = 0f;

    void Start()
    {
        // Bloquer et cacher le curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Récupérer le mouvement de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Debug.Log(Input.GetAxis("Mouse X"));
        
        // Rotation verticale (haut/bas)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // empêche de retourner la caméra

        // Appliquer la rotation
        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + mouseX, 0f);
    }
}