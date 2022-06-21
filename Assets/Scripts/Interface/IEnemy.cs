using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Transform PlayerPos { get; }
    Vector3 OriginalPos { get;}

    bool FoundPlayer { get;}

    bool CloseToTarget(Vector3 target,float distance);
    bool CloseToPlayer();

    void MoveToTargetHorizontal(float speed, Vector3 target);
    void MoveToTarget(float speed, Vector3 target);
   
    void SetRbVelocity(Vector2 velocity);
    void SetRbVelocityOnlyX(Vector2 velocity);
    void SetRbVelocityOnlyY(Vector2 velocity);
    void SetRbVelocityX(float moveX);
    void SetRbVelocityY(float velocityY);

    void Attack1();

    void FaceToTarget(Vector3 target);
    void FaceToPlayer();
}
