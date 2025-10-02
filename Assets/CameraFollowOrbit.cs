using UnityEngine;

public class WeaponSmoothFollow : MonoBehaviour
{
    public Transform cameraTransform;        // La caméra du joueur
    public Vector3 offset = new Vector3(0, -0, 3f); // Décalage de l’arme
    public float smoothSpeed = 10f;          // Vitesse de "smooth"

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Position cible de l’arme par rapport à la caméra
        Vector3 targetPosition = cameraTransform.position + cameraTransform.rotation * offset;

        // Lissage de la position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 1f / smoothSpeed);

        // Lissage de la rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraTransform.rotation, smoothSpeed * Time.deltaTime);
    }
}