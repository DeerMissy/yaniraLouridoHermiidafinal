using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VitaminiHealth : MonoBehaviour
{

    public VitaminiSound vitaminiSound;
    [Header("Health")]
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _currentHealth;

    [Header("UI")]
    [SerializeField]
    private Image _acorn;
    [SerializeField]
    private float _amountLife;

    [Header("Death")]
    [SerializeField]
    private float _forceJumpDeath;
    private bool _dead;
    private Animator _anim;
    private VitaminiMovement _vitaminiMovementScript;
    [SerializeField]
    private GameObject _gameOverPanel;




    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _vitaminiMovementScript = GetComponent<VitaminiMovement>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if(_anim.GetBool("IsHurt") == true || _currentHealth  <= 0.0f)
        {
            
            vitaminiSound.playHurt();
            return;

           
        }

        _currentHealth -= amount;
        _acorn.fillAmount = _currentHealth / _maxHealth;
        _anim.SetBool("IsHurt", true);
        _vitaminiMovementScript.ResetVelocity ();


        if(_currentHealth <= 0.0f)
        {

            _dead = true;
            vitaminiSound.playHurt();
            if (GameManager.instance != null)
            {

                GameManager.instance.ActivarGameOver(_gameOverPanel);
            }
            Death();
            return;
            


        }

        Invoke("HurtToFalse", 1.0f);

    }
    
    private void HurtToFalse()
    {
        _anim.SetBool("IsHurt", false);
    }
    private void Death()
    {
        Debug.Log("Player muerto");
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * _forceJumpDeath);
       
    }
}
