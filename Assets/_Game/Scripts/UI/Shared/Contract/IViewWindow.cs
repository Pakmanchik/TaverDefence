using TowerDefence.UI.Shared.View;
using UnityEngine;

namespace TowerDefence.UI.Shared.Contract
{
    public interface IViewWindow
    {
        public Transform ChildsContainerTransform { get; }
        public void Init(ViewDependencies dependencies);
        public void Show();
        public void Close();

    }
}