using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    //Outlets
    Rigidbody2D bird;
    public float distance = 5;
    public float speed = 100;
    
    private bool moveRight = true;
    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveLeft = false;
    private float startXPos;
    private float startYPos;
    public string pattern;
    private bool facingRight = false;
    private Vector2 center;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<Rigidbody2D>();
        startXPos = transform.position.x;
        startYPos = transform.position.y;
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pattern == "Square"){
            SquareMovement();
        }
        if(pattern == "Circle"){
            CircleMovement();
        }

    }

    //Reset game if goat collides with bird
    void OnCollisionEnter2D(Collision2D goat)
    {
        if(goat.gameObject.GetComponent<CharacterController2D>()){
            //TODO decrease the goat's health
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void SquareMovement(){
        if(bird.position.x >= (startXPos + distance) && moveRight){
            moveUp = true;
            moveRight = false;
        }
        if(bird.position.y >= (startYPos + distance) && moveUp){
            moveUp = false;
            moveLeft = true;
            Flip();
        }
        if(bird.position.x <= (startXPos - distance) && moveLeft){
            moveDown = true;
            moveLeft = false;
        }
        if(bird.position.y <= (startYPos - distance) && moveDown){
            moveDown = false;
            moveRight = true;
            Flip();
        }        
        if(moveRight){
            bird.velocity = new Vector3(speed, 0, 0);
        }
        else if (moveUp){
            bird.velocity = new Vector3(0, speed, 0);
        }
        else if(moveLeft){
            bird.velocity = new Vector3(-speed, 0, 0);
        }
        else if (moveDown){
            bird.velocity = new Vector3(0, -speed, 0);
        }
    }

    void CircleMovement(){
        angle += speed * Time.deltaTime;
        var xpos = Mathf.Sin(angle);
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * distance;
        //maybe find a better way to flip the way the bird is facing
        if((xpos > (distance - .1)  && !facingRight)|| ((xpos < -(distance - .1))  && facingRight)){
            Flip();
        }
        transform.position = center + offset;
    }
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
