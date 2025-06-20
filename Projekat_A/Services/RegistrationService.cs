using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Projekat_A.Services
{
    public class RegistrationService : BaseService
    {
        public RegistrationService()
        {
        }
        public Boolean RegisterMember(String username, String password, String email, String phone,
                String firstName, String lastName, DateTime birthDate, DateTime enrollmentDate)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Users.Any(u => u.Username == username))
                        return false;

                    var user = new User()
                    {
                        Username = username,
                        Password = DBUtil.hashPassword(password),
                        EmailAddress = email,
                        PhoneNumber = phone,
                        AccountType = "Member",
                        Font = "Arial",
                        Mode = 0,
                        Theme = 0,
                        Logged = 0
                    };

                    _context.Users.Add(user);

                    _context.SaveChanges();

                    var member = new Member()
                    {
                        UserId = user.Id,
                        Name = firstName,
                        Surname = lastName,
                        DateOfBirth = DateOnly.FromDateTime(birthDate),
                        DateOfEnrollment = DateOnly.FromDateTime(enrollmentDate)
                    };

                    _context.Members.Add(member);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught DbUpdateException: " + dbEx.Message);
                    Console.WriteLine("Inner Exception: " + dbEx.InnerException?.Message);
                    throw; 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    throw;
                }
            }
        }
        public Boolean RegisterTrainer(String username, String password, String email, String phone,
                    String firstName, String lastName, String specialization, String workHours)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Users.Any(u => u.Username == username))
                        return false;

                    var user = new User()
                    {
                        Username = username,
                        Password = DBUtil.hashPassword(password),
                        EmailAddress = email,
                        PhoneNumber = phone,
                        AccountType = "Trainer",
                        Font = "Arial",
                        Mode = 0,
                        Theme = 0,
                        Logged = 0
                    };

                    _context.Users.Add(user);

                    _context.SaveChanges();

                    var trainer = new Trainer()
                    {
                        UserId = user.Id, 
                        Name = firstName,
                        Surname = lastName,
                        Specialization = specialization,
                        WorkingTime = workHours
                    };

                    _context.Trainers.Add(trainer);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught DbUpdateException: " + dbEx.Message);
                    Console.WriteLine("Inner Exception: " + dbEx.InnerException?.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    throw;
                }
            }
        }
        public Boolean RegisterGroup(String username, String password, String email, String phone,
                    String groupName, String description, int maxMembers, int trainerUserId)
        {
            if (!_context.Trainers.Any(t => t.UserId == trainerUserId))
            {
                Console.WriteLine($"No trainer found with ID: {trainerUserId}");
                return false;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Users.Any(u => u.Username == username))
                        return false;

                    var user = new User()
                    {
                        Username = username,
                        Password = DBUtil.hashPassword(password),
                        EmailAddress = email,
                        PhoneNumber = phone,
                        AccountType = "Group",
                        Font = "Arial",
                        Mode = 0,
                        Theme = 0,
                        Logged = 0
                    };

                    _context.Users.Add(user);

                    _context.SaveChanges();

                    var group = new Group()
                    {
                        UserId = user.Id,
                        Name = groupName,
                        Description = description,
                        MaxNumberOfMembers = maxMembers,
                        TrainerUserId = trainerUserId
                    };

                    _context.Groups.Add(group);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught DbUpdateException: " + dbEx.Message);
                    Console.WriteLine("Inner Exception: " + dbEx.InnerException?.Message);
                    throw; 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
