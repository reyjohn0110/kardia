using Login.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Login.Services
{
    [Headers("Content-Type: application/json", "Accept: application/json")]
    public interface IApi
    {
        [Post("/api/login")]
        Task<SendOtpResponse> LoginUser([Body(BodySerializationMethod.Serialized)] LoginRequest loginRequest);

        [Post("/api/otp/send")]
        Task<SendOtpResponse> SendOtp([Body(BodySerializationMethod.Serialized)] SendOtpRequest sendOtpRequest);

        [Post("/api/otp/verification")]
        Task<VerificationResponse> VerificationOtp([Body(BodySerializationMethod.Serialized)] VerificationRequest verificationRequest);

        [Post("/api/register")]
        Task<MainResponse> Register([Body(BodySerializationMethod.Serialized)] RegisterRequest registerRequest);

        [Get("/api/doctors")]
        Task<DoctorResponse> GetDoctors(SearchDoctorRequest searchDoctorRequest);
    }
}
