using UnityEngine;

public class InputController : BaseController<InputController>
{
    public delegate void HorizontalMovement(float value);
    public event HorizontalMovement OnHorizontalMovement;

    public delegate void VerticalMovement(float value);
    public event VerticalMovement OnVerticalMovement;

    public delegate void LookInput(float x, float y);
    public event LookInput OnLook;

    public delegate void Shoot();
    public event Shoot OnShoot;

    [Header("État d’input")]
    public bool isUsingController = false;  // true = manette, false = clavier/souris
    public float inputSwitchCooldown = 0.2f;

    private float lastInputTime = 0f;

    void Update()
    {
        // Détecte device actif
        DetectInputDevice();

        // Route les inputs selon l’appareil
        if (isUsingController)
            HandleControllerInputs();
        else
            HandleKeyboardMouseInputs();
    }

    void DetectInputDevice()
    {
        float now = Time.time;

        // --- Détection souris/clavier ---
        if (Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0.01f || Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0.01f
            || Input.anyKeyDown) // touches clavier
        {
            if (isUsingController && now - lastInputTime > inputSwitchCooldown)
            {
                isUsingController = false;
                Debug.Log("🔄 Input changé -> Clavier/Souris");
            }
            lastInputTime = now;
        }

        // --- Détection manette ---
        if (Mathf.Abs(Input.GetAxis("Joystick X")) > 0.1f || Mathf.Abs(Input.GetAxis("Joystick Y")) > 0.1f
            || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            if (!isUsingController && now - lastInputTime > inputSwitchCooldown)
            {
                isUsingController = true;
                Debug.Log("🔄 Input changé -> Manette");
            }
            lastInputTime = now;
        }
    }

    void HandleKeyboardMouseInputs()
    {
        // Déplacement clavier
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        OnHorizontalMovement?.Invoke(inputX);
        OnVerticalMovement?.Invoke(inputZ);

        // Look souris
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0) OnLook?.Invoke(mouseX, mouseY);

        // Tir
        if (Input.GetButtonDown("Fire1")) OnShoot?.Invoke();
    }

    void HandleControllerInputs()
    {
        // Déplacement stick gauche
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        OnHorizontalMovement?.Invoke(inputX);
        OnVerticalMovement?.Invoke(inputZ);

        // Look stick droit
        float joyX = Input.GetAxis("Joystick X");
        float joyY = Input.GetAxis("Joystick Y");
        if (Mathf.Abs(joyX) > 0.1f || Mathf.Abs(joyY) > 0.1f) OnLook?.Invoke(joyX, joyY);

        // Tir (trigger ou bouton manette mappé à Fire1)
        if (Input.GetButtonDown("Fire1")) OnShoot?.Invoke();
    }
}