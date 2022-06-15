﻿using Microsoft.AspNetCore.Mvc;
using TestApi.Adapter;
using TestApi.Data;
using TestApi.Entity;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Okpd2Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Okpd2>>> Get([FromQuery]int? top)
        {
            return Ok(AdapterContainer.Okpd2Adapter.GetAllOkpd2s(top));
        }

        [HttpGet("Update")]
        public async Task<ActionResult> Update()
        {
            using (SearchAndRangeContext dbContext = new())
            {
                dbContext.Okpd2s.RemoveRange(dbContext.Okpd2s);

                await dbContext.SaveChangesAsync(true);

                await AdapterContainer.Okpd2Adapter.AddToDb();

                await dbContext.DisposeAsync();
            }

            return Ok();
        }
    }
}
