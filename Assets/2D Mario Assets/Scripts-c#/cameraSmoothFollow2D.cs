using UnityEngine;
using System.Collections;

public class cameraSmoothFollow2D : MonoBehaviour
{

	#region						Fields
			
	public GameObject			cameraTarget;													// object to observe and follow

	public GameObject			playerTarget;													// player object for moving

	public float				smoothTime				=		0.1f;							// time for camera dampen
	public float				cameraHeight			=		2.5f;
	public float				cameraZoomInBound		=		2.5f;							// maximum amount the camera can zoom int
	public float				cameraZoomOutBound		=		4.0f;							// maximum amount the camera can zoom out


	public bool					cameraFollowX			=		true;							// camera follows on the horizontal
	public bool					cameraFollowY			=		true;							// camera follows on the vertical
	public bool					cameraFollowHeight		=		false;							// camera follows cameraTarget object height, not the Y
	public bool					cameraZoom				=		false;							// toggle zoom for the orthographic view
							

	private Transform			cameraTransform;
	private float				currentPosition			=		0.0f;							// current position of the cameraTarget
	private float				playerJumpHeight		=		0.0f;							// stores the jump height of the player

	public Vector2				velocity;				



	#endregion




	void			Update						()
	{
		cameraTransform					=	GetComponent	<Transform>			();
		
		smooth_camera_tracking				( cameraTransform );
		
	}

	void		smooth_camera_tracking			(Transform cameraTransform)
	{
				if ( cameraFollowX )
				{
						float newPosition			=	Mathf.SmoothDamp	( cameraTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
						cameraTransform.position	=	new Vector3			( newPosition, transform.position.y, transform.position.z);
				}
				if ( cameraFollowY )
				{
						float newPosition			=	Mathf.SmoothDamp	( cameraTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime);
						cameraTransform.position	=	new Vector3			( transform.position.x, newPosition, transform.position.z);
				}
				if ( cameraFollowY == false && cameraFollowHeight == true )
				{
					//	cameraTransform.position.x	=	;
				}
				if ( cameraFollowX )
				{
					//	cameraTransform.position.x	=	;
				}		

	}
}
