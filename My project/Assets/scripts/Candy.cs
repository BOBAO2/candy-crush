using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public int x, y;
    public Board board;
    public SpriteRenderer spriteRenderer;

    private void OnMouseDown()
    {
        board.SelectCandy(this);
    }

    public void Init(int x, int y, Board board, Sprite sprite)
    {
        this.x = x;
        this.y = y;
        this.board = board;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}