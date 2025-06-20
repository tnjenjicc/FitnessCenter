using Microsoft.EntityFrameworkCore;
using Projekat_A.Models;

namespace Projekat_A.Services
{
    public class TrainerService : BaseService
    {
        public TrainerService()
        {
        }

        public List<Hall> GetAllHalls()
        {
            try
            {
                return _context.Halls.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching halls: {ex.Message}");
                return new List<Hall>();
            }
        }

        public List<TrainingSession> GetTrainerSessions(int trainerId)
        {
            try
            {
                return _context.Trainingsessions
                    .Where(ts => ts.TrainerUserId == trainerId)
                    .Include(ts => ts.HallIdHallNavigation)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching trainer sessions: {ex.Message}");
                return new List<TrainingSession>();
            }
        }
        public TrainingSession GetTrainingSessionById(int sessionId)
        {
            try
            {
                return _context.Trainingsessions
                    .Include(ts => ts.HallIdHallNavigation)
                    .Include(ts => ts.TrainerUser)
                    .FirstOrDefault(ts => ts.IdSession == sessionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching training session: {ex.Message}");
                return null;
            }
        }
        public List<TrainingSession> GetSessionsByHall(int hallId)
        {
            try
            {
                return _context.Trainingsessions
                    .Where(ts => ts.HallIdHall == hallId)
                    .Include(ts => ts.TrainerUser)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sessions by hall: {ex.Message}");
                return new List<TrainingSession>();
            }
        }


        public bool AddTrainingSession(TrainingSession session)
        {
            try
            {
                _context.Trainingsessions.Add(session);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding training session: {ex.Message}");
                return false;
            }
        }

        public bool UpdateTrainingSession(TrainingSession session)
        {
            try
            {
                _context.Trainingsessions.Update(session);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating training session: {ex.Message}");
                return false;
            }
        }

        public bool DeleteTrainingSession(int sessionId)
        {
            try
            {
                var session = _context.Trainingsessions.Find(sessionId);
                if (session != null)
                {
                    _context.Trainingsessions.Remove(session);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting training session: {ex.Message}");
                return false;
            }
        }

        public bool ValidateTrainingSession(TrainingSession session)
        {
            var trainer = _context.Trainers.Find(session.TrainerUserId);
            if (trainer == null)
                return false;

            var hall = _context.Halls.Find(session.HallIdHall);
            if (hall == null)
                return false;

            return true;
        }
    }
}