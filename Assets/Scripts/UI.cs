using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI manager;

    public TextMeshProUGUI p1scoreText;
    public TextMeshProUGUI p2scoreText;

    private void Awake()
    {
        manager = this;
    }
}
