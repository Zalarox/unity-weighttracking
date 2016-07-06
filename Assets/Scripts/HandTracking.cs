using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandTracking : MonoBehaviour 
{
	public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	public GameObject OverlayObject;
    public Text debugText;

    public float smoothFactor = 5f;
	private float distanceToCamera = 10f;
    private Rigidbody rb;


	void Start()
	{
		if(OverlayObject)
		{
			distanceToCamera = (OverlayObject.transform.position - Camera.main.transform.position).magnitude;
            rb = OverlayObject.GetComponent<Rigidbody>();
		}
	}
	
	void Update() 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			int iJointIndex = (int)TrackedJoint;	
			if(manager.IsUserDetected())
			{
				uint userId = manager.GetPlayer1ID();
				if(manager.IsJointTracked(userId, iJointIndex))
				{
					Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

					if(posJoint != Vector3.zero)
					{
						Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
						Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);
						
						float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
						float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;
                        
                        //if(scaleY < 0.59f)
                        //{
                        //    scaleY = 0.59f;
                        //}
                        //if(scaleX < 0.45f)
                        //{
                        //    scaleX = 0.45f;
                        //}

                        debugText.text = "X: " + scaleX + " Y: " + scaleY;
						
						if(OverlayObject)
						{
							Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, 4.5f));
                            //OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
                            rb.velocity = vPosOverlay - OverlayObject.transform.position;
						}
					}
				}
			}
		}
	}
}
