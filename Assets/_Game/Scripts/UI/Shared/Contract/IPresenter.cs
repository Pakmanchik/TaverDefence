using System.Threading.Tasks;
using TowerDefence.UI.Shared.Presenter;
using TowerDefence.UI.Shared.View;
using UnityEngine;

namespace TowerDefence.UI.Shared.Contract
{
    public interface IPresenter
    {
        public IViewWindow ViewWindow { get; }
        public void Init(PresenterDependencies presenterDependencies,  ViewDependencies viewDependencies);
        public Task ShowWindow(Transform parent);
        public void HideWindow();

    }
}