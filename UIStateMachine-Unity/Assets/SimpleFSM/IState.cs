using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public interface IState
    {
        void BeginEnter();
        void EndEnter();
        IEnumerable Execute();
        event EventHandler<StateBeginExitEventArgs> OnBeginExit;
        void EndExit();
    }
}