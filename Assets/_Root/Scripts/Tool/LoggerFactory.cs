    internal static class LoggerFactory
    {
        public static CustomLogger Create<T>() =>
            new CustomLogger(typeof(T).Name);
    }


