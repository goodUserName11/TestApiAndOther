using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

                if (user == null)
                    return NotFound();

                coefficientValues =
                    context.CoefficientValues
                    .Include(c => c.Coefficient)
                    .Where(c => c.CompanyInn == user.CompanyInn)
                    .ToList();


                await context.DisposeAsync();
            }


            foreach (var coefficientValue in coefficientValues)
            {
                coefficientValuesModel.Add(new GetCoefficientValuesModel(coefficientValue));
            }

            coefficientValuesModel = coefficientValuesModel.OrderBy(c => c.Name).ToList();

            Log.Logger.Information($"Получен список списков поставщиков {user.Email}");



            return Ok(coefficientValuesModel);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> SaveValues(string userId,
            [FromBody] SetCoefficientValueModel coefficientValuesModel)
        {
            User? user = null;
            CoefficientValue coefficientValue;

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.Find(Guid.Parse(userId));

                if (user == null)
                    return NotFound();

                for (int i = 0; i < coefficientValuesModel.Id.Count; i++)
                {
                    coefficientValue = context.CoefficientValues
                        .FirstOrDefault(c => c.Id == Guid.Parse(coefficientValuesModel.Id[i]));

                    if (coefficientValue == null)
                        return NotFound();

                    coefficientValue.Value = coefficientValuesModel.Value[i];
                    coefficientValue.IsActive = coefficientValuesModel.IsActive[i];
                }

                await context.SaveChangesAsync();

                await context.DisposeAsync();
            }
            return Ok();
        }
    }
}
