using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.ViewModels
{
    public class DashboardViewModel
    {
        private readonly ApplicationDbContext _context;

        public Employee Employee { get; set; }
        public Category Category { get; set; }
        public CleaningTask CleaningTask { get; set; }
        /// <summary>
        public Client Client { get; set; }
        public Comment Comment { get; set; }
        public InHouseActivity InHouseActivity { get; set; }
        public Meal Meal { get; set; }
        public Outing Outing { get; set; }
        public Shift Shift { get; set; }
        public ViewingParty ViewingParty { get; set; }
        public WishListItem WishListItem { get; set; }

        /// </summary>

        public SelectList EmployeeList { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList CleaningTaskList { get; set; }
        /// <summary>
        public SelectList ClientList { get; set; }
        public SelectList CommentList { get; set; }
        public SelectList InHouseActivityList { get; set; }
        public SelectList MealList { get; set; }
        public SelectList OutingList { get; set; }
        public SelectList ShiftList { get; set; }
        public SelectList ViewingPartyList { get; set; }
        public SelectList WishListItemList { get; set; }
        /// </summary>
        List<Employee> ListOfEmployees { get; set; }
        List<Category> ListOfCategories { get; set; }
        List<CleaningTask> ListOfCleaningTasks { get; set; }
        List<Client> ListOfClients { get; set; }
        List<Comment> ListOfComments { get; set; }
        List<InHouseActivity> ListOfInHouseActivities { get; set; }
        List<Meal> ListOfMeals { get; set; }
        List<Outing> ListOfOutings { get; set; }
        List<Shift> ListOfShifts { get; set; }
        List<ViewingParty> ListOfViewingParties { get; set; }
        List<WishListItem> ListOfWishListItems { get; set; }



        public DashboardViewModel(ApplicationDbContext context)
        {
            _context = context;

            ListOfEmployees = _context.Employees.Select(e => e).ToList();
            EmployeeList = new SelectList(ListOfEmployees, "Id", "FirstName");

            ListOfCategories = _context.Categories.Select(c => c).ToList();
            CategoryList = new SelectList(ListOfCategories, "Id", "Name");

            ListOfCleaningTasks = _context.CleaningTasks.Select(c => c).ToList();
            CleaningTaskList = new SelectList(ListOfCleaningTasks, "Id", "Name");
            /////
            ///
            ListOfClients = _context.Clients.Select(c => c).ToList();
            ClientList = new SelectList(ListOfClients, "Id", "Name");

            ListOfComments = _context.Comments.Select(c => c).ToList();
            CommentList = new SelectList(ListOfComments, "Id", "Name");

            ListOfInHouseActivities = _context.InHouseActivities.Select(c => c).ToList();
            InHouseActivityList = new SelectList(ListOfInHouseActivities, "Id", "Name");

            ListOfMeals = _context.Meals.Select(c => c).ToList();
            MealList = new SelectList(ListOfMeals, "Id", "Name");

            ListOfOutings = _context.Outings.Select(c => c).ToList();
            OutingList = new SelectList(ListOfOutings, "Id", "Name");

            ListOfShifts = _context.Shifts.Select(c => c).ToList();
            ShiftList = new SelectList(ListOfShifts, "Id", "Name");

            ListOfViewingParties = _context.ViewingParties.Select(c => c).ToList();
            ViewingPartyList = new SelectList(ListOfViewingParties, "Id", "Name");

            ListOfWishListItems = _context.WishListItems.Select(c => c).ToList();
            WishListItemList = new SelectList(ListOfWishListItems, "Id", "Name");
        }
    }
}
