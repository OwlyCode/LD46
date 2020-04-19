using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererHero : MonoBehaviour
{
    [SerializeField] private Sprite[] frameArray;
    //RANGE SPRITES NON MOVE
    int MinNonMoveRange = 0;
    int MaxNonMoveRange = 3;
    //RANGE SPRITES DROITE
    int MinRightRange = 4;
    int MaxRightRange = 9;
    //RANGE SPRITES GAUCHE
    int MinLeftRange = 10;
    int MaxLeftRange = 15;
    //RANGE SPRITES UP
    int MinDownRange = 16;
    int MaxDownRange = 21;
    //RANGE SPRITES JUMP
    int MinJumpRange = 22;
    int MaxJumpRange = 27;
    bool firstExit = false;
    bool firstMove = false;

    private int currentFrame = 0;
    private float timer;
    private float framerate = .05f;
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
        framerate = .05f;
        if (timer >= framerate)
        {
            Vector2 Vectn = GetComponent<Hero>().move;
            //ON BOUGE
            timer -= framerate;
            if (Vectn != Vector2.zero)
            {
                if (Vectn.x > 0)
                {
                    if (firstMove)
                    {
                        currentFrame = MinRightRange;
                    }
                    if (currentFrame + 1 <= MaxRightRange)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = MinRightRange;
                    }
                }
                else if (Vectn.x < 0)
                {
                    if (firstMove)
                    {
                        currentFrame = MinLeftRange;
                    }
                    if (currentFrame + 1 <= MaxLeftRange)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = MinLeftRange;
                    }
                }
                else if (Vectn.y >= 0)
                {
                    if (firstMove)
                    {
                        currentFrame = MinDownRange;
                    }
                    if (currentFrame + 1 <= MaxDownRange)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = MinDownRange;
                    }
                }
                else if (Vectn.y < 0)
                {
                    if (firstMove)
                    {
                        currentFrame = MinDownRange;
                    }
                    if (currentFrame + 1 <= MaxDownRange)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = MinDownRange;
                    }
                }
                spriteRenderer.sprite = frameArray[currentFrame];
                firstExit = true;
                firstMove = false;
            }
            //ON BOUGE PAS
            else
            {
                if (firstExit)
                {
                    currentFrame = 0;
                    firstExit = false;
                }
                firstMove = true;
                framerate = .2f;
                timer -= framerate;
                if (currentFrame + 1 <= MaxNonMoveRange)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame = MinNonMoveRange;
                }
                spriteRenderer.sprite = frameArray[currentFrame];
            }
        }
    }
}
