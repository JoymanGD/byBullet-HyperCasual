using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchLineDrawer : MonoBehaviour
{
    RectTransform TouchLine;
    Image TouchLineImage;
    float LineWidth {
                        get => TouchLine.sizeDelta.x;
                        set => TouchLine.sizeDelta = new Vector2(value, TouchLine.sizeDelta.y);
                    }

    void Start()
    {
        TouchLine = GetComponent<RectTransform>();
        TouchLineImage = GetComponent<Image>();

        var controller = ComponentsSL.GetService(typeof(PlayerControllerOld)) as PlayerControllerOld;
        
        // controller.PlayerControls.Main.Tap.started += cntxt => {
        //     if(GameManager.GameStarted){
        //         TouchLineImage.enabled = true;
        //         TouchLine.position = Pointer.current.position.ReadValue();
        //         TouchLine.sizeDelta = new Vector2(LineWidth, 0);
        //     }
        // };
        
        controller.PlayerControls.Main.Position.performed += cntxt => {
            if(TouchLineImage.enabled){
                var inputPosition = cntxt.ReadValue<Vector2>();
                var direction = inputPosition - (Vector2)TouchLine.position;
                TouchLine.sizeDelta = new Vector2(LineWidth, direction.magnitude);
                TouchLine.up = direction.normalized;
            }
        };

        // controller.PlayerControls.Main.Tap.canceled += cntxt => {
        //     if(TouchLineImage.enabled){
        //         TouchLineImage.enabled = false;
        //     }
        // };
    }
}
