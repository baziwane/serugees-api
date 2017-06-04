
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Serugees.Api.Models;

namespace Serugees.Api.Services
{
    public class SerugeesRepository : ISerugeesRepository
    {
        private SerugeesContext _context;
        public SerugeesRepository(SerugeesContext context)
        {
            _context = context;
        }
        public bool MemberExists(int memberId)
        {
            return _context.Members.Any(m => m.Id == memberId);
        }

        public Loan GetLoanForMember(int memberId, int loanId, bool includePayments)
        {
            if(includePayments)
            {
                return _context.Loans.Include(l => l.Payments)
                        .Where(l => l.MemberId == memberId && l.Id == loanId).FirstOrDefault();
            }
            return _context.Loans
                .Where(l => l.MemberId == memberId && l.Id == loanId).FirstOrDefault();
        }

        public IEnumerable<Loan> GetAllLoansForMember(int memberId)
        {
            return _context.Loans
                .Where(l => l.MemberId == memberId)
                .OrderBy(l=> l.DateRequested).ToList();
        }

        public Member GetMember(int memberId, bool includeLoans)
        {
            if(includeLoans)
            {
                return _context.Members.Include(m => m.Loans)
                        .Where(m => m.Id == memberId).FirstOrDefault();
            }
            return _context.Members.Where(m => m.Id == memberId).FirstOrDefault();
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.OrderBy(m=> m.FirstName).ToList();
        }

        public Payment GetPaymentForLoanForMember(int memberId, int loanId, int paymentId)
        {
            return _context.Loans
                .Where(l => l.MemberId == memberId).FirstOrDefault()
                .Payments
                .Where(p => p.LoanId == loanId && p.Id == paymentId).FirstOrDefault();
        }

        public IEnumerable<Payment> GetAllPaymentsForLoanForMember(int memberId, int loanId)
        {
            return _context.Loans
                .Where(l => l.MemberId == memberId).FirstOrDefault()
                .Payments
                .Where(p => p.LoanId == loanId).ToList();
        }
    }
}