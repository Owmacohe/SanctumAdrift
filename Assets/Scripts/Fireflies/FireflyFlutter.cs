using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyFlutter : MonoBehaviour
{
    [Range(1, 5)]
    public int lightChangeSpeed = 2;
    [Range(0.1f, 2)]
    public float flutterFrequency = 2;
    [Range(0.1f, 1)]
    public float flutterSpeed = 0.5f;

    [HideInInspector]
    public Vector3 halfSpawnerBoxSize;
    private Color defaultColour, complimentaryColour;
    private Light fireflyGlow;
    private Rigidbody rb;
    private bool isWinkingOut;
    private float incrementTimes;

    private void Start()
    {
        fireflyGlow = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        incrementTimes = 100;

        defaultColour.r = fireflyGlow.color.r;
        defaultColour.g = fireflyGlow.color.g;
        defaultColour.b = fireflyGlow.color.b;

        complimentaryColour.r = 1 - defaultColour.r;
        complimentaryColour.g = 1 - defaultColour.g;
        complimentaryColour.b = 1 - defaultColour.b;

        StartCoroutine(winkIn());

        moveRandom();
    }

    private void Update()
    {
        if (Random.Range(0, 100) <= flutterFrequency)
        {
            moveRandom();
        }

        if (
            (transform.localPosition.x < -halfSpawnerBoxSize.x || transform.localPosition.x > halfSpawnerBoxSize.x) ||
            (transform.localPosition.y < -halfSpawnerBoxSize.y || transform.localPosition.y > halfSpawnerBoxSize.y) ||
            (transform.localPosition.z < -halfSpawnerBoxSize.z || transform.localPosition.z > halfSpawnerBoxSize.z)
        )
        {
            StartCoroutine(winkOut());
        }
    }

    private void moveRandom()
    {
        rb.velocity = getRandomVector3(flutterSpeed);
        rb.rotation = Quaternion.Euler(getRandomVector3(360));
    }

    private Vector3 getRandomVector3(float max)
    {
        return new Vector3(
            Random.Range(-max, max),
            Random.Range(-max, max),
            Random.Range(-max, max)
        );
    }

    private IEnumerator winkIn()
    {
        float glowInterval = fireflyGlow.intensity / 100;
        fireflyGlow.intensity = 0;

        for (int i = 0; i < incrementTimes; i++)
        {
            fireflyGlow.intensity += glowInterval;

            yield return new WaitForSeconds(0.01f / lightChangeSpeed);
        }

        StartCoroutine(changeLightColour(defaultColour, complimentaryColour));
    }

    public IEnumerator winkOut()
    {
        isWinkingOut = true;

        float glowInterval = fireflyGlow.intensity / 100;

        for (int i = 0; i < incrementTimes; i++)
        {
            fireflyGlow.intensity -= glowInterval;

            yield return new WaitForSeconds(0.01f / lightChangeSpeed);
        }

        Destroy(gameObject);
    }

    private IEnumerator changeLightColour(Color startColour, Color endColour)
    {
        if (!isWinkingOut)
        {
            Vector3 incrementAmounts = new Vector3(
                Mathf.Abs(startColour.r - endColour.r) / incrementTimes,
                Mathf.Abs(startColour.g - endColour.g) / incrementTimes,
                Mathf.Abs(startColour.b - endColour.b) / incrementTimes
            );

            for (int i = 0; i < incrementTimes; i++)
            {
                Vector3 newColour = new Vector3(
                    fireflyGlow.color.r,
                    fireflyGlow.color.g,
                    fireflyGlow.color.b
                );

                if (startColour.r > endColour.r)
                {
                    newColour.x -= incrementAmounts.x;
                }
                else
                {
                    newColour.x += incrementAmounts.x;
                }

                if (startColour.g > endColour.g)
                {
                    newColour.y -= incrementAmounts.y;
                }
                else
                {
                    newColour.y += incrementAmounts.y;
                }

                if (startColour.b > endColour.b)
                {
                    newColour.z -= incrementAmounts.z;
                }
                else
                {
                    newColour.z += incrementAmounts.z;
                }

                fireflyGlow.color = new Color(newColour.x, newColour.y, newColour.z, 0.5f);
                GetComponent<MeshRenderer>().material.color = fireflyGlow.color;

                yield return new WaitForSeconds(0.01f / lightChangeSpeed);
            }

            StartCoroutine(changeLightColour(endColour, startColour));
        }
    }
}
