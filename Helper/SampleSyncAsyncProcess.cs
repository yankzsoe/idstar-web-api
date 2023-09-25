using System.Diagnostics;

namespace idstar_web_api.Helper {
    public class SampleSyncAsyncProcess {
        public void SampleProses(string processName, int count) {
            Console.WriteLine($"Proses dimulai {processName} pada {DateTime.Now}");

            var sw = Stopwatch.StartNew();
            Thread.Sleep(count * 1000);
            sw.Stop();

            Console.WriteLine($"Proses {processName} selesai, dengan durasi {sw.ElapsedMilliseconds} millisecond.");
        }

        public async Task SampleProsesAsync(string processName, int count) {
            Console.WriteLine($"Proses dimulai {processName} pada {DateTime.Now}");

            var sw = Stopwatch.StartNew();
            await Task.Delay(count * 1000);
            sw.Stop();

            Console.WriteLine($"Proses {processName} selesai, dengan durasi {sw.ElapsedMilliseconds} millisecond.");
        }
    }
}
