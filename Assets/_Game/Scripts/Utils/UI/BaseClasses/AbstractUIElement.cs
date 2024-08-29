using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils.UI.Shared.Helpers;

namespace Utils.UI.Shared.BaseClasses
{
    public abstract class AbstractUIElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] protected ElementStatus _elementStatus;
        [SerializeField] protected PaintedElements _paintedElements;

        public ElementStatus ElementStatus
        {
            get => _elementStatus;
            set
            {
                _elementStatus = value;
                ElementStateManager(value);
            }
        }

        protected void ElementStateManager(ElementStatus elementStatus)
        {
            // Если текущий статус элемента - Selected или Disabled, обработаем их и выйдем из функции
            if (_elementStatus is ElementStatus.Selected or ElementStatus.Disabled)
            {
                HandleSpecialStatuses(_elementStatus);
                return;
            }

            // Обновляем статус и включаем возможность взаимодействия
            _elementStatus       = elementStatus;
            _image.raycastTarget = true;

            // Выполняем действия в зависимости от нового статуса
            switch (elementStatus)
            {
                case ElementStatus.Idle:
                    SetIdle();
                    break;
                case ElementStatus.Entered:
                    SetEnter();
                    break;
                case ElementStatus.Clicked:
                    SetClick();
                    break;
                case ElementStatus.Selected:
                    SetSelected();
                    break;
                case ElementStatus.Disabled:
                    SetDisable();
                    break;
            }
        }

        private void HandleSpecialStatuses(ElementStatus elementStatus)
        {
            // Отключаем возможность взаимодействия
            _image.raycastTarget = false;

            // Обрабатываем специфические статусы
            switch (elementStatus)
            {
                case ElementStatus.Selected:
                    SetSelected();
                    break;
                case ElementStatus.Disabled:
                    SetDisable();
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData) => ElementStateManager(ElementStatus.Clicked);

        public void OnPointerEnter(PointerEventData eventData) => ElementStateManager(ElementStatus.Entered);

        public void OnPointerExit(PointerEventData eventData) => ElementStateManager(ElementStatus.Idle);
        public void OnPointerUp(PointerEventData eventData) => SetEndClick();

        protected virtual void SetIdle() => ApplyPaintedElementStyles(ElementStatus.Idle);

        protected virtual void SetClick() => ApplyPaintedElementStyles(ElementStatus.Clicked);
        
        protected virtual void SetEnter() => ApplyPaintedElementStyles(ElementStatus.Entered);

        protected virtual void SetSelected() => ApplyPaintedElementStyles(ElementStatus.Selected);

        protected virtual void SetDisable() => ApplyPaintedElementStyles(ElementStatus.Disabled);
        
        protected virtual void SetEndClick() { }

        private void ApplyPaintedElementStyles(ElementStatus state)
        {
            foreach (var paintedImage in _paintedElements._paintedImages)
                paintedImage._image.color = GetColorByState(paintedImage, state);
            
            foreach (var paintedText in _paintedElements._paintedTexts)
                paintedText._text.color = GetColorByState(paintedText, state);
            
            foreach (var paintedSprite in _paintedElements._paintedSprites)
                paintedSprite._sprite.sprite = GetSpriteByState(paintedSprite, state);
        }

        private Color GetColorByState(PaintedImage paintedImage, ElementStatus state)
        {
            return state switch
            {
                ElementStatus.Idle     => paintedImage._idle,
                ElementStatus.Clicked  => paintedImage._clicked,
                ElementStatus.Entered  => paintedImage._enter,
                ElementStatus.Selected => paintedImage._selected,
                ElementStatus.Disabled => paintedImage._disable,
                _                      => paintedImage._idle // Значение по умолчанию
            };
        }

        private Color GetColorByState(PaintedText paintedText, ElementStatus state)
        {
            return state switch
            {
                ElementStatus.Idle     => paintedText._idle,
                ElementStatus.Clicked  => paintedText._clicked,
                ElementStatus.Entered  => paintedText._enter,
                ElementStatus.Selected => paintedText._selected,
                ElementStatus.Disabled => paintedText._disable,
                _                      => paintedText._idle // Значение по умолчанию
            };
        }

        private Sprite GetSpriteByState(PaintedSprite paintedSprite, ElementStatus state)
        {
            return state switch
            {
                ElementStatus.Idle     => paintedSprite._idle,
                ElementStatus.Clicked  => paintedSprite._clicked,
                ElementStatus.Entered  => paintedSprite._enter,
                ElementStatus.Selected => paintedSprite._selected,
                ElementStatus.Disabled => paintedSprite._disable,
                _                      => paintedSprite._idle // Значение по умолчанию
            };
        }
        
#if UNITY_EDITOR  
        private void OnValidate()
        {
            if (_image == null)
                _image = GetComponent<Image>();
        }
#endif
    }
}