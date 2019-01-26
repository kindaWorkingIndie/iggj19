using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident : MonoBehaviour
{
    public float speed = 5;

    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetPosition = pos;
    }
}
