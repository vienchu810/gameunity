using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Button[] caroclick;

    private void Awake()
    {
        for (int i = 0; i < caroclick.Length; i++)
        {
            caroclick[i] = transform.Find("GroupBtn/Button " + "(" + i + ")").GetComponent<Button>();
        }
    }
}
