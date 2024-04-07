using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StateMachine
{
    public abstract class UniversalStateMachine<T> where T : IUniversalState
    {
        protected T CurrentState;
        protected T LastState;

        public void Initialize(T state)
        {
            CurrentState = state;
            ChangeState(state);
        }
        public void ChangeState(T newState)
        {
            LastState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }
        public void ChangeLastState()
        {
            CurrentState.Exit();
            var state = CurrentState;
            CurrentState = LastState;
            LastState = state;
            CurrentState.Enter();
        }
    }
}