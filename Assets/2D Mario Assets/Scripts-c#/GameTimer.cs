/*		SIGGRAPH GAMES UPENN - Project 2012
 *		GameTimer	- Controls for tracking  and modulating the passage of gameplay, scene, and actual time
 *		Coders		- Jared Hester,
 */


using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{

	#region Fields


	public float		realTime				=		0.0f;								// holds the real time since the start of the game, not affected by pausing

	public float		playTime				=		0.0f;								// current time printed to the screen
	public float		days					=		0.0f;
	public float		hours					=		0.0f;
	public float		minutes					=		0.0f;
	public float		seconds					=		0.0f;
	public float		fractions				=		0.0f;

	public float		startTime				=		0.0f;								// stores the start of the time period currently tracked
	public float		fromLoadTime			=		0.0f;								// time since the scene was loaded
	public float		playStopTime			=		0.0f;								// used to stop time in game
	public float		continueTimeUp			=		0.0f;								// used to continue time from stopped time, increasing time
	public float		continueTimeDown		=		0.0f;								// used to continue time from stopped time, decreasing time
	
	public float		countDownDelay			=		0.0f;								// used to delay time during a countdown
	public float		countDownAmount			=		0.0f;								// amount to delay
	public float		delayTime				=		0.0f;
	public float		delayedAmount			=		0.0f;
	
	public float		addToTime				=		0.0f;								// used to increase 'playTime'
	public float		addTimeAmount			=		0.0f;								// amount to increase 'playTime'
	
	public bool			playTimeEnabled			=		false;								// toggle tracking
	public bool			realTimeEnabled			=		false;								// toggle tracking
	public bool			fromLoadTimeEnabled		=		false;								// toggle tracking
	public bool			continueTimeEnabled		=		false;								// toggle tracking
	public bool			countDownEnabled		=		false;								// toggle tracking
	public bool			displayTimerValues		=		false;								// toggle display of tracked time values
	public bool			displayTimerControls	=		false;								// toggle display of GameTimer key controls


	#endregion



	void			Update						()																						// Update is called once per frame
	{
					update_times			();
					
					time_commands			();

	}


	void			OnGUI						()
	{
					if ( displayTimerValues )
					{
							GUILayout.Label			( "Playtime "			+	playTime							);

							GUILayout.Label			( "Fractions "			+	fractions		.ToString ( "f3" )	);
							GUILayout.Label			( "Seconds "			+	seconds	 		.ToString ( "f0" )	);
							GUILayout.Label			( "Minutes "			+	minutes	 		.ToString ( "f0" )	);

							GUILayout.Label			( "Time "				+	Time			.time				);


							GUILayout.Label			( "From Load Time "		+	fromLoadTime						);
							
							GUILayout.Label			( "Start Time "			+	startTime							);
							GUILayout.Label			( "Stop Time "			+	playStopTime						);

							
					
							GUILayout.Label			( "Delay Time "			+	delayTime		.ToString ( "f0" )	);

							GUILayout.Label			( "Actual Time "		+	realTime		.ToString ( "f0" )	);
					}

					if ( displayTimerControls )
					{
							GUILayout.Label			( "1 - Start the GameTimer"							);
							GUILayout.Label			( "2 - From load time"								);
							GUILayout.Label			( "3 - Stop the GameTimer"							);
							GUILayout.Label			( "4 - Pause the GameTimer");
							GUILayout.Label			( "5 - Continue the GameTimer");
							GUILayout.Label			( "6 - Reset the GameTimer");
							GUILayout.Label			( "7 - Start countdown timer");
							GUILayout.Label			( "8 - Add to playTime once");
							GUILayout.Label			( "9 - Increment AddtoTime multiplier");
							GUILayout.Label			( "0 - Real time since startup");
					}
	}


	#region			Update Time Functions

	void			update_times				()
	{
					if ( playTimeEnabled ) 						// enables time recording
					{
						update_playTime			();

						update_seconds			();
						update_minutes			();
						update_fractions		();
						update_hours			();
						update_days				();
						update_fromLoadTime		();
						update_ActualTime		();
					}
					
					update_delayTime();
	}






	void			update_seconds						()
	{
					seconds			=		( playTime % 60);									// modulo playtime by the number of seconds passed
	}

	void			update_minutes						()											
	{
					minutes			=		( playTime / 60 ) % 60;								// divide playtime by the number of seconds in a minute
	}

	void			update_fractions					()
	{
					fractions		=		( playTime * 10 ) % 10;								// multiply playtime by 10 and mod of 10
	}

	void			update_hours()
	{
					hours			=		( playTime / 3600f ) % 24;							// divide playtime by the number of seconds in an hour
	}

	void			update_days							()
	{
					days			=		( playTime / 86400 ) % 365;							// divide playtime by the number of seconds in a day
	}

	void			update_playTime						()
	{	
					if ( playTimeEnabled && !countDownEnabled)
					{
							playTime	=		Time.time - startTime - continueTimeDown + addToTime;	// playTime is the current time since start
					}

					if (playTimeEnabled && countDownEnabled)
					{ 
							playTime	=		countDownDelay - Time.time + countDownAmount;		// playTime is the current time since start
					}
					
					if ( realTimeEnabled && !playTimeEnabled && !fromLoadTimeEnabled )
					{
							playTime	=		Time.realtimeSinceStartup + addToTime;				// playtime is now the actual time since the start
					}

					
	}

	void			update_fromLoadTime					()
	{
					if ( fromLoadTimeEnabled )
							fromLoadTime	=		Time.timeSinceLevelLoad;
	}

	void			update_delayTime					()
	{
					delayTime = Time.time + delayedAmount;	
					
	}

	void			update_ActualTime					()
	{
					if ( realTimeEnabled )
							realTime = Time.realtimeSinceStartup;
	}



	#endregion

	#region Time Control Functions

	void			time_commands				()
	{
					set_new_start_time		();
					reset_startTime			();
					stop_time				();
					pause_time				();
					continue_time			();
					countdown				();			


					print_time				();
	}

	void			print_time					()
	{
					if (Input.GetKeyDown("a"))
					{
							print ( "Minutes " + minutes );
							print ( "Seconds " + seconds);
							print ( "Fractions " + fractions);

					}
	}

	void			set_new_start_time			()					
	{
					if ( Input.GetKeyDown ( "1" ) )														// press '1' to activate set 'startTime' and begin recording
					{
							startTime				=		Time.time;									// 'startTime' equals the current time (Time.time)
							addToTime				=		0;											// reset
							continueTimeDown		=		0;											// reset
							playTimeEnabled 		=		true;										// playTimeEnabled enables the execution of many of GameTimer's methods, sets it to true
							countDownEnabled		=		false;										// reset
					}
	}

	void			reset_startTime				()														// press '2' to activate the start of scene time (from Load), playTime and realTime are not being tracked
	{
					if ( Input.GetKeyDown ( "2" ) )
					{
							fromLoadTime			=		Time.timeSinceLevelLoad;					// stores the current fromLoadTime
							startTime				=		0;											// reset 
							addToTime				=		0;											// reset
							playTimeEnabled			=		false;										// reset
							realTimeEnabled			=		false;										// reset
							countDownEnabled		=		false; 										// reset
							fromLoadTimeEnabled		=		true; 										// enables the tracking of time since the scene was loaded
					}
	}
	

	void			stop_time					()														// press '3' to stop the timer
	{
					if ( Input.GetKeyDown ( "3" ) && playTimeEnabled )
					{
							playStopTime			=		playTime;									// record the time 'playTime' was stopped
							addToTime				=		0;											// reset
							playTimeEnabled			=		false;										// stop gameplay time from being tracked
							continueTimeEnabled		=		false;										// reset
							realTimeEnabled			=		false;										// reset
							countDownEnabled		=		false; 										// reset
							fromLoadTimeEnabled		=		false; 										// reset
					}
	}

	void			pause_time					()														// press '4' to pause the timer
	{
					if ( Input.GetKeyDown ( "4" ) )
					{
							Time.timeScale		=		0.0f;											// when the 'timescale' is 0, time is not moving forward
					}
					else if ( Input.GetKeyUp ( "4" ) )
					{
							Time.timeScale		=		1.0f;											// returns 'timescale' to normal speed, 1.0
					}
	}


	void			continue_time				()														// press '5' to continue recording 'playTime'
	{
					if ( Input.GetKeyDown ( "5" ) )
					{
							continueTimeDown	=		Time.time - playTime;							// set 'continueTime' to the time when 'playTime' started
							startTime			=		0;												// reset
							addToTime			=		0;												// reset
							playTimeEnabled		=		true;											// restart the GameTimer
							countDownEnabled	=		false;											// reset
					}
	}

	void			reset_time					()														// press '6' to continue recording 'playTime'
	{
					if ( Input.GetKeyDown ( "6" ) )
					{
							playTime				=		0.0f;										// reset 
							playStopTime			=		0.0f;										// reset 
							continueTimeDown		=		0.0f;										// reset
							addToTime				=		0.0f;										// reset 

							playTimeEnabled			=		false;										// reset
							continueTimeEnabled		=		false;										// reset
							realTimeEnabled			=		false;										// reset
							countDownEnabled		=		false; 										// reset
							fromLoadTimeEnabled		=		false; 										// reset 
					}
	}

	void			countdown					()														// press '7' to start time countdown
	{
					if ( Input.GetKeyDown ( "7" ) )
					{
							countDownDelay		=		Time.time;										// store the current time in 'countDownDelay'
							playTimeEnabled		=		true;											// starts 'playtime' for the countdown
							countDownEnabled	=		true;											// 'countDownEnabled' triggers the time stop at the end of the countdown
							addToTime			=		0;												// reset
					}

					if (Input.GetKeyDown("t"))															// press 't' to countdown from current stopTime
					{
							continueTimeDown	=		playStopTime - ( countDownDelay - Time.time + countDownAmount);  // get the time to continue from
							startTime			=		0;												// reset
							addToTime			=		0;												// reset
							playTimeEnabled		=		true;											// restart the GameTimer
							countDownEnabled	=		true;											// start the countdown
					}
					if ( playTime < 0 )																	// stops time and the countdown
					{
							playTimeEnabled		=		false;											// reset
							countDownEnabled	=		false;											// reset
					}
	}


	void			add_timeAmount				()														// press '8' to add a single amount to the GameTimer
	{
					if ( Input.GetKeyDown ( "8" ) )
					{
							addToTime	=	addTimeAmount;
					}
	}

	void			increment_addToTimeAMount	()														// press '9' to increment the amount added to the GameTimer
	{
					if ( Input.GetKeyDown ( "9" ) )
					{
							addToTime	=	addToTime + addTimeAmount;
					}
	}

	void			activate_realTime			()														// press '9' to increment the amount added to the GameTimer, tracks time when the game is paused
	{
					if ( Input.GetKeyDown ( "9" ) )
					{
							realTime			=	Time.realtimeSinceStartup;							// store the time passed since startup
							startTime			=	0;													// reset
							addToTime			=	0;													// reset
							playTimeEnabled		=	false;												// reset
							realTimeEnabled		=	true;												// allow the tracking of real time even when paused
							fromLoadTimeEnabled	=	false;												// reset
					}
	}


	#endregion



}

