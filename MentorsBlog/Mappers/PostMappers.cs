using System;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Models.Responses;
using System.Collections.Generic;
using MentorsBlog.Models.Requests;

namespace MentorsBlog.Mappers
{
    internal static class PostMappers
    {
        #region Descending
        
        internal static Post ToPost(this RequestCreatePost source)
        {
            return source == null ? default : new Post
            {
                Title = source.Title,
                Preview = source.Preview,
                Body = source.Body
            };
        }
        
        internal static Post ToPost(this RequestUpdatePost source, Guid id)
        {
            return source == null ? default : new Post
            {
                Id = id,
                Title = source.Title,
                Preview = source.Preview,
                Body = source.Body
            };
        }

        #endregion

        #region Ascending

        internal static PostResponse ToResponse(this Post source)
        {
            return source == null ? default : new PostResponse
            {
                Id = source.Id,
                Title = source.Title,
                Preview = source.Preview,
                Body = source.Body,
                PublishDate = source.PublishDate
            };
        }

        internal static IEnumerable<PostResponse> ToResponse(this IEnumerable<Post> source)
        {
            return source?.MapToList(x => x.ToResponse());
        }
        
        #endregion
    }
}