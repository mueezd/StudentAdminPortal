using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Models.DTO;
using StudentAdminPortal.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
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
        private readonly IImageRepository _imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentById")]
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

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequestDto request)
        {
            var student = await _studentRepository.AddStudentAsync(_mapper.Map<Student>(request));
            return CreatedAtAction(nameof(GetStudentById), new { studentId = student.Id}, 
                _mapper.Map<StudentDto>(student));
        }


        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            // check if Id Exist in db

            if (await _studentRepository.Exists(studentId))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                // Upload student image to local storage
                var fileImagePath = await _imageRepository.UploadImage(profileImage, fileName);

                if (await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Upload Image");
                // update profile image path in the database


            }

            return NotFound();


        }

    }
}
