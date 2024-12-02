using UnityEngine;
using TMPro;

public class EndingSceneUI : MonoBehaviour
{
    [SerializeField] private RectTransform videoRect;         // The RectTransform for the video area
    [SerializeField] private float scrollSpeed = 50f;         // Speed of scrolling
    [SerializeField] private float fadeSpeed = 1f;            // Speed of fading text
    [SerializeField] private Transform textParent;            // Parent object to hold the text lines
    [SerializeField] private GameObject textPrefab;           // Prefab for a TextMeshProUGUI object
    [SerializeField] private string[] textLines;              // Array of text content

    private TextMeshProUGUI[] textComponents;
    private bool allTextFaded = false;

    private void Start()
    {
        // Create text components dynamically
        textComponents = new TextMeshProUGUI[textLines.Length];
        for (int i = 0; i < textLines.Length; i++)
        {
            GameObject newText = Instantiate(textPrefab, textParent);
            textComponents[i] = newText.GetComponent<TextMeshProUGUI>();
            textComponents[i].text = textLines[i];
            textComponents[i].transform.localPosition = new Vector3(0, -i * 50f, 0); // Position each line
        }
    }

    private void Update()
    {
        if (!allTextFaded)
        {
            ScrollText();
            FadeOverlappingTextLines();

            // Check if all text has faded and scrolled off-screen
            if (AllTextFadedAndScrolled())
            {
                allTextFaded = true;
                ReturnToMainMenu(); // Automatically return to main menu
            }
        }
    }

    private void ScrollText()
    {
        foreach (var line in textComponents)
        {
            if (line != null)
            {
                line.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
            }
        }
    }

    private void FadeOverlappingTextLines()
    {
        Vector3[] videoCorners = new Vector3[4];
        videoRect.GetWorldCorners(videoCorners);

        float videoTop = videoCorners[1].y;   // Top of the video
        float videoBottom = videoCorners[0].y; // Bottom of the video

        foreach (var line in textComponents)
        {
            if (line != null)
            {
                float lineTop = line.transform.position.y;
                float lineBottom = lineTop - line.preferredHeight;

                if (lineBottom < videoTop && lineTop > videoBottom)
                {
                    // Fade out the line
                    Color textColor = line.color;
                    textColor.a = Mathf.Max(0, textColor.a - fadeSpeed * Time.deltaTime);
                    line.color = textColor;

                    // Once fully faded, destroy the text to prevent it from reappearing
                    if (textColor.a <= 0f)
                    {
                        Destroy(line.gameObject);
                    }
                }
            }
        }
    }

    private bool AllTextFadedAndScrolled()
    {
        foreach (var line in textComponents)
        {
            if (line != null) return false; // If any text still exists, return false
        }
        return true; // All text has faded and been destroyed
    }

    // Method to return to the main menu via button
    public void ReturnToMainMenu()
    {
        if (SceneLoader.Instance != null)
        {
            GameManager.Instance.setGameState(GameManager.GameState.Title);
        }
        else
        {
            Debug.LogError("SceneLoader instance is null! Make sure SceneLoader is initialized first.");
        }
    }
}
