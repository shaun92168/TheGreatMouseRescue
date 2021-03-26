using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SB_PageMgr : MonoBehaviour
{
    public Image PgImageObject;
    public TextMeshProUGUI PageTextObject;
    
    public List<Sprite> PageImages = new List<Sprite>();
    public List<int> PerPageLinesOfText = new List<int>();
    public List<string> PageTextLines = new List<string>();

    private int currPageIndex = -1;
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
        else
        {
            currPgTextIndex = 0;
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
            FindObjectOfType<AudioManager>().Play("NextPage");

            // Clamp so current page index is never great than total used
            if (++currPageIndex > PageImages.Count)
            {
                currPageIndex = PageImages.Count;
            }

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

        Debug.Log("Next Clicked");
    }

    public void PrevPageAsset()
    {
        // Previous Page?
        if (SetPrevTextString() == true)
        {
            FindObjectOfType<AudioManager>().Play("NextPage");
            currPageIndex--;

            // If current is first page do nothing
            if (currPageIndex < 0)
            {
                // Last image so skip to cut scene 1
                GetComponent<SceneMgr>().PrevScene();
                currPageIndex = 0;
            }
            else
            {
                if (PgImageObject != null)
                {
                    PgImageObject.sprite = PageImages[currPageIndex];
                }

                currPgTextIndex = 0;
                for (int i = 0; i <= currPageIndex && i < PerPageLinesOfText.Count; i++)
                {
                    currPgTextIndex += PerPageLinesOfText[i];
                }

                SetPrevTextString();
            }
        }

        Debug.Log("Previous Clicked");
    }

    public void SkipToNextScene()
    {
        GetComponent<SceneMgr>().NextScene();

        Debug.Log("Skip to Next Scene Clicked");
    }

}
