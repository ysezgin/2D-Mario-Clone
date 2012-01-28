using UnityEngine;
using System.Collections;

public class SpawnSaveSetup : MonoBehaviour
{

	#region				Fields

	public Transform			startPoint;
	public AudioClip			soundDie;

	private static float		soundRate				=		0.0f;
	private static float		soundDelay				=		0.0f;
	private Vector3				currentSavePosition;


	#endregion
	
	void				Start						()
	{
						if ( startPoint != null)
						{
							transform.position = startPoint.position;
						}

	}


	static void			play_sound					( AudioSource soundSource, AudioClip soundName, float soundDelay)
	{
						if	( soundSource.isPlaying == false && Time.time > soundRate )
						{
								soundRate			=	Time.time + soundDelay;
								soundSource.clip	=	soundName;
								soundSource.Play();
						}
						
	}

	void				OnTriggerEnter				( Collider other)		
	{
						if (other.tag	==	"savePoint")
						{
								currentSavePosition = transform.position;
						}

						if (other.tag	==	"killbox")
						{
								play_sound ( audio, soundDie, 0);
								transform.position = currentSavePosition;
						}
	}
}
