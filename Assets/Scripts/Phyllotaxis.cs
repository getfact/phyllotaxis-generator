using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Phyllotaxis : MonoBehaviour {

    public Color colorOne;
    public Color colorTwo;
    public Color colorThree;

    public GameObject dot;
    public float degree;
    public float c;
    public int numberOfDots;
    public float dotScale;
    public float colorMultiplier;

    private Color currentColor;
    private Vector2 phyllotaxisPosition;
    private List<Color> colorPie = new List<Color>();
    private int previousNumberOfDots;

    void Start () {

        colorPie.Add(colorOne);
        colorPie.Add(colorTwo);
        colorPie.Add(colorThree);

        currentColor = colorOne;
    }

    void Update () {

        if (Input.GetButton("Jump")) {

            PlotDot();
        }
    }

    private void PlotDot () {

        if (numberOfDots > 0) {

            phyllotaxisPosition = CalculatePhyllotaxis(degree, c, numberOfDots);

            GameObject dotObj = Instantiate(dot) as GameObject;
            dotObj.transform.position = new Vector3(phyllotaxisPosition.x,
                                                    phyllotaxisPosition.y,
                                                    0f);

            SpriteRenderer sprite = dotObj.GetComponent<SpriteRenderer>();


            sprite.color = currentColor;
            dotObj.transform.localScale = new Vector3(dotScale, dotScale, dotScale);
        }

        numberOfDots++;

        if (previousNumberOfDots * colorMultiplier < numberOfDots) {

            ChangeColor();
            previousNumberOfDots = numberOfDots;
        }
    }

    private void ChangeColor () {

        int randomIndex = Random.Range(0, colorPie.Count);

        currentColor = colorPie[randomIndex];
    }

    private Vector2 CalculatePhyllotaxis (float degree, float scale, int count) {

        double angle = count * (degree * Mathf.Deg2Rad);
        float radius = scale * Mathf.Sqrt(count);
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);
        Vector2 vec = new Vector2(x, y);
        return vec;
    }
}
