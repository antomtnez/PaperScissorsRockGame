using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ReadyMessageAnimation : MonoBehaviour{
    [SerializeField] GameObject MicImage;

    public void StartAnimation(){
        transform.DOScale(Vector3.one, .4f).OnComplete(()=>{
            StartCoroutine(DelayedHideAnimation());
        });
        MicImage.transform.DOLocalMoveY(73, .7f);
    }

    IEnumerator DelayedHideAnimation(){
        yield return new WaitForSeconds(1.5f);
        HideAnimation();
    }

    void HideAnimation(){
        transform.DOScale(Vector3.zero, .4f).OnComplete(()=>{
            FindObjectOfType<Match>().enabled = true;
        });
        MicImage.transform.DOLocalMoveY(1000, .7f);
    }
}
