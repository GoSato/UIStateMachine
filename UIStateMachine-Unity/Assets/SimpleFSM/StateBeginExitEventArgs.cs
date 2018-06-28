using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleFSM
{
    public class StateBeginExitEventArgs : EventArgs
    {
        public IState NextState { get; private set; }
        public IStateTransition Transition { get; private set; }

        public StateBeginExitEventArgs(IState nextState, IStateTransition transition)
        {
            NextState = nextState;
            Transition = transition;
        }
    }
}