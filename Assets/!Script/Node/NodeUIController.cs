using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeUIController : MonoBehaviour
{
    public GameObject nodeCanvas;
    public TMP_Text priceText;

    public Node currentNode;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue))
            {
                Node node = hitInfo.collider.gameObject.GetComponent<Node>();
                if (node == null)
                {
                    HideNodeUI();
                }
            }
            else
            {
                HideNodeUI();

            }
        }
    }

    public void ShowNodeUI(Node node)
    {
        currentNode = node;
        transform.position = node.transform.position;
        priceText.text = node.currentTurret.GetComponent<BaseTurret>().sellprice + "$";
        nodeCanvas.SetActive(true);
    }

    public void OnSellBtnClick()
    {
        if(currentNode == null)
        {
            HideNodeUI();
            return;
        }

        MainGameController.instance.gold += currentNode.currentTurret.GetComponent<BaseTurret>().sellprice;
        Destroy(currentNode.currentTurret);
        currentNode.currentTurret = null;
        HideNodeUI();
    }

    public void HideNodeUI()
    {
        nodeCanvas.SetActive(false);
    }
}
