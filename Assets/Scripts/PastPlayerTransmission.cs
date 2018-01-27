using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PastPlayerTransmission : MonoBehaviour
{

    public GameObject tooltipPrefab;
    public GameObject myTooltip;

    public BackendScript.EntityData transmissionData;

    void Start()
    {
        // Instantiate my tooltip
        myTooltip = Instantiate(tooltipPrefab);
        myTooltip.transform.SetParent(GameObject.Find("TooltipCanvas").transform, false);

        myTooltip.GetComponent<TooltipPlacer>().attachedObject = this.gameObject;

        myTooltip.GetComponent<TooltipPlacer>().SetText(transmissionData.username + " says: " + transmissionData.message);
    }




    // Update is called once per frame
    void Update()
    {

    }
}
