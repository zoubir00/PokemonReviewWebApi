﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository:IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }
    }
}
