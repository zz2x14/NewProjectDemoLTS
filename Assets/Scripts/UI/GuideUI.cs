using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

//Sign:字典的key是没有setter的 即便key的类的实例本身可以更改 当它作为key时是不能更改的
//Sign：禁用输入方法在初始化方法中无法正确生效，判断时间过短；在轨道中不能生效的原因是在途中禁用了单项操作又在结尾恢复了所有操作

public class GuideUI : MonoBehaviour
{
    // private PlayerInput playerInput;
    
    //[SerializeField] private InputType respondInput;

    // private void Awake()
    // {
    //     playerInput = FindObjectOfType<PlayerInput>();
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // playerInput.inputRespondTable[respondInput] -= DestroySelf;
        // playerInput.guideUIList.Remove(this);
        //
        // switch (respondInput)
        // {
        //     case InputType.Move:
        //         playerInput.EnableJumpInput();
        //         break;
        //     case InputType.Jump:
        //         playerInput.EnableAttackInput();
        //         playerInput.EnableClimbUpInput();
        //         break;
        //     case InputType.ClimbUp:
        //         playerInput.EnableFallInput();
        //         break;
        // }
    }

    
    // public void DestroySelf()
    // {
    //     Destroy(gameObject);
    // }
}


