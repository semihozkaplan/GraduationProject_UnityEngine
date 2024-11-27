using System.Collections;
using System.Collections.Generic;
using SeniorProject.Abstract.Animation;
using UnityEngine;

namespace SeniorProject.Concrete.Animation{
    public class CharacterAnimation : IAnimation
    {
        private Animator _animator;
        private int moveXAnimationParameterID;
        private int moveZAnimationParameterID;

        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
            moveXAnimationParameterID = Animator.StringToHash("MoveX");
            moveZAnimationParameterID = Animator.StringToHash("MoveZ");
        }

        public void MoveAnimation(float moveX, float moveZ)
        {
            _animator.SetFloat(moveXAnimationParameterID, moveX);
            _animator.SetFloat(moveZAnimationParameterID, moveZ);
        }

    }
}


