﻿using Microsoft.AspNetCore.Mvc;
using QuestionTwo.Model;
using QuestionTwo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionTwo.Controllers
{
    [Route("card-scheme/verify")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardService _service;
        private IAuthService _authService;
        public CardController(ICardService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [Route("{cardPan}")]
        [HttpPost]
        public async Task<IActionResult> VerifyCard([FromRoute]string cardPan, [FromBody] CardDTO d)
        {
            var authService = _authService.Validate(Request.Headers);

            if (!authService.Item1)
                return BadRequest(new { success = false, message = authService.Item2 });


            var result = _service.VerifyCard(cardPan, d);

            if(result != null && result.success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("stats")]
        [HttpGet]
        public async Task<IActionResult> GetHitCount([FromQuery] int start, [FromQuery] int limit)
        {
            var authService = _authService.Validate(Request.Headers);

            if (!authService.Item1)
                return BadRequest(new { success = false, message = authService.Item2 });
            //Request.Headers.
            var result = _service.GetCardHitCount(start, limit);

            return Ok(result);
        }
    }
}
