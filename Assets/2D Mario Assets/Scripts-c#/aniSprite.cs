using UnityEngine;
using System.Collections;

public class aniSprite : MonoBehaviour 
{
   
	
	public static void		animate(Component spriteSheet, int columnSize, int rowSize, int columnFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond)
	{
							float	tileSize                      = 1.0f;
							int		index                         = (int)(Time.time * framesPerSecond);                                 // uses Time and the FPS value to set the animation frame to display
									index                         = index % totalFrames;                                                // modulates the index so the animation loops

							int		u                             = index % columnSize;
							int		v                             = index / columnSize;
							int		uStartPosition                = u + columnFrameStart;
							int		vStartPosition                = v + rowFrameStart;

							Vector2 size                          = new Vector2(tileSize / columnSize, tileSize / rowSize);       		// adjusts the texture to the correct scale
							Vector2 offset                        = new Vector2(uStartPosition * size.x, (1 - size.y) 
																									- (vStartPosition * size.y));		// stores the value to offset the object's texture

							Material spriteSheetMaterial 		  = spriteSheet.renderer.material;

							spriteSheetMaterial.mainTextureOffset = offset;                                                 			// applies offset to the object's texture
							spriteSheetMaterial.mainTextureScale  = size;                                     							// applies scale to the object's texture

							spriteSheetMaterial.SetTextureOffset("_BumpMap", offset);                       							// applies offset to the object's normal map
							spriteSheetMaterial.SetTextureScale("_BumpMap", size);                                   					// applies scale to the object's normal map
	}


}

/*
public struct SpriteSequence
{
	public int columnSize;
	public int rowSize;
	public int columnFrameStart;
	public int rowFrameStart;
	public int totalFrames;
	public int framesPerSecond;

	public	SpriteSequence()
	{
			columnSize 			=	0;
			rowSize 		 	= 	0;
			columnFrameStart 	= 	0;
			rowFrameStart 	 	= 	0;
			totalFrames 	 	= 	0;
			framesPerSecond  	= 	0;
	}
}
*/