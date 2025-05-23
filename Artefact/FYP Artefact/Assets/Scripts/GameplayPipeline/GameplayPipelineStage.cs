using UnityEngine;
using UnityEngine.Events;

public class GameplayPipelineStage : MonoBehaviour
{
    [SerializeField] private UnityEvent StageEnteredEvent;

    [SerializeField] private DelayedUnityEvent[] delayedStageEnteredEvents;

    [SerializeField] private UnityEvent StageCompleteEvent;


    private PlayerGameplayPipeline pipeline;

    private void Awake()
    {
        this.pipeline = GetComponentInParent<PlayerGameplayPipeline>();
    }

    public virtual void StageComplete()
    {
        this.StageCompleteEvent?.Invoke();
        this.pipeline.Next();
    }

    public virtual void StageEntered()
    {
        this.StageEnteredEvent?.Invoke();
        foreach (var e in this.delayedStageEnteredEvents)
        {
            e.Invoke();
        }
    }
}