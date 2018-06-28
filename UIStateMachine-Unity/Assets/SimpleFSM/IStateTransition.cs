using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public interface IStateTransition
    {
        IEnumerable Exit();
        IEnumerable Enter();
    }
}