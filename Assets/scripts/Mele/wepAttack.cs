using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private KeyCode atkKey = KeyCode.Mouse0;
    [SerializeField] private Weapons currentWeapon;
    

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
       
    }
}
