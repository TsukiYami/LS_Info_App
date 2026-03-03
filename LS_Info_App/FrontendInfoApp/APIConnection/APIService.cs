namespace FrontendInfoApp.APIConnection {
    public class APIService {
        /// <summary>
        /// Implementation vom Singleton Pattern, als Vorbereitung auf spätere Anpassungswünsche
        /// </summary>
        private static readonly Lazy<APIService> instance =
        new Lazy<APIService>(() => new APIService());

        public static APIService Instance {
            get {
                return instance.Value;
            }
        }

        private Guid m_oSessionToken;
        private GetFromAPI m_oGetService;

        private APIService() {
            m_oGetService = new GetFromAPI();
        }

        public GetFromAPI Get() {
            return m_oGetService;
        }
    }
}