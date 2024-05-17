using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Refereneces")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Turret turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if(UIManager.instance.IsHoveringUI()) { return; }
        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }
        Tower towerToBuild = BuildManager.instance.GetSelectedTower();

        if(towerToBuild.cost >= LevelManager.instance.currency)
        {
            Debug.Log("ko du tien");
            return;
        }
        LevelManager.instance.SpendCurrency(towerToBuild.cost);


        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();

    }
}
