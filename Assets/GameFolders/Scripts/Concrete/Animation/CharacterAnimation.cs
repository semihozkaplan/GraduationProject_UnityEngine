using System.Collections;
using System.Collections.Generic;
using SeniourProject.Abstract.Animation;
using UnityEngine;

namespace SeniourProject.Concrete.Animation{
    public class CharacterAnimation : IAnimation
    {
        Animator _animator;

        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
        }

        void IAnimation.WalkAnimation()
        {
            throw new System.NotImplementedException();
        }

        void IAnimation.RunAnimation()
        {
            throw new System.NotImplementedException();
        }

        void IAnimation.JumpAnimation()
        {
            throw new System.NotImplementedException();
        }

    }
}


