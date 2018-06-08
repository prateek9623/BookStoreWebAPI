using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserReview
    {
        public User ReviewOwner { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewComment { get; set; }

        public UserReview(User ReviewOwner, int ReviewRating, string ReviewComment)
        {
            this.ReviewOwner = ReviewOwner;
            this.ReviewRating = ReviewRating;
            this.ReviewComment = ReviewComment;
        }
    }
}
