using UnityEngine;

public class shoot : MonoBehaviour
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
