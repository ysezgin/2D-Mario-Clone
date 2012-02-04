using UnityEngine;
using System.Collections;

public static class PlayerMovement 


{
	static float				runSpeed					= PlayerProperties.runSpeed;
	static float				walkSpeed					= PlayerProperties.walkSpeed;
	
	static float				walkJump					= PlayerProperties.walkJump;                     					// jump height from walk    
	static float				runJump						= PlayerProperties.runJump;                     					// jump height from run
	static float				crouchJump					= PlayerProperties.crouchJump; 
	static float				gravity						= PlayerProperties.gravity;
	static float				fallSpeed					= PlayerProperties.fallSpeed;
	static float				collision_repel_above		= PlayerProperties.collision_repel_above;

	#region						Player Movement Functions



	public static void			run_movement					( ref Vector3 velocity)
	{
								velocity.x		=	velocity.x * runSpeed;												// player moves left based on runSpeed
	}

	public static void			set_player_direction			( ref Vector3 velocity, ref int moveDirection)
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

	public static void			set_player_ground_velocity			( ref Vector3 velocity, ref CharacterController playerController )
	{
								if ( playerController.isGrounded == true )
								{
									velocity            =   new Vector3(Input.GetAxis("Horizontal"), 0,  0 );
									velocity            =   playerController.transform.TransformDirection(velocity);
									velocity.x          =   velocity.x * walkSpeed; 
								}
	}

	public static void			set_player_air_velocity				( ref Vector3 velocity, ref CharacterController playerController )
	{
		
								if ( Input.GetButton ("Fire1"))
								{	
										velocity.x          =   Input.GetAxis("Horizontal") * runSpeed;
								}				
								else
								{
										velocity.x          =   Input.GetAxis("Horizontal") * walkSpeed;									// the player can change the direction of movement while they're 
								}
		 
					
								if (playerController.collisionFlags == CollisionFlags.Above)							// if the player's head collides with an object, repel the player downwards
								{
										velocity.y	=	0;
										velocity.y	=	velocity.y - collision_repel_above;
								}
					
	}

	public	static void			player_acceleration_from_gravity	( ref Vector3 velocity, ref CharacterController playerController)
	{
								if (playerController.isGrounded == false)
								{
										velocity.y          =	velocity.y - (gravity * Time.deltaTime);
								}
	}

	public static void			jump_movement				(ref Vector3 velocity)
	{				
								PlayerControl.in_a_jump			=		true;
								if		( Input.GetButton( "Fire1" ))											// player does a run jump
								{	
										velocity.y  =		runJump;
										velocity.x  =		runSpeed * Input.GetAxis ("Horizontal");			// the run jump moves faster in the x direction than the other jumps
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

	public static void			crouch_movement				(ref Vector3 velocity)
	{
								velocity.x = 0;																	// prevents the player from moving while crouching
	}

	

	public static void			walk_movement				(ref Vector3 velocity)
	{
								if (Input.GetAxis ("Horizontal") != 0)																		// sets player animation to walk left
								{
										velocity.x		=	velocity.x * walkSpeed;										// player moves left based on walk speed
								}
	}
	
	public static void			modulate_jump_height			( ref Vector3 velocity )
	{
								if (Input.GetButtonUp("Jump"))
								{
										velocity.y = velocity.y - fallSpeed;									// subtract current height from 1 if the jump button is up
								}
	}



	#endregion
}
