using UnityEngine;
using System.Collections;


public class playerControl : MonoBehaviour
{
	#region Fields

	#region Player Movement Speeds
	
	static public float					walkSpeed           			= 1.5f;                     					// speed of the standard walk
	static public float					runSpeed						= 2.0f;                     					// speed of the run
	
	static public float					walkJump            			= 6.0f;                     					// jump height from walk    
	static public float					runJump							= 9.0f;                     					// jump height from run
	static public float					crouchJump          			= 10.0f;                    					// jump height from crouch 
	
	#endregion

	#region Enivornmental Forces

	static public float					fallSpeed           			= 2.0f;                     					// speed of falling down
	static public float					gravity             			= 20.0f;                    					// downward force applied on the character      
	static public float					collision_repel_above			= 1.0f;	 
	
	#endregion
	
	static int							moveDirection       			= 1;                							// direction the player is facing, 1 = right, -1 = left

	static private Vector3				velocity            			= Vector3.zero;      							// direction and speed of player

	static float						startPosition       			= 0.0f;             							// coordinate of start postioon

	#region Player Sounds

	public AudioClip					jumpSound;
	public AudioClip					crouchJumpSound;

	private static float				soundRate						= 0.0f;
	private static float				soundDelay						= 0.0f;


	#endregion

	#endregion


	void				Update								()
	{
						CharacterController playerController		= GetComponent<CharacterController>	();
						AudioSource			playerAudio				= GetComponent<AudioSource>			();
						player_acceleration_from_gravity	();
						set_player_direction				();
						player_actions						( playerController, playerAudio );
						playerController.Move				( velocity * Time.deltaTime );
						
	}

	#region				Player Action Functions

	void				player_actions						( CharacterController playerController, AudioSource playerAudio )
	{
						ground_actions				( playerController, playerAudio );
						air_actions					( playerController, playerAudio );
	}	

	void				ground_actions				( CharacterController playerController, AudioSource playerAudio )
	{
						if	(playerController.isGrounded == true)												// movements available to the player on the ground
						{
								set_player_ground_velocity		( playerController );
								crouch_action					( playerController, playerAudio );
								run_action						( playerController, playerAudio );
								jump_action						( playerController, playerAudio );
								walk_action						( playerController, playerAudio );
								idle_action						( playerController, playerAudio );
						}
	}

	static void			air_actions					( CharacterController playerController, AudioSource playerAudio )				// movements available to the player in the air
	{
						if (playerController.isGrounded == false)
						{
								set_player_air_velocity					(playerController);
								modulate_jump_height				();
						}
	}

	void				jump_action					( CharacterController playerController, AudioSource playerAudio )
	{
						if ( Input.GetButton( "Jump" ))															// controls which type of jump the player character will execute
						{				
								jump_movement	();
								jump_animation		(playerController);
								use_jump_audio		(playerAudio);
						}
	}

	static void			run_action					( CharacterController playerController, AudioSource playerAudio )
	{
						if (velocity.x != 0  && Input.GetButton ("Fire1"))										// sets player animation to run left()
						{
								run_movement		();
								run_animation		(playerController);
						}
	}

	static void			crouch_action				( CharacterController playerController, AudioSource playerAudio )
	{
						if	( velocity.x == 0 && Input.GetAxis ("Vertical") < 0)
						{
								crouch_movement	();
								crouch_animation	(playerController);
						}
	}

	static void			idle_action					( CharacterController playerController, AudioSource playerAudio )
	{
						if	(	velocity.x == 0	&& 
									velocity.y == 0	&&
										Input.GetAxis ("Vertical") == 0 &&
											Input.GetAxis ("Horizontal") == 0 )
						{
								idle_animation		(playerController);
						}
	}		

	static void			walk_action					( CharacterController playerController, AudioSource playerAudio )
	{
						if (velocity.x != 0 && Input.GetButton ("Fire1") == false)																		// sets player animation to walk left
						{
							walk_movement			();
							walk_animation			(playerController);
						}
	}




	#endregion

	#region				Player Movement Functions

	static void			set_player_direction()
	{
																											//the player character is facing the right
					if (velocity.x > 0)
					{
						moveDirection = 1;
					}

																											//the player character is facing the left
					if (velocity.x < 0)
					{
						moveDirection = -1;
					}
	}

	static void			set_player_ground_velocity			( CharacterController playerController )
	{
						velocity            =    new Vector3(Input.GetAxis("Horizontal"), 0,  0 );
						velocity            =    playerController.transform.TransformDirection(velocity);
						velocity            =    velocity * walkSpeed; 
	}

	static void			set_player_air_velocity				( CharacterController playerController )
	{
						velocity.x          =   Input.GetAxis("Horizontal");									// the player can change the direction of movement while they're in the air
					
						if		( Input.GetButton( "Fire1" ))													// the run jump moves faster in the x direction than the other jumps
						{	
								velocity.x  =		velocity.x * runSpeed;		
						}

						if (playerController.collisionFlags == CollisionFlags.Above)							// if the player's head collides with an object, repel the player downwards
						{
								velocity.y	=	0;
								velocity.y	=	velocity.y - collision_repel_above;
						}
					
	}

