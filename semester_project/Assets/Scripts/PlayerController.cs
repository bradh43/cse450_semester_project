using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Outlet
    Rigidbody2D rigidbody;
    public Animator animator;

    // Horizontal Movement
    public Vector2 direction;
    public float moveSpeed = 10f;
    private bool facingRight = true;

    // Physics
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float jumpSpeed = 10f;
    public int jumpsLeft = 1; 

    // Collision

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        moveCharacter(direction.x);
    }

    void moveCharacter(float horizontal)
    {
        rigidbody.AddForce(Vector2.right * horizontal * moveSpeed);

        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)){
            Flip();
        }

        if(Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }
    }

    void Jump()
    {
        if(jumpsLeft > 0)
        {
            
            print("jump");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpsLeft--;
        }
        
    }

    void modifyPhysics()
    {
        bool changingDirection = (direction.x > 0 && rigidbody.velocity.x < 0) || (direction.x < 0 && rigidbody.velocity.x > 0);
        if (Mathf.Abs(direction.x) < 0.4f || changingDirection)
        {
            rigidbody.drag = linearDrag;
        } else {
            rigidbody.drag = 0;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.6f);
            //Debug.DrawRay(transform.position, -transform.up * 0.7f);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 1;
                }
            }
        }
    }

    // drawer function
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.6f);
    //}
}
