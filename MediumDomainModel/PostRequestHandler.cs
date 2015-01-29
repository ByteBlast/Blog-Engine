﻿using System.Data;
using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class PostRequestHandler : IRequestHandler<PostRequest, PostModel>
    {
        private readonly IDbConnection _connection;

        public PostRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public PostModel Handle(PostRequest request)
        {
            var param = new { Slug = request.PostSlug };

            var post = _connection
                .Query<PostModel>(@"SELECT * FROM [Posts] WHERE [Slug] = @Slug", param)
                .SingleOrDefault();

            post.Tags = _connection
                .Query<TagModel>(@"
                        SELECT 
                            [TagName] AS [Name],
                            (SELECT COUNT (*) 
	                         FROM [Junction]
	                          WHERE [Junc].[TagName] = [TagName]) AS [Count]
                        FROM [Junction] AS [Junc]
                        WHERE [PostSlug] = @Slug", param);

            return post;
        }
    }
}