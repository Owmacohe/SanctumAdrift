using UnityEngine;

/// <summary>
/// Static class to generate nice-sounding Spirit names
/// </summary>
public abstract class SpiritName
{
    static readonly char[] vowels = { 'a', 'e', 'i', 'o', 'u' }; // Vowels to be used
    static readonly string[] consonants =
    {
        "b", "c", "ch", "d", "f", "g", "gh", "h", "j", "k", "l", "m", "n",
        "p", "qu", "r", "s", "sh", "t", "th", "v", "w", "wh", "x", "y", "z"
    }; // Consonants (and consonant sounds) to be used
    
    /// <summary>
    /// Creates a new fine-tuned Spirit name
    /// </summary>
    /// <returns>Generated spirit name</returns>
    public static string Generate()
    {
        // 50% chance to start with a vowel
        bool startsWithVowel = (Random.Range(0, 2) == 0);

        int syllables; // Number of syllables in the name
        int rand = Random.Range(0, 10);

        // 2 syllables = 20% chance
        if (rand <= 1)
        {
            syllables = 2;
        }
        // 3 syllables = 60% chance
        else if (rand <= 7)
        {
            syllables = 3;
        }
        // 4 syllables = 20% chance
        else
        {
            syllables = 4;
        }

        string temp = "";

        if (startsWithVowel)
        {
            temp += GetVowel();
            syllables--;
        }

        // Alternates consonants and vowels for each syllable
        for (int i = 0; i < syllables; i++)
        {
            bool hasRepeated = false;
            
            temp += GetConsonant();

            // 20% chance to add another consonant
            if (Random.Range(0, 5) == 0)
            {
                string tempConsonant = GetConsonant();

                // Repeating only if the last character and the new character aren't h, j, v, or x
                if (
                    !temp[temp.Length - 1].Equals('h') && !temp[temp.Length - 1].Equals('j') && !temp[temp.Length - 1].Equals('v') && temp[temp.Length - 1].Equals('x') &&
                    !tempConsonant[tempConsonant.Length - 1].Equals('h') && !tempConsonant[tempConsonant.Length - 1].Equals('j') && !tempConsonant[tempConsonant.Length - 1].Equals('v') && !tempConsonant[tempConsonant.Length - 1].Equals('x'))
                {
                    temp += tempConsonant;
                    hasRepeated = true;
                }
            }
            
            temp += GetVowel();
            
            // 20% chance to add another vowel (if another consonant hasn't already been added this syllable)
            if (!hasRepeated && Random.Range(0, 5) == 0)
            {
                char tempVowel = GetVowel();

                // Repeating only if the last character and the new character aren't the same
                if (!temp[temp.Length - 1].Equals(tempVowel))
                {
                    temp += tempVowel;
                }
            }
        }
        
        return Capitalize(temp);
    }
    
    /// <summary>
    /// Gets a random vowel from the vowel array
    /// </summary>
    /// <returns>The selected vowel</returns>
    static char GetVowel() { return vowels[Random.Range(0, vowels.Length)]; }
    
    /// <summary>
    /// Gets a random consonant from the consonant array
    /// </summary>
    /// <returns>The selected consonant</returns>
    static string GetConsonant() { return consonants[Random.Range(0, consonants.Length)]; }

    /// <summary>
    /// Capitalizes a string
    /// </summary>
    /// <param name="str">Given string to be capitalized</param>
    /// <returns>The given string with the first character capitalized</returns>
    static string Capitalize(string str)
    {
        char[] split = str.ToCharArray();

        split[0] = char.Parse(split[0].ToString().ToUpper());

        string temp = "";

        foreach (char i in split)
        {
            temp += i;
        }

        return temp;
    }
}