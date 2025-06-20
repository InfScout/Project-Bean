using System;
using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //keyBinds
    [SerializeField]private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField]private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]private KeyCode crouchKey = KeyCode.LeftControl;
    
    //Movement
    [SerializeField]private float baseSpeed =10;
    [SerializeField]private float moveSpeed =10;
    [SerializeField]private float runSpeed =15;
    private float _moveHorizontal;
    private float _moveForward;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    
    //crouch
    [SerializeField]private float crouchSpeed =5;
    [SerializeField]private float crouchYScale;
    [SerializeField]private float baseYScale;
    
     //jump
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
    private MovementState movementState;
    private enum MovementState
    {
        Walking,
        Running,
        Airborne,
        Crouching,
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        _raycastDistance = (_playerHeight / 2f ) + 0.1f;
        baseYScale = transform.localScale.y;
    }
    private void Inputs()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveForward = Input.GetAxis("Vertical");
        
        if (Input.GetKey(jumpKey) && _isGrounded && _jumpReady) 
        {
            Jump();
        }
        
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            moveSpeed = crouchSpeed;
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, baseYScale, transform.localScale.z);
        }

    }

    void FixedUpdate()
    {
        MoveMe();
    }
    void Update()
    {
        StateHandler();
        GroundCheck();
        Inputs();
        Falling();
    }
  
    private void GroundCheck()
    {
        _isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, _raycastDistance, groundLayer);
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

    private void StateHandler()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            movementState = MovementState.Crouching;
            moveSpeed = crouchSpeed;
        }
        if (_isGrounded && Input.GetKey(sprintKey))
        {
            movementState = MovementState.Running;
            moveSpeed = runSpeed;
        }
        else if (_isGrounded)
        {
            movementState = MovementState.Walking;
            moveSpeed = baseSpeed;
        }
        else
        {
            movementState = MovementState.Airborne;
        }
        
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
