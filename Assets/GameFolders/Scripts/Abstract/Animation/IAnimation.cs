using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeniorProject.Abstract.Animation{

    public interface IAnimation
    {
        void MoveAnimation(float moveX, float moveZ);
        void JumpAnimation(bool isJumping);
        void FallAnimation(bool isFalling);
        void LandAnimation(bool isLanding);
    }

}

