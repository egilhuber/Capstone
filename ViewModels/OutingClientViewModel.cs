using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.ViewModels
{
    public class OutingClientViewModel
    {
        private readonly ApplicationDbContext _context;

        public Outing Outing { get; set; }

        public Client Client { get; set; }

        public SelectList ClientList { get; set; }

        public OutingClientViewModel(ApplicationDbContext context)
        {
            _context = context;

            List<Client> ListOfClients = _context.Clients.Select(c => c).ToList();
            ClientList = new SelectList(ListOfClients, "Id", "PrefFirstName");

        }
    }
}
