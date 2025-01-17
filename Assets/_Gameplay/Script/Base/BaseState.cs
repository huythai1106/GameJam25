using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class BaseState<T>
    {
        public T currentState;
        public BaseCharacter character;
        public SkeletonAnimation anim;

        public BaseState(BaseCharacter character, SkeletonAnimation animation)
        {
            this.character = character;
            this.anim = animation;

            Init();
        }

        public virtual void Init()
        {

        }

        protected virtual void UpdateAnimation(Spine.AnimationState.TrackEntryDelegate callback = null)
        {

        }

        public void SetStatePlayer(T name, Spine.AnimationState.TrackEntryDelegate callback = null)
        {
            if (CheckCondition(name))
            {
                currentState = name;
                UpdateAnimation(callback);
            }
        }

        protected virtual bool CheckCondition(T name)
        {
            return true;
        }

        public void SetSkinCharacter(string name)
        {
            anim.skeleton.SetSkin(name);
            anim.skeleton.SetSlotsToSetupPose();
        }
    }
}
