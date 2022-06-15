using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoefficientController : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<GetCoefficientValuesModel>>> GetById(string userId)
        {
            User? user = null;
            List<CoefficientValue>? coefficientValues = null;
            List<GetCoefficientValuesModel> coefficientValuesModel = new List<GetCoefficientValuesModel>();

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.Find(Guid.Parse(userId));

                coefficientValues =
                    context.CoefficientValues
                    .Include(c => c.Coefficient)
                    .Where(c => c == c)
                    .ToList();

                await context.DisposeAsync();
            }

            if (user == null)
                return NotFound();

            foreach (var coefficientValue in coefficientValues)
            {
                coefficientValuesModel.Add(new GetCoefficientValuesModel(coefficientValue));
            }

            return Ok(coefficientValuesModel);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> SaveValues(string userId,
            [FromBody] List<SetCoefficientValueModel> coefficientValuesModel)
        {
            User? user = null;
            CoefficientValue coefficientValue;

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.Find(Guid.Parse(userId));

                if (user == null)
                    return NotFound();

                foreach (var coefficientValueModel in coefficientValuesModel)
                {



                    coefficientValue = context.CoefficientValues
                        .FirstOrDefault(c => c.Id == Guid.Parse(coefficientValueModel.Id));

                    if (coefficientValue == null)
                        return NotFound();

                    coefficientValue.Value = coefficientValueModel.Value;
                    coefficientValue.IsActive = coefficientValueModel.IsActive;
                }

                await context.SaveChangesAsync();

                await context.DisposeAsync();
            }
            return Ok();
        }
    }
}
