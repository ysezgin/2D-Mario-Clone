using UnityEngine;
using System.Collections;

public static class PlayerAnimation
{

#region							Player Animation Functions

	public static void			idle_animation					(ref CharacterController playerController, float moveDirection)
	{
						if (moveDirection == 1)												// sets player animation to idle right
						{
								AniSprite.animate( playerController, 16, 16, 0, 0, 16, 12);
						}

						if (moveDirection == -1)												// sets player animation to idle left
						{
								AniSprite.animate( playerController, 16, 16, 0, 1, 16, 12);
						}
	}

	public static void			walk_animation					(ref CharacterController playerController, ref Vector3 velocity)
	{
						if (velocity.x < 0)																		// sets player animation to walk left
						{								
								AniSprite.animate( playerController, 16, 16, 0, 3, 10, 15);
						}

						if (velocity.x > 0)																		// sets player animation to walk right
						{
								AniSprite.animate( playerController, 16, 16, 0, 2, 10, 15);
						}
	}

	public static void			run_animation					(ref CharacterController playerController, ref Vector3 velocity)
	{
						if (velocity.x < 0  && Input.GetButton ("Fire1"))										// sets player animation to run left()
						{					
								AniSprite.animate( playerController, 16, 16, 0, 5, 16, 24);
						}

						if (velocity.x > 0 && Input.GetButton ("Fire1"))										// sets player animation to run right
						{
								AniSprite.animate( playerController, 16, 16, 0, 4, 16, 24);
						}
	}

	public static void			crouch_animation				(ref CharacterController playerController, ref Vector3 velocity, float moveDirection)
	{
						if	( velocity.x == 0 && Input.GetAxis ("Vertical") < 0)
						{
								if (moveDirection == -1)														// player is facing left
								{
										AniSprite.animate( playerController, 16, 16, 0, 9, 16, 24);				// sets player animation to crouch left
								}
								if (moveDirection == 1)															// player is facing right
								{
										AniSprite.animate( playerController, 16, 16, 0, 8, 16, 24);				// sets player animation to crouch right
								}
						}
	}

	public static void			jump_animation					(ref CharacterController playerController, ref Vector3 velocity, float moveDirection)
	{
						if (moveDirection == -1)														// use a jump animation facing the left
						{
								if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)					// use the left crouch jump animation
								{
									AniSprite.animate(playerController, 16, 16, 12, 11, 4, 12);
								}
								else
								{
									AniSprite.animate( playerController, 16, 16, 11, 3, 4, 12);			// use the left normal jump animtion
								}
						}
							
						if (moveDirection == 1)															// use a jump animation facing the right
						{
								if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)					// use the right crouch jump animation
								{
									AniSprite.animate(playerController, 16, 16, 12, 10, 4, 12);
								}
								else
								{
									AniSprite.animate( playerController, 16, 16, 11, 2, 4, 12);			// use the right normal jump animtion
								}
						}		
	}



	#endregion



}
