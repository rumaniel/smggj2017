using System;
using System.Collections;
using System.Collections.Generic;

public class SceneChangeEvent : GameEvent
{
    public Defines.GameState toState;

    public SceneChangeEvent(Defines.GameState toState)
    {
        this.toState = toState;
    }
}