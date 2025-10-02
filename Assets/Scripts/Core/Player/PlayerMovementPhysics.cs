using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPhysics : MonoBehaviour
{
    [Header("Déplacement")]
    public float maxSpeed = 5f;              // vitesse cible au sol
    public float acceleration = 20f;         // combien vite on atteint la vitesse cible
    public float deceleration = 25f;         // freinage quand pas d'input

    private Rigidbody rb;
    private float inputX;
    private float inputZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // ✅ Réglages conseillés
        rb.isKinematic   = false;
        rb.useGravity    = true;
        rb.mass          = 1f;
        rb.linearDamping          = 0f;           // on gère le freinage nous-mêmes
        rb.angularDamping   = 0.05f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints   = RigidbodyConstraints.FreezeRotation; // pas de Freeze Position
    }

    void OnEnable()
    {
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
        // 1) Direction voulue dans le plan XZ du joueur
        Vector3 wishDir = (transform.right * inputX + transform.forward * inputZ);
        if (wishDir.sqrMagnitude > 1f) wishDir.Normalize();

        // 2) Vitesse actuelle, projetée dans le plan (ignore Y)
        Vector3 vel = rb.linearVelocity;
        Vector3 velPlanar = new Vector3(vel.x, 0f, vel.z);

        // 3) Vitesse cible
        Vector3 targetVel = wishDir * maxSpeed;

        // 4) Accélère vers la cible, freine sinon
        float accel = (wishDir.sqrMagnitude > 0f) ? acceleration : deceleration;

        // Calcul d’un “pas” vers la vitesse cible (pas plus que ce que l’accel permet)
        Vector3 velDelta = targetVel - velPlanar;
        Vector3 velStep  = Vector3.ClampMagnitude(velDelta, accel * Time.fixedDeltaTime);

        // 5) Applique une correction de vélocité **planar** (on conserve la vitesse verticale/gravity)
        Vector3 newVelPlanar = velPlanar + velStep;
        rb.linearVelocity = new Vector3(newVelPlanar.x, vel.y, newVelPlanar.z);
    }
}