using System;
using System.Collections;
using System.Collections.Generic;


public class Defines
{
 	public enum FollowerPosition 
	{
		TOPLEFT = 0,
		TOP,
		TOPROGHT,
		LEFT,
		RIGHT,
		BOTTOMLEFT,
		BOTTOM,
		BOTTOMRIGHT
	}
 
    public enum colorType
    {
        blue = 0,
		green,
		red
    }

	public enum EnemyAppearPattern
	{
		// Back = 0,
		// Side,
		Custom
	}

	public enum EnemyMovingPattern
	{
		Idle = 0,
		IdleAndFacePlayer,
		// HorizontalTop,
		HorizontalMiddle,
		// HorizontalBottom,
		// ZigZagPass,
		ComeToPlayer,
		Custom
	}
}

