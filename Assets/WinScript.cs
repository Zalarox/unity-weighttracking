using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinScript : MonoBehaviour {

    public Text victory;

    void OnTriggerEnter(Collider col)
    {
        victory.gameObject.SetActive(true);
    }
	
}
