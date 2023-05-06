﻿using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class BookReviewRepository : Data.Interfaces.IBookReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public BookReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public BookReview GetById(int id)
        {
            var book = _context.Books5.FromSqlRaw($"SELECT * from Books5 WHERE Id = {id}").FirstOrDefault<BookReview>();
                //FirstOrDefault(i => i.Id == id);
            return book;
        }
        public IEnumerable<BookReview> GetList()
        {
            var bookList = _context.Books5.ToList();
            return bookList;
        }
        public IEnumerable<BookReview> GetTopList()
        {
            var topList = _context.Books5.FromSqlRaw($"SELECT * from Books5 WHERE Rating > 4").ToList();
            //FirstOrDefault(i => i.Id == id);
            return topList;
        }
        public IEnumerable<BookReview> GetByCountry(string country)
        {
            var countryList = _context.Books5.FromSqlRaw($"SELECT * from Books5 WHERE Country = '{country}'").ToList();
            return countryList;
        }
        public bool Add(BookReview book)
        {
            _context.Books5.Add(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            var book = _context.Books5.FirstOrDefault(i => i.Id == id);
            _context.Remove(book);
            return Save();
        }
    }
}