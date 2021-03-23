using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Input keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;
    public float speed = 5f;
    private float _positionX;
    private float moveHorizontal;

    [Header("Gravity Switch")]
    [SerializeField]
    private KeyCode switchGravityKey = KeyCode.Space;
    
    private bool _isGrounded;
    private bool _hasSwitchedGravity = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
        }

        switchGravityAndRotation();

        _positionX = moveHorizontal * speed;

        /// Switch the Gravity Direction
        if (Input.GetKeyDown(switchGravityKey) && _isGrounded)
        {
            _hasSwitchedGravity = !_hasSwitchedGravity;
            rb.gravityScale = -rb.gravityScale;
            if (_hasSwitchedGravity)
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 180);
            } else
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    /// Switch Gravity and Rotate the Player.
    /// 
    /// Gravity which will check the Player's input and switch the gravity of Character.
    /// If the user is Grounded he can switch the direction of the gravity and his rotation
    /// will respectively be rotated to keep track of the walking direction.
    void switchGravityAndRotation()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_hasSwitchedGravity)
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                rb.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_hasSwitchedGravity)
            {
                rb.transform.rotation = Quaternion.Euler(0, -180, 180);
            }
            else
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(_positionX, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {  
            this.transform.parent = collisionData.transform;
        }
        _isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
        _isGrounded = false;
    }
}