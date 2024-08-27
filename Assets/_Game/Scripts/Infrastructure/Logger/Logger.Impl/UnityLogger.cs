using UnityEngine;

namespace Logger
{
    public sealed class UnityLogger : ILog
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}