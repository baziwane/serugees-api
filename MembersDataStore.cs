using System;
using System.Collections.Generic;
using Serugees.Api.Models;

namespace Serugees.Api
{
    public class MembersDataStore
    {
        public static MembersDataStore Current { get; } = new MembersDataStore();
        public List<Member> Members { get; set; }
        public MembersDataStore()
        {
            Members = new List<Member>(){
                new Member(){
                    Id=1,
                    FirstName = "David",
                    LastName = "Batanda",
                    TelephoneNumber = "123456",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Id = 1,
                            Amount = 1000000,
                            Duration = 2,
                            DateRequested = DateTime.Now,
                            IsActive = true,
                            Payments = new List<Payment>(){
                                new Payment(){ Id=1, AmoutPaid=600000, DateDeposited=DateTime.Now, OutstandingBalance = 400000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 400000 },
                                new Payment(){ Id=2, AmoutPaid=400000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 }
                            }
                        },
                        new Loan(){
                            Id = 2,
                            Amount = 25000000,
                            Duration = 3,
                            DateRequested = DateTime.Today,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ Id=1, AmoutPaid=1000000, DateDeposited=DateTime.Now, OutstandingBalance = 1500000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 500000 },
                                new Payment(){ Id=2, AmoutPaid=500000, DateDeposited=DateTime.Now, OutstandingBalance = 1000000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 1000000 },
                                new Payment(){ Id=3, AmoutPaid=1000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 }
                            }
                        },
                        new Loan(){
                            Id = 3,
                            Amount = 40000000,
                            Duration = 1,
                            DateRequested = DateTime.Today,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ Id=1, AmoutPaid=4000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    Id=2,
                    FirstName = "Kenneth",
                    LastName = "Babigumira",
                    TelephoneNumber = "12933911",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Id = 1,
                            Amount = 3000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ Id=1, AmoutPaid=3000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    Id=3,
                    FirstName = "Raymond",
                    LastName = "Matovu",
                    TelephoneNumber = "737373",
                    DateRegistered = "2017-12-12",
                    Active = false,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Id = 1,
                            Amount = 3000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ Id=1, AmoutPaid=3000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    Id=4,
                    FirstName = "Richard",
                    LastName = "Mulema",
                    TelephoneNumber = "6383392",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Id = 1,
                            Amount = 5000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = true
                        }
                    }
                }
               
            };
        }
    }
}
