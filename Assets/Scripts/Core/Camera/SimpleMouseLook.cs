using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    [Header("Sensibilité")]
    public float mouseSensitivity = 100f;     // sensibilité souris
    public float controllerSensitivity = 80f; // sensibilité joystick

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputController.Instance.OnLook += OnLook;
    }

    void OnDisable()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnLook -= OnLook;
    }

    void OnLook(float rawX, float rawY)
    {
        float sens = InputController.Instance.isUsingController ? controllerSensitivity : mouseSensitivity;

        float lookX = rawX * sens * Time.deltaTime;
        float lookY = rawY * sens * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + lookX, 0f);
    }
}