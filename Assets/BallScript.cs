using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float force = 10f;
    Rigidbody rb;
    bool isRunning;

	void Start () {
        rb = GetComponentInParent<Rigidbody>();
	}
	
	void Update () {
	    
	}

    IEnumerator PushBall()
    {
        while (true)
        {
            isRunning = true;
            rb.AddForce(Vector3.left * force);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Hand"))
        {
            if(!isRunning)
                StartCoroutine("PushBall");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Hand"))
        {
            StopAllCoroutines();
            isRunning = false;
        }
    }
}
