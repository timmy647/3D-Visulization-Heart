using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputAxisControl : MonoBehaviour
{
    private CinemachineFreeLook FLC = null;
    private CinemachineFreeLook.Orbit[] OriginalOrbits = new CinemachineFreeLook.Orbit[3];

    [Range(-8f,8f)]
    public float ZoomRange = 0f;
    public float MouseWheelSpeed = 2f;
    public float ZoomDampSpeed = 2f;
    public Slider SliderControl = null;

    private void Awake()
    {
        FLC = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = GetAxisCustom;

        for (int i = 0; i < 3; i++)
            OriginalOrbits[i] = FLC.m_Orbits[i];
    }

    float GetAxisCustom(string Axis)
    {
        if(Axis.Equals("Horizontal"))
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                return Input.GetAxis("Mouse X");
            #endif

            #if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
                return Input.touches[0].deltaPosition.x;
            #endif

            return Input.GetAxis("Horizontal");
        }


        if (Axis.Equals("Vertical"))
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                return Input.GetAxis("Mouse Y");
#endif

#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
                return Input.touches[0].deltaPosition.y;
#endif

            return Input.GetAxis("Vertical");
        }

        return 0f;
    }

    private void Update()
    {
        ScaleOrbits();
    }

    void ScaleOrbits()
    {
        if (Mathf.Abs(Input.mouseScrollDelta.y) == 0) return;

        ZoomRange -= Input.mouseScrollDelta.y * MouseWheelSpeed;
        ZoomRange = Mathf.Clamp(ZoomRange, -8f, 8f);

        for (int i = 0; i < 3; i++)
            FLC.m_Orbits[i].m_Radius = Mathf.Lerp(FLC.m_Orbits[i].m_Radius, OriginalOrbits[i].m_Radius + ZoomRange, Time.deltaTime * ZoomDampSpeed);
    }

    public void UpdateSlider()
    {
        ZoomRange = SliderControl.value;

        for (int i = 0; i < 3; i++)
            FLC.m_Orbits[i].m_Radius = Mathf.Lerp(FLC.m_Orbits[i].m_Radius, OriginalOrbits[i].m_Radius + ZoomRange, Time.deltaTime * ZoomDampSpeed);
    }
}
