using System;
using System.Collections.Generic;


namespace Scripts.StateMachine
{
    public interface IUniversalState
    {
        void Enter();
        void Exit();
        void Perform();
    }
}
