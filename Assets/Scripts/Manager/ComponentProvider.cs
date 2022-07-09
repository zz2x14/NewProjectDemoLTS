using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentProvider : PersistentSingletonTool<ComponentProvider>//TODO:部分系统功能调用了这提供的内容 敌人并没有 思考这样做是否准确 是否有意义
{
    public PlayerController PlayerAvatar { get; private set; }
    public PlayerInput PlayerInputAvatar { get; private set; }

    public Transform PlayerPos { get; set; }

    protected override void Awake()
    {
        base.Awake();

        PlayerAvatar = FindObjectOfType<PlayerController>();
        PlayerInputAvatar = PlayerAvatar.GetComponent<PlayerInput>();
        PlayerPos = PlayerAvatar.transform;
    }
}
