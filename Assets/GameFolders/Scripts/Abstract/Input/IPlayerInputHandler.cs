using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInputHandler
{

    Vector2 GetMoveInput();
    bool GetJumpInput();
    bool GetShootInput();

}
