using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId;
    public bool isFlipped = false;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    public Sprite foreImg;
    [SerializeField]
    private Sprite bcgImg;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        spriteRenderer.sprite = bcgImg;
    }

    public void FlipCard()
    {
        animator.SetTrigger("doFlip");
        if (isFlipped)
        {
            isFlipped = false;
            StartCoroutine(ChangeSprite(bcgImg));
        }
        else
        {
            isFlipped = true;
            StartCoroutine(ChangeSprite(foreImg));
        }
    }

    private IEnumerator ChangeSprite(Sprite s)
    {
        yield return new WaitForSeconds(.2f);
        spriteRenderer.sprite = s;
    }
}
