using System.Globalization;
using api_docmanager.Dtos.Documents;
using api_docmanager.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xceed.Words.NET;

namespace api_docmanager.Controllers;

[ApiController]
[Route("api/docs")]
public class DocumentsController: ControllerBase
{
    private readonly DocManagerContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _envHost;

    public DocumentsController(DocManagerContext context, IMapper mapper, IWebHostEnvironment env)
    {
        this._context = context;
        this._mapper = mapper;
        this._envHost = env;
    }

    [HttpPost(Name = "CreateDocument")]
    public async Task<ActionResult> CreateDocument(CreateDocDto newDoc)
    {
        try
        {
            var doc = this._mapper.Map<Document>(newDoc);
            
            //If the user's code is 0 for either the sender or recip, it means it has to be nullable
            if (doc.UsrSender == 0)
            {
                doc.UsrSender = null;
            }

            if (doc.UsrRecip == 0)
            {
                doc.UsrRecip = null;
            }
            
            _context.Documents.Add(doc);
            
            await _context.SaveChangesAsync();

            //We make a select to retrieve the DocNum generated
            var docNum = await _context.Documents
                .Where(d => d.Id == doc.Id)
                .Select(d => d.DocNum)
                .FirstOrDefaultAsync();
            
            return Created("Document created", new
            {
                message = "Document created successfully",
                status = 201,
                doc_num = docNum
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

    [HttpGet(Name = "GetDocuments")]
    public async Task<ActionResult<List<DocDto>>> GetAllDocuments()
    {
        try
        {
            var docs = await _context.Documents
                .Where(doc => doc.Deleted == false)
                .Include(doc => doc.GenByUsrNavigation)
                .Include(doc => doc.UnitBelongNavigation)
                .OrderByDescending(doc => doc.DateCreate)
                .ToListAsync();

            return Ok(new
            {
                message = "Documents retrieved successfully",
                status = 200,
                docs = _mapper.Map<List<DocDto>>(docs)
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

    [HttpGet("{id:int}", Name = "GetDocumentById")]
    public async Task<ActionResult<DocDto>> GetDocumentById(int id)
    {
        try
        {
            var tempDoc = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.Id == id)
                .Include(doc => doc.GenByUsrNavigation)
                .Include(doc => doc.UnitBelongNavigation)
                .FirstOrDefaultAsync();

            if (tempDoc == null)
            {
                return NotFound(new
                {
                    message = $"Document with the ID '{id}' could not be found",
                    status = 404
                });
            }

            return Ok(new
            {
                message = "Document retrieved successfully",
                status = 200,
                doc = _mapper.Map<DocDto>(tempDoc)
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

    [HttpGet("{id:int}/file", Name="DownloadFile")]
    public async Task<ActionResult> DownloadDocFile(int id)
    {
        try
        {
            var doc = await _context.Documents
                .Where(doc => doc.Id == id && doc.Deleted == false)
                .Include(doc => doc.GenByUsrNavigation)
                .Include(doc => doc.UnitBelongNavigation)
                .FirstOrDefaultAsync();
            
            if (doc == null)
            {
                return NotFound(new
                {
                    message = $"Document with the ID '{id}' could not be found",
                    status = 404
                });
            }
            
            var docDto = _mapper.Map<DocDto>(doc);

            string fileName = doc.DocNum.Replace("/", "-");
            string templatePath = Path.Combine(_envHost.ContentRootPath, "Utils", "template_doc.docx");
            string formatedDate = doc.DateCreate?.ToString("d 'de' MMMM 'del' yyyy", new CultureInfo("es-MX"));
            
            using (var docStream = DocX.Load(templatePath))
            {
                docStream.ReplaceText("{date}", formatedDate);
                docStream.ReplaceText("{doc_num}", doc.DocNum);
                docStream.ReplaceText("{title_recip}", docDto.TitleRecip);
                docStream.ReplaceText("{full_name_recip}", docDto.FullNameRecip);
                docStream.ReplaceText("{position_recip}", docDto.PositionRecip);
                docStream.ReplaceText("{department_recip}", docDto.DeptName);
                docStream.ReplaceText("{body}", docDto.Body);
                docStream.ReplaceText("{title_sender}", docDto.TitleSender);
                docStream.ReplaceText("{full_name_sender}", docDto.FullNameSender);
                docStream.ReplaceText("{position_sender}", docDto.PositionSender);
                
                using (var stream = new MemoryStream())
                {
                    docStream.SaveAs(stream);
                    stream.Position = 0;
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                        $"{fileName}.docx");
                }
            }
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
    
    [HttpGet("docnum", Name = "GetDocumentByDocNum")]
    public async Task<ActionResult<DocDto>> GetDocumentByDocNum([FromQuery] string? value)
    {
        try
        {
            if (value == null)
            {
                return BadRequest(new
                {
                    message = "Document number is empty",
                    status = 400
                });
            }
            
            var tempDoc = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.DocNum.Contains(value))
                .Include(doc => doc.GenByUsrNavigation)
                .Include(doc => doc.UnitBelongNavigation)
                .FirstOrDefaultAsync();

            if (tempDoc == null)
            {
                return NotFound(new
                {
                    message = $"The document with the Document Number '{value}' could not be found",
                    status = 404
                });
            }

            return Ok(new
            {
                message = $"The document with the number '{value}' was found",
                status = 200,
                doc = _mapper.Map<DocDto>(tempDoc)
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
    
    [HttpDelete("{id:int}", Name = "DeleteDocument")]
    public async Task<ActionResult> DeleteDocument(int id)
    {
        try
        {
            var docToDelete = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.Id == id)
                .FirstOrDefaultAsync();

            if (docToDelete == null)
            {
                return NotFound(new
                {
                    message = $"The document with the ID {id} was not found",
                    status = 404
                });
            }
            
            docToDelete.Deleted = true;
            await _context.SaveChangesAsync();
            
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

    [HttpPut("{id:int}", Name = "UpdateDocument")]
    public async Task<ActionResult> UpdateDocument(int id, UpdateDocDto newDoc)
    {
        try
        {
            var oldDoc = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.Id == id)
                .FirstOrDefaultAsync();

            if (oldDoc == null)
            {
                return NotFound(new
                {
                    message = $"The document with the ID {id} was not found",
                    status = 404
                });
            }
            
            //If the user's code is 0 for either the sender or recip, it means it has to be nullable
            if (newDoc.UsrSender == 0)
            {
                newDoc.UsrSender = null;
            }

            if (newDoc.UsrRecip == 0)
            {
                newDoc.UsrRecip = null;
            }
            
            _mapper.Map(newDoc, oldDoc);
            await _context.SaveChangesAsync();

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

    [HttpPut("conclude", Name = "ConcludeDocument")]
    public async Task<ActionResult> ConcludeDocument(ConcludeDocDto newConclude)
    {
        try
        {
            var tempDoc = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.Id == newConclude.DocId && doc.Concluded == false)
                .FirstOrDefaultAsync();

            if (tempDoc == null)
            {
                return NotFound(new
                {
                    message = $"The document with the ID {newConclude.DocId} was not found or has already been concluded",
                    status = 404
                });
            }
            
            tempDoc.Concluded = true;
            //The final assigned user will always be the one who concluded it
            tempDoc.UsrAssign = newConclude.UsrAssign;
            tempDoc.DateDone = DateTime.Now;

            var assignLog = _mapper.Map<AssignmentLog>(newConclude);
            _context.AssignmentLogs.Add(assignLog);
            
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"The document with the ID {newConclude.DocId} was concluded",
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
}