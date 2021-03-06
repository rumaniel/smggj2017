using System;
using System.Collections;
using System.Collections.Generic;


public class Defines
{
	public enum GameState
	{
		Intro,
		Menu,
		InGame,
		GameOver
	}

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
		IdleAndRotate,
		IdleAndFacePlayer,
		// HorizontalTop,
		HorizontalMiddle,
		// HorizontalBottom,
		// ZigZagPass,
		ComeToPlayer,
		Custom
	}

	public enum EnemyLeavePattern
	{
		Stay = 0,
		Custom
	}

	public enum FollwerType
	{
		Attacker = 0,
		Tanker,
		Util
	}

}

