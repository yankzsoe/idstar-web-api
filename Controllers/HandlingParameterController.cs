using Microsoft.AspNetCore.Mvc;

namespace idstar_web_api.Controllers {
    [Route("api/parameter")]
    [ApiController]
    public class HandlingParameterController : ControllerBase {

        [HttpGet("QueryParameter")]
        public IActionResult QueryParameter([FromQuery] string nama, int usia) {
            var param = new {
                nama = nama,
                usia = usia
            };
            return Ok(param);
        }

        // Pada path parameter jika tipe data bukan string 
        // Sebaiknya gunakan tipe datanya juga untuk menegaskan
        // pada contoh ini bahwa parameter id ini adalah integer
        [HttpGet("PathParameter/{id:int}")]
        public IActionResult PathParameter(int id) {
            var result = $"ID yang kamu kirim adalah \"{id}\"";
            return Ok(result);
        }

        [HttpGet("HeaderParameter")]
        public IActionResult HeaderParameter([FromHeader] string authorization) {
            var result = $"Header yang kamu kirim adalah \"{authorization}\"";
            return Ok(result);
        }

        public record Student {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Dob { get; set; }
        }

        [HttpPost("BodyParameter")]
        public IActionResult BodyParameter([FromBody] Student student) {
            return Ok(student);
        }

        // Method ini akan mengirimkan file gambar sebagai respons
        [HttpGet("Download")]
        public IActionResult Download(string imageName) {
            // Tentukan path lengkap ke file gambar dalam folder "Assets"
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", imageName);

            // Periksa apakah file gambar ada
            if (System.IO.File.Exists(imagePath)) {
                // Baca file gambar ke dalam byte array
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);

                // Menentukan jenis konten untuk respons (misalnya, image/jpeg)
                // Kita harus menyesuaikan dengan jenis file gambar yang sesuai
                var contentType = "image/jpeg"; 

                // Mengembalikan file gambar sebagai respons
                return File(imageBytes, contentType);
            } else {
                // Jika file tidak ditemukan, kembalikan respons 404 (Not Found)
                return NotFound();
            }
        }

        public record ImageUploadModel {
            public IFormFile ImageFile { get; set; }
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadModel model) {
            if (model.ImageFile != null && model.ImageFile.Length > 0) {
                // Tentukan folder tempat Anda ingin menyimpan gambar yang diunggah
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

                // Pastikan folder upload ada, jika tidak, buat folder
                if (!Directory.Exists(uploadFolder)) {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Tentukan nama file yang akan disimpan
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

                // Tentukan path lengkap file
                var filePath = Path.Combine(uploadFolder, fileName);

                // Simpan file gambar ke dalam folder
                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // Anda dapat melakukan operasi lain seperti menyimpan informasi file ke database
                // atau mengembalikan informasi tentang file yang berhasil diunggah
                return Ok(new { FileName = fileName });
            } else {
                return BadRequest("File gambar tidak valid atau tidak ditemukan.");
            }
        }

    }
}
