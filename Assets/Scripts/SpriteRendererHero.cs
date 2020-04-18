using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererHero : MonoBehaviour
{
    [SerializeField] private Sprite[] frameArray;
    //RANGE SPRITES DROITE
    int MinRightRange = 1;
    int MaxRightRange = 50;
    //RANGE SPRITES GAUCHE
    int MinLeftRange = 1;
    int MaxLeftRange = 50;
    //RANGE SPRITES UP
    int MinUpRange = 1;
    int MaxUpRange = 50;
    //RANGE SPRITES DOWN
    int MinDownRange = 1;
    int MawDownRange = 50;

    private int currentFrame = 0;
    private float timer;
    private float framerate = .5f;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= framerate)
        {
            if(GetComponent<Hero>().move != Vector2.zero)
            { 
                timer -= framerate;
                currentFrame = (currentFrame + 1) % frameArray.Length;
                spriteRenderer.sprite = frameArray[currentFrame];
            }
            else
            {
                timer -= framerate;
                currentFrame = (currentFrame + 1) % frameArray.Length;
                spriteRenderer.sprite = frameArray[currentFrame];
            }
        }
    }
}
