namespace TinyLittleStudio
{
    public sealed class Configuration
    {
        public static class URLs
        {
            public static readonly string GOOGLE_PUBLIC_DNS = "8.8.8.8";
        }

        public static class Scenes
        {
            public static readonly string LOADING = "0_Loading";

            public static readonly string FAILURE = "1_Failure";

            public static readonly string GAME = "2_Game";
        }

        public static class Tags
        {
            public static readonly string PLAYER = "Player";
        }

        private static readonly Configuration DEFAULT = new Configuration();

        private Configuration()
        {
            if (!IsInitialized)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; }

        public static Configuration Default => Configuration.DEFAULT;
    }
}
