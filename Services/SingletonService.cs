using idstar_web_api.Interface;

namespace idstar_web_api.Services {
    public class SingletonService : ISingletonGenerator {
        private int _number;
        public SingletonService() {
            Random rd = new Random();
            _number = rd.Next(0, 100);
        }
        public int GenerateNumber() {
            return _number;
        }
    }
}
