using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Movement movement;

    private int points;
    private Rigidbody rb;
    private GameController controller;

    public Text pointsText;

    public int GetPoints()
    {
        return points;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = 0;
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        movement = new Movement(speed);
        SetPointsText();
    }

    // FixedUpdate is called once per frame with a constant framerate
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
        if (other.gameObject.CompareTag("blob"))
        {
            points++;
            Destroy(other.gameObject);
            controller.currentBlobs--;
            SetPointsText();
        }
    }

    private void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
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
