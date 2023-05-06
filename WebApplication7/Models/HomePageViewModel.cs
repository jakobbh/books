﻿namespace WebApplication7.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<BookReview> books { get; set; }
        public LoginModel loginModel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
