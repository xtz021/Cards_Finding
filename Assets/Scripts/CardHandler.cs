using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public bool isFaceUp = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FlipCard(Sprite sourceImg)
    {
        StartCoroutine(Flipping(sourceImg));
    }

    IEnumerator Flipping(Sprite sourceImg)
    {
        anim.Play("Flipping");
        yield return new WaitForSeconds(0.25f);
        Image img = GetComponent<Image>();
        img.sprite = sourceImg;
        isFaceUp = !isFaceUp;
        CardsManager cm = FindObjectOfType<CardsManager>();
        yield return new WaitForSeconds(0.25f);
        if (isFaceUp)
        {
            cm.faceUpCardList.Add(this);
        }
        else
        {
            cm.faceUpCardList.Remove(this);
        }
    }

    public void StopAllCour()
    {
        StopAllCoroutines();
    }

    public void OnClickHandler()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Flipping"))
        {
            return;
        }
        CardsManager cm = FindObjectOfType<CardsManager>();
        int pos = cm.FindCardPos(this);
        if(cm.faceUpCardList.Count == 1)
        {
            if(pos == cm.FindCardPos(cm.faceUpCardList[0]) || cm.faceUpCardList[0] == this || pos == cm.posJustClicked)
            {
                return;
            }
        }
        cm.posJustClicked = pos;
        if (cm.faceUpCardList.Count < 2)
        {
            int a = cm.FindCardPos(this);
            Sprite spr = cm.FaceCardOfPos(a);
            FlipCard(spr);
        }
    }
    

}
