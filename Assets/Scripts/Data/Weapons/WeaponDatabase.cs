using System.Collections.Generic;
using Data.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponData> weapons;

    public WeaponData GetDifferentWeapon(WeaponData currentWeapon)
    {
        if (weapons == null || weapons.Count == 0)
        {
            Debug.LogWarning("WeaponDatabase est vide !");
            return null;
        }

        // Filtrer les armes différentes
        List<WeaponData> candidates = weapons.FindAll(w => w != currentWeapon);
        if (candidates.Count == 0)
            return currentWeapon;

        return candidates[Random.Range(0, candidates.Count)];
    }
    
    public WeaponData GetNextWeapon(WeaponData currentWeapon)
    {
        if (weapons == null || weapons.Count == 0)
        {
            Debug.LogWarning("WeaponDatabase est vide !");
            return null;
        }

        int index = weapons.IndexOf(currentWeapon);

        if (index == -1)
        {
            Debug.LogWarning("L'arme actuelle n'est pas dans la liste !");
            return weapons[0]; // fallback = première
        }

        // Arme suivante, avec boucle
        int nextIndex = (index + 1) % weapons.Count;
        return weapons[nextIndex];
    }
}