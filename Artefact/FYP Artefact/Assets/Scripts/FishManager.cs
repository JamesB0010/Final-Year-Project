using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeReference] private Fish[] fish;
    private Fish[] closestFishPlayer = new Fish[2];

    private Fish GetClosestFish(Vector3 position)
    {
        Fish closestFish = fish[0];
        float closestDistance = (fish[0].transform.position - position).sqrMagnitude;
        for (int i = 1; i < fish.Length; i++)
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
    
    public void LockOntoClosestFish(Vector3 hookLocation)
    {
        closestFishPlayer[0] = this.GetClosestFish(hookLocation);
        if(fish == null)
            return;
        
        closestFishPlayer[0].OverideSteeringTowards(hookLocation).lostInterestInBait += FishLostInterestInBait;
        
        void FishLostInterestInBait()
        {
            closestFishPlayer[0].overrideSteeringSettings.lostInterestInBait -= FishLostInterestInBait;
            this.LockOntoClosestFish(hookLocation);
        }
    }

    public void TryHookClosestFish(int playerIndex, Vector3 hookIndex)
    {
        Debug.Log("Try hook fish");
    }
}
