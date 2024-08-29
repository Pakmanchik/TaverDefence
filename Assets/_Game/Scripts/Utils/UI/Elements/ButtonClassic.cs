using System;
using Utils.UI.Shared.BaseClasses;

namespace Utils.UI.Shared.Elements
{
    public sealed class ButtonClassic : AbstractUIElement
    {
        public event Action OnClick;
        public event Action OnEndClick;
        
        public void Init() => ElementStateManager(_elementStatus);

        protected override void SetClick()
        { 
            base.SetClick();
         
            OnClick?.Invoke();
        }

        protected override void SetEndClick() => OnEndClick?.Invoke();
    }
}