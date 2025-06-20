using Microsoft.EntityFrameworkCore;
using Projekat_A.Models;

namespace Projekat_A.Services
{
    public class GroupService : BaseService
    {
        public GroupService() : base()
        {
        }

        public List<Member> GetGroupMembers(int groupId)
        {
            try
            {
                return _context.Groupmemberships
                    .Where(gm => gm.GroupUserId == groupId && gm.LeaveDate == null)
                    .Include(gm => gm.MemberUser)
                    .ThenInclude(m => m.User)
                    .Select(gm => gm.MemberUser)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching group members: {ex.Message}");
                return new List<Member>();
            }
        }

        public List<Member> GetAvailableMembers(int groupId)
        {
            try
            {
                var existingMemberIds = _context.Groupmemberships
                    .Where(gm => gm.GroupUserId == groupId && gm.LeaveDate == null)
                    .Select(gm => gm.MemberUserId)
                    .ToList();

                return _context.Members
                    .Include(m => m.User)
                    .Where(m => !existingMemberIds.Contains(m.UserId))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching available members: {ex.Message}");
                return new List<Member>();
            }
        }

        public bool AddMemberToGroup(int groupId, int memberId)
        {
            try
            {
                Console.WriteLine($"Attempting to add member {memberId} to group {groupId}");

                if (!_context.Groups.Any(g => g.UserId == groupId))
                {
                    Console.WriteLine($"Group with ID {groupId} does not exist");
                    return false;
                }

                if (!_context.Members.Any(m => m.UserId == memberId))
                {
                    Console.WriteLine($"Member with ID {memberId} does not exist");
                    return false;
                }

                var existingMembership = _context.Groupmemberships
                    .FirstOrDefault(gm => gm.GroupUserId == groupId && gm.MemberUserId == memberId && gm.LeaveDate == null);

                if (existingMembership != null)
                {
                    Console.WriteLine("Member already exists in group");
                    return false;
                }

                var group = _context.Groups.Find(groupId);
                if (group == null)
                {
                    Console.WriteLine("Group not found");
                    return false;
                }

                var currentMembersCount = _context.Groupmemberships
                    .Count(gm => gm.GroupUserId == groupId && gm.LeaveDate == null);

                if (currentMembersCount >= group.MaxNumberOfMembers)
                {
                    Console.WriteLine($"Group is full: {currentMembersCount}/{group.MaxNumberOfMembers}");
                    return false;
                }

                var groupMembership = new GroupMembership
                {
                    GroupUserId = groupId,
                    MemberUserId = memberId,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now),
                    LeaveDate = null
                };

                _context.Groupmemberships.Add(groupMembership);
                _context.SaveChanges();

                Console.WriteLine("Member successfully added to group");
                return true;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.Message}");
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {dbEx.InnerException.Message}");

                    if (dbEx.InnerException.Message.Contains("foreign key constraint"))
                    {
                        Console.WriteLine("Foreign key constraint violation - check if GroupUserId and MemberUserId exist");
                    }

                    if (dbEx.InnerException.Message.Contains("duplicate") || dbEx.InnerException.Message.Contains("unique"))
                    {
                        Console.WriteLine("Duplicate key violation - member already exists in group");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error adding member to group: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
        public bool RemoveMemberFromGroup(int groupId, int memberId)
        {
            try
            {
                var membership = _context.Groupmemberships
                    .FirstOrDefault(gm => gm.GroupUserId == groupId && gm.MemberUserId == memberId && gm.LeaveDate == null);

                if (membership != null)
                {
                    membership.LeaveDate = DateOnly.FromDateTime(DateTime.Now);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing member from group: {ex.Message}");
                return false;
            }
        }

        public Group GetGroupById(int groupId)
        {
            try
            {
                return _context.Groups
                    .Include(g => g.TrainerUser)
                    .ThenInclude(t => t.User)
                    .FirstOrDefault(g => g.UserId == groupId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching group: {ex.Message}");
                return null;
            }
        }

        public int GetActiveMembersCount(int groupId)
        {
            try
            {
                return _context.Groupmemberships
                    .Count(gm => gm.GroupUserId == groupId && gm.LeaveDate == null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting active members: {ex.Message}");
                return 0;
            }
        }

        public bool IsGroupFull(int groupId)
        {
            try
            {
                var group = _context.Groups.Find(groupId);
                if (group == null) return true;

                var currentMembersCount = GetActiveMembersCount(groupId);
                return currentMembersCount >= group.MaxNumberOfMembers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if group is full: {ex.Message}");
                return true;
            }
        }
    }
}