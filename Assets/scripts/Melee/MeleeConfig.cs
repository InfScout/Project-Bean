using UnityEngine;

public class MeleeConfig : MonoBehaviour
{
    [SerializeField] public Vector3 hitBoxCenter;
    [SerializeField] public Vector3 hitBoxSize; 
    [SerializeField] public LayerMask hitBoxLayer; 
    [SerializeField] public float damage;
    [SerializeField] public float knockbackForce;
}