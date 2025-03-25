using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeReference] private Fish[] fish;
    
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
    
    public async void LockOntoClosestFish(Vector3 hookLocation)
    {
        while (true)
        {
            Fish closestFish = this.GetClosestFish(hookLocation);
            try
            {
                await closestFish.OverideSteeringTowards(hookLocation);
                return;
            }
            catch
            {
                Debug.Log("Fish Lost Interest");
            }

            await UniTask.Yield();
        }
    }
}
