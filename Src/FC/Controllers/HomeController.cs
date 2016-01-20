using FC.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FC.Models.Session;
using FC.Models;
using System.Threading.Tasks;
using FC.Models.Database;

namespace FC.Controllers
{
    public class HomeController : Controller
    {
        private WorldContext world = WorldContext.Instance;

        private static Dictionary<Guid, object> ActiveConnections = new Dictionary<Guid, object>();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var connectionInfo = this.Session.GetConnectionInfo();
            if (connectionInfo == null || !ActiveConnections.ContainsKey(connectionInfo.ConnectionId))
            {
                return this.View("Login");
            }

            var clientDeviceInfo = this.Session.GetClientDeviceInfo();
            if (clientDeviceInfo == null)
            {
                var clientDeviceInfoHeightCookie = this.Request.Cookies["client-device-height"];
                var clientDeviceInfoWidthCookie = this.Request.Cookies["client-device-width"];
                
                int clientDeviceInfoHeight, clientDeviceInfoWidth;
                if (clientDeviceInfoHeightCookie != null && int.TryParse(clientDeviceInfoHeightCookie.Value, out clientDeviceInfoHeight) &&
                    clientDeviceInfoWidthCookie != null && int.TryParse(clientDeviceInfoWidthCookie.Value, out clientDeviceInfoWidth))
                {
                    clientDeviceInfo = new ClientDeviceInfo
                    {
                        ScreenResolution = new System.Drawing.Size(clientDeviceInfoWidth, clientDeviceInfoHeight)
                    };

                    this.Session.SetClientDeviceInfo(clientDeviceInfo);
                }
                else
                {
                    return View("NewSession");
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string data)
        {
            var connectionInfo = this.Session.GetConnectionInfo();
            if (connectionInfo == null || !ActiveConnections.ContainsKey(connectionInfo.ConnectionId))
            {
                return this.RedirectToAction("Index");
            }

            if (string.Equals(this.Request.QueryString.Get("v"), "c", StringComparison.OrdinalIgnoreCase))
            {
                return this.Json(new[] { new { name = "abc", level = "1", location = "Dalaran", @class = "Mage", race = "Human" } });
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new FCDBDataContext())
                {
                    var userAccount = connection.Accounts
                        .Where(account => account.Email == model.Email && account.Status == (int)UserAccountStatus.Active).ToList()
                        .FirstOrDefault(account => PasswordHash.PasswordHash.ValidatePassword(model.Password, account.PasswordHash));
                    
                    if (userAccount != null)
                    {
                        var connectionInfo = new ConnectionInfo(Guid.NewGuid()) { AccountId = userAccount.Id };

                        this.Session.SetConnectionInfo(connectionInfo);
                        ActiveConnections.Add(connectionInfo.ConnectionId, connectionInfo);

                        userAccount.LastLoggedOn = DateTime.Now;
                        userAccount.LastLoginIp = this.Request.UserHostAddress;

                        connection.SubmitChanges();
                        
                        return this.RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new FCDBDataContext())
                {
                    if (connection.Accounts.Any(account => account.Email == model.Email))
                    {
                        ModelState.AddModelError("", "User with such email has been already regstered.");
                        return View(model);
                    }

                    string userIpAddress = this.Request.UserHostAddress;
                    if (connection.Accounts.Count(account => account.LastLoginIp == userIpAddress) > Constants.MaxNumberAccountsPerIp)
                    {
                        ModelState.AddModelError("", "Number of allowed users reached maximum.");
                        return View(model);
                    }

                    var newUserAccount = new Account
                    { 
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        PasswordHash = PasswordHash.PasswordHash.CreateHash(model.Password),
                        Status = (int)UserAccountStatus.Active,
                        Type = (int)UserAccountType.Player,
                        Money = 0,
                        CreatedOn = DateTime.Now,
                        LastLoggedOn = DateTime.Now,
                        LastLoginIp = userIpAddress
                    };

                    connection.Accounts.InsertOnSubmit(newUserAccount);
                    connection.SubmitChanges();

                    return this.RedirectToAction("Login");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}