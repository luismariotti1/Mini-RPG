using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    
    public float directionChangeInterval;
    public bool followPlayer;
   
    Coroutine moveCoroutine;
    Rigidbody2D rb2d;
    Animator animator;
    
    Transform targetTransform = null;
    
    Vector2 endPosition;
    
    float currentAngle = 0;
    
    private int randnumber = 0;
    
    bool moving;

    int direction;

    CircleCollider2D circleCollider2;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider2 = GetComponent<CircleCollider2D>();
        StartCoroutine(WanderRoutine());
    }

    public IEnumerator WanderRoutine()
    {
        while(true)
        {
            moving = Movement(moving);
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    bool Movement(bool b)
    { 
            if(moving)
        {
            animator.SetBool("isWalking", true);
            currentAngle = changeRandonDirection();
            endPosition = vector2FromaAngle(currentAngle);
            MoveCharacter(rb2d, endPosition, currentSpeed, b);
            return false;
        }
            else
        {
            MoveCharacter(rb2d, endPosition, currentSpeed, b);
            animator.SetBool("isWalking", false);
            return true;
        }
    }
    float changeRandonDirection()
    {
        direction = UnityEngine.Random.Range(0, 8);
        float degree = (45 * direction);
        return degree;
    }

    float changeRandonDirectionCollide()
    {
        float degree = ((direction*45) + 180);
        //degree = Mathf.Repeat(currentAngle, 360);
        if(degree>360)
        {
            degree -= 360;
        }
        endPosition = vector2FromaAngle(degree);
        MoveCharacter(rb2d, endPosition, currentSpeed, true);
        print("direction:"+direction+" degree:"+degree);
        return degree;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Layer_Tree_and_Rocks") || collision.gameObject.CompareTag("Layer_Water"))
        {
            changeRandonDirectionCollide();
        }
    }

    Vector2 vector2FromaAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians));
    }

    public void MoveCharacter(Rigidbody2D rgb2, Vector2 v2, float speed, bool b)
    {
        if(b)
        {
        rgb2.velocity = v2 * speed;
        }
        else
        {
            rgb2.velocity = v2 * 0;
        }
    }

    void OnDrawGizmos()
    {
        if (circleCollider2 != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider2.radius);
        }
    }
}
