using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 6, height = 6;
    public GameObject candyPrefab;
    public Sprite[] candySprites;

    private Candy[,] grid;
    private Candy selectedCandy;

    void Start()
    {
        grid = new Candy[width, height];
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnCandy(x, y);
            }
        }

        StartCoroutine(CheckMatches());
    }

    void SpawnCandy(int x, int y)
    {
        GameObject obj = Instantiate(candyPrefab, new Vector2(x, y), Quaternion.identity);
        obj.transform.parent = transform;
        Candy candy = obj.GetComponent<Candy>();
        Sprite sprite = candySprites[Random.Range(0, candySprites.Length)];
        candy.Init(x, y, this, sprite);
        grid[x, y] = candy;
    }

    public void SelectCandy(Candy candy)
    {
        if (selectedCandy == null)
        {
            selectedCandy = candy;
        }
        else
        {
            if (IsNeighbor(candy, selectedCandy))
            {
                StartCoroutine(SwapAndCheck(candy, selectedCandy));
            }
            selectedCandy = null;
        }
    }

    bool IsNeighbor(Candy a, Candy b)
    {
        return (Mathf.Abs(a.x - b.x) == 1 && a.y == b.y) ||
               (Mathf.Abs(a.y - b.y) == 1 && a.x == b.x);
    }

    IEnumerator SwapAndCheck(Candy a, Candy b)
    {
        Swap(a, b);

        yield return new WaitForSeconds(0.2f);

        var matches = GetMatches();

        if (matches.Count > 0)
        {
            yield return StartCoroutine(DestroyMatches(matches));
        }
        else
        {
            Swap(a, b); // undo swap
        }
    }

    void Swap(Candy a, Candy b)
    {
        Vector2 posA = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = posA;

        grid[a.x, a.y] = b;
        grid[b.x, b.y] = a;

        int tempX = a.x, tempY = a.y;
        a.x = b.x; a.y = b.y;
        b.x = tempX; b.y = tempY;
    }

    List<Candy> GetMatches()
    {
        List<Candy> matches = new List<Candy>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Candy current = grid[x, y];

                // Horizontal
                if (x > 1)
                {
                    if (current.spriteRenderer.sprite == grid[x - 1, y].spriteRenderer.sprite &&
                        current.spriteRenderer.sprite == grid[x - 2, y].spriteRenderer.sprite)
                    {
                        matches.Add(current);
                        matches.Add(grid[x - 1, y]);
                        matches.Add(grid[x - 2, y]);
                    }
                }

                // Vertical
                if (y > 1)
                {
                    if (current.spriteRenderer.sprite == grid[x, y - 1].spriteRenderer.sprite &&
                        current.spriteRenderer.sprite == grid[x, y - 2].spriteRenderer.sprite)
                    {
                        matches.Add(current);
                        matches.Add(grid[x, y - 1]);
                        matches.Add(grid[x, y - 2]);
                    }
                }
            }
        }

        return matches;
    }

    IEnumerator DestroyMatches(List<Candy> matches)
    {
        foreach (Candy candy in matches)
        {
            Destroy(candy.gameObject);
            grid[candy.x, candy.y] = null;
        }

        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FillBoard());
        yield return StartCoroutine(CheckMatches());
    }

    IEnumerator FillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null)
                {
                    SpawnCandy(x, y);
                }
            }
        }

        yield return null;
    }

    IEnumerator CheckMatches()
    {
        yield return new WaitForSeconds(0.2f);

        var matches = GetMatches();
        if (matches.Count > 0)
        {
            yield return StartCoroutine(DestroyMatches(matches));
        }
    }
}