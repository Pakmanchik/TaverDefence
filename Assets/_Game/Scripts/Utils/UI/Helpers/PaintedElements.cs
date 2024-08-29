using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.UI.Shared.Helpers
{
   [Serializable]
    public sealed class PaintedElements 
    {
        public PaintedImage[] _paintedImages;
        public PaintedText[] _paintedTexts;
        public PaintedSprite[] _paintedSprites;
    }

    [Serializable]
    public sealed class PaintedImage
    {
        public Image _image;
        [Space]
        public Color _idle;
        public Color _enter;
        public Color _clicked;
        public Color _selected;
        public Color _disable;
    }

    [Serializable]
    public sealed class PaintedText
    {
        public TextMeshProUGUI _text;
        [Space]
        public Color _idle;
        public Color _enter;
        public Color _clicked;
        public Color _selected;
        public Color _disable;
    }

    [Serializable]
    public sealed class PaintedSprite
    {
        public Image _sprite;
        [Space]
        public Sprite _idle;
        public Sprite _enter;
        public Sprite _clicked;
        public Sprite _selected;
        public Sprite _disable;
    }
}