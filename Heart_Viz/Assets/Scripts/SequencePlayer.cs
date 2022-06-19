using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePlayer : MonoBehaviour
{
    private int Index = 0;

    private void Awake()
    {
        ResetItems();
    }

    public void ResetItems()
    {
        if (transform.childCount <= 0) return;

        Index = 0;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        transform.GetChild(Index).gameObject.SetActive(true);
    }

    public void NextItem()
    {
        if (transform.childCount <= 0) return;

        Index = Mathf.RoundToInt(Mathf.Repeat(Index + 1, transform.childCount));

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        transform.GetChild(Index).gameObject.SetActive(true);
    }
}
