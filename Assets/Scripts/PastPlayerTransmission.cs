using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PastPlayerTransmission : MonoBehaviour
{

    public GameObject tooltipPrefab;
    public GameObject myTooltip;

	public BackendScript.EntityData transmissionData;

	public float distanceToPlayer;

	private PlayerScript player;

    void Start()
    {
        // Instantiate my tooltip
        myTooltip = Instantiate(tooltipPrefab);
        myTooltip.transform.SetParent(GameObject.Find("TooltipCanvas").transform, false);

        myTooltip.GetComponent<TooltipPlacer>().attachedObject = this.gameObject;

		myTooltip.GetComponent<TooltipPlacer>().SetText(transmissionData.message);
    }

	void Awake()
	{
		player = FindObjectOfType<PlayerScript> ();
	}


    // Update is called once per frame
    void Update() {
		// Get distance to player. If close enough, animate opening
		distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

		if (distanceToPlayer < 1.18f) {
			myTooltip.GetComponent<TooltipPlacer> ().OpenTooltip ();
		} else {
			myTooltip.GetComponent<TooltipPlacer> ().CloseTooltip ();
		}
    }
}
