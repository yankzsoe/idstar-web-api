using idstar_web_api.Interface;

namespace idstar_web_api.Services {
    public class SequenceNumberService : INumberGenerator {
        private int _number;
        public SequenceNumberService() {
            _number = 0;
        }
        public int GenerateNumber() {
            return _number++;
        }
    }
}
