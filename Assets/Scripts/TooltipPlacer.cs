using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPlacer : MonoBehaviour
{

    public GameObject attachedObject;

    public Text myWords;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RectTransform my_transform = (RectTransform)transform;
        my_transform.position = Camera.main.WorldToScreenPoint(attachedObject.transform.position);
    }

    public void SetText(string words)
    {
        myWords.text = words;
    }
}
