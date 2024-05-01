using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "Invalid credentials.");

        public static Error EmailNotUsing => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "There is no user with that email address.");

        public static Error WrongPassword => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "Entered password is wrong.");

        public static Error VerificationCodeInvalid => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "Verification code is invalid.");
    }
}