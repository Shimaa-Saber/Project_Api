﻿namespace Project_Api.DTO
{
    public class UserRegister
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool agree { get; set; }




}
}
