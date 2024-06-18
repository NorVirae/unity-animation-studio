using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimationHandler : MonoBehaviour
{

    public Vector2 OverShoot = new Vector2(30, 0);
    public Vector2 StartOffset = new Vector2(30, 0);

    public float delayBetweenAnimations = 0.5f;
    public List<RectTransform> objects = new List<RectTransform>();
    // Start is called before the first frame update
    public List<Vector2> resetLocation = new List<Vector2>();
    public float duration = 2f;
    public float FadeDuration = 2f;
    public float OverShootDuration = 2f;


    void Start()
    {
        if (objects.Count > 0)
        {

            for (int i = 0; i < objects.Count; i++)
            {
                Debug.Log(i + " " + objects[i].name + " c " + objects.Count);
                resetLocation.Add(objects[i].transform.position);
                //objects[i].GetComponent<CanvasGroup>().alpha = 0;
            }
        }

        moveObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveObject()
    {
        if (objects.Count >= 0)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = resetLocation[i] + StartOffset;
                float targetDestinationX = resetLocation[i].x;
                Sequence mySequence = DOTween.Sequence();

                // Calculate the delay for this specific sequence
                float delay = i * delayBetweenAnimations;
                float fadeDuration = i * FadeDuration;
                // Add delay before starting this sequence
                mySequence.AppendInterval(delay);

                // Add a CanvasGroup component at runtime
                CanvasGroup canvasGroup = objects[i].GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = objects[i].gameObject.AddComponent<CanvasGroup>();
                }

                // Set initial alpha to 0
                canvasGroup.alpha = 0;
                //mySequence.Append(canvasGroup.DOFade(0, FadeDuration).SetEase(Ease.InSine));

                mySequence.Append(objects[i].DOMoveX(targetDestinationX - OverShoot.x, OverShootDuration).SetEase(Ease.OutQuad));
                //mySequence.Join(canvasGroup.DOFade(1, fadeDuration).SetAutoKill(true).SetEase(Ease.InOutSine));
                mySequence.Append(objects[i].DOMoveX(targetDestinationX, duration).SetEase(Ease.OutQuad));
                //mySequence.Join(canvasGroup.DOFade(1, FadeDuration).SetEase(Ease.InSine));
                canvasGroup.DOFade(1, FadeDuration).SetEase(Ease.InSine);


                // Start the sequence immediately
                mySequence.Play();
            }

        }
    }
}
