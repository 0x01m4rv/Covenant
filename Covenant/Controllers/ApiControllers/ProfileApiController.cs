﻿// Author: Ryan Cobb (@cobbr_io)
// Project: Covenant (https://github.com/cobbr/Covenant)
// License: GNU GPLv3

using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Covenant.Models;
using Covenant.Models.Listeners;

namespace Covenant.Controllers
{
    [Authorize(Policy = "RequireJwtBearer")]
    [ApiController]
    [Route("api/profiles")]
    public class ProfileApiController : Controller
    {
        private readonly CovenantContext _context;

        public ProfileApiController(CovenantContext context)
        {
            _context = context;
        }

        // GET: api/profiles
        // <summary>
        // Get a list of Profiles
        // </summary>
        [HttpGet(Name = "GetProfiles")]
        public ActionResult<IEnumerable<Profile>> GetProfiles()
        {
            return _context.Profiles.ToList();
        }

        // GET api/profiles/{id}
        // <summary>
        // Get a Profile by id
        // </summary>
        [HttpGet("{id}", Name = "GetProfile")]
        public ActionResult<Profile> GetProfile(int id)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return NotFound($"NotFound - Profile with id: {id}");
            }
            return profile;
        }

        // POST api/profiles
        // <summary>
        // Create a Profile
        // </summary>
        [HttpPost(Name = "CreateProfile")]
        [ProducesResponseType(typeof(Profile), 201)]
        public ActionResult<Profile> CreateProfile([FromBody]Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return CreatedAtRoute(nameof(GetProfile), new { id = profile.Id }, profile);
        }

        // PUT api/profiles
        // <summary>
        // Edit a Profile
        // </summary>
        [HttpPut(Name = "EditProfile")]
        public ActionResult<Profile> EditProfile([FromBody] Profile profile)
        {
            var matching_profile = _context.Profiles.FirstOrDefault(p => profile.Id == p.Id);
            if (matching_profile == null)
            {
                return NotFound($"NotFound - Profile with id: {profile.Id}");
            }
            matching_profile.Id = profile.Id;

            _context.Profiles.Update(matching_profile);
            _context.SaveChanges();

            return matching_profile;
        }

        // DELETE api/profiles/{id}
        // <summary>
        // Delete a Profile
        // </summary>
        [HttpDelete("{id}", Name = "DeleteProfile")]
        [ProducesResponseType(204)]
        public ActionResult DeleteProfile(int id)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return NotFound($"NotFound - Profile with id: {id}");
            }

            _context.Profiles.Remove(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // GET: api/profiles/http
        // <summary>
        // Get a list of HttpProfiles
        // </summary>
        [HttpGet("http", Name = "GetHttpProfiles")]
        public ActionResult<IEnumerable<HttpProfile>> GetHttpProfiles()
        {
            List<HttpProfile> httpProfiles = new List<HttpProfile>();
            foreach (Profile profile in _context.Profiles.ToList())
            {
                httpProfiles.Add((HttpProfile)profile);
            }
            return httpProfiles;
        }

        // GET api/profiles/http/{id}
        // <summary>
        // Get an HttpProfile by id
        // </summary>
        [HttpGet("http/{id}", Name = "GetHttpProfile")]
        public ActionResult<HttpProfile> GetHttpProfile(int id)
        {
            HttpProfile profile = (HttpProfile)_context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return NotFound($"NotFound - HttpProfile with id: {id}");
            }
            return profile;
        }

        // POST api/profiles/http
        // <summary>
        // Create an HttpProfile
        // </summary>
        [HttpPost("http", Name = "CreateHttpProfile")]
        [ProducesResponseType(typeof(HttpProfile), 201)]
        public ActionResult<HttpProfile> CreateHttpProfile([FromBody] HttpProfile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return CreatedAtRoute(nameof(GetHttpProfile), new { id = profile.Id }, profile);
        }

        // PUT api/profiles/http
        // <summary>
        // Edit a Profile
        // </summary>
        [HttpPut("http", Name = "EditHttpProfile")]
        public ActionResult<Profile> EditHttpProfile([FromBody] HttpProfile profile)
        {
            HttpProfile matching_profile = (HttpProfile)_context.Profiles.FirstOrDefault(p => profile.Id == p.Id);
            if (matching_profile == null)
            {
                return NotFound($"NotFound - HttpProfile with id: {profile.Id}");
            }

            matching_profile.HttpRequestHeaders = profile.HttpRequestHeaders;
            matching_profile.HttpUrls = profile.HttpUrls;
            matching_profile.HttpCookies = profile.HttpCookies;
            matching_profile.HttpGetResponse = profile.HttpGetResponse;
            matching_profile.HttpPostRequest = profile.HttpPostRequest;
            matching_profile.HttpPostResponse = profile.HttpPostResponse;

            _context.Profiles.Update(matching_profile);
            _context.SaveChanges();

            return matching_profile;
        }

        // DELETE api/profiles/http/{id}
        // <summary>
        // Delete a HttpProfile
        // </summary>
        [HttpDelete("http/{id}", Name = "DeleteHttpProfile")]
        [ProducesResponseType(204)]
        public ActionResult DeleteHttpProfile(int id)
        {
            HttpProfile profile = (HttpProfile)_context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return NotFound($"NotFound - HttpProfile with id: {id}");
            }

            _context.Profiles.Remove(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
