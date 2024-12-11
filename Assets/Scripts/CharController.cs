using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private static readonly int IsGameStarted = Animator.StringToHash("IsGameStarted");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private Rigidbody _rb;
    private bool _isWalkingRight = true;
    public Transform rayStartRight;
    public Transform rayStartLeft;
    private Animator _animator;
    private GameManager _gameManager;
    public GameObject crystalEffect;
    private PlaySound _soundEngine;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _soundEngine = FindObjectOfType<PlaySound>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        if (!_gameManager.gameStarted)
        {
            return;
        }
        else
        {
            _animator.SetTrigger(IsGameStarted);
        }
        _rb.transform.position = transform.position +  transform.forward * (Time.deltaTime * 2);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchDirection();
        }

        bool rightSideFalling = !Physics.Raycast(rayStartRight.position, -transform.up, out var rightSideHit, Mathf.Infinity);
        bool leftSideFalling = !Physics.Raycast(rayStartLeft.position, -transform.up, out var leftSideHit, Mathf.Infinity);

        if (rightSideFalling && leftSideFalling && transform.position.y < -0.1f)
        {
            _animator.SetTrigger(IsFalling);
        }
        if (transform.position.y < -10)
        {
            _gameManager.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Crystal")) return;
        
        _gameManager.IncreaseScore();
            
        var effect = Instantiate(crystalEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Destroy(effect, 2.0f);
        _soundEngine.PlayBingSound();
        Destroy(other.gameObject);
    }

    private void SwitchDirection()
    {
        if(!_gameManager.gameStarted)
            return;
        _isWalkingRight = !_isWalkingRight;

        transform.rotation = _isWalkingRight ? Quaternion.Euler(0, 45, 0) : Quaternion.Euler(0, -45, 0);
    }
}
