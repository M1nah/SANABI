using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelecteLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public GameObject [] levelbtn;
   public Image levelSelect;

   public Sprite unselectLevel;
   public Sprite selectLevel;

    // Start is called before the first frame update
    void Start()
    {
        levelSelect = GetComponent<Image>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ResetImage();
        levelSelect.sprite = selectLevel;
        ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetImage();
    }


    public void ResetImage()
    {

        if (levelbtn[0])
        {
            levelSelect.sprite = unselectLevel;
        }
        if (levelbtn[1])
        {
            levelSelect.sprite = unselectLevel;
        }
        if (levelbtn[2])
        {
            levelSelect.sprite = unselectLevel;
        }
        if (levelbtn[3])
        {
            levelSelect.sprite = unselectLevel;
        }
    }

}
