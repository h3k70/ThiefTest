using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public float Speed = 3f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rigidbody2D;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private Vector2 _velocity;
    private Animator _animator;
    private Transform _transform;
    private HashAnimationRogue _hashAnimationRogue = new HashAnimationRogue();

    private void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * Speed, 0);

        if (0 < _velocity.x)
        {
            _transform.rotation = new Quaternion(0, 0, 0, 0);
            _animator.SetBool(_hashAnimationRogue.IsGoes, true);
        }
        else if (_velocity.x < 0)
        {
            _transform.rotation = new Quaternion(0, 180, 0, 0);
            _animator.SetBool(_hashAnimationRogue.IsGoes, true);
        }
        else
        {
            _animator.SetBool(_hashAnimationRogue.IsGoes, false);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
            _velocity.y = 5;
    }

    private void FixedUpdate()
    {
        _velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {

        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rigidbody2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rigidbody2D.position = rigidbody2D.position + move.normalized * distance;
    }
}
