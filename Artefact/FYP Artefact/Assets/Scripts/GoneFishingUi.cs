using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GoneFishingUi : MonoBehaviour
{
    private VisualElement rootUi;

    [SerializeField] private PlayableAsset coverUiTimeline;
    private IEnumerator Start()
    {
        this.rootUi = GetComponent<UIDocument>().rootVisualElement;

        this.StartTitleLerp();

        yield return new WaitForSeconds(0.5f);

        this.StartCountdown();
    }
    
    private void StartTitleLerp()
    {
        var titleText = this.rootUi.Q<TextElement>("Whoops");
        Vector3 whoopsScale = titleText.transform.scale;
        
        whoopsScale.LerpTo(new Vector3(1.2f, 1.2f), 1f, value =>
        {
            titleText.transform.scale = value;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        }, GlobalLerpProcessor.easeInOutCurve);
    }
    
     private void StartCountdown()
     {
         var countdownText = this.rootUi.Q<TextElement>("Countdown");
         countdownText.text = "5";
         3f.LerpTo(0, 5f, value =>
         {
             countdownText.text = ((int)value + 1).ToString();
         }, pkg =>
         {
             var directior = GetComponentInChildren<PlayableDirector>();
             directior.playableAsset = this.coverUiTimeline;
             directior.Play();
         });
     }

     public void ReturnToMainMenu()
     {
         SceneManager.LoadScene(0);
     }
}
