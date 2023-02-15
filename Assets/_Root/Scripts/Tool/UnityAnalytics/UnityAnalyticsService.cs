using System.Collections.Generic;

namespace Tool.Analytics
{

    public class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string name)
        {
            UnityEngine.Analytics.Analytics.CustomEvent(name);
        }

        public void SendEvent(string name, Dictionary<string, object> data) =>
            UnityEngine.Analytics.Analytics.CustomEvent(name, data);
    }
}
