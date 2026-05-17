using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] _wayPointsArray;
    [SerializeField]
    private Vector2[] _positionArray;
    private Vector3 _posToGo;
    private int _index;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    [SerializeField]
    private GameObject _player;

    private float _speed;
    [SerializeField]
    private float _speedWalking;
    [SerializeField]
    private float _speedAttack;
    [SerializeField]
    private float _speedAnimation;
    [SerializeField]
    private float _distanceToPlayer;
    private void Awake()
    {
        _speed = _speedWalking;
        _spriteRenderer = GetComponent <SpriteRenderer> ();
        _anim = GetComponent<Animator>();
        _positionArray = new Vector2[_wayPointsArray.Length];
        for (int i = 0; i < _wayPointsArray.Length; i++)
        {
            _positionArray[i] = _wayPointsArray[i].position;
        }

        _posToGo = _positionArray[0];

    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, _player.transform.position, Color.red);
        if (Vector2.Distance(transform.position, _player .transform.position)<=_distanceToPlayer)
        {

            AttackPlayer();
        }
        else
        {
            ChangeTargetPos();
        }
        

        transform.position = Vector3.MoveTowards(transform.position, _posToGo, _speed * Time.deltaTime);
        Flip();

    }

    private void ChangeTargetPos()
    {
        _speed = _speedWalking;
        _anim.speed = 1.0f;
        if (transform.position == _posToGo)
        {
            if (_index == _positionArray.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            _posToGo = _positionArray[_index];
        }

    }

    private void Flip()
    {
        if (_posToGo.x > transform.position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_posToGo.x < transform.position.x)
        {
            _spriteRenderer.flipX = false;



        }
    }

    private void AttackPlayer()
    {
        _speed = _speedAttack;
        _anim.speed = _speedAnimation;
        _posToGo = new Vector2(_player.transform.position.x, _posToGo.y);
    }

    private void OnCollisionEnter2D(Collision2D infocollision)
    {
        if (infocollision.collider.CompareTag("Player") &&
            infocollision.collider.GetComponent<VitaminiMovement>().IsGrounded)
        {
            infocollision.collider.GetComponent<VitaminiHealth>().TakeDamage(20.0f);
        }
    }
}