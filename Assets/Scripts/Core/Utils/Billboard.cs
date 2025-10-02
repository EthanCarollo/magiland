using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Tooltip("La cible à regarder (si null => Main Camera)")]
    public Transform target = null;
    public bool inverse = false;

    void Awake()
    {
        // Si pas de target, prendre la caméra principale
        if (target == null)
        {
            target = Camera.main.transform;
            inverse = true;
        }
    }

    void LateUpdate()
    {
        // Faire regarder l'objet vers la target
        transform.LookAt(target);

        // Inverser l'orientation si besoin
        if (inverse) transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
    }
}