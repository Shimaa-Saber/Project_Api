using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO;
using Project_Api.DTOs;
using Project_Api.Interfaces;
using Project_Api.Models;
using ProjectApi.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapistsController : ControllerBase
    {
        ITherabistProfile _therapistProfile;
        ITherapistReviews _therapistReviews;
        ISlots _slots;
        public TherapistsController(ITherabistProfile therapistProfile ,ITherapistReviews therapistReviews, ISlots slots )
        {
            _therapistProfile = therapistProfile;
            _therapistReviews = therapistReviews;
            _slots = slots;
        }


        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<TherapistProfile> therapists =  _therapistProfile.GetAll();
            return new GeneralResponse
            {
                IsPass = true,
                Data  = therapists,
            };
        }


        [HttpGet("api/therapists/{specialization}/Filteration")]
        public ActionResult<GeneralResponse> Filteration(string specialization)
        {
            List<TherapistDTO> therapists =
            _therapistProfile.Get(t => t.TherapistSpecializations
            .Any(s => s.Specialization.Name==specialization))
           .Select(t => new TherapistDTO
           {
               Id= t.Id,
               Name=t.User.FullName,
               Bio=t.Bio,
               PricePerSession=t.PricePerSession,
               Specialization=t.TherapistSpecializations.Select(s => s.Specialization.Name).ToArray(),
               Timezone=t.Timezone,
               YearsOfExperience=t.YearsOfExperience,
               LicenseCertificatePath=t.LicenseCertificatePath,
               LicenseNumber=t.LicenseNumber,
               ProfilePictureUrl=t.ProfilePictureUrl,
               NumberOfSessions = t.Sessions.Count()
           }).ToList();

            if (therapists.Count != 0)
            {
                return new GeneralResponse
                {
                    IsPass = true,
                    Data  = therapists,
                };
            }
            else
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data  = "There is No Therapist With This Specializations",
                };
            }
        }
      
        
        [HttpGet("id")]
        public ActionResult<GeneralResponse> GetById(string id)
        {
            TherapistDTO? therapistDto =
            _therapistProfile.Get(t => t.TherapistSpecializations
            .Any(t => t.TherapistId == id))
           .Select(t => new TherapistDTO
           {
               Id= t.Id,
               Name=t.User.FullName,
               Bio=t.Bio,
               PricePerSession=t.PricePerSession,
               Specialization=t.TherapistSpecializations.Select(s => s.Specialization.Name).ToArray(),
               Timezone=t.Timezone,
               YearsOfExperience=t.YearsOfExperience,
               LicenseCertificatePath=t.LicenseCertificatePath,
               LicenseNumber=t.LicenseNumber,
               ProfilePictureUrl=t.ProfilePictureUrl,
               NumberOfSessions = t.Sessions.Count()
           }).FirstOrDefault();

            if (therapistDto != null) {
                return new GeneralResponse
                {
                    IsPass = true,
                    Data  = therapistDto,
                };
            }
            else
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data  = "There is No Therapist With This Id",
                };
            }
        }



        [HttpGet("api/therapists/{id}/GetTherapistReviews")]
        public ActionResult<GeneralResponse> GetTherapistReviews(string id)
        {

            TherapistReviewsDTO? therapistReviewsDTO =
            _therapistReviews.Get(t => t.TherapistId == id).Select(t => new TherapistReviewsDTO
            {
               Id = t.Id,
               Content = t.Content,
               Title = t.Title,
               Rating = t.Rating,
           }).FirstOrDefault();

            if (therapistReviewsDTO != null)
            {
                return new GeneralResponse
                {
                    IsPass = true,
                    Data  = therapistReviewsDTO,
                };
            }
            else
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data  = "There is No Reviews for This Therapist",
                };
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("api/therapists/{id}/VerifyTherapist")]
        public ActionResult<GeneralResponse> VerifyTherapist(string id)
        {
            TherapistProfile therapist = _therapistProfile.GetById(id);
            if (therapist == null)
            {
                return new GeneralResponse { IsPass = false, Data = "Therapist not found" };
            }

            therapist.User.IsVerified = true;
            _therapistProfile.Update(therapist);
            _therapistProfile.Save();
            return new GeneralResponse { IsPass = false, Data = "Therapist verified successfully" };
        }





        [Authorize(Roles = "Therapist")]
        [HttpPut("api/therapists/UpdateAvailabilitySlot/{id}")]
        public ActionResult<GeneralResponse> UpdateAvailabilitySlot(int id, [FromBody] AvailabilitySlotDto updatedSlot)
        {

            if (ModelState.IsValid == true)
            {
                var slot = _slots.GetById(id);

                if (slot == null)
                {
                    return new GeneralResponse { IsPass = false, Data = "Slot not found" };
                }
                slot.Date = updatedSlot.Date;
                slot.StartTime = updatedSlot.StartTime;
                slot.EndTime = updatedSlot.EndTime;
                slot.SlotType = updatedSlot.SlotType;
                slot.DayOfWeek = updatedSlot.DayOfWeek;
                _slots.Save();
                return new GeneralResponse { IsPass = false, Data = "Slot updated successfully" };
            }
            else
            {
                return new GeneralResponse { IsPass = false, Data = "Data Is not Valid" };
            }
            
        }

    }
}
