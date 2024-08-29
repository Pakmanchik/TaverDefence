using Localization.Contract;
using Localization.Shared;
using MessageBus.Contract;
using TowerDefence.UI.Shared.Contract;
using TowerDefence.UI.Shared.View;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.UI.Shared.BaseClasses
{
    public abstract class AbstractView : MonoBehaviour, IViewWindow
    {
        [Tooltip("Контейнер для установки окон-наследников")]
        [SerializeField] private Transform _childContainer;
        [Space]
        [Tooltip("Список всех TMP Wrapper для нужд локализации")]
        [SerializeField] private WrapperTMP[] _wrapperObjects;

        protected IMessageBus MessageBus;
        protected ILocalizationCollection Localization;
        
        private ViewDependencies _viewDependencies;

        public virtual Transform ChildsContainerTransform => _childContainer;
        protected virtual LocalizationWindow LocalizationWindow { get; set; }

        public virtual void Init(ViewDependencies viewDependencies)
        {
            gameObject.SetActive(false);
            
            MessageBus   = viewDependencies.MessageBus;
            Localization = viewDependencies.LocalizationCollection;

            _viewDependencies = viewDependencies;

            InitElements();
        }

        protected virtual void InitElements()
        {
            foreach (var wrapper in _wrapperObjects)
                Localization.AddWrapper(wrapper, LocalizationWindow);
        }
        
        public virtual void Show()
        {
            SubscribeOnButtons();
            gameObject.SetActive(true);
        }

        protected virtual void SubscribeOnButtons()
        {
        }

        protected virtual void UnsubscribeOnButtons()
        {
        }
        
        public virtual void Close()
        {
            UnsubscribeOnButtons();

            foreach (var wrapper in _wrapperObjects)
                Localization.RemoveWrapper(wrapper);
        }

#if UNITY_EDITOR       
        /// <summary>
        /// Метод, используемый CustomEditor для автозаполнения SF в GO
        /// </summary>
        private void ForceUpdateInspectorInfo()
        {
            if (!_childContainer)
                _childContainer = transform;

            // Получаем список всех владельцев компонента WrapperTMP в нашем окне
            _wrapperObjects = GetComponentsInChildren<WrapperTMP>(true);
        }
        
        /// <summary>
        /// Editor для добавления кнопки автозаполнения некоторых SF в GO
        /// </summary>
        [CustomEditor(typeof(AbstractView), true)]
        private class BaseViewWindowEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.DrawDefaultInspector();

                var windowInstance = target as AbstractView;
                if (!windowInstance)
                    return;

                // Делаем отступ от базового содержимого инспектора и добавляем кнопку перезаполнения данных
                GUILayout.Space(20);
                var isUpdateButtonPressed = GUILayout.Button("Refill data");

                if (isUpdateButtonPressed)
                    windowInstance.ForceUpdateInspectorInfo();
            }
        }
#endif

    }
}