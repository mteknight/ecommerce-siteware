using System.Diagnostics;

using Dawn;

using JetBrains.Annotations;

namespace Ecommerce.Common.Extensions
{
    [UsedImplicitly]
    public static class GuardExtensions
    {
        /// <summary>Requires the argument not to be <c>null</c>.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="message">
        ///     The factory to initialize the message of the exception that will be thrown if the
        ///     precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument" />.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="argument" /> value is <c>null</c> and the argument is not modified
        ///     since it is initialized.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="argument" /> value is <c>null</c> and the argument is modified after
        ///     its initialization.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        public static ref readonly Guard.ArgumentInfo<T> NotDefault<T>(
            in this Guard.ArgumentInfo<T> argument,
            string? message = null)
        {
            if (typeof(T).IsValueType)
            {
                NotDefaultInternal(argument, message);
            }
            else
            {
                NotNull(argument, message);
            }

            return ref argument;
        }

        [DebuggerStepThrough]
        private static void NotDefaultInternal<T>(
            in Guard.ArgumentInfo<T> argument,
            string? message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(argument.Value, default!))
            {
                return;
            }

            message ??= $"{argument.Name} cannot be {default(T)}.";
            var exception = new ArgumentException(message, argument.Name);

            throw Guard.Fail(exception);
        }

        [DebuggerStepThrough]
        private static void NotNull<T>(
            in Guard.ArgumentInfo<T> argument,
            string? message = null)
        {
            if (argument.HasValue())
            {
                return;
            }

            message ??= $"{argument.Name} cannot be null.";
            var exception = !argument.Modified
                ? new ArgumentNullException(argument.Name, message)
                : (Exception)new ArgumentException(message, argument.Name);

            throw Guard.Fail(exception);
        }
    }
}
