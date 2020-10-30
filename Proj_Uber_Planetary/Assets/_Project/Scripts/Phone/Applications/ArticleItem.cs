﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

//NOTE: Add to the correct namespace
public class ArticleItem : MonoBehaviour
{
    //NOTE: See if there are any variables you are not using anymore, basically cleanup
    #region Variables
    [SerializeField] private TextMeshProUGUI articleTitle;
    [SerializeField] private Button button;
    private GameObject _articlePanel;
    private GameObject _articleHolder;
    private NewsArticleSO _articleSo;
    private NewsApplication _newsApp;
    #endregion

    //NOTE: You can remove redundant things when you are done testing
    private void Start()
    {
        //button = gameObject.GetComponent<Button>();
        //button.onClick.AddListener(TogglePanel);

    }

    //Updates the prefab's text with the headline title from the NewsArticleSO
    public void Initalize(NewsArticleSO newsArticleSO, NewsApplication newsApplication)
    {
        _articleSo = newsArticleSO;
        _newsApp = newsApplication;
        articleTitle.text = newsArticleSO.ArticleHeadline;
    }

    // This method toggles the Article Panel on and populates its field with the information from the NewsArticleSO
    public void TogglePanel() 
    {
        _newsApp.ArticlePanel.gameObject.SetActive(true);
        _newsApp.ArticlePanelHeadline.text = _articleSo.ArticleHeadline;
        _newsApp.ArticlePanelText.text = _articleSo.ArticleStory;
        _newsApp.ArticlePanelImage.sprite = _articleSo.ArticleSprite;

    }
}
