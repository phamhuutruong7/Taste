﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Taste.Utility
{
    public static class SD //SD = Static Details
    {
        public const string ManagerRole = "Manager";
        public const string FrontDeskRole = "FrontDesk";
        public const string KitchenRole = "Kitchen";
        public const string CustomerRole = "Customer";
        public const string ShoppingCart = "ShoppingCart";

        public const string StatusSubmitted = "Submitted";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymenStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";
    }
}
