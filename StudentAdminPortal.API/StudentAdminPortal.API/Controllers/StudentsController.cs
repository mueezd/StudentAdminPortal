using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Models.DTO;
using StudentAdminPortal.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid studentId)
        {
            //fetch single student detais
            var stduent = await _studentRepository.GetStudentByIdAsync(studentId);

            //return student
            if (stduent == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentDto>(stduent));
        }


        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudenyAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequestDto request)
        {
            if (await _studentRepository.Exists(studentId))
            {
                // Update Details
                var updateStudent = await _studentRepository.UpdateStudentAsync(studentId, _mapper.Map<Student>(request));
                if (updateStudent != null)
                {
                    return Ok(_mapper.Map<StudentDto>(updateStudent));
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                //Delete The Student
                var student = await _studentRepository.DeleteStudentAsync(studentId);
                return Ok(_mapper.Map<Student>(student));
            }

            return NotFound();
        }

    }
}
