using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPlacer : MonoBehaviour
{

    public GameObject attachedObject;

    public Text myWords;

	// How many ticks has it been since we started the text garble animation
	public int textAnimTicks;

	// Start/finish goals of the text garbling
	public string garbleStart;
	public string garbleEnd;

    // Use this for initialization
    void Start()
    {

    }

    // Late update, since apparently changing the rect size during animation messes with the position of my precious rectangles
    void LateUpdate()
    {
        RectTransform my_transform = (RectTransform)transform;
        my_transform.position = Camera.main.WorldToScreenPoint(attachedObject.transform.position);
    }

    public void SetText(string words)
    {
        myWords.text = words;
    }

	public void OpenTooltip()
	{
		GetComponent<Animator> ().SetBool ("tooltipOpen", true);


	}

	public void CloseTooltip()
	{
		GetComponent<Animator> ().SetBool ("tooltipOpen", false);
	}
}
