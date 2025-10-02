using System;
using UnityEngine;

public static class RaycastSelective
{
    public static bool IsInMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;

    /// <summary>
    /// Raycast infini "sélectif" :
    /// - passThroughMask => on traverse
    /// - hitMask         => on renvoie un hit
    /// - autres layers   => bloquent sans hit
    /// </summary>
    public static (Ray ray, RaycastHit? hit, bool blocked) Raycast(
        Vector3 origin,
        Vector3 direction,
        LayerMask hitMask,
        LayerMask passThroughMask,
        QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
    {
        var ray = new Ray(origin, direction);

        // On récupère tous les hits (toutes couches), puis on filtre
        var hits = Physics.RaycastAll(ray, Mathf.Infinity, ~0, triggerInteraction);
        if (hits == null || hits.Length == 0)
            return (ray, null, false);

        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance)); // plus proche -> plus loin

        foreach (var h in hits)
        {
            int layer = h.collider.gameObject.layer;

            if (IsInMask(layer, passThroughMask))
            {
                // on ignore : on traverse
                continue;
            }

            if (IsInMask(layer, hitMask))
            {
                // on a touché une cible valide
                return (ray, h, false);
            }

            // ni passThrough, ni hitMask => c’est un obstacle qui bloque
            return (ray, null, true);
        }

        // On a traversé tout ce qu'on a rencontré (tout était passThrough)
        return (ray, null, false);
    }
}