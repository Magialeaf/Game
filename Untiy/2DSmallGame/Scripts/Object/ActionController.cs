using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionController : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private void Start()
    {
        // ��ȡPlayerInput���
        playerInput = GetComponent<PlayerInput>();

        // ��ȡAction Map
        actionMap = playerInput.currentActionMap;
    }

    public void Jump()
    {
        InputAction jumpAction = playerInput.currentActionMap.FindAction("Jump");
    }
}
