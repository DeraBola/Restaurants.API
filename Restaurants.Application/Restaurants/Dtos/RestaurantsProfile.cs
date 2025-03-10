﻿using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
	public class RestaurantsProfile : Profile
	{
		public RestaurantsProfile() {

			// CreateMap<UpdateRestaurantCommand, Restaurant>()

			CreateMap<Restaurant, RestaurantDto>() // Create mapping from src: Restaurant to dest RestaurantDto.
		  .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
		  .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
		  .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
		  .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes)); // We also need to create a mapping from Dish to DishDto


			CreateMap<CreateRestaurantCommand, Restaurant>() // Create mapping from src: CreateRestaurantDto to dest Restaurant.
				.ForMember(dest => dest.Address, opt => opt.MapFrom(
					src => new Address
					{
						City = src.City,
						PostalCode = src.PostalCode,
						Street = src.Street,
					}));
			CreateMap<UpdateRestaurantCommand, Restaurant>();


			/*	CreateMap<CreateRestaurantCommand, Restaurant>()
					.ForMember(d => d.Address, opt => opt.MapFrom(
						src => new Address
						{
							City = src.City,
							PostalCode = src.PostalCode,
							Street = src.Street,
						}));

				CreateMap<Restaurant, RestaurantDto>()
					.ForMember(d => d.Address, opt => opt.MapFrom(src => src.Address))
					.ForMember(d => d.Dishes, opt =>
					opt.MapFrom(src => src.Dishes));
			*/
		}
	}
}
