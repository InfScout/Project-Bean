using UnityEngine;

public class WeapomInventory : MonoBehaviour
{
    [SerializeField] Weapons[] weaponList;

    public Weapons ReturnWeapon(int index)
    {
        if (index > weaponList.Length - 1) return null;
        return weaponList[index];
    }
}
