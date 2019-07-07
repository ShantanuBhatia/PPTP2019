using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSpriteSorting : MonoBehaviour
{
    private SortingLayer[] layers;
    private SpriteRenderer[] sprites;


    public string targetSortingLayer;
    public int targetSortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        layers = SortingLayer.layers;
        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer spr in sprites)
        {
            spr.sortingLayerName = targetSortingLayer;
            spr.sortingOrder = targetSortingOrder;
        }
    }


}
