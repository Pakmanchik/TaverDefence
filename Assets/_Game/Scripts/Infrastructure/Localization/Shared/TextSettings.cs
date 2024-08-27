namespace Localization.Shared
{
    public sealed class TextSettings
    {
        public TextSettings(string text, int sizeText)
        {
            Text = text;
            SizeText = sizeText;
        }

        public string Text { get; }
        public int SizeText { get; }
    }
}