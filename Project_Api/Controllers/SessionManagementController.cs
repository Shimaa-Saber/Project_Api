using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DTO;
using Project_Api.DTOs;
using Project_Api.Interfaces;
using ProjectApi.Models;
using System.Diagnostics.Eventing.Reader;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionManagementController : ControllerBase
    {

        ISessions _sessions;
        IVideoSession _videoSession;
        IAudioSession _audioSession;
        public SessionManagementController(ISessions sessions, IVideoSession videoSession, IAudioSession audioSession)
        {
            _sessions = sessions;
            _videoSession=videoSession; 
            _audioSession=audioSession;
        }

        [HttpGet("api/SessionManagement/{id}/details")]
        public ActionResult<GeneralResponse> GetById(int id)
        {
            SessionDTO? sessionDTO =
            _sessions.Get(s => s.Id == id)
           .Select(s => new SessionDTO
           {
               Id= s.Id,
               Notes = s.Notes,
               Status = s.Status.ToString(),
               Type = s.Type.ToString(),
           }).FirstOrDefault();

            if (sessionDTO != null)
            {
                return new GeneralResponse
                {
                    IsPass = true,
                    Data  = sessionDTO,
                };
            }
            else
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data  = "There is No Session With This Id",
                };
            }
        }


        [HttpPost("{id}/start")]
        public  ActionResult<GeneralResponse> StartSession(int id)
        {
            var session = _sessions.GetById(id);

            if (session == null)
            {
                return new GeneralResponse { IsPass= false, Data = "Session Not Found" };
            }

            if (session.Status == SessionStatus.Cancelled )
            {
                return new GeneralResponse { IsPass= false, Data = "Cannot start a cancelled session" };
            }
            if(session.Status == SessionStatus.Completed)
            {
                return new GeneralResponse { IsPass= false, Data = "Cannot start a completed session" };
            }
            if (session.Status == SessionStatus.Pending)
            {
                return new GeneralResponse { IsPass= false, Data = "Session must be confirmed before starting" };
            }

            switch (session.Type)
            {
                case SessionType.Video:
                    var videoSessionDetail = new VideoSessionDetail
                    {
                        SessionId = session.Id,
                        MeetingUrl = "https://example.com/video/" + Guid.NewGuid(),
                        Platform = "Zoom", 
                        Resolution = "1080p",
                        WasRecorded = false
                    };
                    _videoSession.insert(videoSessionDetail);
                    _videoSession.Save();
                    break;

                case SessionType.Audio:
                    var audioSessionDetail = new AudioSessionDetail
                    {
                        SessionId = session.Id,
                        CallUrl = "https://example.com/audio/" + Guid.NewGuid(),
                        Platform = "Skype",
                        Bitrate = 256,
                        SampleRate = 44100
                    };
                    _audioSession.insert(audioSessionDetail);
                    _audioSession.Save();
                    break;

                default:
                    return new GeneralResponse { IsPass= false, Data = "Invalid session type" };
            }

            return new GeneralResponse { IsPass= false, Data = "Session started successfully" };
        }


        [HttpPost("{id}/end")]
        public ActionResult<GeneralResponse> EndSession(int id)
        {
            var session = _sessions.GetById(id);

            if (session == null)
            {
                return new GeneralResponse { IsPass = false, Data = "Session not found" };
            }

            session.Status = SessionStatus.Completed;
            _sessions.Update(session);
            _sessions.Save();
            return new GeneralResponse { IsPass = true, Data = new { SessionId = id, NewStatus = session.Status } };
        }

        [HttpPost("{id}/notes")]
        public ActionResult<GeneralResponse> AddSessionNotes(int id, [FromBody] string notes)
        {
            var session = _sessions.GetById(id);

            if (session == null)
            {
                return new GeneralResponse { IsPass = false, Data ="Session not found." };
            }

            if (string.IsNullOrWhiteSpace(notes))
            {
                return new GeneralResponse { IsPass = false, Data ="Notes cannot be empty." };
            }

            session.Notes = notes;

            _sessions.Update(session);
            _sessions.Save();

            return new GeneralResponse { IsPass = true, Data =new { SessionId = id, Notes = notes } };
        }


    }
}
