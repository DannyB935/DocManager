using System.Linq.Expressions;
using api_docmanager.Dtos.Unit;
using api_docmanager.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_docmanager.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class UnitsController: ControllerBase
    {
        private readonly DocManagerContext _context;
        private readonly IMapper _mapper;

        public UnitsController(DocManagerContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetUnits()
        {
            try
            {
                var units = await this._context.Units.Where(ent => ent.Deleted == false).ToListAsync();
                if (units.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "There are no units",
                        status = 404
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Units retrieved successfully.",
                    units = this._mapper.Map<List<UnitDto>>(units)
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    status = 500
                });
            }
        }

        [HttpGet("{id:int}", Name = "GetUnitById")]
        public async Task<ActionResult> GetUnitById(int id)
        {
            try
            {
                var unit = await this._context.Units.FirstOrDefaultAsync(ent => ent.Id == id && ent.Deleted == false);
                if (unit == null)
                {
                    return NotFound(new{
                        message = $"The unit with the ID: {id} was not found.",
                        status = 404
                    });
                }

                return Ok(new
                {
                    message = "Unit retrieved successfully.",
                    status = 200,
                    unit = this._mapper.Map<UnitDto>(unit)
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, new
                { 
                    message = e.Message,
                    status = 500
                });
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> PostUnit(CreateUnitDto unit)
        {
            try
            {
                var tempUnit = this._mapper.Map<Unit>(unit);
                this._context.Units.Add(tempUnit);
                await this._context.SaveChangesAsync();

                return Created("New unit created" ,new
                {
                    status = 201,
                    message="New unit created successfully.",
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    status = 500
                });
            }
        }

        [HttpPut("{id:int}", Name = "Update Unit")]
        public async Task<ActionResult> PutUnit(int id, CreateUnitDto newUnit)
        {
            try
            {
                var tmpUnit = await this._context.Units.FirstOrDefaultAsync(ent => ent.Id == id && ent.Deleted == false);
                if (tmpUnit == null)
                {
                    return NotFound(new
                    {
                        message = $"The unit with the ID: {id} was not found.",
                        status = 404
                    });
                }
                
                var updatedUnit = this._mapper.Map(newUnit, tmpUnit);
                await this._context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    status = 500
                });
            }
        }

        [HttpDelete("{id:int}", Name = "Delete Unit")]
        public async Task<ActionResult> DeleteUnit(int id)
        {
            try
            {
                var tmpUnit = await this._context.Units.FirstOrDefaultAsync(ent => ent.Id == id && ent.Deleted == false);
                if (tmpUnit == null)
                {
                    return NotFound(new
                    {
                        message = $"The unit with the ID: {id} was not found.",
                        status = 404
                    });
                }

                tmpUnit.Deleted = true;
                await this._context.SaveChangesAsync();
                
                return NoContent();
                
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    status = 500
                });
            }
        }
        
    }
}

