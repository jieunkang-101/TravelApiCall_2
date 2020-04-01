using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelApi.Models;
using TravelApi.Helpers;

namespace TravelApi.Services
{
  public interface IUserService
  {
    User Authenticate(string username, string password);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User Create(User user, string password);
    void Update(User user, string password = null);
    void Delete(int id);
  }

  public class UserService : IUserService
  {
    private readonly AppSettings _appSettings;
    private TravelApiContext _context;

    public UserService(IOptions<AppSettings> appSettings, TravelApiContext context)
    {
      _appSettings = appSettings.Value;
      _context = context;
    }

    public User Authenticate(string username, string password)
    {
      var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

      // return null if user not found
      if (user == null)
      {
        return null;
      }

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] 
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      // remove password before returning
      user.Password = null;

      return user;
    }

    // GET users
    public IEnumerable<User> GetAll()
    {
      return _context.Users;
    }

    public User GetById(int id) 
    {
      var user = _context.Users.FirstOrDefault(x => x.Id == id);

      // return user without password
      if (user != null) 
      {
        user.Password = null;
      }
      return user;
    }

    public User Create(User user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new AppException("Password is required");
      }
          
      if (_context.Users.Any(x => x.Username == user.Username))
      {
        throw new AppException("Username \"" + user.Username + "\" is already taken");
      }

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public void Update(User userParam, string password = null)
    {
      var user = _context.Users.Find(userParam.Id);

      if (user == null)
      {
        throw new AppException("User not found");
      }
          
      if (userParam.Username != user.Username)
      {
        // username has changed so check if the new username is already taken
        if (_context.Users.Any(x => x.Username == userParam.Username))
        {
          throw new AppException("Username " + userParam.Username + " is already taken");
        }     
      }

      // update user properties
      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.Username = userParam.Username;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        user.Password = password;
      }

      _context.Users.Update(user);
      _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var user = _context.Users.Find(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        _context.SaveChanges();
      }
    }
  }
}