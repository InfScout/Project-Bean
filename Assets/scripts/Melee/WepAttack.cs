using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private InputController _inputController;
    [SerializeField] private Weapons currentWeapon;
    

    [SerializeField] private Transform playerHands;
    private WeapomInventory _weaponInventory;
    
    
    [SerializeField] private Weapons equippedWeapon;

    private void Awake()
    {
       
        _weaponInventory = GetComponent<WeapomInventory>();
        if (equippedWeapon == null)
        {
            equippedWeapon = GetComponentInChildren<Weapons>();
        }
    }

    private void Start()
    {
        _inputController.AttackEvent += UseWeapon;
        _inputController.AttackEventCancelled += StopUsingWeapon;
        _inputController.EquipEvent += EquipWeapon;
    }     

    private void EquipWeapon(int weaponIndex)
    {
        Weapons weaponToEquip = _weaponInventory.ReturnWeapon(weaponIndex);
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon.gameObject);
        }

        if (weaponToEquip != null)
        {
            equippedWeapon = Instantiate(weaponToEquip, playerHands);
        }
    }

    void UseWeapon()
    {
        equippedWeapon.Use();
    }
    void StopUsingWeapon()
    {
        equippedWeapon.StopUsing();
    }
}
