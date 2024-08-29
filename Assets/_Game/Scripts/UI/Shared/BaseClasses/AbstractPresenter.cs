using System.Threading.Tasks;
using Addressable.Contract;
using MessageBus.Contract;
using TowerDefence.UI.Shared.Contract;
using TowerDefence.UI.Shared.Presenter;
using TowerDefence.UI.Shared.View;
using UnityEngine;

namespace TowerDefence.UI.Shared.BaseClasses
{
    public abstract class AbstractPresenter : IPresenter
    {
        protected IAddressable Addressable;
        protected IMessageBus MessageBus;
        
        protected IPresenter PresenterNow;

        private ViewDependencies _viewDependencies;
        private PresenterDependencies _presenterDependencies;
        
        private IViewWindow _window;

        public void Init(PresenterDependencies presenterDependencies, ViewDependencies viewDependencies)
        {
            Addressable = presenterDependencies.Addressable;
            MessageBus  = presenterDependencies.MessageBus;

            _viewDependencies      = viewDependencies;
            _presenterDependencies = presenterDependencies;
        }

        public IViewWindow ViewWindow => _window;
    

        public virtual Task ShowWindow(Transform parent)
        {
            return Task.CompletedTask;
        }

        protected async Task GetView(string assetKey, Transform parent)
        {
            _window = await Addressable.GetInstanceAsset<IViewWindow>(assetKey, parent);

            Debug.Log($"Get window {_window}");

            _window.Init(_viewDependencies);
        }

        protected async Task CreatePresenter(IPresenter presenter)
        {
            PresenterNow = presenter;
            PresenterNow.Init(_presenterDependencies, _viewDependencies);

            await CreateWindow();
        }

        private async Task CreateWindow()
        {
            await PresenterNow.ShowWindow(_window.ChildsContainerTransform)!;
        }

        protected virtual void SubscribeOnWindow()
        {
        }

        protected virtual void UnsubscribeOnWindow()
        {
        }
        
        public virtual void HideWindow()
        {
            UnsubscribeOnWindow();

            _window?.Close();
            _window = null;

            PresenterNow?.HideWindow();
            PresenterNow = null;
        }
    }
}