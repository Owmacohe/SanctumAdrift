using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBannerSanctus : MonoBehaviour
{
    enum BannerSizes { Small, Medium, Large, Huge };
    [Tooltip("Length of the banner to flutter")]
    [SerializeField] BannerSizes size;

    float letterScale, startOffset, defaultOffset;

    void Start()
    {
        string text = "";
        string[] smallText = { "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx", "yzab" };
        string[] mediumText = { "abcdefgh", "ijklmnop", "qrstuvwx", "yzabcdef" };

        // Setting the text from a random string
        switch (size)
        {
            case BannerSizes.Small:
                text = GetRandomFromArray(smallText);
                letterScale = 0.13f;
                startOffset = -0.25f;
                defaultOffset = -0.35f;
                break;
            case BannerSizes.Medium:
                text = GetRandomFromArray(mediumText);
                letterScale = 0.1f;
                startOffset = -0.12f;
                defaultOffset = -0.18f;
                break;
        }

        float cumulativeOffset = 0;

        for (int i = 0; i < text.Length; i++)
        {
            float offset = defaultOffset;

            // Making the first offset a little smaller so that it aligns with the banner
            if (i == 0)
            {
                offset = startOffset;
            }

            cumulativeOffset += offset;

            // Creating and positioning the new letter
            GameObject newLetter = Instantiate(Resources.Load<GameObject>("Misc/SanctusLetter"), transform);
            newLetter.transform.localPosition = Vector3.up * cumulativeOffset;
            newLetter.transform.localScale = Vector3.one * letterScale;

            int asciiCode = text[i].ToString().ToUpper().ToCharArray()[0] - 64; // Getting the letter's ASCII code

            switch (asciiCode)
            {
                // Mapping F to V
                case 6:
                    asciiCode = 22;
                    break;
                // Mapping Q to K
                case 17:
                    asciiCode = 11;
                    break;
                // Mapping S to Z
                case 19:
                    asciiCode = 26;
                    break;
            }

            newLetter.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Sanctus/script_" + asciiCode); // Setting the new letter's sprite
        }
    }

    string GetRandomFromArray(string[] arr)
    {
        return arr[Random.Range(0, arr.Length - 1)];
    }
}
