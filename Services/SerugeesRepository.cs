
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Serugees.Api.Entities;

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
            var loanAssoc = _context.Loans.Include(l => l.Payments)
                .Where(l => l.Id == loanId && l.MemberId == memberId).FirstOrDefault();

            return loanAssoc.Payments
                .Where(p => p.Id == paymentId && p.LoanId == loanId).FirstOrDefault();
        }
        public IEnumerable<Payment> GetAllPaymentsForLoanForMember(int memberId, int loanId)
        {
            var loanAssoc = _context.Loans.Include(l => l.Payments)
                .Where(l => l.Id == loanId && l.MemberId == memberId).FirstOrDefault();

            return loanAssoc.Payments.ToList();
        }
        public void AddLoanForMember(int memberId, Loan loan)
        {
            var member = GetMember(memberId, false);
            member.Loans.Add(loan);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool AddMember(Member member)
        {
            _context.Members.Add(member);
            return Save();
        }
        public void AddLoanPaymentForMember(int memberId, int loanId, Payment payment)
        {
            var loan = GetLoanForMember(memberId, loanId, false);
            loan.Payments.Add(payment);
        }
    }
}