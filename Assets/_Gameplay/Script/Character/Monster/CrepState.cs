using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum StateCreep
    {
        Idle,
        Run,
        Jump,
    }
    public class CrepState : BaseState<StateCreep>
    {
        public CrepState(BaseCharacter character, SkeletonAnimation animation) : base(character, animation) { }

        public override void Init()
        {
            currentState = StateCreep.Idle;
            UpdateAnimation();
        }

        protected override void UpdateAnimation(Spine.AnimationState.TrackEntryDelegate callback = null)
        {
            switch (currentState)
            {
                case StateCreep.Idle:
                    anim.state.SetAnimation(0, "Idle_0", true);
                    break;
                case StateCreep.Jump:
                    anim.state.SetAnimation(0, "Jump_0", true);
                    break;
                case StateCreep.Run:
                    anim.state.SetAnimation(0, "Run_0", true);
                    break;
            }
        }

        protected override bool CheckCondition(StateCreep name)
        {
            return currentState != name;
        }

        public void PlayAnim(int track, string name, bool value, Spine.AnimationState.TrackEntryDelegate callback = null)
        {
            anim.state.SetAnimation(track, name, value).Complete += callback;
        }
    }
}
