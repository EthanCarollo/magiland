using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ZoneEnter : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    public bool PlayerInside = false;
    public delegate void EnterZone();
    public event EnterZone OnEnterZone;
    public delegate void ExitZone();
    public event ExitZone OnExitZone;

    void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject))
        {
            PlayerInside = true;
            OnEnterZone?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(other.gameObject))
        {
            PlayerInside = false;
            OnExitZone?.Invoke();
        }
    }
    
    bool IsInLayerMask(GameObject obj)
    {
        return (targetLayers.value & (1 << obj.layer)) != 0;
    }
}
