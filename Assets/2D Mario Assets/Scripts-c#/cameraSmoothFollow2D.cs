using UnityEngine;
using System.Collections;

public class cameraSmoothFollow2D : MonoBehaviour
{

	#region						Fields
			
	public GameObject			cameraTarget;													// object to observe and follow

	public GameObject			player;													// player object for moving

	public float				smoothTime				=		0.1f;							// time for camera dampen
	public float				cameraHeight			=		2.5f;
	public float				cameraZoomMin			=		2.6f;							// maximum amount the camera can zoom int
	public float				cameraZoomMax			=		4.0f;							// maximum amount the camera can zoom out
	public float				cameraZoomTime			=		0.003f;							// speed for camera zooming

	public bool					cameraFollowX			=		true;							// camera follows on the horizontal
	public bool					cameraFollowY			=		true;							// camera follows on the vertical
	public bool					cameraFollowHeight		=		false;							// camera follows cameraTarget object height, not the Y
	public bool					cameraZoom				=		false;							// toggle zoom for the orthographic view
							

	private Transform			cameraTransform;
	private float				currentPosition			=		0.0f;							// current position of the cameraTarget
	private float				playerJumpHeight		=		0.0f;							// stores the jump height of the player

	public Vector2				velocity;				



	#endregion

	void			Start						()
	{
					

	}


	void			Update						()
	{
		cameraTransform					=	GetComponent	<Transform>			();
		
		smooth_camera_tracking				( cameraTransform );
		
	}

	void		smooth_camera_tracking			(Transform cameraTransform)
	{
				if ( cameraFollowX )
				{
						float newPosition				=	Mathf.SmoothDamp	( cameraTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
						cameraTransform.position		=   new Vector3			( newPosition, transform.position.y, transform.position.z );
				}

				if ( cameraFollowY )
				{
						float newPosition			=	Mathf.SmoothDamp	( cameraTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime);
						cameraTransform.position	=	new Vector3			( cameraTransform.position.x, newPosition, cameraTransform.position.z );
				}

				if ( !cameraFollowY && cameraFollowHeight )
				{
					cameraTransform.Translate ( cameraTransform.position.x, cameraHeight, cameraTransform.position.z );
				}

				if ( cameraZoom )
				{
					
					currentPosition			=		player.transform.position.y;				// set the current postion to the player's current y position

					playerJumpHeight = currentPosition - PlayerControl.startPosition;			// subtract the current heigh from the playerControl start position

					if ( playerJumpHeight < 0)
					{
						playerJumpHeight = playerJumpHeight * -1;
					}

					if (playerJumpHeight > cameraZoomMax)
					{
						playerJumpHeight = cameraZoomMax;
					}
					

					this.camera.orthographicSize		=	Mathf.Lerp ( this.camera.orthographicSize, playerJumpHeight + cameraZoomMin, Time.time * cameraZoomTime);
				}		

	}
}
