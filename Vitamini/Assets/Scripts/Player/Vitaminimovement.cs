using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VitaminiMovement : MonoBehaviour
    
{
    public VitaminiSound vitaminiSound;
    [Header("Velocity")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _smoothTime;

    public Rigidbody2D Rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _targetVelocity;
    private Vector2 _dampVelocity;

    [Header("Jump")]
    [SerializeField]
    private float _jumpForce;
    private bool _jumpPressed;


    [Header("Raycast")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _rayLenght;

    [Header("Acorn")]
    [SerializeField]
    private int _numAcorn;
    [SerializeField]
    private TextMeshProUGUI _textAcornUI;

    [SerializeField]
    public bool IsGrounded;







    private void Awake()
    {
        _textAcornUI.text = "Bellotas perdidas: " + _numAcorn.ToString();
        _jumpPressed= false;
        Rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
        CanJump();
        RaycastGrounded();
        ChangeGravity();
    }

    private void CanJump()
    {
        if(_jumpPressed == true )
        {
            Jump();     
        }
    }

    private void RaycastGrounded()
    {
       IsGrounded  = Physics2D.Raycast(_groundCheck.position, Vector2.down, _rayLenght, _groundLayer);

        Debug.DrawRay(_groundCheck.position, Vector2.down * _rayLenght, Color.red);
    }

    private void Jump()
    {
        _jumpPressed = false;
        Rb.AddForce(Vector2.up * _jumpForce);
    }


    private void ChangeGravity()
    {

        if (Rb.velocity.y < 0.0f)
        {
            Rb.gravityScale = 3.5f;
        }
        else
        {
            Rb.gravityScale = 1.0f;
        }


    }

    // Update is called once per frame
    void Update()
    {
        InputsPlayer();
       
    }

    private void InputsPlayer()
    {

        float horizontal = Input.GetAxis("Horizontal");
        _targetVelocity = new Vector2(horizontal * _speed, Rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && IsGrounded ==true)
        {
            vitaminiSound.playSaltar();
            _jumpPressed = true;
        }

        Flip(horizontal);
        Animating(horizontal);

    }

    public void ResetVelocity()
    {
        _targetVelocity = Vector2.zero;
    }




    private void Move()
    {
        Rb.velocity = Vector2.SmoothDamp(Rb.velocity, _targetVelocity, ref _dampVelocity, _smoothTime );
    }

    private void Animating(float h)
    {
        if(h != 0.0f)
        {
            _anim.SetBool("IsRunning", true);
        }
        else
        {
            _anim.SetBool("IsRunning", false);
        }

        _anim.SetBool("IsJumping", !IsGrounded);

    }

    private void Flip(float h)
    {
        if(h > 0.0f) 
        {
            _spriteRenderer.flipX = false;
        }
        else if(h < 0.0f)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if(infoCollision.collider.CompareTag("Acorn"))
        {
            Destroy(infoCollision.gameObject);
            _numAcorn--;
            _textAcornUI.text = "Bellotas perdidas: " + _numAcorn.ToString();
            if(_numAcorn == 0)
            {
                GetNewScene();
            }
        }
    }

    private void  GetNewScene()
    {
        SceneManager.LoadScene(0);
    }




}
