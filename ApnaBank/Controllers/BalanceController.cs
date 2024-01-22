using ApnaBank.Data;
using ApnaBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaBank.Controllers
{
    public class BalanceController : Controller
    {
        private readonly ApnaBankDbContext _apnaBankDbContext;
        public BalanceController(ApnaBankDbContext apnaBankDbContext)
        {
            _apnaBankDbContext = apnaBankDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AccountBalance(AccountDetails accountDetails)
        {
            var acDetails = _apnaBankDbContext.AccountDetails.Where(a => a.UserName == accountDetails.UserName && a.Password == accountDetails.Password).FirstOrDefault();
            if (acDetails == null)
            {
                TempData["ResultNotFound"] = "Please check your Username and Password";
                return RedirectToAction("Index");
            }
            return View(acDetails);
        }
    }
}
