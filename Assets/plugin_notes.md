# Plugin Notes

Notes/reminders about the main plugins that I’ll be using throughout production

---



## Dialogue System for Unity



### Setup

1. Add **Dialogue Manager** to scene

2. Create **Database** (if needed) from the **Dialogue System Controller** script

   - Author
   - Version
   - Description
   - etc.

3. Create **Conversations** within the **Database** (+ symbol)

   - Nodes
     - Start node (orange)
     - NPC dialogue (grey)
     - Player dialogue (blue)

   - Trees
     - Double click to edit node Dialogue Text
     - Right click > Create Child Node to create new node

4. Add **Dialogue System Trigger** script to the NPC

   1. **Add Action** > **Start Conversation**
   2. Select target **Conversation** from dropdown
      - Set **Actor** to the Player
      - Set **Conversant** to the NPC
   3. Set **Trigger** to **On Use** (other options available)

5. Add **Usable** script to the NPC

6. Add **Selector** script to the Player

   - Set **Select At** to **Mouse Position**
   - Set **Max Selection Distance** as wanted
   - Set **Text Style** fields as wanted for the popup
   - Set **Use Key** and **Use Button** as wanted for initiation
   - Set **Default Use Message** as wanted

7. Add **Dialogue System Events** script

   - Add the Player’s **Selector** script to **On Conversation Start**, set the function to **Selector.enabled**, and set the value to false
   - Add the Player’s **Selector** script to **On Conversation End**, and set the function to **Selector.enabled**, and set the value to true



---



## Text Animator



### Setup

1. Have a GameObject with a **TMP_Text** on it

   - Add **Behaviours**

   - Closing tags

     - Close each tag with `</behaviour_name>` or `{/appearance_name}`
     - Multiple tags can be closed with `</>` or `{/}`
     - The last tag doesn’t need to be closed

   - Modifiers

     - `d` for delay (frame rate)
     - `a` for strength
     - `f` for speed
     - `w` for vertical (wave) size

   - Events

     - Can be written with `<?eventName>`

     2. In an attached custom script:
        1. `public TextAnimator textAnimator;` (the **TextAnimator** script from below)
        2. In `Awake()`, add `textAnimator.onEvent += OnEvent;`
        3. Add `void OnEvent(string message)` function with a switch case for the message and its effects
   
2. Add a **TextAnimator** script to allow for processing

   - Under ‘Fallback Effects’, the default **Appearance** can be set for the typewriter
   - Default **Behaviours** can also be set for the whole text

3. Enable **Use Easy Integration**

4. Add a **TextAnimatorPlayer** script to allow for typewriter writing

   - Use the **Typewriter Wait** settings to adjust overall speed and character skip times
   - The `<speed=...>` tag can also be used to multiply the base typewriter speed while moving through a text



### Behaviours

*Subtle*

- `<fade>` stays normal for a few seconds before quickly disappearing
  - `d`
- `<incr>` increases and decreases the size of each character in an oscillating manner
  - `a`, `f`, `w`

*Playful*

- `<bounce>` pops each character up one after the other and they bounce when they land back down
  - `a`, `f`, `w`
- `<dangle>` stretches each letter left and right against some invisible pendulum point
  - `a`, `f`, `w`
- `<pend>` is similar to `dangle`, but has a pendulum for each character individually
  - `a`, `f`, `w`
- `<rainb>` oscillates an rainbow colour through the characters
  - `f`, `w`
- `<rot>`  slowly rotates each character on a 2D 360deg axis
  - `f`, `w`
- `<slide>` shears each character back and forth along the same axis
  - `a`, `f`, `w`
- `<swing>` is similar to `rot`, but only rotates back and forth about 45deg
  - `a`, `f`, `w`

*Crazy*

- `<shake>` crazily jitters each character around in a seemingly random pattern
  - `a`, `d`
- `<wave>` is similar to `bounce`, but pops the while string up and down quite high and low
  - `a`, `f`, `w`
- `<wiggle>` is similar to `shake`, but is much smoother
  - `a`, `f`



### Appearances

*Subtle*

- `{fade}` slowly makes each character appear
  - `a`
- `{horiexp}` flows each character in from the left
  - `a`
- `{offset}` subtly slots each character in diagonally
  - `a`
- `{vertexp}` flows each character in from the top
  - `a`

*Playful*

- `{diagexp}` flows each character in from a diagonal angle
  - `a`
- `{rot}` rolls in each character
  - `a`
- `{size}` grows and shrinks each character individually as they flow in
  - `a`
