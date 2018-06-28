using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public interface IStateMachine
    {
        IEnumerable Execute();
    }
}