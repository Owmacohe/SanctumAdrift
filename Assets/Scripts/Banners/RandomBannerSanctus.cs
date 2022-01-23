using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBannerSanctus : MonoBehaviour
{
    public enum BannerSizes { Small, Medium, Large, Huge };
    [Tooltip("Length of the banner to flutter")]
    public BannerSizes size;

    private void Start()
    {
        string text = "";
        string[] smallText = { "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx", "yzab" };
        string[] mediumText = { "abcdefgh", "ijklmnop", "qrstuvwx", "yzabcdef" };

        // Setting the text from a random string
        switch (size)
        {
            case BannerSizes.Small:
                text = getRandomFromArray(smallText);
                break;
            case BannerSizes.Medium:
                text = getRandomFromArray(mediumText);
                break;
        }

        for (int i = 0; i < text.Length; i++)
        {
            float offset = -0.35f;

            // Making the first offset a little smaller so that it aligns with the banner
            if (i == 0)
            {
                offset = -0.25f;
            }

            // Creating and positioning the new letter
            GameObject newLetter = Instantiate(Resources.Load<GameObject>("Misc/SanctusLetter"), transform);
            newLetter.transform.position = new Vector3(
                transform.position.x,
                transform.position.y + (offset * (i + 1)),
                transform.position.z
            );

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

    private string getRandomFromArray(string[] arr)
    {
        return arr[Random.Range(0, arr.Length - 1)];
    }
}
