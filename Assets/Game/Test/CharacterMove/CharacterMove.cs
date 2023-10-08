using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : MonoBehaviour
{
    public Vector2 LastDirection;
    Rigidbody2D rigidbody;

    private void Awake()
    {
        LastDirection = new(1, 0);
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        if(velocity != Vector2.zero)
        {
            LastDirection = velocity;
        }
        rigidbody.velocity = velocity;
    }
}
