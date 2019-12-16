using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/comments")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("{id}")]
        public IEnumerable<Comment> GetCommentsByAd(int id)
        {
            var comments = _commentRepository.GetAll();

            return comments.Where(comment => comment.AdvertisementId == id).ToList();
        }

        [HttpPost]
        public IActionResult PostComment([FromBody] Comment comment)
        {
            try
            {
                _commentRepository.Add(comment);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}