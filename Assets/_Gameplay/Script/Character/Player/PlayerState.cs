using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum StateCharacter
    {
        Idle,
        Run,
        Jump,
        Fly,
    }

    [Serializable]
    public class PlayerState : BaseState<StateCharacter>
    {
        public PlayerState(BaseCharacter character, SkeletonAnimation animation) : base(character, animation) { }


        public override void Init()
        {
            currentState = StateCharacter.Idle;
            UpdateAnimation();
        }

        protected override void UpdateAnimation(Spine.AnimationState.TrackEntryDelegate callback = null)
        {
            switch (currentState)
            {
                case StateCharacter.Idle:
                    anim.state.SetAnimation(0, "Idle_0", true);
                    break;
                case StateCharacter.Jump:
                    anim.state.SetAnimation(0, "Jump_0", true);
                    break;
                case StateCharacter.Run:
                    anim.state.SetAnimation(0, "Run_0", true);
                    break;
                case StateCharacter.Fly:
                    anim.state.SetAnimation(0, "Attack_2B", true);
                    break;
            }
        }

        protected override bool CheckCondition(StateCharacter name)
        {
            return currentState != name;
        }

        public void PlayAnim(int track, string name, bool value, Spine.AnimationState.TrackEntryDelegate callback = null)
        {
            anim.state.SetAnimation(track, name, value).Complete += callback;
        }
    }
}
