using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float jumpSpeed = 7f;

    Rigidbody2D _rigidbody;

    // bool _isClimb;

    bool _isGrounded = false;


    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

        Vector2 movement = new Vector2(horizontalMovement, _rigidbody.velocity.y);

        _rigidbody.velocity = movement;

        if (horizontalMovement != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalMovement), 1f, 1f);
        }

        if (Input.GetButtonDown("Jump") && !_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _isGrounded = false;
        }


    }
}
