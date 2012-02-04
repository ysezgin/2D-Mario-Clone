using UnityEngine;
using System.Collections;
using UnityEditor;

[AddComponentMenu("Mario Clone/Actor/Player Properties Script")]

public class PlayerProperties : MonoBehaviour
{

	#region									_Fields_
	
	#region Player Movement Speeds
	
	static public float					walkSpeed           			= 1.5f;                     					// speed of the standard walk
	static public float					runSpeed						= 2.0f;                     					// speed of the run
	
	static public float					walkJump            			= 6.0f;                     					// jump height from walk    
	static public float					runJump							= 9.0f;                     					// jump height from run
	static public float					crouchJump          			= 10.0f;                    					// jump height from crouch 
	
	#endregion

	#region Enivornmental Forces

	static public float					fallSpeed           			= EnvironmentalProperties.fallSpeed;                     					// speed of falling down
	static public float					gravity             			= EnvironmentalProperties.gravity;                    					// downward force applied on the character      
	static public float					collision_repel_above			= EnvironmentalProperties.collision_repel_above;	 
	
	#endregion

	

	public AudioClip								jumpSound;
	public AudioClip								crouchJumpSound;

	public Transform								particleJump;	



	public static int								lives							=	3;
	public static int								keys							=	0;
	public static int								coins							=	0;

	public 	Rigidbody								projectileFire;

	public 	Transform								projectile_socket_left;
	public 	Transform								projectile_socket_right;

	public 	Material								material_player_standard;
	public 	Material								material_player_fire;

	public bool										changeMario						=	false;
	public bool										hasFire							=	false;

	private static int								coinLife						=	20;
	private static bool								canShoot						=	false;

	public static CharacterController				playerController;		
	public static Transform							playerTransform;
	public static MeshRenderer						playerMeshRender;
	
	public PlayerState								active_player_state				=	PlayerState.MarioSmall;


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
					playerMeshRender				=	GetComponent		<MeshRenderer>			();		

					change_player_state		();
					Shoot					();

	}

		void OnGUI() 
	{ 
		int ammo  = 0;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Ammo");
		int target = EditorGUILayout.IntField(ammo);
		EditorGUILayout.EndHorizontal();
	}




	#endregion

	


	void			Shoot					()
	{
					
					float playerDirection	=	PlayerControl.moveDirection;

					Rigidbody		clone;
					if ( canShoot && Input.GetButtonDown ("Fire1") &&  playerDirection < 0)
					{
							
							Vector3			left_socket			=	projectile_socket_left.transform.position;
							Quaternion		player_rotation		=	playerController.transform.rotation;

							clone = Instantiate ( projectileFire, left_socket, player_rotation) as Rigidbody;
							clone.AddForce		( -90, 0, 0);
							
					}

					if ( canShoot && Input.GetButtonDown ("Fire1") && playerDirection > 0)
					{
							
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
											Destroy ( gameObject );
											changeMario					= false;
							break;

							case	PlayerState.MarioSmall:
											
											player_scale_small		();
																				
											canShoot					=	false;
											changeMario					=	false;
											playerMeshRender.material	=	material_player_standard;
							break;			

							case	PlayerState.MarioLarge:
											player_scale_normal		();
											canShoot					=	false;
											changeMario					=	false;
											playerMeshRender.material	=	material_player_standard;
							break;

							case	PlayerState.MarioFire:
											player_scale_normal		();
											canShoot					=	true;
											changeMario					=	false;
											playerMeshRender.material	=	material_player_fire;
							break;

		}
	}



	

	void			player_scale_small			()
	{
					
					playerTransform.localScale	=	new Vector3	( 1.0f, 0.75f, 1.0f);
					playerTransform.Translate	(0.0f, 0.2f, 0f);
					playerController.height		=	0.45f;
					
	}

	void			player_scale_normal			()
	{
					
					playerTransform.Translate	(0.0f, 0.4f, 0.0f);
					playerTransform.localScale	=	new Vector3	( 1.0f, 1.0f, 1.0f);
					playerController.height		=	0.5f;
					
					playerTransform.Translate	(0.0f, 0.0f, 0.0f);
	}


	public AudioClip get_jump_sound				()
	{
		return this.jumpSound;
	}

	public AudioClip get_crouch_jump_sound		()
	{
		return this.crouchJumpSound;
	}

}


