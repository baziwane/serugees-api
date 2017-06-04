using System;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Entities;

namespace Serugees.Api
{
    public static class SerugeesExtensions
    {
        public static void EnsureSeedDataForContext (this SerugeesContext context)
        {
            if(context.Members.Any())
            {
                return;
            }

            // init seed data
            var members = new List<Member>(){
                new Member(){
                    FirstName = "David",
                    LastName = "Batanda",
                    TelephoneNumber = "123456",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Amount = 1000000,
                            Duration = 2,
                            DateRequested = DateTime.Now,
                            IsActive = true,
                            Payments = new List<Payment>(){
                                new Payment(){ AmoutPaid=600000, DateDeposited=DateTime.Now, OutstandingBalance = 400000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 400000 },
                                new Payment(){ AmoutPaid=400000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 }
                            }
                        },
                        new Loan(){
                            Amount = 25000000,
                            Duration = 3,
                            DateRequested = DateTime.Today,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ AmoutPaid=1000000, DateDeposited=DateTime.Now, OutstandingBalance = 1500000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 500000 },
                                new Payment(){ AmoutPaid=500000, DateDeposited=DateTime.Now, OutstandingBalance = 1000000, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 1000000 },
                                new Payment(){ AmoutPaid=1000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 }
                            }
                        },
                        new Loan(){
                            Amount = 40000000,
                            Duration = 1,
                            DateRequested = DateTime.Today,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ AmoutPaid=4000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    FirstName = "Kenneth",
                    LastName = "Babigumira",
                    TelephoneNumber = "12933911",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Amount = 3000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ AmoutPaid=3000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    FirstName = "Raymond",
                    LastName = "Matovu",
                    TelephoneNumber = "737373",
                    DateRegistered = "2017-12-12",
                    Active = false,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Amount = 3000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = false,
                            Payments = new List<Payment>(){
                                new Payment(){ AmoutPaid=3000000, DateDeposited=DateTime.Now, OutstandingBalance = 0, NextInstallmentDueDate= DateTime.Today, MinimumPaymentDueAtNextInstallment = 0 },
                            }
                        }
                    }
                },
                new Member(){
                    FirstName = "Richard",
                    LastName = "Mulema",
                    TelephoneNumber = "6383392",
                    DateRegistered = "2017-12-12",
                    Active = true,
                    Loans = new List<Loan>(){
                        new Loan(){
                            Amount = 5000000,
                            Duration = 3,
                            DateRequested = DateTime.Now,
                            IsActive = true
                        }
                    }
                }              
            };

            context.Members.AddRange(members);
            context.SaveChanges();
        }
    }
}