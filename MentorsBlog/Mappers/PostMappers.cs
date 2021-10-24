using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Models.Responses;
using System.Collections.Generic;

namespace MentorsBlog.Mappers
{
    internal static class PostMappers
    {
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
            return source == null ? default : source.MapToList(x => x.ToResponse());
        }
    }
}