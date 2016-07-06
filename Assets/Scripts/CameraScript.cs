using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    GameObject linkTo;
    Vector3 newPos;

	void Start () {
        linkTo = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () {
        newPos = linkTo.transform.position;
        newPos.z = -5f;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
	}
}
