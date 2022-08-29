using System;
namespace CommandsService.Models
{
    public class UserConstants
    {
        public UserConstants()
        {
        }

        public static List<UserModel> users = new List<UserModel>()
        {
            new UserModel(){Username="yotam",EmailAddress="jason.admin@email.com",Password="1234",GivenName="yotam",Surname="saadon",Role="Administrator"},
            new UserModel(){Username="gal",EmailAddress="jason.admin@email.com",Password="1234",GivenName="gal",Surname="saadon",Role="Seller"}
        };
    }
}

