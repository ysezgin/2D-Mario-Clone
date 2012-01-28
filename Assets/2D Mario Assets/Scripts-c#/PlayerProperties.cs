using UnityEngine;
using System.Collections;

[AddComponentMenu("Mario Clone/Actor/Player Properties Script")]

public class PlayerProperties : MonoBehaviour
{

	#region									_Fields_

	public 	int								lives							=	3;
	public 	int								keys							=	0;
	public 	int								coins							=	0;

	public 	Rigidbody						projectileFire;

	public 	Transform						projectile_socket_left;
	public 	Transform						projectile_socket_right;

	public 	Material						material_player_standard;
	public 	Material						material_player_fire;

	public 	bool							changeMario						=	false;
	public 	bool							hasFire							=	false;

	private int								coinLife						=	20;
	private bool							canShoot						=	false;

	public static CharacterController		playerController;		
	public static Transform					playerTransform;
	
	public PlayerState						active_player_state				=	PlayerState.MarioSmall;


	#endregion


	#region			_Properties_

	public enum		PlayerState
	{
					MarioDead	=	0,														// the player is dead
					MarioSmall	=	1,														// sets the size of the player to small
					MarioLarge	=	2,														// ses the size of the player to large
					MarioFire	=	3,														// enable the fireball power
	}

	#endregion


	#region			UnityEngine Functions

	
	// Update is called once per frame
	void			Update()
	{
					playerController				=	GetComponent		<CharacterController>	();
					playerTransform					=	GetComponent		<Transform>				();
					
					change_player_state		();
					Shoot					();

	}

	#endregion

	


	void			Shoot					()
	{
					
					float playerDirection	=	playerController.velocity.x;
					
					if ( canShoot && Input.GetButtonDown ("Fire1") &&  playerDirection < 0)
					{
							Rigidbody		clone;
							Vector3			left_socket			=	projectile_socket_left.transform.position;
							Quaternion		player_rotation		=	playerController.transform.rotation;

							clone = Instantiate ( projectileFire, left_socket, player_rotation) as Rigidbody;
							clone.AddForce		( -90, 0, 0);
							
					}

					if ( canShoot && Input.GetButtonDown ("Fire1") && playerDirection > 0)
					{
							Rigidbody		clone;
							Vector3			right_socket		=	projectile_socket_right.transform.position;
							Quaternion		player_rotation		=	playerController.transform.rotation;

							clone = Instantiate ( projectileFire, right_socket, player_rotation) as Rigidbody;
							clone.AddForce		( 90, 0, 0);
							

					}
	}


	void			AddKeys					( int numkey )
	{
					keys	=	keys + numkey;
	}

	void			AddCoin					( int numCoin )
	{
					coins	=	coins + numCoin;
	}

	void			change_player_state		()
	{
					if (changeMario == true)
					{ 
							SetPlayerState						();
					}
	}


	void			SetPlayerState			()
	{
					switch ( active_player_state )
					{
							case	PlayerState.MarioDead:
											player_scale_normal		();
							break;

							case	PlayerState.MarioSmall:
											player_scale_small		();
											canShoot			=	false;
							break;			

							case	PlayerState.MarioLarge:
											player_scale_normal		();
											canShoot			=	false;
							break;

							case	PlayerState.MarioFire:
											player_scale_normal		();
											canShoot			=	true;
							break;

							default:
							break;
		}
	}



	

	void			player_scale_small			()
	{
					playerTransform.localScale	=	new Vector3	( 1.0f, 0.75f, 1.0f);
	}

	void			player_scale_normal			()
	{
					playerTransform.localScale	=	new Vector3	( 1.0f, 1.0f, 1.0f);
	}





}
