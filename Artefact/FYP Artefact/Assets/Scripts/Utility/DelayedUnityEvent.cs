using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DelayedUnityEvent
{
    [SerializeField] private float delayTime;

    public UnityEvent OnEventInvoke;

    public void Invoke()
    {
        this.WaitThenInvokeMethod();
    }

    private async UniTask WaitThenInvokeMethod()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(this.delayTime));
        this.OnEventInvoke?.Invoke();
    }
}
