using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomLaserPointer : MonoBehaviour
{
    public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Input_Sources handType;

    private SteamVR_Behaviour_Pose pose;
    private SteamVR_LaserPointer laserPointer;
    private EventSystem eventSystem;

    void Awake()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void OnEnable()
    {
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
    }

    private void OnDisable()
    {
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer In: " + e.target.gameObject.name);
        Button button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            Debug.Log("Button Selected: " + button.gameObject.name);
            if (interactWithUI.GetStateDown(pose.inputSource))
            {
                Debug.Log("Button Clicked: " + button.gameObject.name);
                button.onClick.Invoke();
            }
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer Out: " + e.target.gameObject.name);
        if (e.target.GetComponent<Button>() != null)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    void Update()
    {
        if (interactWithUI.GetStateDown(pose.inputSource))
        {
            PointerEventData data = new PointerEventData(eventSystem);
            data.position = new Vector2(Screen.width / 2, Screen.height / 2);
            data.clickCount = 1;
            data.button = PointerEventData.InputButton.Left;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            eventSystem.RaycastAll(data, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}
