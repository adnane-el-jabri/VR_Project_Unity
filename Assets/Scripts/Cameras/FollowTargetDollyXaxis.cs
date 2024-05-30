using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that follows a target despite how far the path is and in which direction the player is going. Auto-Dolly must be disabled in order to use this extension and it must be used a CinemachineSmoothPath.
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class FollowTargetDollyXaxis : CinemachineExtension
{
    public enum ThresholdAxis { X, Y, Z }

    [Tooltip("It determines which axis to use for the threshold beyond which is activated the dolly. The threshold is calculated according to the position of the path first and last waypoint.")]
    public ThresholdAxis thresholdAxis = ThresholdAxis.X;
    [Tooltip("If the previous live camera has an offset put it here, consider only the thresholdAxis value")]
    public float previousCameraOffset = 0;
    [Tooltip("Boost the priority of the camera so to make it active.")]
    public int priorittBoost = 1;


    CinemachineTrackedDolly dolly;
    float sequenceStart;
    float sequenceEnd;
    bool following = false;

    void Start()
    {
        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();

        CinemachineSmoothPath path = (CinemachineSmoothPath)dolly.m_Path;

        SetSequenceExtremes(path);
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {

        float targetPos = GetTargetPos(vcam);

        if (!following && targetPos >= sequenceStart)
        {
            following = true;
            vcam.Priority += priorittBoost;
            vcam.MoveToTopOfPrioritySubqueue();
        }
        else if (following && targetPos < sequenceStart)
        {
            following = false;
            vcam.Priority -= priorittBoost;

        }

        if (following)
        {

            dolly.m_PathPosition = Mathf.InverseLerp(sequenceStart, sequenceEnd, targetPos);
        }
    }

    private void SetSequenceExtremes(CinemachineSmoothPath p)
    {
        switch (thresholdAxis)
        {
            case ThresholdAxis.Y:
                sequenceStart = p.m_Waypoints[0].position.y + gameObject.transform.position.y - previousCameraOffset;
                sequenceEnd = p.m_Waypoints[p.m_Waypoints.Length - 1].position.y + gameObject.transform.position.y - previousCameraOffset;
                break;
            case ThresholdAxis.X:
                sequenceStart = p.m_Waypoints[0].position.x + gameObject.transform.position.x - previousCameraOffset;
                sequenceEnd = p.m_Waypoints[p.m_Waypoints.Length - 1].position.x + gameObject.transform.position.x - previousCameraOffset;
                break;
        }
    }

    private float GetTargetPos(CinemachineVirtualCameraBase vcam)
    {
        switch (thresholdAxis)
        {
            // #TODO IMPLEMENT OTHER AXIS ALSO HERE
            case ThresholdAxis.Y:
                return vcam.Follow.position.y;
            case ThresholdAxis.X:
                return vcam.Follow.position.x;
            default:
                Debug.LogError("No thresholdAxis selected! Returning 0");
                return 0;
        }
    }
}

