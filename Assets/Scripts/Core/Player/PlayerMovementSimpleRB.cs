using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimpleRB : MonoBehaviour
{
    [Header("Vitesse")]
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // On garde la physique, mais sans rotation parasite
        rb.freezeRotation = true;
    }

    void OnEnable()
    {
        // S'abonne aux inputs
        InputController.Instance.OnHorizontalMovement += OnHorizontalMovement;
        InputController.Instance.OnVerticalMovement += OnVerticalMovement;
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement en physique
        Vector3 move = moveInput * moveSpeed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    void OnHorizontalMovement(float inputX)
    {
        moveInput = (transform.right * inputX).normalized;
    }

    void OnVerticalMovement(float inputZ)
    {
        moveInput = (transform.forward * inputZ).normalized;
    }

    void OnDisable()
    {
        // S'abonne aux inputs
        InputController.Instance.OnHorizontalMovement -= OnHorizontalMovement;
        InputController.Instance.OnVerticalMovement -= OnVerticalMovement;
    }
}