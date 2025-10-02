using Data.Player;
using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputController.Instance.OnLook += OnLook;
    }

    void OnDisable()
    {
        // Sometimes, we don't really know why but this shit throw an error, I hate that shit so I put a null
        // validation
        if (InputController.Instance != null)
            InputController.Instance.OnLook -= OnLook;
    }

    void OnLook(float rawX, float rawY)
    {
        float sens = InputController.Instance.isUsingController ? PlayerSettingsData.Instance.controllerSensitivity : (PlayerSettingsData.Instance.mouseSensitivity * 100f);

        float lookX = rawX * sens * Time.deltaTime;
        float lookY = rawY * sens * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + lookX, 0f);
    }
}