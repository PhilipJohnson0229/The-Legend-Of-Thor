using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //public if we want to read it but private if we want to overwrite it
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}