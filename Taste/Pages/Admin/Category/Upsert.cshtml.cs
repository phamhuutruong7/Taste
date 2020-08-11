﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Taste.Models.Category CategoryObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Models.Category();
            if(id != null)
            {
                CategoryObj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id.GetValueOrDefault());
                if(CategoryObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost(Models.Category CategoryObj)  //if you have Notation BindProperty on top, you can let OnPort with empty parameter
        {
            if(!ModelState.IsValid) //ModelState is how the model define their attribute. (required? Key? name?)
            {
                return Page();
            }
            if(CategoryObj.Id == 0)
            {
                _unitOfWork.Category.Add(CategoryObj);
            }
            else
            {
                _unitOfWork.Category.Update(CategoryObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}