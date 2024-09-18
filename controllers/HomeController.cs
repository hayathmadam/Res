using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using NuGet.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using System.Text.Json;
using Azure.Core;
using static System.Reflection.Metadata.BlobBuilder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace EmployeeManagment.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        private IEmployeeRepository _employeeRepository;
        //private  IHostEnvironment hostEnvironment;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IAddItemRepository addItemRepository;
        private readonly IitemtypeRepository iitemtypeRepository;

        //private readonly ILogger logger;
        public HomeController(IEmployeeRepository employeeRepository,
                             IHostingEnvironment hostingEnvironment, IAddItemRepository addItemRepository,
                             IitemtypeRepository iitemtypeRepository)
        {
            _employeeRepository = employeeRepository;
            _hostEnvironment = hostingEnvironment;
            this.addItemRepository = addItemRepository;
            this.iitemtypeRepository = iitemtypeRepository;
        }
        [HttpGet]
        public IActionResult Cahser()
        {
            return View();

        }
        public IActionResult addCahser(ViewModels.FullItem model)
        {

            if (ModelState.IsValid)
            {

            }
            return Json("hello");
        }

            [HttpGet]
        [AllowAnonymous]
        public IActionResult ItemType()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult ItemType(ItemTypecreateViewModel model)
        {

            if (ModelState.IsValid)
            {

                ItemType itemTypes = new ItemType
                {
                    Name = model.Name,

                };
                iitemtypeRepository.add(itemTypes);
                return RedirectToAction("ItemTypeDetails");
            }
            return View();

        }


        public IActionResult ItemTypeDetails()
        {

            return View();

        }
        public IActionResult ItemTypeGetDetails()
        {
            var model = iitemtypeRepository.GetAll();
            return Json(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ItemTypeEdit(int id)
        {

            ItemType itemTypee = iitemtypeRepository.GettypeItem(id);

            ItemTypeEditViewModel itemTypeEditViewModel = new ItemTypeEditViewModel()
            {
                Name = itemTypee.Name,

            };

            return View(itemTypeEditViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ItemTypeEdit(ItemTypeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                ItemType itemType = iitemtypeRepository.GettypeItem(model.Id);
                itemType.Name = model.Name;
                iitemtypeRepository.update(itemType);
                return RedirectToAction("ItemTypeDetails");
            }
            return View();

        }

        public IActionResult ItemTypeDelete(int id)
        {


            ItemType itemdelete = iitemtypeRepository.GettypeItem(id);
            iitemtypeRepository.delete(id);
            return RedirectToAction("ItemTypeDetails");

        }


        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            //return _employeeRepository.GetEmployee(1).Department
            AddItemItemTypeViewModel model = new AddItemItemTypeViewModel();
            model.typeItemlist = iitemtypeRepository.GetAll();
            model.addItemlist = addItemRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult additem()
        {
            ItemViewItemCreateViewModel model = new ItemViewItemCreateViewModel();
            model.ItemType = iitemtypeRepository.GetAll();
            return View(model);
        }


        [HttpPost]
        public IActionResult AddItem(ItemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.AddItem newitem = new Models.AddItem
                {
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemType = model.ItemType,
                };
                addItemRepository.add(newitem);
                return Json("Data has saved");

            }
            else
            {
             return Json("Data hasn,t saved");

            }
       
        }

        public IActionResult ItemDetails()
        {

            return View();

        }
        public IActionResult GetItemDetails()
        {
            var model = addItemRepository.GetAll();
            return Json(model);

        }

        [AllowAnonymous]
        [HttpGet]


        public IActionResult ItemEdit(int id)
        {
            Models.AddItem addItem = addItemRepository.GetItem(id);

            ItemEditViewModel itemEditViewModel = new ItemEditViewModel()
            {
                ItemName = addItem.ItemName,
                ItemType = addItem.ItemType,
                ItemPrice = addItem.ItemPrice,

            };
            return View(itemEditViewModel);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ItemEdit(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
               Models.AddItem addItem = addItemRepository.GetItem(model.Id);
                addItem.ItemName = model.ItemName;
                addItem.ItemType = model.ItemType;
                addItem.ItemPrice = model.ItemPrice;
                addItemRepository.update(addItem);
                return Json("response");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ItemDetete(int id)
        {
            Models.AddItem addItem = addItemRepository.GetItem(id);
            addItemRepository.delete(id);
            return RedirectToAction("Itemdetails");
        }


        public IActionResult Home()
        {
            var model = _employeeRepository.GetAll();
            return View(model);

        }



       
        [Route("Home/Details/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            //throw new NotImplementedException("Details View is not found");
            //logger.LogTrace("logtrace");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetuailsViewModel homeDetuailsViewModel = new HomeDetuailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details",
            };

            //Employee Model=  _employeeRepository.GetEmployee(2);

            // passing Data to view using viewData concept
            //ViewData ["Employee"] =model;
            //ViewData["PageTitle"] = "Employee Datails

            //passing Data to view using viewpag concept  
            //ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employess Detuails";
            //passing Data to view using srtongly concept
            return View(homeDetuailsViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,

            };
            return View(employeeEditViewModel);
        }

        public IActionResult Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                _employeeRepository.update(employee);
                return RedirectToAction("Index");
            }

            return View();

        }






        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                string UniuqeFileName = null;
                if (model.Photo != null)
                {

                    string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                    UniuqeFileName = Guid.NewGuid().ToString() + "-" + model.Photo.FileName;
                    string filepath = Path.Combine(UniuqeFileName, UploadsFolder);
                    //model.Photo.CopyTo(new FileStream(filepath,FileMode.Create));
                }
                Employee newemployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    photopat = UniuqeFileName
                };
                _employeeRepository.add(newemployee);
                return RedirectToAction("Details", new { id = newemployee.Id });
            }
            return View();


        }



    }

}
