using Unity.VisualScripting;
using UnityEngine;

public class InputController : BaseController<InputController>
{
    public delegate void HorizontalMovement(float value);
    public event HorizontalMovement OnHorizontalMovement;

    public delegate void VerticalMovement(float value);
    public event VerticalMovement OnVerticalMovement;

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        OnHorizontalMovement?.Invoke(inputX);

        float inputZ = Input.GetAxisRaw("Vertical");
        OnVerticalMovement?.Invoke(inputZ);

        Debug.Log(inputX + " " + inputZ);
    }
}
