using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendlumController : MonoBehaviour
{
    public float speed = 1.5f; // Speed of the pendulum
    public float angle = 45.0f; // Maximum angle from the initial position
    private Quaternion _qStart, _qEnd;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize start and end rotations
        _qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        _qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        // Oscillate the pendulum from start to end rotations
        transform.localRotation = Quaternion.Lerp(_qStart, _qEnd, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
    }
}
