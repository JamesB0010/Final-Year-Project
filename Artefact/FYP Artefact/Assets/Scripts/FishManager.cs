using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class FishManager : MonoBehaviour
{
    [SerializeReference] private Fish[] fish;
    private Fish[] closestFishPlayer = new Fish[2];
    [SerializeField] private float maxHookDistance;

    private float[] hookAttemptTimestamp = {float.MinValue, float.MinValue};

    [SerializeField] private float minTimeBetweenHookAttempts;

    [HideInInspector] public UnityEvent[] MissedHookFish = new UnityEvent[2];

    [HideInInspector] public UnityEvent[] HookedFish = new UnityEvent[2];

    [SerializeField] private UnityEvent<Fish>[] StartedPossessedFishEvent = new UnityEvent<Fish>[2];
    
    [SerializeField] private FishStartPositions2DArray fishStartPositions;

    [SerializeField] private Transform replaceFishLocation;

    [SerializeField] private Transform FishParentTransform;
    private void Awake()
    {
        this.MissedHookFish[0] = new UnityEvent();
        this.MissedHookFish[1] = new UnityEvent();

        this.HookedFish[0] = new UnityEvent();
        this.HookedFish[1] = new UnityEvent();
    }

    private Fish GetClosestFish(Vector3 position)
    {
        Fish closestFish = fish[0];
        float closestDistance = (fish[0].transform.position - position).sqrMagnitude;
        for (int i = 0; i < fish.Length; i++)
        {
            if (!fish[i].HasLostInterestForLongEnough())
                continue;
            
            float dist = (fish[i].transform.position - position).sqrMagnitude;

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestFish = fish[i];
            }
        }
        return closestFish;
    }
    
    public void LockOntoClosestFish(Vector3 hookLocation, int playerIndex)
    {
        closestFishPlayer[playerIndex] = this.GetClosestFish(hookLocation);
        if(fish == null)
            return;
        
        closestFishPlayer[playerIndex].OverideSteeringTowards(hookLocation).lostInterestInBait += FishLostInterestInBait;
        
        void FishLostInterestInBait()
        {
            closestFishPlayer[playerIndex].overrideSteeringSettings.lostInterestInBait -= FishLostInterestInBait;
            this.LockOntoClosestFish(hookLocation, playerIndex);
        }
    }
    
    public void TryHookClosestFish(int playerIndex, Vector3 hookPosition)
    {
        float timeSinceLastHookAttempt = Time.timeSinceLevelLoad - this.hookAttemptTimestamp[playerIndex];
        if (timeSinceLastHookAttempt < this.minTimeBetweenHookAttempts)
            return;
        
        if (Vector3.Distance(this.closestFishPlayer[playerIndex].transform.position, hookPosition) <= this.maxHookDistance)
        {
            this.HookedFish[playerIndex]?.Invoke();
        }
        else
        {
            this.MissedHookFish[playerIndex]?.Invoke();
            this.hookAttemptTimestamp[playerIndex] = Time.timeSinceLevelLoad;
        }
    }

    private Fish PossessClosestFish(int playerIndex)
    {
        Fish fish = this.closestFishPlayer[playerIndex];
        fish.PosessFish();
        this.StartedPossessedFishEvent[playerIndex]?.Invoke(fish);
        return fish;
    }

    public UniTask<Fish> MoveTargetFishToStartPoint(int playerIndex, Vector3 playerPosition)
    {
        Fish fish = this.PossessClosestFish(playerIndex);
        
        UniTaskCompletionSource<Fish> fishMoveCompletion = new UniTaskCompletionSource<Fish>();

        Vector3 startPosition = this.fishStartPositions.transformArrays[playerIndex].transforms[0].position;

        float yPos = fish.transform.position.y;
        
        fish.transform.forward = fish.transform.position - new Vector3(playerPosition.x, yPos, playerPosition.z);
        
        fish.transform.position.LerpTo(startPosition, 2f, value =>
        {
            fish.transform.position = value;
        }, pkg =>
        {
            fishMoveCompletion.TrySetResult(fish);
        });

        return fishMoveCompletion.Task;
    }

    public UniTask MoveFishToStartPoint(Fish fish, int playerIndex, int startPositionLayer)
    {
        UniTaskCompletionSource fishMoveTCS = new UniTaskCompletionSource();

        Vector3 startPosition = this.fishStartPositions.transformArrays[playerIndex].transforms[startPositionLayer].position;

        fish.transform.position.LerpTo(startPosition, 2f, value =>
        {
            fish.transform.position = value;
        }, pkg =>
        {
            fishMoveTCS.TrySetResult();
        });

        return fishMoveTCS.Task;
    }

    public void PlaceFishBackIntoPond(Transform fish)
    {
        fish.parent = this.FishParentTransform;
        fish.position = this.replaceFishLocation.position;
    }
}
