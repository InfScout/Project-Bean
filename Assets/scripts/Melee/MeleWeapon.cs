using UnityEngine;

public class MeleWeapon : Weapons
{
    
    [SerializeField] private MeleeConfig meleeConfig;
    
    public override void Use()
    {
        if(_onCooldown) return;
        base.Use();
        Vector3 hitBoxCenter = GetHitBoxCenter();
        Collider[] hitTargets = Physics.OverlapBox(hitBoxCenter, meleeConfig.hitBoxSize, transform.rotation, meleeConfig.hitBoxLayer);

        foreach (Collider target in hitTargets)
        {
            Enemy targetEnemy = target.gameObject.GetComponent<Enemy>();
            if (targetEnemy != null)
            {
                KnockBackTarget(targetEnemy.gameObject);
            }
        }
    }

    private void KnockBackTarget(GameObject target)
    {
        Rigidbody enemyRb = target.gameObject.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            Vector3 knockBackDir = target.transform.position - transform.position;
            knockBackDir.Normalize();
            enemyRb.AddForce(knockBackDir * meleeConfig.knockbackForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetHitBoxCenter(), meleeConfig.hitBoxSize);
    }

    Vector3 GetHitBoxCenter()
    {
        return transform.position
               + meleeConfig.hitBoxCenter.x * transform.right
               + meleeConfig.hitBoxCenter.y * transform.up
               + meleeConfig.hitBoxCenter.z * transform.forward;
    }
}

