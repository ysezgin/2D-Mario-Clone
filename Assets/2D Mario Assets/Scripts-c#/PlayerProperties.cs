using UnityEngine;
using System.Collections;

[AddComponentMenu("Mario Clone/Actor/Player Properties Script")]

public class PlayerProperties : MonoBehaviour
{

	#region Fields

	public int						lives							=	3;
	public int						keys							=	0;
	public int						coins							=	0;

	public GameObject				projectileFire;

	public Transform				projectile_socket_left;
	public Transform				projectile_socket_right;

	public Material					material_player_standard;
	public Material					material_player_fire;

	public bool						changeMario						=	false;
	public bool						hasFire							=	false;

	private int						coinLife						=	20;
	private bool					canShoot						=	false;

	static CharacterController		playerController;
	static PlayerControl			playerControlScript;	
	
	public PlayerState				active_player_state				=	PlayerState.MarioSmall;


	#endregion


	#region Properties



	#endregion


	#region UnityEngine Functions

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void			Update()
	{
					playerController				=	GetComponent		<CharacterController>	();
					playerControlScript				=	GetComponent		<PlayerControl>			();
					SetPlayerState						();

	}

	#endregion

	public enum		PlayerState
	{
					MarioDead	=	0,														// the player is dead
					MarioSmall	=	1,														// sets the size of the player to small
					MarioLarge	=	2,														// ses the size of the player to large
					MarioFire	=	3,														// enable the fireball power
	}


	


	void			AddKeys					( int numkey )
	{
					keys	=	keys + numkey;
	}

	void			AddCoin					( int numCoin )
	{
					coins	=	coins + numCoin;
	}

	void			SetPlayerState			()
	{
		switch ( active_player_state )
		{
				case		PlayerState.MarioDead:
							print ("MarioDead");
							break;

				case		PlayerState.MarioSmall:
							print ("MarioSmall");
							break;

				case		PlayerState.MarioLarge:
							print ("MarioLarge");
							break;

				case		PlayerState.MarioFire:
							print ("MarioFire");
							break;

				default:
							break;
		}
	}

	#region Unity Inspector Functions

	



	#endregion
	


	/*
	public struct Player_Properties
	{

	}
	*/



}
