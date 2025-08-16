using api_docmanager.Dtos.Assignments;
using api_docmanager.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_docmanager.Controllers;

[ApiController]
[Route("api/assignments")]
public class AssignmentsController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DocManagerContext _context;
    
    public AssignmentsController(DocManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    
    [HttpPost(Name = "AssignDocument")]
    public async Task<ActionResult> AssignDocument(CreateAssignDto newAssign)
    {
        try
        {
            var assignment = _mapper.Map<AssignmentLog>(newAssign);
            
            var tempDoc = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.Id == newAssign.DocId && doc.Concluded == false)
                .FirstOrDefaultAsync();

            tempDoc.UsrAssign = newAssign.UsrAssign;

            if (tempDoc == null)
            {
                return NotFound(new
                {
                    message = $"The document with the id {newAssign.DocId} was not found or has been concluded"
                });
            }
            
            _context.AssignmentLogs.Add(assignment);
            
            await _context.SaveChangesAsync();
            
            return Created("Log registered", new
            {
                message = "Assignment log registered successfully",
                status = 200
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

    [HttpGet("{id_doc:int}")]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments(int id_doc)
    {
        try
        {
            var assignLogs = await _context.AssignmentLogs
                .Where(log => log.DocId == id_doc)
                .Include(log => log.UsrAssignNavigation)
                .OrderByDescending(assign => assign.DateAssign)
                .ToListAsync();

            return Ok(new
            {
                message = "Assignment logs retrieved successfully",
                status = 200,
                logs = _mapper.Map<IEnumerable<AssignmentDto>>(assignLogs)
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
}