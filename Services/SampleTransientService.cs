using idstar_web_api.Interface;

namespace idstar_web_api.Services {
    public class SampleTransientService : ITransientGenerator {
        private int _number = 0;
        public SampleTransientService() {
            Random rd = new Random();
            _number = rd.Next(0, 100);
        }
        public int GenerateNumber() {
            return _number;
        }
    }
}
