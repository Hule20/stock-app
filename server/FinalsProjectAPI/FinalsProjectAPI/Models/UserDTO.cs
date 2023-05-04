﻿using static FinalsProjectAPI.Controllers.UserController;

namespace FinalsProjectAPI.Models
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public List<StockDTO> Stocks { get; set; } = new List<StockDTO>();


        public static UserDTO MapFrom(User user)
        {
            var userDto = new UserDTO();

            userDto.ID = user.ID;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.Email = user.Email;
            userDto.Password = user.Password;

            if (user.UserStocks != null)
            {
                foreach (var item in user.UserStocks.ToList())
                {
                    userDto.Stocks.Add(StockDTO.MapFrom(item.Stock));
                }
            }

            return userDto;
        }
    }
}
