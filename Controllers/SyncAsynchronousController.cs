using idstar_web_api.Helper;
using Microsoft.AspNetCore.Mvc;

namespace idstar_web_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SyncAsynchronousController : ControllerBase {
        private SampleSyncAsyncProcess _process;

        public SyncAsynchronousController() {
            _process = new SampleSyncAsyncProcess();
        }

        /// <summary>
        /// Contoh untuk proses Synchronous
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleSync")]
        public IActionResult SampleSync(int detik = 3) {

            var startTime = DateTime.Now;

            _process.SampleProses("proses 1", detik);

            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");

        }

        /// <summary>
        /// Contoh untuk proses dengan Asynchronous
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleAsync")]
        public async Task<IActionResult> SampleAsync(int detik = 3) {
            var startTime = DateTime.Now;

            await _process.SampleProsesAsync("proses 1", detik);
            _process.SampleProsesAsync("proses 2", detik);

            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses dengan Async tapi prosesnya tidak ditunggu
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleNotAwaitAsync")]
        public async Task<IActionResult> SampleNotAwaitAsync(int detik = 3) {
            var startTime = DateTime.Now;
            _process.SampleProsesAsync("proses 1", detik);
            _process.SampleProsesAsync("proses 2", detik);
            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses dengan async tapi prosesnya akan berjalan sesuai dengan urutan
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleWaitAllAsync")]
        public IActionResult SampleWaitAllAsync(int detik = 3) {
            var startTime = DateTime.Now;
            Task task1 = Task.Run(() => _process.SampleProsesAsync("proses 1", detik));
            Task task2 = Task.Run(() => _process.SampleProsesAsync("proses 2", detik));
            Task.WaitAll(task1, task2);

            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses dengan async tapi prosesnya tetap berjalan berbarengan
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleContinueWithAsync")]
        public IActionResult SampleContinueWithAsync(int detik = 3) {
            var startTime = DateTime.Now;
            Task task = Task.Run(() => _process.SampleProsesAsync("proses 1", detik));
            task.ContinueWith(prevTask => {
                _process.SampleProsesAsync("proses 2", detik);
            });
            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }
    }
}
