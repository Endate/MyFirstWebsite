using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Data;
using MyFirstWebsite.Web.Models.Domain;
using MyFirstWebsite.Web.Models.ViewModels;
using MyFirstWebsite.Web.Repositories;

namespace MyFirstWebsite.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;
           
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            ValidateAddTagRequest(addTagRequest);
            if(ModelState.IsValid == false)
            {
                return View();
            }
            // Mapping AddTagRequest to Tag domain model

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

                await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var tag = await tagRepository.GetAsync(id);
            
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                    return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //Show success notification
            }
            else
            {
                //Show error notification
            }

            return RedirectToAction("Edit" , new { id = editTagRequest.Id });
        }

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            // show an error notification
            return RedirectToAction("Edit", new { editTagRequest.Id });  
        }
            
        private void ValidateAddTagRequest(AddTagRequest addTagRequest)
        {
            if (addTagRequest.Name != null && addTagRequest.DisplayName != null)
            {
                if(addTagRequest.Name == addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
    }
}
