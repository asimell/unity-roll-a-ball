using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobRotator : MonoBehaviour
{
    private float rotationSpeed;

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
