using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineSkipper : MonoBehaviour
{
    private abstract class Skipper
    {
        public PlayerGameplayPipeline player1;

        public Skipper(PlayerGameplayPipeline player1Pipeline)
        {
            this.player1 = player1Pipeline;
        }

        public abstract void HandleInputs(bool upInputted, bool downInputted);
    }

    private class singlePlayerSkipper : Skipper
    {
        public singlePlayerSkipper(PlayerGameplayPipeline player1Pipeline) : base(player1Pipeline)
        {
        }

        public override void HandleInputs(bool upInputted, bool downInputted)
        {
            if(upInputted)
                base.player1.SkipCurrentStage();
        }
    }

    private class multiplayerSkipper : Skipper
    {
        public PlayerGameplayPipeline player2;

        public multiplayerSkipper(PlayerGameplayPipeline player1Pipeline, PlayerGameplayPipeline player2Pipeline) : base(player1Pipeline)
        {
            this.player2 = player2Pipeline;
        }

        public override void HandleInputs(bool upInputted, bool downInputted)
        {
            if(upInputted)
                base.player1.SkipCurrentStage();
            
            if(downInputted)
                this.player2.SkipCurrentStage();
        }
    }

    private Skipper skipper;
    public void Setup(PlayerGameplayPipeline player1Pipeline)
    {
        this.skipper = new singlePlayerSkipper(player1Pipeline);
    }

    public void Setup(PlayerGameplayPipeline player1Pipeline, PlayerGameplayPipeline player2Pipeline)
    {
        this.skipper = new multiplayerSkipper(player1Pipeline, player2Pipeline);
    }

    private void Update()
    {
        bool upInputted = Input.GetKeyUp(KeyCode.UpArrow);
        bool downInputted = Input.GetKeyUp(KeyCode.DownArrow);

        this.skipper.HandleInputs(upInputted, downInputted);
    }
}
