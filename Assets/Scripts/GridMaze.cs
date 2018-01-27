using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaze : MonoBehaviour 
{
    [SerializeField]
    private bool _areGridTilesVisible = true;

	// Use this for initialization
	void Start () 
    {
		var sprites = GetComponentsInChildren(typeof(SpriteRenderer));

		
		if (sprites != null)
        {
			foreach(var gridTile in sprites)
            {
                if (_areGridTilesVisible)
                    gridTile.gameObject.SetActive(true);
                else
                    gridTile.gameObject.SetActive(false);
            }
        }
	}
}
