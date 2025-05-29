using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private float weaponCooldown = .2f;
    private bool _onCooldown ;
    
    public virtual void Use()
    {
        StartCoroutine(InitiateWeaponCooldown());
    }

    public void StopUsing()
    {
        
    }

    IEnumerator InitiateWeaponCooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(weaponCooldown);
        _onCooldown = false;
    }
}
