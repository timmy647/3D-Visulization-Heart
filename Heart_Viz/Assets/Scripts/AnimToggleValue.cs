using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToggleValue : MonoBehaviour
{
    public string AnimValue = string.Empty;
    public float MaxValue = 1.0f;
    private float DestValue = 0f;
    private float CurrentValue = 0f;
    public float Speed = 2f;
    private Animator ThisAnimator = null;

    private void Awake()
    {
        ThisAnimator = GetComponent<Animator>();
        DestValue = CurrentValue = 0f;
    }

    public void ToggleValue()
    {
        DestValue = (DestValue < MaxValue) ? MaxValue : 0f;
    }

    private void Update()
    {
        CurrentValue = Mathf.Lerp(CurrentValue, DestValue, Time.deltaTime * Speed);
        ThisAnimator.SetFloat(AnimValue, CurrentValue);

        //Debug lines
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ToggleValue();
        }
    }
}
