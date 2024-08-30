using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    [SerializeField]
    private string text;
    private string previousText;

    [Serializable]
    public class _16T
    {
        [SerializeField]
        private string letter;
        public string Letter => letter;
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }

    [SerializeField]
    private _16T[] datas;

    [SerializeField]
    private Image letterPrefab;
    [SerializeField]
    private Transform letterParent;

    private List<GameObject> letterList = new List<GameObject>();

    private void Update() => Translate();

    private void Translate()
    {
        bool pressedEnterKey = Input.GetKeyDown(KeyCode.Return);
        bool can = text != null && text.Length > 0 && !text.Equals(previousText) && pressedEnterKey;

        if (!can)
        {
            return;
        }

        if (letterList.Count > 0)
        {
            DeleteLetters();
        }

        previousText = text;

        for (int i = 0; i < text.Length; i++)
        {
            MakeLetterSprite(text[i].ToString());
        }
    }

    private void DeleteLetters()
    {
        for (int i = letterList.Count - 1; i >= 0; i--)
        {
            Destroy(letterList[i]);
        }

        letterList.Clear();
    }

    private Sprite GetLetterSprite(string letter)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (!datas[i].Letter.Equals(letter))
            {
                continue;
            }

            return datas[i].Sprite;
        }

        return null;
    }

    private void MakeLetterSprite(string letter)
    {
        Image instance = Instantiate(letterPrefab, letterParent);
        Sprite sprite = GetLetterSprite(letter);

        instance.sprite = sprite;

        letterList.Add(instance.gameObject);
    }
}
