using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HawkSoftWebApp.Models;
using HawkSoftWebApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HawkSoftWebApp.Controllers
{
    public class UserController : Controller
    {
        private IUserServices services;

        public UserController(IUserServices userServices)
        {
            services = userServices;
        }
        // GET: User
        public async Task<ActionResult> Index()
        {
            var user = await services.UserList();
            return View(user);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                User user = new User()
                {
                    UserId = Convert.ToInt32(collection["UserId"]),
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Address = collection["Address"],
                    PhoneNumber = collection["PhoneNumber"],
                    Email = collection["Email"],
                };


                services.InsertUser(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await services.GetUser(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                User user = new User()
                {
                    UserId = Convert.ToInt32(collection["UserId"]),
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Address = collection["Address"],
                    PhoneNumber = collection["PhoneNumber"],
                    Email = collection["Email"],
                };
                

                services.UpdateUser(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}