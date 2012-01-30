using UnityEngine;
using System.Collections;

public static class PlayerSounds
{


	
	private static float			soundRate						= 0.0f;											// current time + soundDelay
	private static float			soundDelay						= 0.0f;											// 



	#region							Player Sound Functions
	




	public static void				play_sound							( ref AudioSource soundSource, AudioClip soundName, float soundDelay)
	{
									if	( soundSource.isPlaying == false && Time.time > soundRate )
									{
											soundRate			=	Time.time + soundDelay;
											soundSource.clip	=	soundName;
											soundSource.Play();
									}
						
	}

	public static void				use_jump_audio						( ref AudioSource soundSource, AudioClip jumpSound, AudioClip crouchJumpSound, ref Vector3 velocity)
	{
									if (velocity.x == 0 && Input.GetAxis("Vertical") < 0)							// player does a crouch jump
									{
											play_sound( ref soundSource, crouchJumpSound, 0);
									}
									else
									{
											play_sound( ref soundSource, jumpSound, 0);
									}
	}

	#endregion
}
