﻿namespace Application.Models
{
    public class LoggerMessages
    {
        public const string SUCCESS = "Operation successfully completed";
        public const string GET_ORDER_MESSAGE = "Trying to get order with id: ";
        public const string GET_ORDERS_MESSAGE = "Trying to get orders with filter: ";
        public const string GET_DISTRICT_ORDERS_COUNT = "Trying to get orders count in district: ";
        public const string CREATE_ORDER = "Trying to create order: ";
        public const string UPDATE_ORDER = "Trying to update order: ";
        public const string DELETE_ORDER = "Trying to delete order with id: ";
        public const string SERVICE_EXCEPTION = "Service exception was thrown with message: ";
        public const string UNKNOWN_EXCEPTION = "Unknown exception was thrown with message: ";
    }
}
