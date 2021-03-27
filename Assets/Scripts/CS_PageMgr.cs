using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CS_PageMgr : MonoBehaviour
{
    public Image PgImageObject;
    public TextMeshProUGUI PageTextObject;
    public Button NextButton;
    public Button PreviousButton;

    public List<Sprite> PageImages = new List<Sprite>();
    public List<int> PerPageLinesOfText = new List<int>();
    public List<string> PageTextLines = new List<string>();
    public List<int> AnimTakeContolOnTextLine = new List<int>();
    public List<Animator> Animators = new List<Animator>();

    private int currPageIndex = 0;
    private int currPgTextIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (PageImages.Count > 0)
        {
            if (PgImageObject != null)
            {
                // Pull the first image in list (making sure it is populated and display
                PgImageObject.sprite = PageImages[++currPageIndex];
            }
            else
            {
                Debug.Log("Page Image Object must be set");
            }

            NextPageAsset();
        }
        else
        {
            Debug.Log("No Page Image 0 Found...please add!");
        }
    }

    private bool SetNextTextString()
    {
        int linesPerPage = 0;
        bool isAllTextDone = false;

        // Determine the current page text line by iterating through all lines of text per page until we reach our current page
        for (int i = 0; i < PerPageLinesOfText.Count; i++)
        {
            linesPerPage += PerPageLinesOfText[i];

            if (currPageIndex == i)
            {
                break;
            }
        }

        if (currPgTextIndex < linesPerPage - 1)
        {
            FindObjectOfType<AudioManager>().Play("CloseMenu");
            PageTextObject.text = PageTextLines[++currPgTextIndex];
        }
        else
        {
            isAllTextDone = true;
        }

        return isAllTextDone;
    }

    private bool SetPrevTextString()
    {
        int linesPerPage = 0;
        bool isAllTextDone = false;

        if (--currPgTextIndex >= 0)
        {
            FindObjectOfType<AudioManager>().Play("CloseMenu");
            PageTextObject.text = PageTextLines[currPgTextIndex];
        }

        for (int i = 0; i < PerPageLinesOfText.Count; i++)
        {
            linesPerPage += PerPageLinesOfText[i];

            if (currPgTextIndex < linesPerPage)
            {
                isAllTextDone = (currPageIndex != i || currPgTextIndex < 0);
                break;
            }
        }

        return isAllTextDone;
    }

    public void NextPageAsset()
    {
        // Next Page?
        if (SetNextTextString() == true)
        {
            currPageIndex++;

            // If current is last page do skip action (next Scene)
            if (currPageIndex == PageImages.Count)
            {
                // Last image so skip to cut scene 1
                GetComponent<SceneMgr>().NextScene();
            }
            else
            {
                if (PgImageObject != null)
                {
                    PgImageObject.sprite = PageImages[currPageIndex];
                }
                SetNextTextString();
            }
        }
        else
        {
            for (int i = 0; i < AnimTakeContolOnTextLine.Count; i++)
            {
                if (currPgTextIndex == AnimTakeContolOnTextLine[i])
                {
                    if (i < Animators.Count)
                    {
                        Animators[i].SetTrigger("Start");
                    }
                }
            }
        }

        Debug.Log("Next Clicked");
    }

    public void PrevPageAsset()
    {
        // Previous Page?
        if (SetPrevTextString() == true)
        {
            currPageIndex--;

            // If current is first page do nothing
            if (currPageIndex < 0)
            {
                // Last image so skip to cut scene 1
                GetComponent<SceneMgr>().PrevScene();
            }
            else
            {
                if (PgImageObject != null)
                {
                    PgImageObject.sprite = PageImages[currPageIndex];
                }
                SetPrevTextString();
            }
        }
        else
        {
            for (int i = 0; i < AnimTakeContolOnTextLine.Count; i++)
            {
                if (currPgTextIndex == AnimTakeContolOnTextLine[i])
                {
                    if (i < Animators.Count)
                    {
                        Animators[i].SetTrigger("Reset");
                    }
                }
            }
        }

        Debug.Log("Previous Clicked");
    }

    public void SkipToNextScene()
    {
        GetComponent<SceneMgr>().NextScene();

        Debug.Log("Skip to Next Scene Clicked");
    }

    public void AnimationEvent(int eventID)
    {
        switch (eventID)
        {
            case 1: // Animation start
                // Disable buttons
                if (NextButton != null)
                {
                    NextButton.interactable = false;
                }
                if (PreviousButton != null)
                {
                    PreviousButton.interactable = false;
                }
                break;

            case 2: // Animation stop
                // Eanble buttons
                if (NextButton != null)
                {
                    NextButton.interactable = true;
                }
                if (PreviousButton != null)
                {
                    PreviousButton.interactable = true;
                }
                break;

            case 3: // Next Text
                NextPageAsset();
                break;

            case 4: // Previous Text
                PrevPageAsset();
                break;

            default:
                Debug.LogWarning("Unsupportted Animation Event ID!");
                break;
        }
    }

}
