using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> TextMeshProList;
    public GameObject PanelDef;
    int spacebet = 120;
    private void Awake()
    {
        
    }
    void Start()
    {
        foreach (GameObject Obj in TextMeshProList)
        {
            Obj.SetActive(false);
            if (TextMeshProList.IndexOf(Obj) != 0) {
                Obj.transform.position = new Vector3(Obj.transform.position.x, TextMeshProList[TextMeshProList.IndexOf(Obj) - 1].transform.position.y - spacebet, Obj.transform.position.z);
            }
            else
            {
                Obj.transform.position = new Vector3(Obj.transform.position.x, 10, Obj.transform.position.z);
            }
            print(TextMeshProList[1].transform.position.y);
            /*
            print(PanelDef.GetComponent<RectTransform>().rect.yMax);
            print(PanelDef.GetComponent<RectTransform>().rect.yMin);
            print(TextMeshProList.IndexOf(Obj) + "-" + Obj.name + " ->" + Obj.transform.position.x);
            print(PanelDef.GetComponent<RectTransform>().anchorMin.y);
            print(PanelDef.GetComponent<RectTransform>().anchorMax.y);
            print(PanelDef.GetComponent<RectTransform>().anchoredPosition.y);
            print(PanelDef.GetComponent<RectTransform>().rect.yMax);
            print(PanelDef.GetComponent<RectTransform>().rect.yMin);
            print("Panel Max: " + PanelDef.GetComponent<RectTransform>().rect.yMax + " - Panel " + Obj.name +" Min: " + PanelDef.GetComponent<RectTransform>().rect.yMin + " - Obj Y pos: " + Obj.transform.position.y);
            */
            print(PanelDef.GetComponent<RectTransform>().rect.yMax);
            print(PanelDef.GetComponent<RectTransform>().rect.yMin);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(TextMeshProList[1].transform.position.y);
        foreach (GameObject Obj in TextMeshProList)
        {
            if (Obj.transform.position.y <= PanelDef.GetComponent<RectTransform>().rect.yMax + 210)
            {
                Obj.SetActive(true);
            }
            else
            {
                Obj.SetActive(false);
            }

            if (Obj.transform.position.y > PanelDef.GetComponent<RectTransform>().rect.yMax + 220)
            {
                int Previndex = TextMeshProList.IndexOf(Obj) - 1;
                if(Previndex <= 0)
                {                    
                    Previndex = TextMeshProList.Count - 1;
                }
                Obj.transform.position = new Vector3(Obj.transform.position.x, TextMeshProList[Previndex].transform.position.y - spacebet, Obj.transform.position.z);
            }
            Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y + 1, Obj.transform.position.z);
        }
    }}
