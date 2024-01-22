using ApnaBank.Data;
using ApnaBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaBank.Controllers
{
    public class TransferAmountController : Controller
    {
        private readonly ApnaBankDbContext _apnaBankDbContext;

        public TransferAmountController(ApnaBankDbContext apnaBankDbContext)
        {
            _apnaBankDbContext = apnaBankDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TransferAmount(AccountInfo accountInfo)
        {
            var account = _apnaBankDbContext.AccountDetails.Where(a => a.AccountNumber == accountInfo.AccountNumber && a.UserName == accountInfo.UserName && a.Password == accountInfo.Password).FirstOrDefault();
            TempData["TransferredAmount"] = accountInfo.Amount.ToString();
            if (account != null)
            {
                account.Amount -= accountInfo.Amount;
                _apnaBankDbContext.AccountDetails.Update(account);
                _apnaBankDbContext.SaveChanges();
                TempData["SourceAccountBalance"] = account.Amount.ToString();

                var targetAccount = _apnaBankDbContext.AccountDetails.Where(a => a.AccountNumber == accountInfo.TargetAccountNumber).FirstOrDefault();
                if (targetAccount != null)
                {
                    targetAccount.Amount += accountInfo.Amount;
                    _apnaBankDbContext.AccountDetails.Update(targetAccount);
                    _apnaBankDbContext.SaveChanges();
                    TempData["TargetAccountBalance"] = targetAccount.Amount.ToString();
                    return RedirectToAction("AccountInfo");
                }
            }
            TempData["ResultNotFound"] = "Please Enter Correct details!....";
            return View();
        }

        public IActionResult AccountInfo()
        {
            return View();
        }
    }
}
