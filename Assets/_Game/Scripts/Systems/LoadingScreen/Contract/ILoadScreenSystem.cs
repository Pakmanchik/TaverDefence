namespace Systems.LoadingScreen.Contract
{
    public interface ILoadScreenSystem
    {
        public void Show();
        public void SetPercent(uint percent);
        public void Hide();
    }
}