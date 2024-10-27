using Application.Exceptions;
using Domain.Models.ViewModels;

namespace Application.Models
{
    internal static class ValidationExtensions
    {
        internal static void Validate(this string arg)
        {
            if (arg == null || !arg.Any(char.IsLetter))
                throw new EmptyArgumentException();
        }
        internal static void Validate(this Guid arg)
        {
            if (arg == Guid.Empty)
                throw new EmptyArgumentException();
        }
        internal static void Validate(this OrderInputModel arg)
        {
            if (arg == null)
                throw new EmptyArgumentException();
            arg.District.Validate();
            if (arg.Weight <= 0)
                throw new ArgumentValueException();
            if (arg.DeliveryDateTime == DateTime.MinValue)
                throw new EmptyArgumentException();
        }
        internal static void Validate(this OrdersFilters arg)
        {
            if (arg == null)
                throw new EmptyArgumentException();
            if (arg.District != null && !arg.District.Any(char.IsLetter))
                throw new EmptyArgumentException();
            if (arg.MinimalDateTime == DateTime.MinValue || arg.MaximalDateTime == DateTime.MinValue)
                throw new EmptyArgumentException();
            if (arg.PageSize < 1)
                throw new ArgumentValueException();
        }
    }
}
