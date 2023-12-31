using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    private Node target;
    
    public void SetTarget(Node _target)
    {
        
        target = _target;

        transform.position = target.GetBuildPosition();

        upgradeCost.text  = "$" + target.turretBlueprint.cost;

       // ui.SetActive(true);
    }

    public void Hide()
    {
        //ui.SetActive(false);
    }
    
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode(); 
    }
    

}
