using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Movement movement;

    private int points;
    private Rigidbody rb;

    public int GetPoints()
    {
        return points;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = 0;
        movement = new Movement(speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float deltaTime = Time.deltaTime;

        rb.AddForce(movement.Calculate(x, z, deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "blob")
        {
            points++;
            Destroy(other.gameObject);
        }
    }
}

// Humble object for unit testing
public class Movement
{
    public float speed;

    public Movement(float moveSpeed)
    {
        speed = moveSpeed;
    }

    public Vector3 Calculate(float x, float z, float deltaTime)
    {
        return new Vector3(x, 0f, z) * speed * deltaTime;
    }
}
