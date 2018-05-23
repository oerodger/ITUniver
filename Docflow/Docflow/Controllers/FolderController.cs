﻿using Docflow.Models;
using Docflow.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docflow.Controllers
{
    [Authorize]
    public class FolderController : Controller
    {
        private FolderRepository folderRepository;

        public FolderController(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        public ActionResult Index(long? id)
        {
            var folder = id.HasValue ? folderRepository.Load(id.Value) : null;
            var model = new FolderViewModel
            {
                CurrentFolder = folder,
                Parent = folder != null && folder.ParentFolder != null ? folder.ParentFolder : null,
                Folders = folderRepository.GetFolders(id)
            };
            return View(model);
        }
    }
}