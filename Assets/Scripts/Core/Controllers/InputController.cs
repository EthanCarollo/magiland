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

    public delegate void Interact();
    public event Interact OnInteract;

    public delegate void Pause();
    public event Pause OnPause;

    [Header("Input State")]
    public bool isUsingController = false;  // true = controller, false = keyboard/mouse
    public float inputSwitchCooldown = 0.2f;
    private float lastInputTime = 0f;


    void Update()
    {
        DetectInputDevice();

        if (isUsingController)
            HandleControllerInputs();
        else
            HandleKeyboardMouseInputs();
    }

    void DetectInputDevice()
    {
        float now = Time.time;

        if (Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0.01f || Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0.01f
            || Input.anyKeyDown)
        {
            if (isUsingController && now - lastInputTime > inputSwitchCooldown)
            {
                isUsingController = false;
                Debug.Log("🔄 Input switched -> Keyboard/Mouse");
            }
            lastInputTime = now;
        }

        if (Mathf.Abs(Input.GetAxis("Joystick X")) > 0.1f || Mathf.Abs(Input.GetAxis("Joystick Y")) > 0.1f
            || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            if (!isUsingController && now - lastInputTime > inputSwitchCooldown)
            {
                isUsingController = true;
                Debug.Log("🔄 Input switched -> Controller");
            }
            lastInputTime = now;
        }
    }

    void HandleKeyboardMouseInputs()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        OnHorizontalMovement?.Invoke(inputX);
        OnVerticalMovement?.Invoke(inputZ);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0) OnLook?.Invoke(mouseX, mouseY);

        if (Input.GetButtonDown("Fire1")) OnShoot?.Invoke();

        if (Input.GetKeyDown(KeyCode.E)) OnInteract?.Invoke();

        if (Input.GetButtonDown("Pause")) OnPause?.Invoke();
    }

    void HandleControllerInputs()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        OnHorizontalMovement?.Invoke(inputX);
        OnVerticalMovement?.Invoke(inputZ);

        float joyX = Input.GetAxis("Joystick X");
        float joyY = Input.GetAxis("Joystick Y");
        if (Mathf.Abs(joyX) > 0.1f || Mathf.Abs(joyY) > 0.1f) OnLook?.Invoke(joyX, joyY);

        if (Input.GetButtonDown("Fire1")) OnShoot?.Invoke();

        if (Input.GetButtonDown("Interact")) OnInteract?.Invoke();

        if (Input.GetButtonDown("Pause")) OnPause?.Invoke();
    }
}
