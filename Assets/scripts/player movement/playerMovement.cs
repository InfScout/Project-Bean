using System;
using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Movement
    [SerializeField]private float baseSpeed =10;
    [SerializeField]private float moveSpeed =10;
    private float _moveHorizontal;
    private float _moveForward;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    
     //jump
    [SerializeField]private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]private float jumpForce = 10f;
    [SerializeField]private float jumpCooldown = 1f;
    [SerializeField]private float fallMultiplier = 2.5f;
    [SerializeField]private float groundCheckDelay = 0.3f;
    private float _groundCheckTimer;
    public float ascendMultiplier = 2f;
    private float _playerHeight;
    private float _raycastDistance;
    private bool _jumpReady = true;
   
    [SerializeField] private Transform direction;
  
    //floor check stuff
    [SerializeField]private LayerMask groundLayer;
    
    //raycast stuff
    private bool _isGrounded = true;
  
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        _raycastDistance = (_playerHeight / 2f ) + 0.1f;
    }
    private void Inputs()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveForward = Input.GetAxis("Vertical");
        
        if (Input.GetKey(jumpKey) && _isGrounded && _jumpReady) 
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MoveMe();
    }
    void Update()
    {
        GroundCheck();
        Inputs();
        Falling();
    }
    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(_rb.position, Vector3.down, _raycastDistance, groundLayer);
    }
    private void MoveMe()
    {
        Vector3 movement = (direction.right * _moveHorizontal + direction.forward * _moveForward);
        Vector3 targetVelocity = movement * moveSpeed;
        
        Vector3 velocity = _rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        _rb.linearVelocity = velocity;
        
        if (_isGrounded && _moveHorizontal == 0 && _moveForward == 0)
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }
    
    private void Jump()
    {
        _isGrounded = false;
        _groundCheckTimer = groundCheckDelay;
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.z);
        StartCoroutine(JumpCoolDown());
    }
    
    void Falling()
    {
        if (_rb.linearVelocity.y < 0) 
        {
            _rb.linearVelocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime);
        } 
        else if (_rb.linearVelocity.y > 0)
        {
            _rb.linearVelocity += Vector3.up * (Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime);
        }
    }

    IEnumerator JumpCoolDown()
    {
        _jumpReady = false;
        yield return new WaitForSeconds(jumpCooldown);
        _jumpReady = true;
    }
    
}
