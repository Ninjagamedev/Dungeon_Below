using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BuffCardBlueprint
{
    public BuffCard card;
    public int cost;

    public string getAttribute()
    {
        return card.Attribute;
    }

}

//Para adicionar uma carta nova basta adicionar a carta na lista da Shop, criar o prefab da carta