﻿using Mapster;
using SuperNews.Domains;
using SuperNews.Models;

namespace SuperNews.Map
{
    public class MapModels
    {
        /// <summary>
        /// маппинг моделей для News
        /// </summary>
        public static void InitNewsMapping()
        {
            // первый класс - куда, второй - откуда
            TypeAdapterConfig<NewsViewModel, News>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.NewsId, src => src.NewsId)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreationDate, src => src.CreationDate)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.NewsRubric.Name, src => src.Rubric)
                .Map(dest => dest.RubricId, src => src.Rubric)
                .Map(dest => dest.RubricId, src => src.Rubrics)
                .Map(dest => dest.Likes, src => src.Likes)
                .Map(dest => dest.Dislikes, src => src.Dislikes);
                

            TypeAdapterConfig<NewsShortModel, News>
                .NewConfig()
                .Map(dest => dest.NewsId, src => src.NewsId)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.CreationDate, src => src.CreationDate);

            TypeAdapterConfig<ChatViewModel, Chat>
                .NewConfig()
                .Map(dest => dest.ChatId, src => src.ChatId);
               
        }
    }
}
