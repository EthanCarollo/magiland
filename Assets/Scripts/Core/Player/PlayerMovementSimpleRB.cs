using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimpleRB : MonoBehaviour
{
    [Header("Vitesse")]
    public float moveSpeed = 5f;

    private Rigidbody rb;

    // On garde les deux axes séparés, combinés en FixedUpdate
    private float inputX;
    private float inputZ;

    // Optionnel : légère inertie (0 = instantané)
    [Range(0f, 1f)] public float smoothing = 0f;
    private Vector3 smoothedMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;          // évite de tomber sur les côtés
        rb.interpolation = RigidbodyInterpolation.Interpolate; // mouvement plus fluide
    }

    void OnEnable()
    {
        // L'InputController peut ne pas être prêt en tout début de vie
        var ic = InputController.Instance;
        if (ic != null)
        {
            ic.OnHorizontalMovement += OnHorizontalMovement;
            ic.OnVerticalMovement   += OnVerticalMovement;
        }
    }

    void OnDisable()
    {
        var ic = InputController.Instance;
        if (ic != null)
        {
            ic.OnHorizontalMovement -= OnHorizontalMovement;
            ic.OnVerticalMovement   -= OnVerticalMovement;
        }
    }

    void OnHorizontalMovement(float x) => inputX = x;
    void OnVerticalMovement(float z)   => inputZ = z;

    void FixedUpdate()
    {
        // Combiner les axes selon l’orientation actuelle du joueur
        Vector3 wantedMove = (transform.right * inputX + transform.forward * inputZ);
        // Limiter la diagonale à 1 (au lieu de 1.414)
        wantedMove = Vector3.ClampMagnitude(wantedMove, 1f);

        // Lissage optionnel
        if (smoothing > 0f)
            smoothedMove = Vector3.Lerp(smoothedMove, wantedMove, 1f - Mathf.Pow(1f - smoothing, Time.fixedDeltaTime * 60f));
        else
            smoothedMove = wantedMove;

        // Application du déplacement physique
        rb.MovePosition(rb.position + smoothedMove * moveSpeed * Time.fixedDeltaTime);
    }
}