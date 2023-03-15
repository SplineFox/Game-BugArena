using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}