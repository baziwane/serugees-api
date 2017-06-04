using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Entities;

namespace Serugees.Api.Services
{
    public interface ISerugeesRepository
    {
         bool MemberExists(int memberId); 
         IEnumerable<Member> GetAllMembers();
         Member GetMember(int memberId, bool includeLoans);
         IEnumerable<Loan> GetAllLoansForMember(int memberId);
         Loan GetLoanForMember(int memberId, int loanId, bool includePayments);
         IEnumerable<Payment> GetAllPaymentsForLoanForMember(int memberId, int loanId);
         Payment GetPaymentForLoanForMember(int memberId, int loanId, int paymentId);
    }
}