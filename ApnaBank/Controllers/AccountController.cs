using ApnaBank.Data;
using ApnaBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApnaBankDbContext _apnaBankDbContext;

        public AccountController(ApnaBankDbContext apnaBankDbContext)
        {
            _apnaBankDbContext = apnaBankDbContext;
        }

        public IActionResult Index()
        {
            var existingAccounts = _apnaBankDbContext.AccountDetails.ToList();
            return View(existingAccounts);
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountInfo accountInfo)
        {
            AccountDetails accountDetails = new AccountDetails();
            accountDetails.UserName = accountInfo.UserName;
            accountDetails.Password = accountInfo.Password;
            accountDetails.Amount = accountInfo.Amount;
            accountDetails.Address = accountInfo.Address;
            accountDetails.PhoneNumber = accountInfo.PhoneNumber;
            if (ModelState.IsValid)
            {
                _apnaBankDbContext.AccountDetails.Add(accountDetails);
                _apnaBankDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult CloseAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CloseAccount(AccountDetails accountDetails)
        {
            var account = _apnaBankDbContext.AccountDetails.Where(a => a.AccountNumber == accountDetails.AccountNumber && a.UserName == accountDetails.UserName && a.Password == accountDetails.Password).FirstOrDefault();
            if (account != null)
            {
                _apnaBankDbContext.AccountDetails.Remove(account);
                _apnaBankDbContext.SaveChanges();
                TempData["ResultOK"] = account.UserName + " Your Account De-Activated Successfully!...";
                return RedirectToAction("Index");
            }
            TempData["ResultNotFound"] = "Please check your Username and Password";
            return View();
        }
    }
}
