using Microsoft.EntityFrameworkCore;
using Projekat_A.Data;
using Projekat_A.Models;
using Projekat_A.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_A.Services
{
    public class MemberService : BaseService
    {
        public async Task<Member> GetMemberByUserIdAsync(int userId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Members
                    .Include(m => m.User)
                    .FirstOrDefaultAsync(m => m.UserId == userId);
            }
        }

        public async Task<List<TrainingSession>> GetAvailableTrainingSessionsAsync()
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Trainingsessions
                    .Include(ts => ts.TrainerUser)
                    .Include(ts => ts.HallIdHallNavigation)
                    .ToListAsync();
            }
        }

        public async Task<bool> ReserveTrainingSessionAsync(int memberId, int trainingSessionId)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var existingReservation = await context.Trainingreservations
                        .AnyAsync(tr => tr.MemberUserId == memberId && 
                                  tr.TrainingSessionIdSession == trainingSessionId);

                    if (existingReservation)
                    {
                        return false;
                    }

                    var reservation = new TrainingReservation
                    {
                        MemberUserId = memberId,
                        TrainingSessionIdSession = trainingSessionId
                    };

                    context.Trainingreservations.Add(reservation);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TrainingSession>> GetMemberReservationsAsync(int memberId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Trainingreservations
                    .Where(r => r.MemberUserId == memberId)
                    .Include(r => r.TrainingSessionIdSessionNavigation)
                        .ThenInclude(ts => ts.TrainerUser)
                    .Include(r => r.TrainingSessionIdSessionNavigation)
                        .ThenInclude(ts => ts.HallIdHallNavigation)
                    .Select(r => r.TrainingSessionIdSessionNavigation)
                    .ToListAsync();
            }
        }

        public async Task<bool> CancelReservationAsync(int memberId, int trainingSessionId)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var reservation = await context.Trainingreservations
                        .FirstOrDefaultAsync(tr => tr.MemberUserId == memberId && 
                                           tr.TrainingSessionIdSession == trainingSessionId);

                    if (reservation == null)
                    {
                        return false;
                    }

                    context.Trainingreservations.Remove(reservation);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Membership>> GetMembershipsByMemberIdAsync(int memberId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Memberships
                    .Include(m => m.MembershipTypeIdTypeNavigation)
                    .Where(m => m.MemberUserId == memberId)
                    .ToListAsync();
            }
        }

        public async Task<List<MembershipType>> GetAllMembershipTypesAsync()
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Membershiptypes.ToListAsync();
            }
        }

        public async Task<bool> AddMembershipAsync(int memberId, int membershipTypeId, DateOnly expirationDate)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var membership = new Membership
                    {
                        MemberUserId = memberId,
                        MembershipTypeIdType = membershipTypeId,
                        ExpirationDate = expirationDate
                    };

                    context.Memberships.Add(membership);
                    await context.SaveChangesAsync();

                    var membershipType = await context.Membershiptypes
                        .FirstOrDefaultAsync(mt => mt.IdType == membershipTypeId);

                    if (membershipType != null)
                    {
                        var payment = new MembershipPayment
                        {
                            MembershipIdMembership = membership.IdMembership,
                            BillingDate = DateOnly.FromDateTime(DateTime.Now),
                            PaymentMethod = "Cash",
                            Price = membershipType.CurrentPrice,
                            IsPaid = false 
                        };

                        context.Membershippayments.Add(payment);
                        await context.SaveChangesAsync();
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExtendMembershipAsync(int membershipId, DateOnly newExpirationDate)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var membership = await context.Memberships
                        .FirstOrDefaultAsync(m => m.IdMembership == membershipId);

                    if (membership == null)
                        return false;

                    membership.ExpirationDate = newExpirationDate;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> IsMembershipActiveAsync(int membershipId)
        {
            using (var context = DBUtil.getContext())
            {
                var membership = await context.Memberships
                    .FirstOrDefaultAsync(m => m.IdMembership == membershipId);

                if (membership == null)
                    return false;

                return membership.ExpirationDate >= DateOnly.FromDateTime(DateTime.Now);
            }
        }



        public async Task<List<MembershipPayment>> GetUnpaidPaymentsAsync(int memberId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Membershippayments
                    .Include(mp => mp.MembershipIdMembershipNavigation)
                        .ThenInclude(m => m.MembershipTypeIdTypeNavigation)
                    .Where(mp => mp.MembershipIdMembershipNavigation.MemberUserId == memberId && !mp.IsPaid)
                    .OrderBy(mp => mp.BillingDate)
                    .ToListAsync();
            }
        }

        public async Task<List<MembershipPayment>> GetPaymentHistoryAsync(int memberId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Membershippayments
                    .Include(mp => mp.MembershipIdMembershipNavigation)
                        .ThenInclude(m => m.MembershipTypeIdTypeNavigation)
                    .Where(mp => mp.MembershipIdMembershipNavigation.MemberUserId == memberId)
                    .OrderByDescending(mp => mp.BillingDate)
                    .ToListAsync();
            }
        }
        public async Task<bool> ProcessPaymentAsync(int paymentId, string paymentMethod, int? promotionId = null)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var payment = await context.Membershippayments
                        .Include(mp => mp.MembershipIdMembershipNavigation)
                        .FirstOrDefaultAsync(mp => mp.IdPayment == paymentId);

                    if (payment == null || payment.IsPaid)
                        return false;

                    decimal finalPrice = payment.Price;

                    if (promotionId.HasValue)
                    {
                        var promotion = await context.Promotions
                            .FirstOrDefaultAsync(p => p.IdPromotion == promotionId.Value);

                        if (promotion != null && IsPromotionValid(promotion))
                        {
                            finalPrice = payment.Price - (payment.Price * promotion.Discount / 100);
                        }
                    }

                    payment.PaymentMethod = paymentMethod;
                    payment.Price = finalPrice;
                    payment.IsPaid = true;

                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> CreateTestPromotionsAsync(int memberId)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Starting for member ID: {memberId}");

                    var member = await context.Members
                        .Include(m => m.Memberships)
                        .FirstOrDefaultAsync(m => m.UserId == memberId);

                    if (member == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Member not found for ID: {memberId}");
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Found member with {member.Memberships?.Count ?? 0} memberships");

                    var activeMemberships = member.Memberships
                        .Where(m => m.ExpirationDate >= DateOnly.FromDateTime(DateTime.Now))
                        .ToList();

                    System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Found {activeMemberships.Count} active memberships");

                    if (!activeMemberships.Any())
                    {
                        System.Diagnostics.Debug.WriteLine("CreateTestPromotions - No active memberships found");
                        return false;
                    }

                    var random = new Random();
                    int promotionsCreated = 0;

                    foreach (var membership in activeMemberships)
                    {
                        System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Processing membership ID: {membership.IdMembership}");

                        if (true)
                        {
                            var existingPromotion = await context.Promotions
                                .AnyAsync(p => p.MemberUserId == memberId &&
                                              p.MembershipIdMembership == membership.IdMembership &&
                                              p.ValidUntil >= DateTime.Now);

                            System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Existing promotion for membership {membership.IdMembership}: {existingPromotion}");

                            if (!existingPromotion)
                            {
                                var discountTypes = new[] { 5, 10, 15, 20, 25 };
                                var descriptions = new[]
                                {
                                    "promo_seasonal",
                                    "promo_loyalty",
                                    "promo_offer",
                                    "promo_monthly",
                                    "promo_newyear"
                        };

                                var discount = discountTypes[random.Next(discountTypes.Length)];
                                var descriptionKey = descriptions[random.Next(descriptions.Length)];
                                var description = App.Current.Resources[descriptionKey] as string;
                                var validDays = random.Next(7, 31);

                                System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Creating promotion: {description} - {discount}%, valid for {validDays} days");

                                var success = await CreatePromotionAsync(
                                    memberId,
                                    membership.IdMembership,
                                    $"{description} - {discount}%",
                                    DateTime.Now,
                                    DateTime.Now.AddDays(validDays),
                                    discount
                                );

                                if (success)
                                {
                                    promotionsCreated++;
                                    System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Successfully created promotion #{promotionsCreated}");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("CreateTestPromotions - Failed to create promotion");
                                }
                            }
                        }
                    }

                    System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Total promotions created: {promotionsCreated}");
                    return promotionsCreated > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"CreateTestPromotions - Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<List<Promotion>> GetAvailablePromotionsAsync(int memberId, int membershipId)
        {
            using (var context = DBUtil.getContext())
            {
                var currentDate = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"GetAvailablePromotions - Member: {memberId}, Membership: {membershipId}, Current date: {currentDate}");

                var allPromotionsForMember = await context.Promotions
                    .Where(p => p.MemberUserId == memberId)
                    .ToListAsync();

                System.Diagnostics.Debug.WriteLine($"GetAvailablePromotions - Total promotions for member: {allPromotionsForMember.Count}");

                foreach (var promo in allPromotionsForMember)
                {
                    System.Diagnostics.Debug.WriteLine($"  Promotion ID: {promo.IdPromotion}, MembershipId: {promo.MembershipIdMembership}, Valid from: {promo.ValidFrom}, Valid until: {promo.ValidUntil}, Description: {promo.Description}");
                }

                var result = await context.Promotions
                    .Where(p => p.MemberUserId == memberId &&
                               p.MembershipIdMembership == membershipId &&
                               p.ValidFrom <= currentDate &&
                               p.ValidUntil >= currentDate)
                    .ToListAsync();

                System.Diagnostics.Debug.WriteLine($"GetAvailablePromotions - Valid promotions found: {result.Count}");

                return result;
            }
        }
        public async Task<bool> CreatePromotionAsync(int memberId, int membershipId, string description,
            DateTime validFrom, DateTime validUntil, decimal discount)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var promotion = new Promotion
                    {
                        MemberUserId = memberId,
                        MembershipIdMembership = membershipId,
                        Description = description,
                        ValidFrom = validFrom,
                        ValidUntil = validUntil,
                        Discount = discount
                    };

                    context.Promotions.Add(promotion);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool IsPromotionValid(Promotion promotion)
        {
            var currentDate = DateTime.Now;
            return promotion.ValidFrom <= currentDate && promotion.ValidUntil >= currentDate;
        }

        public async Task<decimal> CalculatePriceWithPromotionAsync(int paymentId, int? promotionId = null)
        {
            using (var context = DBUtil.getContext())
            {
                var payment = await context.Membershippayments
                    .FirstOrDefaultAsync(mp => mp.IdPayment == paymentId);

                if (payment == null)
                    return 0;

                decimal finalPrice = payment.Price;

                if (promotionId.HasValue)
                {
                    var promotion = await context.Promotions
                        .FirstOrDefaultAsync(p => p.IdPromotion == promotionId.Value);

                    if (promotion != null && IsPromotionValid(promotion))
                    {
                        finalPrice = payment.Price - (payment.Price * promotion.Discount / 100);
                    }
                }

                return finalPrice;
            }
        }

        public async Task<decimal> GetTotalDebtAsync(int memberId)
        {
            using (var context = DBUtil.getContext())
            {
                return await context.Membershippayments
                    .Where(mp => mp.MembershipIdMembershipNavigation.MemberUserId == memberId && !mp.IsPaid)
                    .SumAsync(mp => mp.Price);
            }
        }

        public async Task<bool> CreateLoyaltyPromotionAsync(int memberId)
        {
            try
            {
                using (var context = DBUtil.getContext())
                {
                    var member = await context.Members
                        .Include(m => m.Memberships)
                        .FirstOrDefaultAsync(m => m.UserId == memberId);

                    if (member == null)
                        return false;

                    var enrollmentDate = member.DateOfEnrollment;
                    var oneYearAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-1));

                    if (enrollmentDate <= oneYearAgo)
                    {
                        var activeMembership = member.Memberships
                            .FirstOrDefault(m => m.ExpirationDate >= DateOnly.FromDateTime(DateTime.Now));

                        if (activeMembership != null)
                        {
                            var existingPromotion = await context.Promotions
                                .AnyAsync(p => p.MemberUserId == memberId &&
                                              p.ValidFrom.Month == DateTime.Now.Month &&
                                              p.ValidFrom.Year == DateTime.Now.Year);

                            if (!existingPromotion)
                            {
                                await CreatePromotionAsync(
                                    memberId,
                                    activeMembership.IdMembership,
                                    "Loyalty discount for long-term members",
                                    DateTime.Now,
                                    DateTime.Now.AddDays(30),
                                    10 // 10% popust
                                );
                                return true;
                            }
                        }
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}