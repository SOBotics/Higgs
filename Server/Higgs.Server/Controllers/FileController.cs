using System.Linq;
using System.Text;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.File;
using Higgs.Server.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly HiggsDbContext _dbContext;

        public FileController(HiggsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddFile(AddFileRequest request)
        {
            var dbFile = new DbFile
            {
                ContentType = request.ContentType,
                FileName = request.FileName,
                Contents = Encoding.UTF8.GetBytes(request.Contents)
            };
            _dbContext.Files.Add(dbFile);
            _dbContext.SaveChanges();

            return Ok(dbFile);
        }

        [HttpGet]
        public IActionResult GetFiles(int id)
        {
            var files = _dbContext.Files.ToList();
            return Ok(files);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60 * 60 * 24 * 1 /* One day */)]
        public IActionResult GetFile(int id)
        {
            var file = _dbContext.Files.FirstOrDefault(f => f.Id == id);
            if (file == null)
                return BadRequest(new ErrorResponse($"File with id {id} not found."));

            return File(file.Contents, file.ContentType);
        }
    }
}