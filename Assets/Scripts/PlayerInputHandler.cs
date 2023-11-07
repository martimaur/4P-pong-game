using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private Mover mover;

    [SerializeField] private MeshRenderer playerMesh;

    private NewPlayerControls controls;
    private void Awake()
    {
        controls = new NewPlayerControls();
        mover = GetComponent<Mover>();
    }

    public void OnMove(CallbackContext context)
    {
        if (mover != null)
            mover.SetInput(context.ReadValue<Vector2>());
    }

    public void InitPlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        SetMaterial(pc.playerMaterial);
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Menu.Moving.name)
        {
            OnMove(obj);
        }
    }

    private void SetMaterial(Material newMat) 
    {
        Material[] materials = playerMesh.materials;
        materials[materials.Length-1] = newMat; //gets last material (should be color)
        playerMesh.materials = materials;
        Debug.Log("Material swapped succesfully!");
    }
}