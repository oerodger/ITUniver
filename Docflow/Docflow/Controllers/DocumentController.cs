using Docflow.Models;
using Docflow.Models.Repositories;
using Docflow.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    public class DocumentController : BaseController
    {
        private FolderRepository folderRepository;
        private DocumentRepository documentRepository;

        public DocumentController(FolderRepository folderRepository, 
            DocumentRepository documentRepository, UserRepository userRepository) :
            base(userRepository)
        {
            this.folderRepository = folderRepository;
            this.documentRepository = documentRepository;
        }


        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(long id)
        {
            var folder = folderRepository.Load(id);
            var model = new DocumentEditViewModel {
               Folder = folder 
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DocumentEditViewModel model)
        {            
            var document = new Document
            {
                Name = model.Name,
                Description = model.Description,
                ParentFolder = model.Folder
            };
            var version = new DocumentVersion
            {
                Document = document,
                File = new File {
                    Name = model.File.FileName,
                    Content = model.File.InputStream.ToByteArray()
                }
            };
            document.Versions.Add(version);
            documentRepository.Save(document);
            return RedirectToAction("Index", "Folder", new { id = document.ParentFolder.Id });
        }
    }
}