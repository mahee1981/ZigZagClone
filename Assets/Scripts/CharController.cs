using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isWalkingRight = true;
    public Transform rayStartRight;
    public Transform rayStartLeft;
    private Animator _animator;
    private GameManager _gameManager;
    public GameObject crystalEffect;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("IsGameStarted");
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

        if (rightSideFalling && leftSideFalling)
        {
            _animator.SetTrigger("IsFalling");
        }
        if (transform.position.y < -10)
        {
            _gameManager.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            _gameManager.IncreaseScore();
            
            GameObject effect = Instantiate(crystalEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(effect, 2.0f);
            Destroy(other.gameObject);
            
        }
    }

    private void SwitchDirection()
    {
        if(!_gameManager.gameStarted)
            return;
        _isWalkingRight = !_isWalkingRight;

        transform.rotation = _isWalkingRight ? Quaternion.Euler(0, 45, 0) : Quaternion.Euler(0, -45, 0);
    }
}
