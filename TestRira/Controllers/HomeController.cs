using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestRira.Models;
using TestRira.Core.interfaces;
using TestRira.Core.ModelViews;

namespace TestRira.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _userService;

        public HomeController(ILogger<HomeController> logger, IUsersService usersService)
        {
            _logger = logger;
            _userService = usersService;

        }

        public IActionResult Index()
        {
            var item=_userService.listOfUsers();
            var datamodel = ListOfUSer.Parser.ParseFrom(item);
            var model = new List<UserViewModel>();
            foreach (var x in datamodel.UserList)
            {
                model.Add(new UserViewModel
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    Name = x.Nam,
                    NationCode = x.NationCode
                });
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult addUser(string name,string lastName,string nation,string birth)
        {
            //create message
            var user = new Users
            {
                BirthDate = birth,
                LastName = lastName,
                Nam = name,
                NationCode = nation
            };
            //
            var inp = user.ToByteArray();
            //send to Service
            var results = _userService.addUser(inp);
            var model = result.Parser.ParseFrom(results);
            //
            return Json(new {statusCode=model.StatusCode,statusMessage=model.StatusMessage});
        }

        public IActionResult updateUser(string Name, string LastName,string Nation,int id)
        {
            var editUser = new updateUser
            {
                LastName = LastName,
                Name = Name,
                NationCode = Nation,
                Id=id
            };
            var updateModel = editUser.ToByteArray();
            var resultss = _userService.updateUsers(updateModel);
            var res = result.Parser.ParseFrom(resultss);
            return Json(new {statusCode=res.StatusCode,statusMessage=res.StatusMessage});
        }

        public IActionResult deleteUser(int id)
        {
            var item = new delete
            {
                Id = id
            };
            var deleteUser = item.ToByteArray();
            var results = _userService.deleteUser(deleteUser);
            var res = result.Parser.ParseFrom(results);
            return Json(new {statusCode=res.StatusCode,statusMessage=res.StatusMessage});
        }
        public IActionResult getById(int id)
        {
            var item = new delete
            {
                Id = id
            };
            var model=item.ToByteArray();
            var result = _userService.getByUser(model);
            var res=Users.Parser.ParseFrom(result);
            return Json(new {name=res.Nam,lastName=res.LastName,nation=res.NationCode,birth=res.BirthDate,id=res.Id});
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
