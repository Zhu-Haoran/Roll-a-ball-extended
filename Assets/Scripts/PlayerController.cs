using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Create public variables for player speed, jump height and spawn location
    public float speed = 10;
    public float jump = 400;
    public GameObject spawn;

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;

    // Jump flag
    private bool isOnGround = true;
    // Inverse player movement axis flag
    bool isInverse = false;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();
    }

    // Each physics step..
    void FixedUpdate()
    {
        // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
        // multiplying it by 'speed' - our public player speed that appears in the inspector

        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        if (isInverse)
        {
            float temp = moveHorizontal;
            moveHorizontal = moveVertical;
            moveVertical = temp;
        }

        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 movement;
        if (isOnGround == true && Input.GetAxisRaw("Jump") != 0)
        {
            movement = new Vector3(moveHorizontal, jump, moveVertical);
            isOnGround = false;
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    // When this game object intersects a collider with 'is trigger' checked, 
    // store a reference to that collider in a variable named 'other'..
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reset Zone"))
        {
            this.gameObject.transform.position = spawn.transform.position;
        }

        if (other.gameObject.CompareTag("Inverter Pick Up"))
        {
            isInverse = !isInverse;
        }
    }
}