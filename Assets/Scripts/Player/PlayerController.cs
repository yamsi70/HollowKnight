using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 상태를 나타내는 변수들
    public float moveSpeed;  // _1 이동 속도	
    public float jumpSpeed;  // _1 점프 속도
    public float fallSpeed;  // _2 낙하 속도


    // 컴포넌트 변수들
    Rigidbody2D _rigidbody;  // _1 리지드바디 컴포넌트
    Animator _animator;  // _2 애니메이터 컴포넌트
    SpriteRenderer _spriteRenderer;  // _2 스프라이트 런더러 컴포넌트

    // 플레이어 상태를 나타내는 변수들
    bool _isGrounded;  // _2 땅에 닿아 있는지 여부
    bool _isJumping;  // _2 점프 중인지 여부


    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();  // _1
        _animator = gameObject.GetComponent<Animator>();  // _2
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();  // _2
    }

    // Update is called once per frame
    void Update()
    {
        move();  // _2
        jump();  // _2
        CheckGrounded();  // _3
        UpdateAnimation();  // _3

    }
        
    void move()  // _2
    {
        // _1 이동 계산 : 업데이트에 작성했던 코드를 move() 함수로 뺌 
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 movement = new Vector2(horizontalMovement, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        // _1 스프라이트의 방향
        if (horizontalMovement != 0)
        {
            _spriteRenderer.flipX = (horizontalMovement < 0);
        }

    }
    
    void jump()  // _2
    {
        // 점프 버튼을 누르고 땅에 닿았을 때 점프를 실행
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Vector2 jumpVelocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _rigidbody.velocity = jumpVelocity;  // 리지드바디에 새 속도 적용
            _isJumping = true;
        }

        // 낙하 속도 조절
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }

    }

    void CheckGrounded()  // _3
    {
        // 플레이어가 지면에 닿아있는지 확인
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (_isGrounded)
        {
            _isJumping = false;
        }
    }

    void UpdateAnimation()  // _3
    {
        // 현재 상태에 따라 애니메이션을 업데이트한다.
        _animator.SetBool("IsRun", Mathf.Abs(_rigidbody.velocity.x) > 0.1f);
        _animator.SetBool("IsJump", _isJumping);
        _animator.SetBool("IsGrounded", _isGrounded);

        // 회전 애니메이션 (지면에 있을 때만 실행)
        if (_isGrounded && Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
        {
            _animator.SetTrigger("IsRotate");
        }
    }

}