	static void			player_acceleration_from_gravity	()
	{
						velocity.y          =	velocity.y - (gravity * Time.deltaTime);
	}

	static void			jump_movement				()
	{				
						if		( Input.GetButton( "Fire1" ))											// player does a run jump
						{	
								velocity.y  =		runJump;
 								velocity.x  =		velocity.x * runSpeed;								// the run jump moves faster in the x direction than the other jumps
						}
						else
						{	
								velocity.y	=		walkJump;											// player does a walk jump
						}
						if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)							// player does a crouch jump
						{	
								velocity.y	=		crouchJump;		
								velocity.x  =		velocity.x * walkSpeed;
						}
	}

	static void			crouch_movement				()
	{
						velocity.x = 0;																	// prevents the player from moving while crouching
	}

	static void			run_movement					()
	{
						velocity.x		=	velocity.x * runSpeed;										// player moves left based on runSpeed
	}

	static void			walk_movement				()
	{
						if (Input.GetAxis ("Horizontal") != 0)																		// sets player animation to walk left
						{
								velocity.x		=	velocity.x * walkSpeed;										// player moves left based on walk speed
						}
	}

	static void			modulate_jump_height			()
	{
						if (Input.GetButtonUp("Jump"))
						{
								velocity.y = velocity.y - fallSpeed;									// subtract current height from 1 if the jump button is up
						}
	}

	#endregion

	#region				Player Animation Functions

	static void			idle_animation					(CharacterController playerController)
	{
						if (moveDirection == 1)												// sets player animation to idle right
						{
								aniSprite.animate( playerController, 16, 16, 0, 0, 16, 12);
						}

						if (moveDirection == -1)												// sets player animation to idle left
						{
								aniSprite.animate( playerController, 16, 16, 0, 1, 16, 12);
						}
	}

	static void			walk_animation					(CharacterController playerController)
	{
						if (velocity.x < 0)																		// sets player animation to walk left
						{								
								aniSprite.animate( playerController, 16, 16, 0, 3, 10, 15);
						}

						if (velocity.x > 0)																		// sets player animation to walk right
						{
								aniSprite.animate( playerController, 16, 16, 0, 2, 10, 15);
						}
	}

	static void			run_animation					(CharacterController playerController)
	{
						if (velocity.x < 0  && Input.GetButton ("Fire1"))										// sets player animation to run left()
						{					
								aniSprite.animate( playerController, 16, 16, 0, 5, 16, 24);
						}

						if (velocity.x > 0 && Input.GetButton ("Fire1"))										// sets player animation to run right
						{
								aniSprite.animate( playerController, 16, 16, 0, 4, 16, 24);
						}
	}

	static void			crouch_animation				(CharacterController playerController)
	{
						if	( velocity.x == 0 && Input.GetAxis ("Vertical") < 0)
						{
								if (moveDirection == -1)														// player is facing left
								{
										aniSprite.animate( playerController, 16, 16, 0, 9, 16, 24);				// sets player animation to crouch left
								}
								if (moveDirection == 1)															// player is facing right
								{
										aniSprite.animate( playerController, 16, 16, 0, 8, 16, 24);				// sets player animation to crouch right
								}
						}
	}

	static void			jump_animation					(CharacterController playerController)
	{
						if (moveDirection == -1)														// use a jump animation facing the left
						{
								if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)					// use the left crouch jump animation
								{
									aniSprite.animate(playerController, 16, 16, 12, 11, 4, 12);
								}
								else
								{
									aniSprite.animate( playerController, 16, 16, 11, 3, 4, 12);			// use the left normal jump animtion
								}
						}
							
						if (moveDirection == 1)															// use a jump animation facing the right
						{
								if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)					// use the right crouch jump animation
								{
									aniSprite.animate(playerController, 16, 16, 12, 10, 4, 12);
								}
								else
								{
									aniSprite.animate( playerController, 16, 16, 11, 2, 4, 12);			// use the right normal jump animtion
								}
						}		
	}



	#endregion

	#region Player Sound Functions

	/*
	IEnumerator			play_sound							( AudioSource soundSource, AudioClip soundName, float soundDelay)
	{
						if	( soundSource.isPlaying == false && Time.time > soundRate )
						{
								soundRate			=	Time.time + soundDelay;
								soundSource.clip	=	soundName;
								soundSource.Play();
								yield return new WaitForSeconds (soundSource.clip.length);
								print("play_sound.out");
						}
						
	}

	 */
 
	void				play_sound							( AudioSource soundSource, AudioClip soundName, float soundDelay)
	{
						if	( soundSource.isPlaying == false && Time.time > soundRate )
						{
								soundRate			=	Time.time + soundDelay;
								soundSource.clip	=	soundName;
								soundSource.Play();
						}
						
	}

	void				use_jump_audio						( AudioSource soundSource)
	{
						if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)							// player does a crouch jump
						{
								play_sound(soundSource, crouchJumpSound, 0);
						}
						else
						{
								play_sound( soundSource, jumpSound, 0);
						}
	}

	#endregion
}
