using UnityEngine;
using System.Collections;



public class PlayerControl : MonoBehaviour
{
	#region Fields

		



	#region Player Sounds






	#endregion	

	public static int					moveDirection       			=	1;                							// direction the player is facing, 1 = right, -1 = left

	static private Vector3				velocity            			=	Vector3.zero;      							// direction and speed of player

	public static float						startPosition       		=	0.0f;             							// coordinate of start postioon

	public static bool					in_a_jump						=	false;

	static	CharacterController			playerController				=	PlayerProperties.playerController;
	static	AudioSource					playerAudio;
	
	#region Particles

																	// particle for feet hitting the ground
	static private Vector3				particlePlacement;		




	#endregion

	static PlayerProperties playerProps;

	#endregion

	public bool active = false;

	void				Update								()
	{
						
						CharacterController playerController		=		GetComponent<CharacterController>	();
						AudioSource			playerAudio				=		GetComponent<AudioSource>			();
						playerProps									=		GetComponent<PlayerProperties>		();
						

						player_actions											( ref playerController, ref playerAudio );

						PlayerMovement.player_acceleration_from_gravity			( ref velocity, ref playerController);
						PlayerMovement.set_player_direction						( ref velocity, ref moveDirection);
						
						playerController.Move				( velocity * Time.deltaTime );
						
						
	}



	#region				Player Action Functions

	void				player_actions						( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						air_actions							( ref playerController, ref playerAudio );
						ground_actions						( ref playerController, ref playerAudio );
						
	}	

	void				ground_actions						( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if	(playerController.isGrounded == true)												// movements available to the player on the ground
						{
								in_a_jump					=		false;
								startPosition				=		transform.position.y;
								PlayerMovement.set_player_ground_velocity		( ref velocity, ref playerController );
								crouch_action									( ref playerController, ref playerAudio );
								run_action										( ref playerController, ref playerAudio );
								jump_action										( ref playerController, ref playerAudio );
								walk_action										( ref playerController, ref playerAudio );
								idle_action										( ref playerController, ref playerAudio );
						}
	}

	static void			air_actions							( ref CharacterController playerController, ref AudioSource playerAudio )				// movements available to the player in the air
	{
						if ( playerController.isGrounded == false )
						{
								PlayerMovement.modulate_jump_height					( ref velocity);
								PlayerMovement.set_player_air_velocity				( ref velocity, ref playerController );

								if (in_a_jump)
								{ 
										PlayerAnimation.jump_animation						( ref playerController, ref velocity, moveDirection );
								}
						}
	}

	static void			jump_action					( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if ( Input.GetButton( "Jump" ))															// controls which type of jump the player character will execute
						{				
								
								PlayerMovement.jump_movement		( ref velocity);
								PlayerAnimation.jump_animation		( ref playerController, ref velocity, moveDirection);
								PlayerSounds.use_jump_audio			( ref playerAudio, playerProps.jumpSound, playerProps.crouchJumpSound, ref velocity);
								jump_particle						( playerController );
						}
	}

	static void			run_action					( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if (velocity.x != 0  && Input.GetButton ("Fire1"))										// sets player animation to run left()
						{
								PlayerMovement.run_movement			( ref velocity );
								PlayerAnimation.run_animation		( ref playerController, ref velocity );
						}
	}

	static void			crouch_action				( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if	( velocity.x == 0 && Input.GetAxis ("Vertical") < 0)
						{
								PlayerMovement.crouch_movement		( ref velocity);
								PlayerAnimation.crouch_animation	( ref playerController, ref velocity, moveDirection);
						}
	}

	static void			idle_action					( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if	(	velocity.x == 0	&& 
									velocity.y == 0	&&
										Input.GetAxis ("Vertical") >= 0 &&
											Input.GetAxis ("Horizontal") == 0 )
						{
								PlayerAnimation.idle_animation		( ref playerController, moveDirection );
						}
	}		

	static void			walk_action					( ref CharacterController playerController, ref AudioSource playerAudio )
	{
						if (velocity.x != 0 && 
								velocity.y == 0 &&
									Input.GetButton ("Fire1") == false)																		// sets player animation to walk left
						{
							PlayerMovement.walk_movement			( ref velocity);
							PlayerAnimation.walk_animation			( ref playerController, ref velocity);
						}
	}




	#endregion



	#region Particle Functions

	static void				jump_particle						( CharacterController playerController )
	{
							Vector3 playerPosition = playerController.transform.position;
							particlePlacement = new Vector3 ( playerPosition.x, (playerPosition.y - 0.5f) , playerPosition.z );
							Instantiate( playerProps.particleJump, particlePlacement, playerController.transform.rotation );
	}

	#endregion
}
