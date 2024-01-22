using ApnaBank.Data;
using ApnaBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaBank.Controllers
{
    public class WithdrawController : Controller
    {
        private readonly ApnaBankDbContext _apnaBankDbContext;
        public WithdrawController(ApnaBankDbContext apnaBankDbContext)
        {
            _apnaBankDbContext = apnaBankDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AccountInfo(AccountDetails accountDetails)
        {
            var acDetails = _apnaBankDbContext.AccountDetails.Where(a => a.UserName == accountDetails.UserName && a.Password == accountDetails.Password).FirstOrDefault();
            if (acDetails != null)
            {
                acDetails.Amount -= accountDetails.Amount;
                _apnaBankDbContext.AccountDetails.Update(acDetails);
                _apnaBankDbContext.SaveChanges();
                TempData["ResultOK"] = Convert.ToInt32(acDetails.Amount);
                return View();
            }
            else
            {
                TempData["ResultNotFound"] = "Please enter correct account details!..";
            }
            return View("Index");
        }
    }
}
