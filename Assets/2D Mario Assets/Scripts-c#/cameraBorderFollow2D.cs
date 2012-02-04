using UnityEngine;
using System.Collections;

public class cameraBorderFollow2D : MonoBehaviour
{

	#region Fields

	public GameObject cameraTarget;
	public GameObject player;

	public float cameraHeight	=	0.0f;

	public float smoothTime		=	0.2f;

	public float borderX		=	2.0f;
	public float borderY		=	2.0f;


	private Vector2 velocity;

	private bool moveScreenRight = false;

	private bool moveScreenLeft  = false;

	float targetX;
	float cameraX;
	float cameraY;
	float cameraZ;
	#endregion



	void Start ()
	{
		cameraHeight = camera.transform.position.y;
	}

	// Update is called once per frame
	void Update () 
	{
		float moveDirection = PlayerControl.moveDirection;
		targetX = cameraTarget.transform.position.x;
		cameraX = camera.transform.position.x;
		cameraZ = camera.transform.position.z;
		shiftLeft();
		shiftRight();
		
		
		camera.transform.position = new Vector3 ( cameraX, cameraHeight, cameraZ );


	}

	void shiftLeft()
	{
		if ( targetX < cameraX - borderX && PlayerControl.moveDirection == -1 )
		{
			moveScreenLeft = true;
		}

		if ( moveScreenLeft )
		{
			float newXpos = Mathf.SmoothDamp ( cameraX, (cameraX - borderX), ref velocity.y, smoothTime );
			camera.transform.position = new Vector3 ( newXpos, camera.transform.position.y, camera.transform.position.z); 
		}

		if ( targetX > cameraX + borderX)
		{
			moveScreenLeft = false;
		}
	}

	void shiftRight()
	{
		if ( targetX > cameraX + borderX  && PlayerControl.moveDirection == 1 )
		{
			moveScreenRight = true;
		}

		if ( moveScreenRight )
		{
			float newXpos = Mathf.SmoothDamp ( cameraX, (cameraX + borderX), ref velocity.y, smoothTime );
			camera.transform.position = new Vector3 ( newXpos, camera.transform.position.y, camera.transform.position.z); 
		}

		if ( targetX < cameraX - borderX)
		{
			moveScreenRight = false;
		}
	}
}
