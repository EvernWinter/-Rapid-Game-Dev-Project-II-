using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite openSprite;    // Sprite to show when the door is open
    [SerializeField] private Sprite closeSprite;   // Sprite to show when the door is closed
    [SerializeField] private Sprite[] openingSprites; // Array of sprites for the opening animation
    [SerializeField] private float animationSpeed = 0.1f; // Speed of the sprite animation

    [Header("State")]
    [SerializeField] public bool isPass = false;   // Whether the door should open
    [SerializeField] private string nextScene;       // Name of the scene to load when passing through the door
    private bool previousPassState = false;         // To track if isPass has changed
    [SerializeField] public GameObject gems;
    [SerializeField] public bool open = false;

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer
    private Vector3 defaultScale;            // Store the default scale
    [SerializeField] private bool isThisPassable = true; 
    [SerializeField] private bool isThisPortalWin = false;
    [SerializeField] private Vector3 teleportPosition;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject gameWinUI;

    // Start is called before the first frame update
    void Start()
    {
        if(!gameWinUI)
        {
            gameWinUI = GameObject.Find("GameWinUI");
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale; // Store the initial scale of the door
        UpdateDoorState(); // Set the initial state of the door
    }

    // Update is called once per frame
    private void Update()
    {
        // Only update if the state has changed
        if (open != previousPassState || isPass != previousPassState)
        {
            UpdateDoorState();  // Update door state when isPass or open changes
            previousPassState = open;  // Store the new state
        }
    }

    // Update the door's visual state based on isPass
    private void UpdateDoorState()
    {
        Debug.Log($"isPass: {isPass}, open: {open}");
        if (isPass && open)
        {
            Debug.Log("Starting Open Door Coroutine");
            StartCoroutine(OpenDoor()); // Start opening animation
        }
        else
        {
            spriteRenderer.sprite = closeSprite; // Set closed sprite if door is not passable
            transform.localScale = defaultScale; // Reset the scale when the door closes
        }
    }

    public void OpenDoorMethod()
    {
        StartCoroutine(OpenDoor());
    }

    // Coroutine to handle door opening "animation" by changing sprites and scaling
    private IEnumerator OpenDoor()
    {
        // Scale the door to three times its default size over the duration of the opening animation
        Vector3 targetScale = defaultScale * 3; // Target scale (default * 3)
        float duration = animationSpeed * openingSprites.Length; // Total duration of the animation
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the scale based on time
            transform.localScale = Vector3.Lerp(defaultScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait until the next frame
        }

        // Ensure the scale is set to the target scale at the end
        transform.localScale = targetScale;

        // Loop through the opening sprites to simulate an animation
        for (int i = 0; i < openingSprites.Length; i++)
        {
            spriteRenderer.sprite = openingSprites[i]; // Set sprite to the current frame
            yield return new WaitForSeconds(animationSpeed); // Wait before changing to the next frame
        }

        // After the "animation" finishes, set the openSprite
        spriteRenderer.sprite = openSprite;
    }

    // Call this method to change the isPass value and update the door
    public void SetIsPass(bool value)
    {
        isPass = value;
        UpdateDoorState();
    }

    // Trigger the scene transition when the player enters the door's trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPass && other.CompareTag("Player") && isThisPassable && !isThisPortalWin) // Ensure the object is the player
        {
            // Load the next scene specified by nextScene
            player.transform.position = teleportPosition;
        }
        else if (isPass && other.CompareTag("Player") && isThisPassable && isThisPortalWin) // Ensure the object is the player
        {
            Debug.Log("SUIIIIIIIIIIIIIIII You WIN");
            gameWinUI.GetComponent<GameWinUI>().DoFadeGameWinUI();
        }
    }
    
    
}
