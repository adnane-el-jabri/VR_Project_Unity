using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.UI;

public class LaserButtonClicker : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private Button btn;
    private SteamVR_Behaviour_Pose pose;
    private bool pointerOnButton = false;
    private GameObject myEventSystem;

    void Start()
    {
        myEventSystem = GameObject.Find("EventSystem");
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        laserPointer.PointerIn += LaserPointer_PointerIn;
        laserPointer.PointerOut += LaserPointer_PointerOut;
    }

    private void LaserPointer_PointerOut(object sender, PointerEventArgs e)
    {
        if (btn != null)
        {
            pointerOnButton = false;
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            btn = null;
        }
    }

    private void LaserPointer_PointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.GetComponent<Button>() != null)
        {
            btn = e.target.GetComponent<Button>();
            btn.Select();
            pointerOnButton = true;
        }
    }

    void Update()
    {
        if (pointerOnButton && pose != null)
        {
            if (SteamVR_Actions.default_InteractUI.GetStateDown(pose.inputSource))
            {
                btn.onClick.Invoke();
            }
        }
    }
}
