using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public enum SIDE
{
  NONE = 0,
  PLAYER,
  ENEMY,
}

public enum ENEMY_TYPE
{
    NORMAL_ENEMY,
    SWING_ENEMY,//摇摆的敌人
    SPEED_ENEMY,
    BOSS,
}