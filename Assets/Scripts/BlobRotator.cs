using UnityEngine;

public class BlobRotator : MonoBehaviour
{
    private float rotationSpeed;

    // Awake vs Start
    // Awake will be called first, and is called as soon as the object has been initialized
    // by Unity. However, the object might not yet appear in your scene because it has not been
    // enabled.As soon as your object is enabled, the Start is called.
    void Awake()
    {
        rotationSpeed = Random.Range(1f, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(10f, 10f, 10f) * rotationSpeed * Time.deltaTime);
    }

}
