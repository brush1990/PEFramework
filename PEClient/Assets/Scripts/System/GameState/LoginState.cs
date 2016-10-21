/************************************************************
    File      : LoginState
	Author    : Plane
    Version   : 1.0
    Function  : Nothing
    Date      : 2016/10/21 11:48:53
    Copyright : 2016 CodeVFX Inc.
*************************************************************/
using UnityEngine;

public class LoginState : IGameState 
{
	public override void Init()
    {

    }

    public override void Enter()
    {
        Debug.Log("flag");
    }

    public override GameStateType GetStateType()
    {
        return GameStateType.LoginState;
    }
}